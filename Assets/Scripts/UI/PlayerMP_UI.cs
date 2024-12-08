using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMP_UI : MonoBehaviour
{
    private GameObject spaceship;
    [SerializeField]
    private PlayerMP playerMP;
    private Slider   sliderMP; // 플레이어 마나바
    [SerializeField]
    private TextMeshProUGUI textMP; // 플레이어 마나바 텍스트 (현재마나/최대마나)
    
    public Image ultManaImg;

 
    void Awake()
    {

        sliderMP = GetComponent<Slider>();
    //   sliderHP.value      = playerHP.curHP / playerHP.maxHP;
    //   textHP.text         = $"{playerHP.curHP}/{playerHP.maxHP}";
    }

    void Start()
    {
        spaceship = GameObject.FindGameObjectWithTag("Player");
        playerMP = spaceship.GetComponent<PlayerMP>();
    }

    void Update()
    {
        // Slider UI에 현재 체력 정보를 업데이트
        sliderMP.value = playerMP.curMP / playerMP.maxMP;
        textMP.text = $"{playerMP.curMP}/{playerMP.maxMP}";

        // CurHp.text = (curHp).ToString(); 텍스트에 숫자를 텍스트로 변환하여 대입
        // MaxHp.text = (maxHp).ToString(); 텍스트에 숫자를 텍스트로 변환하여 대입
    }
    // public void TakeDataMP()
    // {
    //     sliderMP = GetComponent<Slider>();
    //     spaceship = GameObject.FindGameObjectWithTag("Player");
    //     playerMP = spaceship.GetComponent<PlayerMP>();
    //     StartCoroutine("UpdateMana");
    // }

    // IEnumerator UpdateMana()
    // {
    //     while (true)
    //     {
    //         if (PlayManager.isOver)
    //             yield break;

    //         sliderMP.value = playerMP.curMP / playerMP.maxMP;
    //         textMP.text = $"{Mathf.CeilToInt(playerMP.curMP)}/{playerMP.maxMP}";
    //         yield return new WaitForSeconds(0.2f);
    //     }
    // }
}


/*
 * File : PlayerHP_UI.cs
 * Desc
 *  : 플레이어의 체력 정보를 Slider UI에 업데이트
 *
 */
