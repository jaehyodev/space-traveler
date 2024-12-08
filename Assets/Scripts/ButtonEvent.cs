using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
//    내가 생각한 방법... 하지만 비효율적
//     public void GoBackToMenuScene()
//     {
//           SceneManager.LoadScene("MenuScene");  
//     }
    
//     public void GoToPlayScene()
//     {
//           SceneManager.LoadScene("PlayScene");  
//     }

      public void SceneLoader(string sceneName)
      {
            if (sceneName == "Stage01_1")
            {
                  LoadingSceneController.LoadScene(sceneName);
            }
            else
            {
                  SceneManager.LoadScene(sceneName);
            }
      }
}

/*
 * File : ButtonEvent.cs
 * Desc
 *  : Button UI 오브젝트에 부착해서 사용
 *  : 버튼을 눌렀을 때 호출되는 함수들을 작성
 *
 * Functions
 *  : SceneLoader() - 씬 전환
 *
 */