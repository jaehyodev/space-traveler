using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private float lifeTime = 0.25f;
    [SerializeField] GameObject deathPrefab; // 다이아몬드 폭발 효과
    PlayerController playerController;

    float moveSpeed = 3.0f;
    Vector3 moveDirection = new Vector3(-1,0,0);

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            ManagerDiamond managerDiamond = GameObject.Find("ManagerDiamond").GetComponent<ManagerDiamond>();    
            managerDiamond.PlaySoundDiamondPickup();
            Instantiate(deathPrefab, transform.position, Quaternion.identity);

            PlayerController.getDiamonds++;
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.diamondsCount.text = PlayerController.getDiamonds.ToString();

            Destroy(gameObject);
        }    
    }
}