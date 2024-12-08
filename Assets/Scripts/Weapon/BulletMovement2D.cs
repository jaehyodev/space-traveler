using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    
    //VirtualJoystick virtualJoystick;
    public GameObject weaponPrefab; // 총알에 맞는 무기
    public float bulletSpeed; // 무기 설명에서 참조해야함
    //[SerializeField]
    //private Vector3 bulletDirection = Vector3.zero;

    void Start()
    {
        bulletSpeed = weaponPrefab.GetComponent<Weapon>().bulletSpeed;
        // Invoke("DestroyBullet", 2); // invoke는 앞에꺼를 몇초후에 발동 시키는 거 말하는듯
        // bulletDirection = virtualJoystick.weapon.position;
    }

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    /* 배경애서 벗어나면 지우는 방법
    // void OnBecameInvisible()
    // {
    //     Destroy(gameObject);
    // }
    // void DestroyBullet()
    // {
    //     Destroy(gameObject);
    // }
    */
}

/*
 * File : ProjectileMovement2D.cs
 * Desc
 *  : 플레이어 캐릭터의 공격 발사체 이동
 *
 * 발사체 이동속도 speed는 발사체 프리팹에 설정되어 있음.
 *
 */