using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemObjs;
    [SerializeField]
    private GameObject  itemPrefab;

    [SerializeField]
    private StageData   stageData;

    private float       minSpawnTime = 15.0f;
    private float       maxSpawnTime = 45.0f;

    private int         randomItem;
    
    void Start()
    {
        Invoke("StartSpawnItem", Random.Range(30.0f, 60.0f));
    }
    
    void StartSpawnItem()
    {
        StartCoroutine("SpawnItem");
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            if (PlayManager.isOver)
                yield break;

            // 아이템 생성 대기시간 설정
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            print("아이템 젠시간:"+ spawnTime);
            
            if (WormholeSpawner.isWormholeOn)
                yield return new WaitForSeconds(spawnTime); // 아예 코루틴을 끝내는거임 그래서... 플레이매니저에서 false로 바꾸기 전에 이미 끝나버린거....

            // y 위치는 스테이지 크기 범위 내에서 임의의 값을 선택
            float positionY = Random.Range(stageData.LimitMin.y + 3.0f, stageData.LimitMax.y -3.0f);
            
            // 아이템 오브젝트 생성
            Vector3 itemPos = new Vector3(stageData.LimitMax.x + 3.0f, positionY, 0);
            
            if (WormholeSpawner.isWormhole)
            {
                randomItem = Random.Range(1, 9);
            }
            else
            {
                randomItem = Random.Range(1, 11);
            }
            
            switch (randomItem)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    randomItem = 0;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    randomItem = 1;
                    break;
                case 9:
                case 10:
                    randomItem = 2;
                    break;
            }
            
            Instantiate(itemObjs[randomItem], itemPos, Quaternion.identity);

            // 해당 시간만큼 대기 후 다음 로직 실행, 잠깐 대기만 하는거임
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void SoundItemPickUp()
    {
        GetComponent<AudioSource>().Play();
    }
}