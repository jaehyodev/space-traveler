using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float           maxHP = 100;
    public float           curHP = 100;
    private SpriteRenderer  spriteRenderer;
    public GameObject explosionPrefab;
    public GameObject playManager;

    private float seoryTime = 0;
    private float spannerTime = 0;

    public GameObject HeartEffectObj; // 하트 아이템 먹으면 나오는 회복 효과 
    public GameObject ShieldEffectObj;
    public GameObject ShieldEffectTemp;
    
    private bool isShieldOn;
    private float shieldTime;


    void Awake()
    {
        curHP               = maxHP;
        spriteRenderer      = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        playManager = GameObject.Find("PlayManager");
        isShieldOn = false;
    }

    void Update()
    {
        // 체력이 100을 넘으면 안된다.
        curHP = Mathf.Min(curHP, 100);

        seoryTime += Time.deltaTime;
        if (seoryTime >= 10f)
        {
            if(curHP < 100f)
            {
                curHP += 1;
            }
            seoryTime = 0;
        }

        if (ManagerInitPlay.isSpanner)
        {
            spannerTime += Time.deltaTime;
            if (spannerTime >= 10f)
            {
                if(curHP < 100f)
                {
                    curHP += 1;
                }
                spannerTime = 0;
            }
        }

        if (isShieldOn)
        {
            shieldTime -= Time.deltaTime;
            if (shieldTime <= 0)
            {
                Destroy(ShieldEffectTemp);
                isShieldOn = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        // 힛코루틴이 자꾸 발동되니 멈추게 해야함.
        if (isShieldOn || PlayManager.isOver)
            return;

        // 현재 체력을 damage만큼 감소
        curHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");
    
        // 체력이 0 이하 = 플레이어 캐릭터 사망
        if ( curHP <= 0)
        {                
            StopCoroutine("GameTime");
            StopCoroutine("GameDistance");
            // 폭발 이펙트 생성
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);   
            Debug.Log("Player HP : 0.. Die");
            
            // 두개 이상의 값 중 가장 큰 것을 반환
            curHP = Mathf.Max(curHP, 0);
            //gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.SetActive(false);

            PlayManager playOver = playManager.GetComponent<PlayManager>();
            playOver.GameOver();
        }
    }

    private IEnumerator HitColorAnimation()
    {       
        // 플레이어의 색상을 빨간색으로
        spriteRenderer.color = Color.red;
        // 0.1초 동안 대기
        yield return new WaitForSeconds(0.1f);
        // 플레이어의 색상을 원래 색상인 하얀색으로
        // (원래 색상이 하얀색이 아닐 경우 원래 색상 변수 선언)
        spriteRenderer.color = Color.white;
    }

    /* 아이템 효과 */
    public void HeartPickUp()
    {
        if (PlayManager.isOver)
            return;

        Instantiate(HeartEffectObj, transform.position, Quaternion.identity);
        if(curHP < 100f)
        {
            curHP += 10;
        }
    }

    public void ShieldPickUp()
    {
        if (PlayManager.isOver)
            return;

        Instantiate(ShieldEffectObj, transform.position, Quaternion.identity);
        isShieldOn = true;
        shieldTime = 5.0f;
    }
}
