using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private MeshRenderer render;
    public float speed;
    public float accel;
    private float offset;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        BGScroller bGScroller1 = GameObject.FindWithTag("Background").GetComponent<BGScroller>();
        bGScroller1.speed = 0.01f;
        bGScroller1.accel = 0.0075f;

        BGScroller bGScroller2 = GameObject.Find("BGStarSmall").GetComponent<BGScroller>();
        bGScroller2.speed = 0.01f;
        bGScroller2.accel = 0.0075f;
                    
        BGScroller bGScroller3 = GameObject.Find("BGStarBig").GetComponent<BGScroller>();
        bGScroller3.speed = 0.01f;
        bGScroller3.accel = 0.0075f;
    }

    void Update()
    {
        if (PlayManager.isOver)
        {
            speed -= ( accel * Time.deltaTime );
            if ( speed <= 0 )
            {
                return;
            }
        }
        else
        {
            if (speed < 0.5f)
                speed += ( accel * Time.deltaTime );
        }
        
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0); 
    }
}
