using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlide : MonoBehaviour
{
    public int preStage = 1;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;

    [SerializeField]
    private Button prevBtn;
    [SerializeField]
    private Button nextBtn;
    
    private void Awake()
    {
        prevBtn.gameObject.SetActive(true);
        nextBtn.gameObject.SetActive(true);
    }
    
    private void Start()
    {
        if(!PlayerPrefs.HasKey("preStage"))
        {
            preStage = 1;
            StageLoad();
            Debug.Log("키가 없음" + preStage);
        }

        else
        {
            preStage = PlayerPrefs.GetInt("preStage");
            StageLoad();
            Debug.Log("키가 있음" + preStage);
        }
    }

    public void PrevStage()
    {
        if (!(preStage == 1))
        {
            preStage--;
            StageLoad();
        }
    }

    public void NextStage()
    {
        Debug.Log("다음버튼 실행");
        if (!(preStage == 3))
        {
            preStage++;
            StageLoad();
            Debug.Log("현재스테이지는" + preStage);
        }
    }

    public void StageLoad()
    {
        Debug.Log("스테이지로드실행");
        PlayerPrefs.SetInt("preStage", preStage);
        if (preStage == 1)
        {
            prevBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
            stage1.SetActive(true);
            stage2.SetActive(false);
            stage3.SetActive(false);
        }
       
        else if (preStage == 2)
        {
            prevBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(true);
            stage1.SetActive(false);
            stage2.SetActive(true);
            stage3.SetActive(false);
        }

        else if (preStage == 3)
        {
            prevBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
        }
    }
}
