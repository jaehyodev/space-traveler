using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public static bool isOver;
    public GameObject record;
    public GameObject spaceship;

    public Button pauseBtn;
    public Button continueBtn;
    public Button quitBtn;
    public Button restartOfPauseBtn;
    public Button homeBtn;
    public Button restartOfGameOverBtn;
    public GameObject pauseSet;
    
    public GameObject managerScene;
    public GameObject gameOver;
    
    public TextMeshProUGUI gameTime;
    public TextMeshProUGUI gameDistance;

    public static float time;
    public static float msec;
    public static float sec;
    public static float min;

    public float distance;
    public static float roundDistance;
    public float longestDistance;
    public static float recordMsec;
    public static float recordSec;
    public static float recordMin;

    public float speed;


    void Start()
    {
        isOver = false;
           
        spaceship = GameObject.FindWithTag("Player");

        distance = 0;
        roundDistance = 0;
        time = 0;
        msec = 0;
        sec = 0;
        min = 0;

        if ( PlayerPrefs.HasKey("LongestDistance"))
        {
            longestDistance = PlayerPrefs.GetFloat("LongestDistance");
        }
        else
        {
            longestDistance = 0;
        }

        if ( PlayerPrefs.HasKey("RecordMsec"))
        {
            recordMsec = PlayerPrefs.GetFloat("RecordMsec");
        }
        else
        {
            recordMsec = 0;
        }

        if ( PlayerPrefs.HasKey("RecordSec"))
        {
            recordSec = PlayerPrefs.GetFloat("RecordSec");
        }
        else
        {
            recordSec = 0;
        }

        if ( PlayerPrefs.HasKey("RecordMin"))
        {
            recordMin = PlayerPrefs.GetFloat("RecordMin");
        }
        else
        {
            recordMin = 0;
        }

        StartCoroutine("GameTime");
        StartCoroutine("GameDistance");
        pauseBtn.onClick.AddListener(() => Pause());
        continueBtn.onClick.AddListener(Continue);
        restartOfPauseBtn.onClick.AddListener(Restart);
        quitBtn.onClick.AddListener(Quit);
        homeBtn.onClick.AddListener(Home);
        restartOfGameOverBtn.onClick.AddListener(Restart);
    }

    IEnumerator GameTime()
    {
        /* 시간:분:초
        while(true)
        {
            gameTime += Time.deltaTime;
            msec = (int)((gameTime - (int)gameTime) * 100);
            sec = (int)(gameTime % 60);
            min = (int)(gameTime / 60 % 60);

            gameTimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
            
            yield return null;
        }
        */

        while(true)
        {
            time += Time.deltaTime;
            //print("총시간 : " + time + ", 분은 : " + min + ", 초는 " + sec);
            msec = ((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            gameTime.text = string.Format("{0:00}:{1:00}", min, sec);
            
            yield return null;
        }
    }

    /* 1초에 4km씩 증가 */
    IEnumerator GameDistance()
    {
        while ( true )
        {
            speed = spaceship.GetComponent<PlayerController>().spaceshipSpeed + ManagerInitPlay.gadgetSpeed + WormholeSpawner.wormholeSpeed;
            distance += speed * Time.deltaTime;
            roundDistance = (float)(( Mathf.Round(distance * 10) ) / 10);
            gameDistance.text = string.Format("{0:0.0}", roundDistance);
            
            // if ( roundDistance >= longestDistance )
            // {
            //     longestDistance = roundDistance;
            //     recordMsec = msec;
            //     recordSec = sec;
            //     recordMin = min;
            // }

            yield return null;
            //https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=pxkey&logNo=221321776845
        }
    }

    public void Pause()
    {
        pauseSet.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseSet.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        //Application.Quit();
        managerScene.GetComponent<ManagerScene>().SceneLoadMain();
        Time.timeScale = 1;
    }

    public void Restart()
    {
        managerScene.GetComponent<ManagerScene>().SceneLoadPlay();
        Time.timeScale = 1;
    }

    public void Home()
    {
        managerScene.GetComponent<ManagerScene>().SceneLoadMain();
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        isOver = true;
        Debug.Log("Player HP : 0.. Die");
        StopCoroutine("GameTime");
        StopCoroutine("GameDistance");
            /* 플레이어를 참조하니 미리 파괴시키자 */
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // foreach(GameObject enemy in enemies )
            // {
            //     Destroy(enemy);
            // }

            // GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
            // foreach(GameObject meteorite in meteorites )
            // {
            //     Destroy(meteorite);
            // }

        if ( roundDistance >= longestDistance )
        {
            longestDistance = roundDistance;
            recordMsec = msec;
            recordSec = sec;
            recordMin = min;

            PlayerPrefs.SetFloat("LongestDistance", longestDistance);
            PlayerPrefs.SetFloat("RecordMsec", recordMsec);
            PlayerPrefs.SetFloat("RecordSec", recordSec);
            PlayerPrefs.SetFloat("RecordMin", recordMin);
            PlayerPrefs.Save();
        }
        
        //다른 버튼들은 못 움직이게 막아야함.
        pauseBtn.enabled = false;
        
        //Time.timeScale = 0; 0으로 하면 아예 멈춰서 자연스럽지 않다.

        Invoke("OpenGameOverSet", 2.0f);        
    }

    public void OpenGameOverSet()
    {
        gameOver.SetActive(true);
    }

        
    // 테스트
    public void HpZero()
    {
        PlayerHP playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        playerHP.TakeDamage(100);
  
        GameOver();
    }

    // 테스트
    public void RecordReset()
    {
        longestDistance = 0;
        recordMsec = 0;
        recordSec = 0;
        recordMin = 0;
        PlayerPrefs.DeleteKey("LongetDistance");
        PlayerPrefs.DeleteKey("RecordMsec");
        PlayerPrefs.DeleteKey("RecordSec");
        PlayerPrefs.DeleteKey("RecordMin");
    }
}
