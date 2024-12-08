using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInitPlay : MonoBehaviour
{
    public GameObject backgroundRed;
    public GameObject backgroundBlue;
    public GameObject backgroundPink;

    public GameObject[] prefabSpaceship;
    public GameObject[] prefabWeapon;

    public Transform trPlayerPos;

    [SerializeField]
    private int spaceshipTempId;
    [SerializeField]
    private int[] weaponTempId = new int[8];
    [SerializeField]
    private GameObject[] weaponObjs = new GameObject[8];
    [SerializeField]
    private int[] gadgetTempId = new int[3];

    public static bool isSpanner;
    public static bool isDollars;
    public static bool isBattery;
    public static bool isHelmet;
    public static bool isSatellite;

    public static float gadgetSpeed = 0;

    public GameObject managerDataSpaceship;
    public GameObject managerDataWeapon;


    // weaponSlot[0], weaponID[0] = 1번 슬롯 무기
    // weaponSlot[1], weaponID[1] = 2번 슬롯 무기
    // weaponSlot[2], weaponID[2] = 3번 슬롯 무기
    // weaponSlot[3], weaponID[3] = 4번 슬롯 무기
    // weaponSlot[4], weaponID[4] = 5번 슬롯 무기
    // weaponSlot[5], weaponID[5] = 6번 슬롯 무기
    // weaponSlot[6], weaponID[6] = 7번 슬롯 무기
    // weaponSlot[7], weaponID[7] = 8번 슬롯 무기
    
    // prefabWeapon[0] = 무기 고유번호 0
    // prefabWeapon[1] = 무기 고유번호 1
    // prefabWeapon[2] = 무기 고유번호 2
    // prefabWeapon[3] = 무기 고유번호 3
    // prefabWeapon[4] = 무기 고유번호 4
    // prefabWeapon[5] = 무기 고유번호 5
    // prefabWeapon[6] = 무기 고유번호 6
    // prefabWeapon[7] = 무기 고유번호 7

    // weapon[0] = 0, 소형 입자포 보라
    // weapon[1] = 1, 소형 입자포 그린
    // weapon[2] = 2, 소형 입자포 레드
    // weapon[3] = 3, 소형 미니건

    void Start() 
    {       
        /* 배경 받아오기 */
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                backgroundRed.SetActive(true);
                backgroundBlue.SetActive(false);
                backgroundPink.SetActive(false);
                break;
            case 1:
                backgroundRed.SetActive(false);
                backgroundBlue.SetActive(true);
                backgroundPink.SetActive(false);
                break;
            case 2:   
                backgroundRed.SetActive(false);
                backgroundBlue.SetActive(false);
                backgroundPink.SetActive(true);
                break; 
        }

        /* 우주선 데이터 받아오기 */
        SetSpaceship();
        /* 무기 데이터 받아오기 */
        SetWeapons();
        /* 가젯 데이터 받아오기 */
        SetGadgets(); 
        
    }


    void SetSpaceship()
    {
        managerDataSpaceship = GameObject.Find("ManagerDataSpaceship");
        spaceshipTempId = ManagerDataSpaceship.instanceDataSpaceship.spaceshipId;
        // 프리팹우주선에 먼저 속도를 적용하고 나서 생성해야 생성된 프리팹에 속도가 적용된다.
        prefabSpaceship[spaceshipTempId].GetComponent<PlayerController>().spaceshipSpeed = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[spaceshipTempId];
        Instantiate(prefabSpaceship[spaceshipTempId], Vector3.zero, Quaternion.identity);
        //PlayerMP_UI playerMP_UI = GameObject.Find("SliderMP").GetComponent<PlayerMP_UI>();
        //playerMP_UI.TakeDataMP();
        
        switch (spaceshipTempId)
        {
            case 0:
                GameObject.Find("Button - Ult").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Buttons/Ults/Button_Ult_LaserShow");
                break;
            case 1:
                GameObject.Find("Button - Ult").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Buttons/Ults/Button_Ult_EMP");
                break;
        }
    }

    void SetWeapons()
    {
        GameObject spaceship = GameObject.FindGameObjectWithTag("Player");
        trPlayerPos = spaceship.transform;

        managerDataWeapon = GameObject.Find("ManagerDataWeapon");

        print("무기1번" + ManagerDataWeapon.instanceDataWeapon.weaponId[0]); // 3
        print("무기2번" + ManagerDataWeapon.instanceDataWeapon.weaponId[1]); // 2
        print("무기3번" + ManagerDataWeapon.instanceDataWeapon.weaponId[2]); // 1
        print("무기4번" + ManagerDataWeapon.instanceDataWeapon.weaponId[3]); // 0
        print("무기5번" + ManagerDataWeapon.instanceDataWeapon.weaponId[4]); // 3
        print("무기6번" + ManagerDataWeapon.instanceDataWeapon.weaponId[5]); // 2
        print("무기7번" + ManagerDataWeapon.instanceDataWeapon.weaponId[6]); // 1
        print("무기8번" + ManagerDataWeapon.instanceDataWeapon.weaponId[7]); // 0
            
        for ( int i = 0; i <= 7; i++ )
        {
            // weaponTempId[0~7] = 1~8번 슬롯의 현재 무기에 대한 고유 번호
            weaponTempId[i] = ManagerDataWeapon.instanceDataWeapon.weaponId[i];
            //Debug.Log(weaponTempId[i]);

            if ( weaponTempId[i] == 100 )
            {
                continue; // 무기가 비어있으면 생성하지 않아야함.
            }  
            
            // 무기 태그 설정
            switch ( i )
            {
                case 0:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponFirst";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;                
                case 1:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponSecond";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
                case 2:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponThird";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
                case 3:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponFourth";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
                case 4:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponFifth";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
                case 5:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponSixth";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
                case 6:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponSeventh";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
                case 7:
                    prefabWeapon[weaponTempId[i]].tag = "WeaponEighth";
                    Debug.Log(i + " : " + weaponTempId[i] + prefabWeapon[weaponTempId[i]].tag);
                    break;
            }
            
            prefabWeapon[weaponTempId[i]].GetComponent<Weapon>().damage = managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponDamage[weaponTempId[i]];
            prefabWeapon[weaponTempId[i]].GetComponent<Weapon>().attackRate = managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponAttackRate[weaponTempId[i]];
            prefabWeapon[weaponTempId[i]].GetComponent<Weapon>().bulletSpeed = managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponBulletSpeed[weaponTempId[i]];
            
            weaponObjs[i] = Instantiate(prefabWeapon[weaponTempId[i]], trPlayerPos);
            weaponObjs[i].transform.RotateAround(spaceship.transform.position, new Vector3(0, 0, 1), i*-45f);
        }
    }

    void SetGadgets()
    {
        isSpanner = false;
        isDollars = false;
        isBattery = false;
        isHelmet = false;
        isSatellite = false;

        for (int i = 0; i <= 2; i++)
        {
            gadgetTempId[i] = ManagerDataGadget.instanceDataGadget.gadgetId[i];

            switch (gadgetTempId[i])
            {
                case 0:
                    isSpanner = true;
                    break;
                case 1:
                    isDollars = true;
                    break;
                case 2:
                    isBattery = true;
                    break;
                case 3:
                    isHelmet = true;
                    gadgetSpeed += 1;
                    break;
                case 4:
                    isSatellite = true;
                    break;
            }    
        }        
    }
}
