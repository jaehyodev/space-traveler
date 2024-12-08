using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무기와 각도 상 가장 가까운 적을 공격한다
public class AutoTargetAngle4 : MonoBehaviour
{
    public GameObject spaceship;
    public GameObject target;
    public GameObject weapon;

    public AutoTargetAngle2 autoTargetAngle2;
    public AutoTargetAngle3 autoTargetAngle3;


    public int weaponType;
    private float range;
    public bool targetOn;


    private Vector3 enemyVec;
    private float rotateSpeed;
    private Coroutine runningCoroutine;

    Vector2 angleToEnemies1_Vec;
    Vector2 angleToEnemies2_Vec;
    Vector2 m_MyFirstVector;
    Vector2 m_MySecondVector;
    public float m_Angle;


    private void Awake()
    {
        targetOn = false;

    }

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("DetectTarget", 0f, 0.2f); // 초당 5번 실행하는데 0.2초마다 적 탐지 실행
        range = 10;
        //Debug.Log("사거리는 : " + range);

        switch (weaponType)
        {
            case 1:
                rotateSpeed = 3;
                break;
            case 2:
                rotateSpeed = 2;
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

    private void Update()
    {
        Debug.Log("락온인가?" + targetOn); // 락온은 아님
        Debug.Log("타겟은 없는가?" + (target == null)); // 타겟은 있음
    }

    private void DetectTarget()
    {
        if (targetOn && target == null)
        {
            targetOn = false;
        }
        if (!targetOn) // 락온되지 않은 경우
        {
            Debug.Log("탐지 중");
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float smallestAngle = Mathf.Infinity; // 가장 짧은 거리
            GameObject nearestEnemy = null; // 가장 가까운 적 몬스터
            
            //가장 각도가 좁은 적을 확인
            foreach (GameObject enemy in Enemies)
            {

                angleToEnemies1_Vec = new Vector2(weapon.transform.position.x, weapon.transform.position.y);
                angleToEnemies2_Vec = new Vector2(enemy.transform.position.x, enemy.transform.position.y); // 나와 적과의 거리
                float angleToEnemies = Vector2.Angle(angleToEnemies1_Vec, angleToEnemies2_Vec);

                if (angleToEnemies < smallestAngle) // 첫 원소는 쇼티스트가 무한이라 무조건 이프문 안이 돌아감
                {
                    if ((autoTargetAngle2.target == enemy || autoTargetAngle3.target == enemy) && Enemies.Length >= 3) //가장 가까운 적이 2,3번의 타겟이고, 적이 3명 이상이라면
                    {
                        continue; // 그 타겟은 건너뛴다.
                    }
                    else // 잡은 타겟이 23포의 타겟이 아니거나, 적이 1명 2명이거나 없을 경우 -> 그냥 23포와 같이 공격
                    {
                        smallestAngle = angleToEnemies;
                        nearestEnemy = enemy;
                    }
                }
            }

            // 각도 상, 가장 가까운 적이 있다면 조준
            if (nearestEnemy != null)
            {
                Debug.Log("조준 시작 " + this);
                target = nearestEnemy;
                Vector3 enemyVec = target.transform.position.normalized;
                TurnAngle(enemyVec);
            }
            else
            {
                // 무기 회전 정지, 목표 해제
                target = null;
                targetOn = false;
                Debug.Log("락 오프 " + this);
            }

            if (target != null) // 타겟이 있으면 각도를 비교해서 일직선이되면 목표된다.
            {
                //Fetch the first GameObject's position
                m_MyFirstVector = new Vector2(weapon.transform.position.x, weapon.transform.position.y);
                //Fetch the second GameObject's position
                m_MySecondVector = new Vector2(target.transform.position.x, target.transform.position.y);
                //Find the angle for the two Vectors
                m_Angle = Vector2.Angle(m_MyFirstVector, m_MySecondVector);
                Debug.Log("적과의 각도 : " + m_Angle);
                if (m_Angle <= 10)
                {
                    Debug.Log("락 온 " + this);
                    targetOn = true;
                    Debug.Log(targetOn);
                }
                else
                {
                    Debug.Log("락 오프 " + this);
                    targetOn = false;
                }
            }
        }
    }

                
    private void TurnAngle(Vector3 enemyVec)
    {   
        //Debug.Log("각도함수실행");
        Vector3 currentWeaponVec = weapon.transform.up;
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
}

/*
https://www.youtube.com/watch?v=yiN0N78d668
*/