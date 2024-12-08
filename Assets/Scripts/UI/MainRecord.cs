using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainRecord : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI diamonds;

    public TextMeshProUGUI longestDistance;
    public TextMeshProUGUI recordTime;
    

    void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        else
        {
            coins.text = "0";
        }

        if (PlayerPrefs.HasKey("Diamonds"))
        {
            diamonds.text = PlayerPrefs.GetInt("Diamonds").ToString();
        }
        else
        {
            diamonds.text = "0";
        }

        if (PlayerPrefs.HasKey("LongestDistance"))
        {
            longestDistance.text = string.Format("{0:0.0}" + "km", PlayerPrefs.GetFloat("LongestDistance"));
            recordTime.text = string.Format("{0:00}:{1:00}:{2:00}", PlayerPrefs.GetFloat("RecordMin"),
                                                                    PlayerPrefs.GetFloat("RecordSec"),
                                                                    PlayerPrefs.GetFloat("RecordMsec"));
        }
    }
}