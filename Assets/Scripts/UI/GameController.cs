using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
//     public static TextMeshProUGUI gameTime;
//     public static TextMeshProUGUI gameDistance;

//     public static float time = 0;
//     private float msec;
//     private float sec;
//     private float min;

    
//     public float distance = 0;

//     void Start()
//     {
//         StartCoroutine("GameTime");
//         StartCoroutine("GameDistance");
//     }

//     IEnumerator GameTime()
//     {
//         /* 시간:분:초
//         while(true)
//         {
//             gameTime += Time.deltaTime;
//             msec = (int)((gameTime - (int)gameTime) * 100);
//             sec = (int)(gameTime % 60);
//             min = (int)(gameTime / 60 % 60);

//             gameTimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
            
//             yield return null;
//         }
//         */

//         while(true)
//         {
//             time += Time.deltaTime;
//             sec = (int)(time % 60);
//             min = (int)(time / 60 % 60);

//             gameTime.text = string.Format("{0:00}:{1:00}", min, sec);
            
//             yield return null;
//         }
//     }

//     /* 1초에 4km씩 증가 */
//     IEnumerator GameDistance()
//     {
//         while ( true )
//         {
//             distance += 4 * Time.deltaTime;
//             gameDistance.text = string.Format("{0:0.00}", distance);
//             yield return null;
//             //https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=pxkey&logNo=221321776845
//         }
//     }

//     void Update()
//     {
//         /* 1초 씩 증가하는 타이머
//         time += Time.deltaTime;
//         gameTimer.text = "Time : " + (int)gameTime;
//         Debug.Log((int)gameTime); // 정수로 출력
//         */

//         /* 게임 타이머가 줄어들도록 하는 코딩
//         if((int)gameTime ==0 )
//         {
//             gameTimeText.text = "게임 종료";
//             Debug.Log("게임 종료");
//         }
//         else
//         {
//             gameTime -= Time.deltaTime;
//             gameTimeText.text = "Time : " + (int)gameTime;
//             Debug.Log((int)gameTime); // 정수로 출력
//         }
//         */
//     }
}
