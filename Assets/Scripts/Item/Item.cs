using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPickUpObj;    // 아이템 먹는 이펙트

    private float moveSpeed = 3.0f;

    private Vector3 moveDirection = new Vector3(-1,0,0);


    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Death();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Death();
            Destroy(collision.gameObject);
        }
    }
    
    public void Death()
    {
        Instantiate(itemPickUpObj, transform.position, Quaternion.identity);

        ItemSpawner itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        itemSpawner.SoundItemPickUp();

        ItemCheck();   
    }

    void ItemCheck()
    {
        if (this.gameObject.name.Contains("Heart"))
        {
            PlayerHP playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
            playerHP.HeartPickUp();
        }

        if (this.gameObject.name.Contains("Shield"))
        {
            PlayerHP playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
            playerHP.ShieldPickUp();
        }

        if (this.gameObject.name.Contains("Wormhole"))
        {
            WormholeSpawner wormholeSpawner = GameObject.Find("WormholeSpawner").GetComponent<WormholeSpawner>();
            wormholeSpawner.Invoke("SpawnWormhole", 5.0f);
            WormholeSpawner.isWormhole = true;
        }

        Destroy(gameObject);
    }
}


/*
 * File : Meteorite.cs
 * Desc
 *  : 운석 오브젝트에 부착해서 사용
 *
 * Functions
 *
 */
