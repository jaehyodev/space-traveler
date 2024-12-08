using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BlinkingText : MonoBehaviour {
    
    TextMeshProUGUI startText;

    
    void Start()
    {
        startText = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 1);
            yield return new WaitForSeconds(0.5f);
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void StartBlinking()
    {
        StartCoroutine("Blink");
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}