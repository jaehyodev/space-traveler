using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDiamond : MonoBehaviour
{
    public void PlaySoundDiamondPickup()
    {
        GetComponent<AudioSource>().Play();
    }
}
