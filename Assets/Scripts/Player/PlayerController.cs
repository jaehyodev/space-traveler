using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    private StageData stageData;

    private Movement2D movement2D;

    [SerializeField]
    private GameObject playerBulletPrefab;    // 공격할 때 생성되는 발사체 프리팹

    [SerializeField]
    private GameObject firePoint;    // 생성위치
    [SerializeField]
    private GameObject userWeapon;    // 생성각도
    
    public float Range;

    /* 공격 */
    public bool isButtonAttack;
    public bool isButtonUlt;

    [SerializeField]
    private KeyCode KeyCodeAttack = KeyCode.Space;
    [SerializeField]
    private float attackRate; // 플레이어의 공격속도
    [SerializeField]
    private float shotDelay; // 플레이어의 현재 공격시간 체크
    public Button attackBtn; // 플레이어 공격버튼, 게임오버되면 클릭못하게 막으려고

    public float spaceshipSpeed;

    /* 공격 (코루틴 방식) */
    // public bool shooting;
    // [SerializeField]
    // private float attackRate;    // 공격속도
    // [SerializeField]
    // private float timer; // 플레이어의 현재 공격시간 체크

    /* 스킬 */
    public GameObject laserShowEffect;
    public GameObject[] laserShow = new GameObject[8];
    public GameObject empEffect;
    public Image ultCooltimeEffect;
    public Button ultBtn;
    public bool isLaserShowCast;
    public bool isEMPCast;
    public bool[] isEMPReady = new bool[8];
    public bool isUltCooltime;
    public float ultCooltime = 7f;
    public TextMeshProUGUI cooltimeCounter; // 남은 쿨타임을 표시할 텍스트
    private float curUltCooltime; // 현재 쿨타임
    public Vector3[] newVec = new Vector3[8]; // 무기의 각도를 모두 바꾸기

    /* 점수 */
    //private int score; // enemy 클래스에서 카운터한다.
    public static int getCoins; // enemy 클래스에서 카운터한다.
    public static int coins; // 총 보유한 코인
    public static int getDiamonds; // enemy 클래스에서 카운터한다.
    public static int diamonds; // 총 보유한 코인
    public TextMeshProUGUI coinsCount;
    public TextMeshProUGUI diamondsCount;

    // 외부에서 접근 가능한 프로퍼티 설정
    // public int Score
    // {
    //     // score 값이 음수가 되지 않도록
    //     set { score = Mathf.Max(0, value); }
    //     get { return score; }
    // }

    public GameObject[] weapons = new GameObject[8];
    public int weaponNumber;

    void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        //timer = 0f; 코루틴 공격방식에  썼음
        isUltCooltime = false;
    }

    void Start()
    {
        userWeapon = GameObject.FindWithTag("WeaponFirst");
        attackBtn = GameObject.Find("Button - Attack").GetComponent<Button>();
        ultBtn = GameObject.Find("Button - Ult").GetComponent<Button>();
        ultCooltimeEffect = GameObject.Find("Ult Cooltime Effect").GetComponent<Image>();
        cooltimeCounter = GameObject.Find("Text - Ult Cooltime").GetComponent<TextMeshProUGUI>();
        coinsCount = GameObject.Find("Text - Coin Count").GetComponent<TextMeshProUGUI>();
        diamondsCount = GameObject.Find("Text - Diamond Count").GetComponent<TextMeshProUGUI>();

        attackRate = userWeapon.GetComponent<Weapon>().attackRate;
        playerBulletPrefab = userWeapon.GetComponent<AutoFire>().bulletPrefab;
        firePoint = userWeapon.transform.GetChild(0).gameObject;

        shotDelay = attackRate; // 처음에는 바로 공격 가능

        ultCooltimeEffect.fillAmount = 0; // 처음에는 스킬 버튼을 가리지 않음

        for (int i = 0; i <= 7; i++ )
        {
            if (ManagerDataWeapon.instanceDataWeapon.weaponId[i] == 100)
            {
                continue;
            }

            switch ( i )
            {
                case 0:
                    weapons[i] = GameObject.FindWithTag("WeaponFirst");
                    weaponNumber++;
                    break;
                case 1:
                    weapons[i] = GameObject.FindWithTag("WeaponSecond");    
                    weaponNumber++;
                    break;
                case 2:
                    weapons[i] = GameObject.FindWithTag("WeaponThird");    
                    weaponNumber++;
                    break;
                case 3:
                    weapons[i] = GameObject.FindWithTag("WeaponFourth"); 
                    weaponNumber++;
                    break;
                case 4:
                    weapons[i] = GameObject.FindWithTag("WeaponFifth");    
                    weaponNumber++;
                    break;
                case 5:
                    weapons[i] = GameObject.FindWithTag("WeaponSixth");   
                    weaponNumber++; 
                    break;
                case 6:
                    weapons[i] = GameObject.FindWithTag("WeaponSeventh");  
                    weaponNumber++;
                    break;
                case 7:
                    weapons[i] = GameObject.FindWithTag("WeaponEighth");    
                    weaponNumber++;
                    break;
            }        
        }

        isEMPReady[0] = false;
        isEMPReady[1] = false;
        isEMPReady[2] = false;
        isEMPReady[3] = false;
        isEMPReady[4] = false;
        isEMPReady[5] = false;
        isEMPReady[6] = false;
        isEMPReady[7] = false;
    }


    void Update()
    {
        Move();
        Attack();
        SpacebarAttack();
        Reload();
        Ult();
    /* 공격을 코루틴으로 짰을 경우 */
    //     // 공격 키를 DOWN/UP으로 공격 시작/종료 (GetButton은 누르는 중에도 가능)
    //     if ( Input.GetKeyDown(KeyCodeAttack) )
    //     {
    //         if (!shooting)
    //         {
    //             shooting = true;
    //             print("공격");
    //             StartCoroutine(TryAttack());
    //         }
    //         else
    //         {
    //             print("대기");
    //         }
    //     }

    //     if ( timer <= 0 && !shooting )
    //     {
    //         if ( Input.GetKey(KeyCodeAttack) )
    //         {
    //             shooting = true;
    //             print("공격");
    //             StartCoroutine(TryAttack());
    //         }
    //         timer = attackRate;
    //     }
    //     timer -= Time.deltaTime;
    // }

    // IEnumerator TryAttack() // TryAttack 코루틴 함수
    // {
    //         // 발사체 오브젝트 생성
    //         Instantiate(projectilePrefab, firePoint.transform.position, weapon.transform.rotation); // 각도에 Quaternion.identity를 넣으면 각도는 0인듯?
    //         //Debug.Log(transform.rotation);
    //         // attackRate 시간만큼 대기
    //         yield return new WaitForSeconds(attackRate);
    //         shooting = false;
    // }
    }
    

    void LateUpdate()
    {
        // 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하도록 함
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
    
    
    // 방향키를 눌러 우주선 이동
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");  
        movement2D.MoveTo(new Vector3(x, y, 0));
    }
    

    // 공격버튼 또는 스페이스바를 눌러 공격
    void Attack()
    {
        if ( !isButtonAttack ) // 공격버튼이 눌러졌다면
            return;

        if ( shotDelay < attackRate ) // 공격시간 조절
            return;

        if (isLaserShowCast || isEMPCast)
            return;
        
        Instantiate(playerBulletPrefab, firePoint.transform.position, userWeapon.transform.rotation);
        GameObject weaponFirst = transform.GetChild(0).gameObject;

        // Vector3 vvc = new Vector3((Random.Range(-10, 11), Random.Range(-10, 11), 0f));
        weaponFirst.GetComponent<AudioSource>().Play();
        shotDelay = 0; // 공격시간 다시 0부터 체크
    }

    void Reload()
    {
        shotDelay += Time.deltaTime;
    }

    void Ult()
    {
        if (!isButtonUlt || weaponNumber != 8 || isUltCooltime || GetComponent<PlayerMP>().curMP < 100)
        {
            isButtonUlt = false;
            return;
        }

        

        /* 마나 감소 */
        GetComponent<PlayerMP>().UseUlt();
    
        if (gameObject.name.Contains("Gumball"))
        {
            isLaserShowCast = true;

            laserShow[0] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponFirst").transform.position, GameObject.FindGameObjectWithTag("WeaponFirst").transform.rotation);
            laserShow[0].transform.parent = GameObject.FindGameObjectWithTag("WeaponFirst").transform;
            //laserShow[0].transform.localScale = new Vector3(1,1,1);

            laserShow[1] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponSecond").transform.position, GameObject.FindGameObjectWithTag("WeaponSecond").transform.rotation);
            laserShow[1].transform.parent = GameObject.FindGameObjectWithTag("WeaponSecond").transform;
            //laserShow[1].transform.localPosition = new Vector3(0, 10.5f, 0);
            
            laserShow[2] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponThird").transform.position, GameObject.FindGameObjectWithTag("WeaponThird").transform.rotation);
            laserShow[2].transform.parent = GameObject.FindGameObjectWithTag("WeaponThird").transform;
            //laserShow[2].transform.localPosition = new Vector3(0, 10.5f, 0);

            laserShow[3] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponFourth").transform.position, GameObject.FindGameObjectWithTag("WeaponFourth").transform.rotation);
            laserShow[3].transform.parent = GameObject.FindGameObjectWithTag("WeaponFourth").transform;
            //laserShow[3].transform.localPosition = new Vector3(0, 10.5f, 0);

            laserShow[4] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponFifth").transform.position, GameObject.FindGameObjectWithTag("WeaponFifth").transform.rotation);
            laserShow[4].transform.parent = GameObject.FindGameObjectWithTag("WeaponFifth").transform;
            //laserShow[4].transform.localPosition = new Vector3(0, 10.5f, 0);

            laserShow[5] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponSixth").transform.position, GameObject.FindGameObjectWithTag("WeaponSixth").transform.rotation);
            laserShow[5].transform.parent = GameObject.FindGameObjectWithTag("WeaponSixth").transform;
            //laserShow[5].transform.localPosition = new Vector3(0, 10.5f, 0);

            laserShow[6] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponSeventh").transform.position, GameObject.FindGameObjectWithTag("WeaponSeventh").transform.rotation);
            laserShow[6].transform.parent = GameObject.FindGameObjectWithTag("WeaponSeventh").transform;
            //laserShow[6].transform.localPosition = new Vector3(0, 10.5f, 0);

            laserShow[7] = Instantiate(laserShowEffect, GameObject.FindGameObjectWithTag("WeaponEighth").transform.position, GameObject.FindGameObjectWithTag("WeaponEighth").transform.rotation);
            laserShow[7].transform.parent = GameObject.FindGameObjectWithTag("WeaponEighth").transform;

            for (int i = 0; i <= 7; i++)
            {
                switch (laserShow[i].transform.parent.localScale.y)
                {
                    case 1f:
                        laserShow[i].transform.localPosition = new Vector3(0, 10.5f, 0);
                        break;
                    case 1.25f:
                        laserShow[i].transform.localPosition = new Vector3(0, 8.5f, 0);
                        break;
                    case 2f:
                        laserShow[i].transform.localPosition = new Vector3(0, 5.5f, 0);
                        break;
                }
            }
            
            
            //laserShow[7].transform.localScale = new Vector3(1,1,1);

            Invoke("UltLaserShowEnd", 5f);
        }

        if (gameObject.name.Contains("Spriteball"))
        {
            

            for ( int wn = 0; wn <= 7; wn++ )
            { 
                switch ( wn )
                {
                    case 0:
                        newVec[wn] = new Vector3( 0, 1, 0 );
                        GameObject.FindGameObjectWithTag("WeaponFirst").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 1:
                        newVec[wn] = new Vector3( 1, 1, 0 );
                        GameObject.FindGameObjectWithTag("WeaponSecond").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 2:
                        newVec[wn] = new Vector3( 1, 0, 0 );
                        GameObject.FindGameObjectWithTag("WeaponThird").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 3:
                        newVec[wn] = new Vector3( 1, -1, 0 );
                        GameObject.FindGameObjectWithTag("WeaponFourth").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 4:
                        newVec[wn] = new Vector3( 0, -1, 0 );
                        GameObject.FindGameObjectWithTag("WeaponFifth").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 5:
                        newVec[wn] = new Vector3( -1, -1, 0 );
                        GameObject.FindGameObjectWithTag("WeaponSixth").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 6:
                        newVec[wn] = new Vector3( -1, 0, 0 );
                        GameObject.FindGameObjectWithTag("WeaponSeventh").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                    case 7:
                        newVec[wn] = new Vector3( -1, 1, 0 );
                        GameObject.FindGameObjectWithTag("WeaponEighth").GetComponent<AutoTargetTest>().UltRotateAngle(newVec[wn]);
                        break;
                }
            }

            StartCoroutine(EMPReady());
            isEMPCast = true; // 캐스팅 시작
        }

        GetComponent<AudioSource>().Play(); // 스킬 사운드 재생

        //#1. Effect visible
        isButtonUlt = false; // ?

        ColorBlock cb1 = ultBtn.colors;
        cb1.normalColor = cb1.highlightedColor;
        ColorBlock cb2 = ultBtn.colors;
        cb2.normalColor = cb2.normalColor;
        ultBtn.colors = cb1;
        ultCooltimeEffect.fillAmount = 0;
        
        isUltCooltime = true; // 쿨타임 시작
        curUltCooltime = ultCooltime; // 남은 쿨타임은 스킬의 쿨타임과 같다.
        cooltimeCounter.text = string.Format("{0:0.0}", curUltCooltime); // 쿨타임 표시
        StartCoroutine(UltCooltimer()); // 쿨타임 타이머 가동   
        StartCoroutine(UltCooltimeCounter());

        /* 쿨타임 이펙트 표시 */
        IEnumerator UltCooltimer()
        {
            while ( ultCooltimeEffect.fillAmount < 1 )
            {
                ultCooltimeEffect.fillAmount += Time.smoothDeltaTime / ultCooltime;
                yield return null;
            }
            ultCooltimeEffect.fillAmount = 0;
            yield break;
        }

        /* 쿨타임 텍스트 표시 */
        IEnumerator UltCooltimeCounter()
        {
            while ( curUltCooltime > 0 )
            {
                yield return new WaitForSeconds(0.1f);

                curUltCooltime -= 0.1f;          
                cooltimeCounter.text = string.Format("{0:0.0}", curUltCooltime);
            }
            curUltCooltime = 0f;
            cooltimeCounter.text = ""; // 쿨타임 텍스트 안보이게 한다.
            isUltCooltime = false; // 쿨타임 끝
            ultBtn.colors = cb2;
            yield break;
        }




        //#3. Remove Enemy Bullet (But there is no enemyBullet now)
        // GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        // for (int index = 0; index < enemyBullets.Length; index++)
        // {
        //     Destroy(enemyBullets[index]);
        // }
    }

    public void ButtonAttackDown()
    {
        isButtonAttack = true;
    }

    public void ButtonAttackUp()
    {
        isButtonAttack = false;
    }

    void SpacebarAttack()
    {
        if ( Input.GetKeyDown(KeyCodeAttack) )
        {
            isButtonAttack = true;
        }

        if ( Input.GetKeyUp(KeyCodeAttack) )
        {
            isButtonAttack = false;
        }
    }

    public void ButtonUltDown()
    {
        if (isUltCooltime)
            return;
        isButtonUlt = true;
    }


    IEnumerator EMPReady()
    {
        while (true)
        {
            if ( isEMPReady[0] && isEMPReady[1] && isEMPReady[2] && isEMPReady[3]
                && isEMPReady[4] && isEMPReady[5] && isEMPReady[6] && isEMPReady[7] )
                {
                    Invoke("EMPCast", 0.5f);
                    yield break;
                }
            yield return new WaitForSeconds(0.1f);
        }  
    }


    void EMPCast()
    {
        // 궁극기 캐스팅 종료
        isEMPCast = false;
        isEMPReady[0] = false;
        isEMPReady[1] = false;
        isEMPReady[2] = false;
        isEMPReady[3] = false;
        isEMPReady[4] = false;
        isEMPReady[5] = false;
        isEMPReady[6] = false;
        isEMPReady[7] = false;
        Instantiate(empEffect, transform.position, Quaternion.identity);
        
        //#2. Remove Enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemy = enemies[index].GetComponent<Enemy>();
            enemy.OnHit(1000);
        }
    }


    // 우주선 주변 사거리 표시기
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range); // this.gameObject.transform.position에서 this.gameObject 같다    
    }


    /* 레이저쇼 궁극기 삭제 */
    void UltLaserShowEnd()
    {
        isLaserShowCast = false;

        for (int i = 0; i <= 7; i++)
        {
            Destroy(laserShow[i]);
        }
    }

    IEnumerator WormholeVibe()
    {
        while (true)
        {
            if (!WormholeSpawner.isWormholeOn || PlayManager.isOver)
            {
                this.gameObject.transform.position = new Vector3(0,0,0);
                yield break;            
            }
            print("움직이세요");
                this.gameObject.transform.position = new Vector3(0,Random.Range(-0.1f, 0.1f),0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

/*
LimitMin, LimitMax에 빨간 줄 오류...
스크립트 런타임 버전 오류입니다.
현재 사용하는 유니티 버전이 몇인지 모르겠지만
Edit - Project Settings - Player - Other Settings - Configure에 있는 Scripting Runtime Version을 .NET 3.5가 아닌 .NET 4.x로 바꿔주세요.
이 메뉴가 없으면
public Vector2 LimitMax => limitMax;
대신
public Vector2 LimitMax { get { return limitMax; } }
와 같이 작성해 주세요

=> 이 표현이 C# 6.0 이상에서 메소드나 프로퍼티의 Body가 간단할 때 람다식처럼 표현하는 기능입니다.

https://www.youtube.com/watch?v=Bi-IK4uTBTg
*/
