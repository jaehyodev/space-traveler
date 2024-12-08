using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    void Update() 
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    
    // PlayerController.cs에서 플레이어가 방향키 입력 시, 이동할 수 있도록 방향을 지정해주는 함수
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
