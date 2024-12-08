using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public StageData stageData;
    public GameObject[] enemyPrefabs;
    public GameObject enemy;
    
    //public GameObject test; // 화면 크기를 알아보기 위해 생성할 오브젝트

    public float maxSpawnDelay;
    public float curSpawnDelay;
    
    public int gameContents;    

    void Start() 
    {
        curSpawnDelay = 0;

        // // 화면 크기를 알아보기 위해 오브젝트를 생성
        // Instantiate(test, new Vector3(15, 0, 0), Quaternion.identity); 
        // Instantiate(test, new Vector3(16, 0, 0), Quaternion.identity); 
        // Instantiate(test, new Vector3(17, 0, 0), Quaternion.identity);    
        // Instantiate(test, new Vector3(18, 0, 0), Quaternion.identity); 
        
        // Instantiate(test, new Vector3(0, 7, 0), Quaternion.identity); 
        // Instantiate(test, new Vector3(0, 8, 0), Quaternion.identity); 
        // Instantiate(test, new Vector3(0, 9, 0), Quaternion.identity);    
        // Instantiate(test, new Vector3(0, 10, 0), Quaternion.identity); 
    }

    void Update()
    {
        if (WormholeSpawner.isWormholeOn || PlayManager.time < 3.0f || PlayManager.isOver)
            return;
            
        curSpawnDelay += Time.deltaTime;
        
        /* 시간대에 나올 몬스터 설정 */
        if (PlayManager.time < 60.0f)
        {
            enemy = enemyPrefabs[0];
        } else if (PlayManager.time >= 60.0f && PlayManager.time < 120.0f)
        {
            enemy = enemyPrefabs[1];
        } else if (PlayManager.time >= 120.0f)
        {
            enemy = enemyPrefabs[2];
        }

        /* 몬스터 생성 */
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.25f, 0.5f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int randomLoc = Random.Range(0, 4);
        float randomPosX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
        float randomPosY = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);

        switch (randomLoc)
        {
            case (0):
                //print("상단에서 출현");
                Instantiate(enemy, new Vector3(randomPosX, stageData.LimitMax.y+1.0f, 0.0f), enemy.transform.rotation);
                break;
            case (1):
                //print("우측에서 출현");
                Instantiate(enemy, new Vector3(stageData.LimitMax.x+1.0f, randomPosY, 0.0f), enemy.transform.rotation);
                break;
            case (2):
                //print("하단에서 출현");
                Instantiate(enemy, new Vector3(randomPosX, stageData.LimitMax.y-1.0f, 0.0f), enemy.transform.rotation);
                break;
            case (3):
                //print("좌측에서 출현");
                Instantiate(enemy, new Vector3(stageData.LimitMax.x-1.0f, randomPosY, 0.0f), enemy.transform.rotation);
                break;
        }
    }

    /* 코루틴으로 적을 생성하는 방법 */
    // Start 함수에 StartCoroutine("SpawnEnemy"); 입력
    //
    // IEnumerator SpawnEnemy()
    // {
    //     while ( true )
    //     {
    //         x 위치는 스테이지의 크기 범위 내에서 임의의 값을 선택
    //         float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
    //         적 캐릭터 생성(생성유닛, 위치, 각도)
    //         Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y-3.0f, 0.0f), enemyPrefab.transform.rotation);
    //         spawnTime만큼 대기
    //         yield return new WaitForSeconds(WhiteFlyspawnTime);
    //     }
    // }

}
