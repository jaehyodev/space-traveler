using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoOption : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    void Start()
    {
        Debug.Log("시작");
    }

    void InitUI()
    {
        Debug.Log("함수시작");
     resolutions.AddRange(Screen.resolutions);
      foreach (Resolution item in resolutions)
      {
        Debug.Log(item.width + "x" + item.height + " " + item.refreshRate);
      }
    }
}
