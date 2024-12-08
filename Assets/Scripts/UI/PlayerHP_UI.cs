using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP_UI : MonoBehaviour
{
    private GameObject spaceship;
    [SerializeField]
    private PlayerHP playerHP;
    private Slider   sliderHP; // 플레이어 체력바
    [SerializeField]
    private TextMeshProUGUI textHP; // 플레이어 체력바 텍스트 (현재체력/최대체력)
    
    
    
    void Awake()
    {

        sliderHP = GetComponent<Slider>();
    //   sliderHP.value      = playerHP.curHP / playerHP.maxHP;
    //   textHP.text         = $"{playerHP.curHP}/{playerHP.maxHP}";
    }

    void Start()
    {
        spaceship = GameObject.FindGameObjectWithTag("Player");
        playerHP = spaceship.GetComponent<PlayerHP>();
    }
    // <summary>
    // 더 정확한 방법으로는 이벤트를 이용해 체력 정보가 바뀔때만 ui 정보 갱신
    // </summary>
    void Update()
    {
        // Slider UI에 현재 체력 정보를 업데이트
        sliderHP.value = playerHP.curHP / playerHP.maxHP;
        textHP.text = $"{playerHP.curHP}/{playerHP.maxHP}";

        // CurHp.text = (curHp).ToString(); 텍스트에 숫자를 텍스트로 변환하여 대입
        // MaxHp.text = (maxHp).ToString(); 텍스트에 숫자를 텍스트로 변환하여 대입
    }
}


/*
 * File : PlayerHP_UI.cs
 * Desc
 *  : 플레이어의 체력 정보를 Slider UI에 업데이트
 *
 */
