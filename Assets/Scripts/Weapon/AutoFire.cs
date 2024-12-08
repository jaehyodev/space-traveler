using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    public GameObject spaceship;
    public GameObject bulletPrefab;    // 공격할 때 생성되는 발사체 프리팹, 1번 무기는 PlayerController에서 참조
    private Movement2D movement2D;


    public AutoTargetTest autoTargetTest;

    [SerializeField]
    private bool isUserWeapon; // 유저무기는 자동공격 불가
    public bool shooting; // 사격 중인가
    public float attackRate; // 공격속도
    public float attackTimer = 0f; // 현재 공격시간
    
    void Awake()
    {
        
        movement2D = GetComponent<Movement2D>();
        attackRate = GetComponent<Weapon>().attackRate;
    }

    void Start()
    {        
        spaceship = GameObject.FindGameObjectWithTag("Player");

        if ( gameObject.CompareTag("WeaponFirst") )
        {
            isUserWeapon = true;
        }
    }
    void Update()
    {
        if (isUserWeapon || spaceship.GetComponent<PlayerController>().isLaserShowCast || spaceship.GetComponent<PlayerController>().isEMPCast)
            return;
            
        //적을 찾는다 -> 움직인다 -> 멈춘다 -> 죽일 때까지 쏜다 -> 타겟 죽어서 없음 -> 적을 찾는다 
        if (autoTargetTest.isLockOn && attackTimer <= 0 && !shooting)
        {
            shooting = true;
            StartCoroutine(TryAttack());
            attackTimer = attackRate;
        }
        else
        {
            //print("아직 공격 쿨타임입니다.");
        }
        attackTimer -= Time.deltaTime;
    }

    IEnumerator TryAttack() // TryAttack 코루틴 함수
    {
            // 발사체 오브젝트 생성
            Instantiate(bulletPrefab, this.transform.GetChild(0).transform.position, transform.rotation); // 각도에 Quaternion.identity를 넣으면 각도는 0인듯?
            GetComponent<AudioSource>().Play();
            shooting = false;
            yield return new WaitForSeconds(attackRate); // attackRate 시간만큼 대기
            
    }
}
