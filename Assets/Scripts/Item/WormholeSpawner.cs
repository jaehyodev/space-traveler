using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeSpawner : MonoBehaviour
{
    public GameObject wormholePrefab;

    [SerializeField]
    private StageData   stageData;

    public static bool isWormhole = false; // 웜홀을 먹음 그러면 웜홀이 더이상 나오면 안됨
    public static bool isWormholeOn = false; // 웜홀여행중
    public static float wormholeSpeed;


    public void SpawnWormhole()
    {
        Vector3 wormholePos = new Vector3(stageData.LimitMax.x + 3.0f, (stageData.LimitMin.y + stageData.LimitMax.y) / 2, 0);
        Instantiate(wormholePrefab, wormholePos, Quaternion.identity);
    }

    public void WormholeSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void WormholeEnd()
    {
        GetComponent<AudioSource>().Stop();

        isWormhole = false;
        isWormholeOn = false;
        wormholeSpeed = 0;

        BGScroller bGScroller1 = GameObject.FindWithTag("Background").GetComponent<BGScroller>();
        bGScroller1.speed = 0.01f;
        bGScroller1.accel = 0.0075f;

        BGScroller bGScroller2 = GameObject.Find("BGStarSmall").GetComponent<BGScroller>();
        bGScroller2.speed = 0.01f;
        bGScroller2.accel = 0.0075f;
                    
        BGScroller bGScroller3 = GameObject.Find("BGStarBig").GetComponent<BGScroller>();
        bGScroller3.speed = 0.01f;
        bGScroller3.accel = 0.0075f;

        MeteoriteSpawner meteoriteSpawner = GameObject.Find("MeteoriteSpawner").GetComponent<MeteoriteSpawner>();
        meteoriteSpawner.speedTime = 0f;
    }
}