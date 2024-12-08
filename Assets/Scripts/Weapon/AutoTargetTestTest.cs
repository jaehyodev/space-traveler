using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AutoTargetTestTest : MonoBehaviour
{
    public GameObject spaceship;
    public GameObject target;
    public GameObject[] weapons = new GameObject[8];

    public int weapon; // 현재 무기는 몇번 무기인가?
    public int weaponType; // 1이면 소형, 2이면 중형, 3이면 대형
    private float range;
    public bool isLockOn;


    private Vector3 enemyVec;
    private float rotateSpeed;
    private Coroutine runningCoroutine;

    Vector2 angleToEnemies1_Vec;
    Vector2 angleToEnemies2_Vec;
    Vector2 m_MyFirstVector;
    Vector2 m_MySecondVector;
    public float m_Angle;
    
    Vector2 weaponCurVec;
    Vector2 weaponLineupVec;
    public float lineupAngle;


    [SerializeField]
    private bool isUserWeapon;

    [SerializeField]
    private GameObject nearestEnemy = null; // 가장 가까운 적 몬스터

    void Awake()
    {
        isLockOn = false;
    }

    void Start()
    {
        spaceship = GameObject.FindGameObjectWithTag("Player");

        if ( this.CompareTag("WeaponFirst") )
            weapon = 0;
        if ( this.CompareTag("WeaponSecond") )
            weapon = 1;
        if ( this.CompareTag("WeaponThird") )
            weapon = 2;                           
        if ( this.CompareTag("WeaponFourth") )
            weapon = 3;        
        if ( this.CompareTag("WeaponFifth") )
            weapon = 4;
        if ( this.CompareTag("WeaponSixth") )
            weapon = 5;
        if ( this.CompareTag("WeaponSeventh") )
            weapon = 6;
        if ( this.CompareTag("WeaponEighth") )
            weapon = 7;

        for (int i = 0; i <= 7; i++ )
        {
            //if ( weapons[i] == null )
            //{
                switch ( i )
                {
                    case 0:
                        weapons[i] = GameObject.FindWithTag("WeaponFirst");   
                        break;
                    case 1:
                        weapons[i] = GameObject.FindWithTag("WeaponSecond");    
                        break;
                    case 2:
                        weapons[i] = GameObject.FindWithTag("WeaponThird");    
                        break;
                    case 3:
                        weapons[i] = GameObject.FindWithTag("WeaponFourth"); 
                        break;
                    case 4:
                        weapons[i] = GameObject.FindWithTag("WeaponFifth");    
                        break;
                    case 5:
                        weapons[i] = GameObject.FindWithTag("WeaponSixth");    
                        break;
                    case 6:
                        weapons[i] = GameObject.FindWithTag("WeaponSeventh");  
                        break;
                    case 7:
                        weapons[i] = GameObject.FindWithTag("WeaponEighth");    
                        break;
                }
            //}
        }

        if ( gameObject.CompareTag("WeaponFirst") )
        {
            isUserWeapon = true;
        }

        InvokeRepeating("DetectTarget", 0f, 0.2f); // 초당 5번 실행하는데 0.2초마다 적 탐지 실행
        

        range = 10; // 무기를 감지하는 사거리, 감지하면 무기를 회전시킨다.;

        switch ( weaponType ) // weaponType과 반대로 회전속도를 설정한다.
        {
            case 1:
                rotateSpeed = 5;
                break;
            case 2:
                rotateSpeed = 3;
                break;
            case 3:
                rotateSpeed = 1;
                break;
        }
        angleToEnemies1_Vec = Vector2.zero;
        angleToEnemies2_Vec = Vector2.zero;
        m_MyFirstVector = Vector2.zero;
        m_MySecondVector = Vector2.zero;
        m_Angle = 0.0f;
    }


    void DetectTarget()
    {
        if ( this.CompareTag("WeaponFirst") || spaceship.GetComponent<PlayerController>().isEMPCast || PlayManager.isOver )
            return;

        if ( target !=null && isLockOn )
            return;

        if ( target == null && isLockOn ) // 락온되있고 타겟이 아예 없다면 (타겟이 죽었는데 아직 락온된 상태임) 디버그용
        {
            isLockOn = false; 
        }

        if ( !isLockOn ) // 락온되지 않은 경우
        {
            //Debug.Log("탐지 중");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float smallestAngle = Mathf.Infinity; // 가장 작은 각도 for 중형, 대형
            
            //가장 각도가 좁은 적을 확인
            foreach (GameObject enemy in enemies)
            {
                    angleToEnemies1_Vec = new Vector2(transform.position.x, transform.position.y);
                    angleToEnemies2_Vec = new Vector2(enemy.transform.position.x, enemy.transform.position.y); // 나와 적과의 거리
                    float angleToEnemies = Vector2.Angle(angleToEnemies1_Vec, angleToEnemies2_Vec);

                    if (angleToEnemies < smallestAngle) // 첫 원소는 쇼티스트가 무한이라 무조건 이프문 안이 돌아감
                    {
                        smallestAngle = angleToEnemies;
                        nearestEnemy = enemy;
                    }   
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy;
                Vector3 enemyVec = target.transform.position.normalized;
                TraceTarget();
                TurnAngle(enemyVec);
                //print("무기 회전 시작");
            }
        }
    }


    void TraceTarget()
    {
        if (target != null) // 타겟이 있으면 각도를 비교해서 일직선이되면 목표된다.
        {

            //print("락온하나요");
            //Fetch the first GameObject's position
            m_MyFirstVector = new Vector2(transform.position.x, transform.position.y);
            //Fetch the second GameObject's position
            m_MySecondVector = new Vector2(target.transform.position.x, target.transform.position.y);
            //Find the angle for the two Vectors
            m_Angle = Vector2.Angle(m_MyFirstVector, m_MySecondVector);
            //Debug.Log("적과의 각도 : " + m_Angle);
                    
            if (m_Angle <= 10)
            {
                //print("조준반경에 옴");
                isLockOn = true;
            }
            else
            {
                //print("조준반경에 안옴 " + m_Angle);
            }
        }
    }     
                
    public void TurnAngle(Vector3 enemyVec)
    {   
        if ( PlayManager.isOver )
            return;
        //Debug.Log("각도함수실행");
        Vector3 currentWeaponVec = transform.up;
        // character가 바라보고 있는 벡터
 
        float angle = Vector3.Angle(enemyVec, currentWeaponVec);
        //Debug.Log("각도 :" + angle);
        int sign = (Vector3.Cross(enemyVec, currentWeaponVec).z > 0) ? -1 : 1;
        //Debug.Log("좌+1우-1 :" + sign);
        // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
        // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-
    
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
        // 코루틴이 실행중이면 실행 중인 코루틴 중단 후 코루틴 실행 
        // 코루틴이 한 개만 존재하도록.
        // => 회전 중에 새로운 회전이 들어왔을 경우, 회전 중이던 것을 멈추고 새로운 회전을 함.
    }
    
    IEnumerator RotateAngle(float angle, int sign)
    {
        //Debug.Log("각도코루틴함수실행");
        float mod = angle % rotateSpeed; // 남은 각도 계산
        for (float i = mod; i < angle; i += rotateSpeed)
        {   
            //weapon.transform.Rotate(0, 0, sign * rotateSpeed); // 캐릭터 rotateSpeed만큼 회전
            transform.RotateAround(spaceship.transform.position, new Vector3(0, 0, 1), sign * rotateSpeed);
            yield return new WaitForSeconds(0.01f); // 0.01초 대기
        }
        //weapon.transform.Rotate(0, 0, sign * mod); // 남은 각도 회전
        transform.RotateAround(spaceship.transform.position, new Vector3(0, 0, 1), sign * mod);


    }

    public void UltRotateAngle(Vector3 newVec)
    {
        TurnAngle(newVec);
        StartCoroutine(UltLineup(newVec)); // 초당 10번 실행하는데 0.1초마다 적 탐지 실행
    }

    IEnumerator UltLineup(Vector3 newVec)
    {
        while (true)
        {
            weaponCurVec = new Vector2(transform.position.x, transform.position.y);
            weaponLineupVec = newVec;
            print(newVec);
            lineupAngle = Vector2.Angle( weaponCurVec, weaponLineupVec);
            print(weapon + "번쨰 각도" + lineupAngle);
            if ( lineupAngle <= 10)
            {
            /* 언제 정렬이 다 된지 체크를 어떻게 해야햘 가 */        
                PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                playerController.GetComponent<PlayerController>().isEMPReady[weapon] = true;
                print("isUltReady " + playerController.GetComponent<PlayerController>().isEMPReady[weapon]);
                yield break;
            }
        
            yield return new WaitForSeconds(0.1f);
        }
    }
}

// 무기와 각도 상 가장 가까운 적을 공격한다
// 그러면 아예 반대쪽을 공격하러 안 올수도 있으니 정렬 스킬을 기본적으로 추가해준다.

/*
https://www.youtube.com/watch?v=yiN0N78d668
*/