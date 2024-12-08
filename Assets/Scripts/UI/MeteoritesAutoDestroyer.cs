using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritesAutoDestroyer : MonoBehaviour
{
    [SerializeField]
    private StageData   stageData;
    
    private float       destroyAreaX = 15.0f;
    private float       destroyAreaY = 15.0f;

    private void LateUpdate()
    {
        if ( transform.position.x < stageData.LimitMin.x - destroyAreaX ||
             transform.position.x > stageData.LimitMax.x + destroyAreaX ||
             transform.position.y < stageData.LimitMin.y - destroyAreaY ||
             transform.position.y > stageData.LimitMax.y + destroyAreaY )
        {
            Destroy(gameObject);
        }  
    }
}


/*
 * File : AutoDestroyer.cs
 * Desc
 *  : 화면 밖으로 나갈 수 있는 오브젝트에 부착해서 사용
 *  : 오브젝트가 일정 범위 바깥으로 낙면 삭제
 */
