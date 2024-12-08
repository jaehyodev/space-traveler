using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDataGadget : MonoBehaviour
{
    public static ManagerDataGadget instanceDataGadget;
    public int[] gadgetId;

    public GameManager gameManager;

    /* 무기 스프라이트 */
    public Sprite[] gadgetSprites = new Sprite [5]; // 무기 이미지에 사용할 스프라이트, 무기 장착 스크립트에서 참조
    public string[] gadgetNames = new string[5];
    public string[] gadgetContents = new string[5];

    // 무기 데미지는 Bullet 프리팹 - Bullet에서 설정
    // 무기 공격속도는 gadget 프리팹 - AutoFire에서 설정
    // 총알 이동속도는 Bullet 프리팹 - BulletMovement2D에서 설정

    void Awake()
    {        
        if ( instanceDataGadget != null )
        {
            Destroy(gameObject);
        }
        else
        {
            instanceDataGadget = this;

            DontDestroyOnLoad(gameObject);
        }
        // instanceDatagadget = this;
        
        // DontDestroyOnLoad(gameObject); //  gameObject변수는 이 클래스가 붙은 게임오브젝트를 지칭
    
        
    }
    

    void Start()
    {
        gadgetSprites[0] = Resources.Load<Sprite>("Images/Gadgets/Gadget_Spanner");
        gadgetSprites[1] = Resources.Load<Sprite>("Images/gadgets/Gadget_Dollars");
        gadgetSprites[2] = Resources.Load<Sprite>("Images/gadgets/Gadget_Battery");
        gadgetSprites[3] = Resources.Load<Sprite>("Images/gadgets/Gadget_Helmet");
        gadgetSprites[4] = Resources.Load<Sprite>("Images/gadgets/Gadget_Satellite");

        gadgetNames[0] = "Spanner";
        gadgetNames[1] = "Dollars";
        gadgetNames[2] = "Battery";
        gadgetNames[3] = "Helmet";
        gadgetNames[4] = "Satellite";

        gadgetContents[0] = " It'll repair your spaceship. Regenerates 1 health every 5 seconds.";
        gadgetContents[1] = " You can get dollars when you get a coin.";
        gadgetContents[2] = " When you use an ult, you can take 20 mana back.";
        gadgetContents[3] = " You can enjoy +1km/s spaceship speed.";
        gadgetContents[4] = " You can find meteorites in advance.";

        gameManager.InitGadget();

        /* 가젯 로드 or 가젯 초기화 */
        if ( PlayerPrefs.GetInt("IsGadgetSaved") == 1 )
            print("가젯 세이브변수 온 그래서 로드합니다.");
            gameManager.LoadGadget();
    }

    /* 처음에만 한번 실행 */
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /* 씬이 다시 로드될 때 실행 */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //gameManager.InitGadget();
        /* 가젯 로드 or 가젯 초기화 */
        if ( PlayerPrefs.GetInt("IsGadgetSaved") == 1 )
            print("가젯 세이브변수 온 그래서 로드합니다.");
            //gameManager.LoadGadget();
    }

        void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
