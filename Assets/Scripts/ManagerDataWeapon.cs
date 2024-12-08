using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDataWeapon : MonoBehaviour
{
    public static ManagerDataWeapon instanceDataWeapon;
    public int[] weaponId;

    public GameManager gameManager;

    /* 무기 스프라이트 */
    public Sprite[] weaponSprites = new Sprite [30]; // 무기 이미지에 사용할 스프라이트, 무기 장착 스크립트에서 참조
    public string[] weaponName = new string[30];
    public string[] weaponType = new string[30];
    public float[] waeaponRange = new float[30];
    public int[] weaponDamage = new int[30];
    public float[] weaponAttackRate = new float[30];
    public float[] weaponBulletSpeed = new float[30];

    // 무기 데미지는 Bullet 프리팹 - Bullet에서 설정
    // 무기 공격속도는 Weapon 프리팹 - AutoFire에서 설정
    // 총알 이동속도는 Bullet 프리팹 - BulletMovement2D에서 설정

    void Awake()
    {        
        if ( instanceDataWeapon != null )
        {
            Destroy(gameObject);
        }
        else
        {
            instanceDataWeapon = this;

            DontDestroyOnLoad(gameObject);
        }
        // instanceDataWeapon = this;
        
        // DontDestroyOnLoad(gameObject); //  gameObject변수는 이 클래스가 붙은 게임오브젝트를 지칭
    }
    
    void Start()
    {
        weaponSprites[0] = Resources.Load<Sprite>("Images/Weapons/L_Cannon-L");
        weaponSprites[1] = Resources.Load<Sprite>("Images/Weapons/L_MachineGun");
        weaponSprites[2] = Resources.Load<Sprite>("Images/Weapons/L_ParticleCannon");
        weaponSprites[3] = Resources.Load<Sprite>("Images/Weapons/L_LaserGun");
        weaponSprites[10] = Resources.Load<Sprite>("Images/Weapons/M_Cannon-M");
        weaponSprites[11] = Resources.Load<Sprite>("Images/Weapons/M_GrenadeLauncher");
        weaponSprites[12] = Resources.Load<Sprite>("Images/Weapons/M_MiniGun");
        weaponSprites[20] = Resources.Load<Sprite>("Images/Weapons/H_DoubleCannon-H");
        weaponSprites[21] = Resources.Load<Sprite>("Images/Weapons/H_FlameThrower");
        weaponSprites[22] = Resources.Load<Sprite>("Images/Weapons/H_Vulcan");
        weaponSprites[23] = Resources.Load<Sprite>("Images/Weapons/H_RailGun");
        weaponName[0] = "Cannon-L";
        weaponName[1] = "MachineGun";
        weaponName[2] = "ParticleCannon";
        weaponName[3] = "Laser";

        weaponName[10] = "Cannon-M";
        weaponName[11] = "GrenadeLauncher";
        weaponName[12] = "MiniGun";
        
        weaponName[20] = "DoubleCannon-H";
        weaponName[21] = "FlameThrower";
        weaponName[22] = "Vulcan";
        weaponName[23] = "RailGun";

        for ( int i = 0; i <= 29; i++ )
        {
            if ( i <= 9 )
            {
                weaponType[i] = "Light";
            } else if ( i <= 19 )
            {
                weaponType[i] = "Medium";
            } else
            { 
                weaponType[i] = "Heavy";
            }
        }

        weaponDamage[0] = 15;   // 30
        weaponDamage[1] = 10;   // 40
        weaponDamage[2] = 15;   // 75 
        weaponDamage[3] = 20;   // 120

        weaponDamage[10] = 25;
        weaponDamage[11] = 40;
        weaponDamage[12] = 30;
        
        weaponDamage[20] = 50;
        weaponDamage[21] = 80;
        weaponDamage[22] = 60;
        weaponDamage[23] = 100;

        weaponAttackRate[0] = 0.4f;
        weaponAttackRate[1] = 0.25f;
        weaponAttackRate[2] = 0.2f;
        weaponAttackRate[3] = 0.15f;

        weaponAttackRate[10] = 1.5f;
        weaponAttackRate[11] = 3.0f;
        weaponAttackRate[12] = 0.15f;

        weaponAttackRate[20] = 2.0f;
        weaponAttackRate[21] = 2.5f;
        weaponAttackRate[22] = 0.1f;
        weaponAttackRate[23] = 3.0f;

        weaponBulletSpeed[0] = 10.0f;
        weaponBulletSpeed[1] = 15.0f;
        weaponBulletSpeed[2] = 20.0f;
        weaponBulletSpeed[3] = 30.0f;

        weaponBulletSpeed[10] = 15.0f;
        weaponBulletSpeed[11] = 20.0f;
        weaponBulletSpeed[12] = 50.0f;

        weaponBulletSpeed[20] = 20.0f;
        weaponBulletSpeed[21] = 10.0f;
        weaponBulletSpeed[22] = 75.0f;
        weaponBulletSpeed[23] = 80.0f;

        gameManager.InitWeapon();
        /* 무기 로드 or 무기 초기화 */
        if ( PlayerPrefs.GetInt("isWeaponSaved") == 1 )
            print("무기 세이브변수 온 그래서 로드합니다.");
            gameManager.LoadWeapon();
    }
}
