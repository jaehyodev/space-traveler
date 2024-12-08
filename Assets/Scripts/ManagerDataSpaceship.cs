using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDataSpaceship : MonoBehaviour
{
    public static ManagerDataSpaceship instanceDataSpaceship;
    public int spaceshipId;

    public GameManager gameManager;

    public string[] spaceshipName = new string[5];
    public float[] spaceshipHealth = new float[5];
    public float[] spaceshipSpeed = new float[5];
    public string[] spaceshipSpellName = new string[5];
    public string[] spaceshipSpellContent = new string[5];

    // 무기 데미지는 Bullet 프리팹 - Bullet에서 설정
    // 무기 공격속도는 Weapon 프리팹 - AutoFire에서 설정
    // 총알 이동속도는 Bullet 프리팹 - BulletMovement2D에서 설정

    void Awake()
    {        
        if ( instanceDataSpaceship != null )
        {
            Destroy(gameObject);
        }
        else
        {
            instanceDataSpaceship = this;

            DontDestroyOnLoad(gameObject);
        }
        //instanceDataSpaceship = this;
        
        //DontDestroyOnLoad(gameObject); //  gameObject변수는 이 클래스가 붙은 게임오브젝트를 지칭

        
    }
    
    void Start()
    {
        spaceshipName[0] = "Gumball";
        spaceshipName[1] = "Spriteball";

        spaceshipHealth[0] = 100;
        spaceshipHealth[1] = 100;

        spaceshipSpeed[0] = 3f;
        spaceshipSpeed[1] = 5f;

        spaceshipSpellName[0] = "LASER SHOW";
        spaceshipSpellName[1] = "EMP";

        spaceshipSpellContent[0] = "Launchs lasers from all weapons.";
        spaceshipSpellContent[1] = "Destroys all enemies around the spaceship.";

        gameManager.InitSpaceship();
        /* 우주선 로드 or 우주선 초기화 */
        if ( PlayerPrefs.GetInt("isSpaceshipSaved") == 1 )
            gameManager.LoadSpaceship();
    }

    // /* 처음에만 한번 실행 */
    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // /* 씬이 다시 로드될 때 실행 */
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     //gameManager.InitSpaceship();
    //     /* 우주선 로드 or 우주선 초기화 */
    //     if ( PlayerPrefs.GetInt("isSpaceshipSaved") == 1 )
    //     {
    //         //gameManager.LoadSpaceship();
    //     }
            
    // }

    //     void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }
}
