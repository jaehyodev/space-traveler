using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : MonoBehaviour
{
    public float           maxMP = 100;
    public float           curMP = 0;

    public float            manaRegenTime = 0;
    

    void Update()
    {
        // 마나가 100을 넘으면 안된다.
        curMP = Mathf.Min(curMP, 100);
        
        
        manaRegenTime += Time.deltaTime;
        if (manaRegenTime >= 1f)
        {
            if(curMP < 100f)
            {
                curMP += 1;
            }
            manaRegenTime = 0;
        }
    }

    public void UseUlt()
    {
        if (!ManagerInitPlay.isBattery)
        {
            curMP = 0;
        }
        else
        {
            curMP = 20;
        }
    }
}
