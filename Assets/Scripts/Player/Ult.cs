using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ult : MonoBehaviour
{
    /* 충돌 */
    void OnTriggerEnter2D(Collider2D collision)
    {         
        if (this.gameObject.name.Contains("LaserShow"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Death();
            }
            
            if (collision.gameObject.CompareTag("Meteorite"))
            {
                collision.gameObject.GetComponent<Meteorite>().Death();
            }    
            if (collision.gameObject.CompareTag("Item"))
            {
                collision.gameObject.GetComponent<Item>().Death();
            }        
        }      
    }
}
