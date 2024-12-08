using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject weaponPrefab; // 총알에 맞는 무기
    public float damage; // 데미지 설정은 ManagerDataWeapon 클래스에서 설정하고 대입은 ManagerInitPlay에서 한다.
    public int n = 1; // 적을 몇 명 타겟할 수 있는 가?

    void Start()
    {
        damage = weaponPrefab.GetComponent<Weapon>().damage;
        if (this.gameObject.name.Contains("RailGun") || this.gameObject.name.Contains("FlameThrower"))
        {
            n = 100;
        }   
    }

    /* 충돌 */
    void OnTriggerEnter2D(Collider2D collision)
    {         
        if (collision.gameObject.CompareTag("Enemy"))
        {
            n -= 1;
            
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.OnHit(damage);
        }

        if (collision.gameObject.CompareTag("Meteorite"))
        {
            n -= 100;
            
            Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();
            meteorite.OnHit(damage);
        }
        
        if ( n <= 0 )
        {
            Destroy(gameObject);
        }
    }
}


/*
 * File : Projectile.cs
 * Desc
 *  : 플레이어 캐릭터의 공격 발사체
 *
 * Functions
 *  : OnTriggerEnter2D(나와 충돌한 녀석) - 적과 충돌했을 때 처리
 *  나에게는 콜리젼, 리지드 필요
 *  적에게는 콜리젼 필요
 *
 */
