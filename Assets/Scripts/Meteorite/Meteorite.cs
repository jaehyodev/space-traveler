using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private int             damage = 20;        // 운석 충돌 데미지
    [SerializeField]
    private GameObject      hitPrefab;    // 운석 충돌 이펙트
    [SerializeField]
    private GameObject      deathPrefab;    // 운석 사망 이펙트
    private float           health = 500; // 운석 체력

    [SerializeField] GameObject diamondPrefab; // 다이아몬드 생성

    private float moveSpeed;

    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    private int              scorePoint = 100; // 적 처치시 획득하는 점수
    //private PlayerController playerController; // 플레이어의 점수 정보에 접근하기 위해
    public float rotateSpeed;

    private Vector3 sizeTemp;               // 임시 운석 크기

    void Awake()
    {
        // 현재 코드에서는 한번만 호출하기 때문에 OnDie()에서 바로 호출해도 되지만
        // 오브젝트 풀링을 이용해 오브젝트를 재사용할 경우에는 최초 1번만 Find를 이용해
        // PlayerController의 정보를 저장해두고 사용하는 것이 연산에 효율적

        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        //moveSpeed = Mathf.Min(Mathf.Max(moveSpeed * 5.0f * PlayManager.time, 1.0f), 5.0f);
        rotateSpeed = Random.Range(-100f, 100f);
    }

    void Start() 
    {
        // 운석의 크기 설정
        sizeTemp = transform.localScale * Random.Range(1.0f, 2.0f);
        transform.localScale = sizeTemp;

        // 운석 스피드 
        MeteoriteSpawner meteoriteSpawner = GameObject.Find("MeteoriteSpawner").GetComponent<MeteoriteSpawner>();
        moveSpeed = meteoriteSpawner.meteoritesSpeed;
    }
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.Rotate(0,0, Time.deltaTime * rotateSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에게 부딪힌 오브젝트의 태그가 "Player"이면
        if (collision.CompareTag("Player"))
        {
            // 운석 공격력만큼 플레이어 체력 감소
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            // 운석 충돌 이펙트 생성
            DeathEffect();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);
            Instantiate(hitPrefab, bullet.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        // 적에게 부딪힌 오브젝트의 태그가 "Enemy"이면
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.DeathEffect();
            Destroy(collision.gameObject);
        }
    }
    
    /* 적이 받는 데미지함수 */
    public void OnHit(float damage)
    {
        health -= damage;

        // 적이 체력이 0이하, 사망한다면
        if(health <= 0)
        {
            DeathEffect();
            Death();
        }
    }
    public void DeathEffect()
    {
        // 폭발 이펙트 생성
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }

    /* 유성 사망 함수 */
    public void Death()
    {   
        // 마나를 1 리젠한다.
        PlayerMP playerMP = GameObject.FindWithTag("Player").GetComponent<PlayerMP>();
        playerMP.curMP += 5;

        // 다이아몬드 생성
        int rand = Random.Range(1, 11);
        if (rand <= 1)
            Instantiate(diamondPrefab, transform.position, Quaternion.identity);

        // 적 오브젝트 제거
        Destroy(gameObject);
    }
}


/*
 * File : Meteorite.cs
 * Desc
 *  : 운석 오브젝트에 부착해서 사용
 *
 * Functions
 *
 */
