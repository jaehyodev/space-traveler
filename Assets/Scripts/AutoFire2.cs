using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFire2 : MonoBehaviour
{
    private Movement2D movement2D;

    [SerializeField]
    private GameObject projectilePrefab;    // 공격할 때 생성되는 발사체 프리팹
    [SerializeField]
    private GameObject firePoint;    // 생성위치
    [SerializeField]
    private GameObject weapon;    // 생성각도
    public AutoTargetAngle2 autoTargetAngle2;

    public bool shooting;           // 사격 중인가
    public float delayTime = 0.1f;
    public float timer = 0f;
    
    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }

    void Start()
    {
        firePoint = GameObject.Find("FirePoint2");
    }

    private void Update()
    {
        //적을 찾는다 -> 움직인다 -> 멈춘다 -> 죽일 때까지 쏜다 -> 타겟 죽어서 없음 -> 적을 찾는다 
        if (autoTargetAngle2.targetOn && timer <= 0 && !shooting)
        {
            shooting = true;
            //print("자동공격");
            //Debug.Log("타겟온 : " + autoTargetAngle.targetOn);
            StartCoroutine(TryAttack());
            timer = delayTime;
        }
        else
        {
            //print("자동공격대기");
        }
        timer -= Time.deltaTime;
    }

    IEnumerator TryAttack() // TryAttack 코루틴 함수
    {
            // 발사체 오브젝트 생성
            Instantiate(projectilePrefab, firePoint.transform.position, weapon.transform.rotation); // 각도에 Quaternion.identity를 넣으면 각도는 0인듯?
            //Debug.Log(transform.rotation);
            // attackRate 시간만큼 대기
            yield return new WaitForSeconds(delayTime);
            shooting = false;
    }
}
