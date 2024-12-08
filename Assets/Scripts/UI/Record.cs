using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Record : MonoBehaviour
{
    public TextMeshProUGUI distance;
    public TextMeshProUGUI playtime;
    public TextMeshProUGUI longestDistance;
    public TextMeshProUGUI recordTime;

    public TextMeshProUGUI rewordCoin;
    public TextMeshProUGUI rewordDiamond;

    public GameObject playManager;

    void Start()
    {
        distance.text = string.Format("{0:0.0}" + "km", PlayManager.roundDistance);
        playtime.text = string.Format("{0:00}:{1:00}:{2:00}", PlayManager.min, PlayManager.sec, PlayManager.msec);
        
        longestDistance.text = string.Format("{0:0.0}" + "km", PlayerPrefs.GetFloat("LongestDistance"));
        recordTime.text = string.Format("{0:00}:{1:00}:{2:00}", PlayerPrefs.GetFloat("RecordMin"),
                                                                PlayerPrefs.GetFloat("RecordSec"),
                                                                PlayerPrefs.GetFloat("RecordMsec"));

        rewordCoin.text = PlayerController.getCoins.ToString();
        PlayerController.coins = PlayerPrefs.GetInt("Coins") + PlayerController.getCoins;
        PlayerPrefs.SetInt("Coins", PlayerController.coins);

        rewordDiamond.text = PlayerController.getDiamonds.ToString();
        PlayerController.diamonds = PlayerPrefs.GetInt("Diamonds") + PlayerController.getDiamonds;
        PlayerPrefs.SetInt("Diamonds", PlayerController.diamonds);
    }
}
