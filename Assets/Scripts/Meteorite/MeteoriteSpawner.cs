using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject[] meteoriteObjs;
    
    [SerializeField]
    private StageData   stageData;
    [SerializeField]
    private GameObject  alertLinePrefab;
    [SerializeField]
    private GameObject  meteoritePrefab;

    private float       minSpawnTime = 10.0f;
    private float       maxSpawnTime = 15.0f;

    public float       meteoritesSpeed; // 메테오의 스피드를 지정
    public float       speedTime; // 시간이 쌓이먄 메테오 속도 변경. 웜홀 끝나면 속도 다시 돌아와야함 웜홀스포너에서 참조
    
    void Start()
    {
        Invoke("StartSpawnMeteorite", Random.Range(20.0f, 30.0f));
    }

    void Update()
    {
        speedTime = Time.deltaTime;
        if (speedTime <= 30f)
        {
            meteoritesSpeed = 30f;
        }
        else if (speedTime <= 60f)
        {
            meteoritesSpeed = 40f;
        }
        else if (speedTime >= 90f)
        {
            meteoritesSpeed = 50f;
        }
    }

    void StartSpawnMeteorite()
    {
        StartCoroutine("SpawnMeteorite");
    }

    IEnumerator SpawnMeteorite()
    {
        while (true)
        {
            if (PlayManager.isOver)
                yield break;

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            
            if (WormholeSpawner.isWormholeOn)
                yield return new WaitForSeconds(spawnTime); // 아예 코루틴을 끝내는거임 그래서... 플레이매니저에서 false로 바꾸기 전에 이미 끝나버린거....

            // y 위치는 스테이지 크기 범위 내에서 임의의 값을 선택
            float positionY = Random.Range(stageData.LimitMin.y + 3.0f, stageData.LimitMax.y -3.0f);
            //Quaternion rotation = Quaternion.Euler(0, 0, 90);
            
            if (ManagerInitPlay.isSatellite)
            {
                // 경고선 오브젝트 생성 (rotation에 Quaternion.identity를 사용 시, 내가 설정한 각도가 아닌 원래 오브젝트 각도로 고정됨)
                GameObject alertLineClone = Instantiate(alertLinePrefab, new Vector3(0, positionY, 0), Quaternion.identity);
                // 1초간 대기
                yield return new WaitForSeconds(1.0f);
                // 경고선 오브젝트 삭제
                Destroy(alertLineClone);
            }

            // 메테오 오브젝트 생성
            Vector3 meteoritePosition = new Vector3(stageData.LimitMax.x + 10f, positionY, 0);
            int randomMeteorite = Random.Range(0, 4);
            Instantiate(meteoriteObjs[randomMeteorite], meteoritePosition, Quaternion.identity);
            
            // 해당 시간만큼 대기 후 다음 로직 실행
            yield return new WaitForSeconds(spawnTime);
        }
    } 
}
