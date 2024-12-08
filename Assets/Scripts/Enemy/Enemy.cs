using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;   // 플레이어의 점수 정보에 접근하기 위해
    private Rigidbody2D rb;
    private Vector2 movement;
    private ManagerCoin managerCoin;
    
    public float distance; // 우주선과 적과의 거리 -> 타겟 우선순위를 결정한다.

    /* 적 능력치 */
    public int damage;         // 적 공격력
    public float health;             // 적 체력
    public float moveSpeed;          // 적 이동속도

    /* 적 점수 포인트 */
    public int scorePoint = 10;    // 적 처치시 획득하는 점수

    /* 적 충돌 효과 */
    public GameObject crashPrefab;  // 충돌 효과
    /* 적 사망 효과 */
    public GameObject deathPrefab;  // 사망 효과

    /* 코인 */
    public GameObject coinObj;
    /* 코인 생성 */
    public GameObject coinPrefab;
    /* 달러 생성 */
    public GameObject dollarsPrefab;
    public int dollarsPercent;

    /* 적 공격 */
    //public bool isAttack; // 공격 중인가



    

    void Awake()
    {
        // 현재 코드에서는 한번만 호출하기 때문에 OnDie()에서 바로 호출해도 되지만
        // 오브젝트 풀링을 이용해 오브젝트를 재사용할 경우에는 최초 1번만 Find를 이용해
        // PlayerController의 정보를 저장해두고 사용하는 것이 연산에 효율적
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        managerCoin = GameObject.Find("ManagerCoin").GetComponent<ManagerCoin>();

        //isAttack = false;
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveReadyEnemy();
    }

    void FixedUpdate()
    {
        moveEnemy(movement);
        //attackPlayer(movement); 공격 함수
        //checkDistance(); 공격 함수
    }

    /* 이동 준비 */
    void moveReadyEnemy()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        //90도를 더해야하는 이유!
        //https://gamedev.stackexchange.com/questions/14602/what-are-atan-and-atan2-used-for-in-games
        direction.Normalize();

        movement = direction;
        // movement = new Vector2(0, -1); 아래로만 움직이게
    }
    
    /* 이동 */
    void moveEnemy(Vector2 direction)
    {
        //if ( Vector2.Distance(transform.position, player.position) > 3 && !isAttack ) //공격 중이 아니라면 이동
        //{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        //} 
    }

    // /* 공격 */
    // void attackPlayer(Vector2 direction)
    // {
        
    //     if ( Vector2.Distance(transform.position, player.position) <= 3.0f )
    //     {
    //         isAttack = true;
    //         StartCoroutine("Attack1", direction);
    //     }
    // }
    
    // IEnumerator Attack1(Vector2 direction)
    // {
    //     while ( ( Vector2.Distance(transform.position, player.position) < 3.2f ) )
    //     {
    //         rb.MovePosition((Vector2)transform.position - (direction * 100 * Time.deltaTime));
    //         yield return new WaitForSeconds(0.1f);
    //     }
    //     StartCoroutine("Attack2", direction);
    //     yield break;
    // }

    // IEnumerator Attack2(Vector2 direction)
    // {
    //     while ( ( Vector2.Distance(transform.position, player.position) > 3.0f ) )
    //     {
    //         rb.MovePosition((Vector2)transform.position + (direction * 100 * Time.deltaTime));
    //         yield return new WaitForSeconds(0.1f);
    //     }
    //     player.GetComponent<PlayerHP>().TakeDamage(1);
    //     yield break;
        
    //}

    /* 충돌 */
    void OnTriggerEnter2D(Collider2D collision)
    {
        //적에게 부딪힌 오브젝트의 태그가 "Player"이면
        if (collision.CompareTag("Player"))
        {
            //적 공격력만큼 플레이어 체력 감소
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            // 충돌 이펙트 생성
            Instantiate(crashPrefab, transform.position, Quaternion.identity);
            // 적 오브젝트 제거
            Destroy(gameObject);
        }
    }

    /* 적이 받는 데미지함수 */
    // 총알이 부딪힐 경우, Bullet.cs에서 OnHit 함수 호출
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

    /* 적 사망 함수 */
    public void Death()
    {
        // 플레이어의 점수를 scorePoint만큼 증가시킨다.
        //playerController.Score += scorePoint; 이 게임에 점수는 필요가 없다.
        


        // 코인 또는 달러 생성
        if (!ManagerInitPlay.isDollars)
        {
            coinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            dollarsPercent = Random.Range(1, 101);
            if (dollarsPercent <= 5)
            {
                coinObj = Instantiate(dollarsPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                coinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
        }
        
        managerCoin.StartCoinMove(coinObj, transform.position, ()=>
        {
            if (dollarsPercent <= 5)
            {
                PlayerController.getCoins += 10 ;
            }     
            else
            {
                PlayerController.getCoins++; 
            }
            playerController.coinsCount.text = PlayerController.getCoins.ToString();
        });
        
        // 마나를 1 리젠한다.
        if (!PlayManager.isOver)
        {
            PlayerMP playerMP = GameObject.FindWithTag("Player").GetComponent<PlayerMP>();
            playerMP.curMP += 0.25f;
        }
        
        // 적 오브젝트 제거
        Destroy(gameObject);
    }
    

    // /* 거리 체크 */
    // public void checkDistance()
    // {
    //     distance = Vector2.Distance(transform.position, player.position);
    // }
}

/*
 * File : Enemy.cs
 * Desc
 * : 적 캐릭터 오브젝트에 부착해서 사용
 *
 * Functions
 *
 */

 // https://www.youtube.com/watch?v=4Wh22ynlLyk 여러명의 적이 플레이어 위치로 이동
