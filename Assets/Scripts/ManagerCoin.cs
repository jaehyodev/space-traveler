using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; // system과 UnityEngine에서 충돌이 일어나서 기입해준다.


public class ManagerCoin : MonoBehaviour
{
    public float speed;
    public Transform coinCollection;
    public GameObject CoinPrefab;
    public Camera cam;

    public AudioClip[] sound;

    // Start is called before the first frame update
    void Start()
    {
        if ( cam == null )
        {
            cam = Camera.main;
        }
    }

    public void StartCoinMove (GameObject coinObj, Vector3 initial, Action onComplete)
    {
        Vector3 targetPos = cam.ScreenToWorldPoint(new Vector3(coinCollection.position.x, coinCollection.position.y, cam.transform.position.z * -1));
        StartCoroutine(MoveCoin ( coinObj.transform, initial, targetPos, onComplete) );
    }

    IEnumerator MoveCoin (Transform obj, Vector3 startPos, Vector3 endPos, Action onComplete)
    {
        float time = 0;

        while (time < 1)
        {
            time += speed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }
        onComplete.Invoke();
        ManagerCoin managerCoin = GameObject.Find("ManagerCoin").GetComponent<ManagerCoin>();    
        managerCoin.PlaySoundCoinPickup();
        Destroy(obj.gameObject);
    }

    public void PlaySoundCoinPickup()
    {
        int rand = Random.Range(0, sound.Length);
        GetComponent<AudioSource>().clip = sound[rand];
        GetComponent<AudioSource>().Play();
    }
}
