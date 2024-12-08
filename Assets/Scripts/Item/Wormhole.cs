using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    private float moveSpeed = 2.0f;

    private Vector3 moveDirection = new Vector3(-1,0,0);


    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에게 부딪힌 오브젝트의 태그가 "Player"이면
        if (collision.CompareTag("Player"))
        {
            WormholeSpawner.isWormholeOn = true;
            WormholeSpawner wormholeSpawner = GameObject.Find("WormholeSpawner").GetComponent<WormholeSpawner>();
            wormholeSpawner.Invoke("WormholeEnd", 5.0f);
            wormholeSpawner.WormholeSound();

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().DeathEffect(); 
                Destroy(enemy);
            }
            GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
            foreach (GameObject meteorite in meteorites)
            {
                meteorite.GetComponent<Meteorite>().DeathEffect();
                Destroy(meteorite);
            }
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            foreach (GameObject item in items)
            {
                Destroy(item);
            }

            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.StartCoroutine("WormholeVibe");

            WormholeSpawner.wormholeSpeed = 100;

            BGScroller bGScroller1 = GameObject.FindWithTag("Background").GetComponent<BGScroller>();
            bGScroller1.speed = 10.0f;
            bGScroller1.accel = 0;

            BGScroller bGScroller2 = GameObject.Find("BGStarSmall").GetComponent<BGScroller>();
            bGScroller2.speed = 5.0f;
            bGScroller2.accel = 0;
                    
            BGScroller bGScroller3 = GameObject.Find("BGStarBig").GetComponent<BGScroller>();
            bGScroller3.speed = 1.0f;
            bGScroller3.accel = 0;
            Destroy(gameObject);
        }
    }


}