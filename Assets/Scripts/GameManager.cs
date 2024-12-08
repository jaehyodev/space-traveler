using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanceGameManager;
    
    public TextMeshProUGUI coin;
    public TextMeshProUGUI diamond;
    public TextMeshProUGUI recordDistance;
    public TextMeshProUGUI recordTime;
    
    public SpaceshipEquipment spaceshipEquipment;
    public WeaponEquipment weaponEquipment;
    public GadgetEquipment gadgetEquipment;

    public GameObject managerDataSpaceship;
    
    public GameObject managerDataWeapon;
    public ManagerDataWeapon managerDataWeaponSprites;

    public GameObject managerDataGadget;
    public ManagerDataGadget managerDataGadgetSprites;

    /* 우주선 스프라이트 */
    public Sprite[] spaceshipSprite = new Sprite [5]; // 우주선 이미지에 사용할 스프라이트, 우주선 장착 스크립트에서 참조



    // /* 무기 스프라이트 */
    // public Sprite[] weaponSprite = new Sprite [30]; // 무기 이미지에 사용할 스프라이트, 무기 장착 스크립트에서 참조

    /* 무기 인벤토리 버튼 */
    [SerializeField]
    private Button spaceshipBtn; // 우주선 인벤토리 버튼을 누르면 강조표시가 0번으로 이동하고 우주선 설명을 보여준다.
    [SerializeField]
    private Button weaponBtn; // 무기 인벤토리 버튼을 누르면 강조표시가 0번으로 이동하고 무기 설명을 보여준다.
    [SerializeField]
    private Button gadgetBtn; // 무기 인벤토리 버튼을 누르면 강조표시가 0번으로 이동하고 무기 설명을 보여준다.
    [SerializeField]
    private Button playBtn; // 플레이 버튼을 누르면 무기 데이터를 넘겨준다.

    
    void Awake()
    {
        if (instanceGameManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceGameManager = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    void Start ()
    {
        managerDataWeaponSprites = managerDataWeapon.GetComponent<ManagerDataWeapon>();
        spaceshipSprite[0] = Resources.Load<Sprite>("Images/Spaceships/Gumball");
        spaceshipSprite[1] = Resources.Load<Sprite>("Images/Spaceships/Spriteball");
        managerDataGadgetSprites = managerDataGadget.GetComponent<ManagerDataGadget>();
        // weaponSprite[0] = Resources.Load<Sprite>("Images/Weapons/L_Cannon-L");
        // weaponSprite[1] = Resources.Load<Sprite>("Images/Weapons/L_MachineGun");
        // weaponSprite[2] = Resources.Load<Sprite>("Images/Weapons/L_ParticleCannon");
        // weaponSprite[3] = Resources.Load<Sprite>("Images/Weapons/L_LaserGun");
        // weaponSprite[10] = Resources.Load<Sprite>("Images/Weapons/M_Cannon-M");
        // weaponSprite[11] = Resources.Load<Sprite>("Images/Weapons/M_GrenadeLauncher");
        // weaponSprite[12] = Resources.Load<Sprite>("Images/Weapons/M_MiniGun");
        // weaponSprite[20] = Resources.Load<Sprite>("Images/Weapons/H_DoubleCannon-H");
        // weaponSprite[21] = Resources.Load<Sprite>("Images/Weapons/H_FlameThrower");
        // weaponSprite[22] = Resources.Load<Sprite>("Images/Weapons/H_Vulcan");
        // weaponSprite[23] = Resources.Load<Sprite>("Images/Weapons/H_RailGun");

        spaceshipBtn.onClick.AddListener(() => OnClickSpaceship()); // >?
        weaponBtn.onClick.AddListener(() => OnClickWeapon()); // 저장 버튼을 누르면 세이브 함수 실행(?)
        gadgetBtn.onClick.AddListener(() => OnClickGadget()); // 저장 버튼을 누르면 세이브 함수 실행(?)
        playBtn.onClick.AddListener(() => OnClickPlay());
    }

    /* 처음에만 한번 실행 */
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /* 씬이 다시 로드될 때 실행 */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            spaceshipEquipment =  GameObject.Find("Window - Spaceship").GetComponent<SpaceshipEquipment>();
            weaponEquipment =  GameObject.Find("Window - Weapon").GetComponent<WeaponEquipment>();
            gadgetEquipment =  GameObject.Find("Window - Gadget").GetComponent<GadgetEquipment>();
            managerDataWeaponSprites = managerDataWeapon.GetComponent<ManagerDataWeapon>();
            managerDataGadgetSprites = managerDataGadget.GetComponent<ManagerDataGadget>();

            gadgetEquipment.LoadGadgetEquipment();
            InitGadget();
            /* 가젯 로드 or 가젯 초기화 */
            if ( PlayerPrefs.GetInt("IsGadgetSaved") == 1 )
                print("가젯 세이브변수 온 그래서 로드합니다.");
                LoadGadget();
            
            spaceshipEquipment.LoadSpaceshipEquipment();
            InitSpaceship();
            /* 우주선 로드 or 우주선 초기화 */
            if ( PlayerPrefs.GetInt("isSpaceshipSaved") == 1 )
                LoadSpaceship();

            weaponEquipment.LoadWeaponEquipment();
            InitWeapon();
            /* 무기 로드 or 무기 초기화 */
            if ( PlayerPrefs.GetInt("isWeaponSaved") == 1 )
                print("무기 세이브변수 온 그래서 로드합니다.");
                LoadWeapon();

            spaceshipBtn = GameObject.Find("Button - Spaceship").GetComponent<Button>();
            weaponBtn = GameObject.Find("Button - Weapon").GetComponent<Button>();
            gadgetBtn = GameObject.Find("Button - Gadget").GetComponent<Button>();

            spaceshipBtn.onClick.AddListener(() => OnClickSpaceship());
            weaponBtn.onClick.AddListener(() => OnClickWeapon());
            gadgetBtn.onClick.AddListener(() => OnClickGadget());
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* 우주선 초기화 실행 */
    public void InitSpaceship()
    {
        for (int i = 0; i <= 1; i++)
        {
            spaceshipEquipment.spaceshipSlot = 100;
            spaceshipEquipment.isSpaceshipUsed[i] = false;
            spaceshipEquipment.spaceshipStatusTxt[i].text = "EQUIP";
        }
        spaceshipEquipment.isEquipped = true;
        /* 0번 우주선 착용 */
        spaceshipEquipment.spaceshipSlot = 0;
        spaceshipEquipment.isSpaceshipUsed[0] = true;
        spaceshipEquipment.spaceshipStatusTxt[0].text = "UNEQUIP";
        /* 0번 우주선 데이터 넘기기 */
        ManagerDataSpaceship.instanceDataSpaceship.spaceshipId = PlayerPrefs.GetInt("Spaceship");
        /* 메인화면 우주선 이미지 로드 */
        Color color = spaceshipEquipment.mainSpaceship.color;
        color.a = 1;
        spaceshipEquipment.mainSpaceship.color = color;
        spaceshipEquipment.mainSpaceship.sprite = spaceshipSprite[0];
            
        spaceshipEquipment.spaceshipMainName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[0];
        spaceshipEquipment.spaceshipMainLavelHealth.text = "Health";
        spaceshipEquipment.spaceshipMainSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[0].ToString();
        spaceshipEquipment.spaceshipMainLavelSpeed.text = "Speed";
        spaceshipEquipment.spaceshipMainSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[0].ToString();
        spaceshipEquipment.spaceshipMainSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[0];
        spaceshipEquipment.spaceshipMainSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[0];
    }


    /* 무기 초기화 실행 */
    public void InitWeapon()
    {
        for ( int i = 0; i <= 7; i++ )
        {
            weaponEquipment.weaponSlot[i] = 100; // 무기 고유번호 초기화
            weaponEquipment.weaponSlot[i] = 100; // 무기 고유번호 초기화
            weaponEquipment.isEquipped[i] = false; // 처음에는 장착된 무기가 없음
        }
        for ( int i = 0; i <= 29; i++ )
        {
            weaponEquipment.isWeaponUsed[i] = false; // 인벤토리의 무기가 장착 상태가 아님
        }

        weaponEquipment.weaponStatusTxt[0].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[1].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[2].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[3].text = "EQUIP";

        weaponEquipment.weaponStatusTxt[10].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[11].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[12].text = "EQUIP";

        weaponEquipment.weaponStatusTxt[20].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[21].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[22].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[23].text = "EQUIP";

        /* 0번 무기 착용 */
        weaponEquipment.isEquipped[0] = true;
        weaponEquipment.isWeaponUsed[0] = true;
        weaponEquipment.weaponSlot[0] = 0;  
        weaponEquipment.weaponStatusTxt[0].text = "UNEQUIP";
        ManagerDataWeapon.instanceDataWeapon.weaponId[0] = 0;

        Color color = weaponEquipment.mainWeapons[0].color;
        color.a = 1;
        weaponEquipment.mainWeapons[0].color = color;
        weaponEquipment.mainWeapons[0].sprite = managerDataWeaponSprites.weaponSprites[0];

        Color color2 = weaponEquipment.curWeapon[0].color;
        color2.a = 1;
        weaponEquipment.curWeapon[0].color = color2;
        weaponEquipment.curWeapon[0].sprite = managerDataWeaponSprites.weaponSprites[0];
    }


    /* 가젯 초기화 실행 */
    public void InitGadget()
    {
        for ( int i = 0; i <= 2; i++ )
        {
            gadgetEquipment.gadgetSlot[i] = 100; // 무기 고유번호 초기화
            gadgetEquipment.gadgetSlot[i] = 100; // 무기 고유번호 초기화
            gadgetEquipment.isEquipped[i] = false; // 처음에는 장착된 무기가 없음
        }
        for ( int i = 0; i <= 4; i++ )
        {
            gadgetEquipment.isGadgetUsed[i] = false; // 인벤토리의 무기가 장착 상태가 아님
            gadgetEquipment.gadgetStatusTxt[i].text = "EQUIP";
        }
    }

    public void OnClickPlay()
    {
        /* 우주선 또는 무기를 하나라도 장착하지 않으면 씬을 넘기지 못하죠 */
        if (spaceshipEquipment.spaceshipSlot == 100 || weaponEquipment.weaponSlot[0] == 100)
        {
            return;
        }

        print("시작 버튼을 눌렀으니, 우주선 데이터를 넘길게요");
        ManagerDataSpaceship.instanceDataSpaceship.spaceshipId = spaceshipEquipment.spaceshipSlot; // weaponId[슬롯번호] = 현재 슬롯 안의 무기 고유 번호

        print("시작 버튼을 눌렀으니, 무기 데이터를 넘길게요");
        for (int i = 0; i <= 7; i++ )
        {
            ManagerDataWeapon.instanceDataWeapon.weaponId[i] = weaponEquipment.weaponSlot[i]; // weaponId[슬롯번호] = 현재 슬롯 안의 무기 고유 번호
        }
        print("시작 버튼을 눌렀으니, 가젯 데이터를 넘길게요");
        for (int i = 0; i <= 2; i++ )
        {
            ManagerDataGadget.instanceDataGadget.gadgetId[i] = gadgetEquipment.gadgetSlot[i]; // weaponId[슬롯번호] = 현재 슬롯 안의 무기 고유 번호
        }

        ManagerScene managerScene = GameObject.Find("ManagerScene").GetComponent<ManagerScene>();
        managerScene.SceneLoadPlay();
    }


    public void OnClickSpaceship()
    {
        spaceshipEquipment.OnClickSpaceshipInventory(0); // 강조표시를 0번으로 설정
        
        /* 우주선 인벤토리 화면에 들어갈 시, 장비창에 보여줄 우주선 이미지 */        
        if (spaceshipEquipment.spaceshipSlot == 100)
        {
            spaceshipEquipment.isEquipped = false;
                
            Color color = spaceshipEquipment.curSpaceship.color;
            color.a = 0;
            spaceshipEquipment.curSpaceship.color = color;

            spaceshipEquipment.spaceshipName.text = "";
            spaceshipEquipment.spaceshipLavelHealth.text = "";
            spaceshipEquipment.spaceshipSpecHealth.text = "";
            spaceshipEquipment.spaceshipLavelSpeed.text = "";
            spaceshipEquipment.spaceshipSpecSpeed.text = "";
            spaceshipEquipment.spaceshipSpellName.text = "";
            spaceshipEquipment.spaceshipSpellContent.text = "";
        }
        else
        {
            spaceshipEquipment.isEquipped = true;
            spaceshipEquipment.isSpaceshipUsed[spaceshipEquipment.spaceshipSlot] = true;
            
            Color color = spaceshipEquipment.curSpaceship.color;
            color.a = 1;
            spaceshipEquipment.curSpaceship.color = color;     
            spaceshipEquipment.curSpaceship.sprite = spaceshipSprite[spaceshipEquipment.spaceshipSlot];

            spaceshipEquipment.spaceshipStatusTxt[spaceshipEquipment.spaceshipSlot].text = "UNEQUIP";

            spaceshipEquipment.spaceshipName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[spaceshipEquipment.spaceshipSlot];
            spaceshipEquipment.spaceshipLavelHealth.text = "Health";
            spaceshipEquipment.spaceshipSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[spaceshipEquipment.spaceshipSlot].ToString();
            spaceshipEquipment.spaceshipLavelSpeed.text = "Speed";
            spaceshipEquipment.spaceshipSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[spaceshipEquipment.spaceshipSlot].ToString();
            spaceshipEquipment.spaceshipSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[spaceshipEquipment.spaceshipSlot];
            spaceshipEquipment.spaceshipSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[spaceshipEquipment.spaceshipSlot];
        }
    }


    public void OnClickWeapon()
    {
        weaponEquipment.OnClickInventoryWeapon(0);
        weaponEquipment.OnClickEquipmentWeapon(0);

        weaponEquipment.weaponStatusTxt[0].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[1].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[2].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[3].text = "EQUIP";

        weaponEquipment.weaponStatusTxt[10].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[11].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[12].text = "EQUIP";

        weaponEquipment.weaponStatusTxt[20].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[21].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[22].text = "EQUIP";
        weaponEquipment.weaponStatusTxt[23].text = "EQUIP";

        /* 무기 화면에 들어갈 시, 장비창에 보여줄 무기 이미지 */
        for ( int i = 0; i <= 7; i++ )
        {
            weaponEquipment.weaponSlot[i] = weaponEquipment.weaponSlot[i];
            if ( weaponEquipment.weaponSlot[i] == 100 )
            {
                weaponEquipment.isEquipped[i] = false;
                
                Color color = weaponEquipment.curWeapon[i].color;
                color.a = 0;
                weaponEquipment.curWeapon[i].color = color;
            }
            else
            {
                weaponEquipment.isEquipped[i] = true;
                weaponEquipment.isWeaponUsed[weaponEquipment.weaponSlot[i]] = true;
                Color color = weaponEquipment.curWeapon[i].color;
                color.a = 1;
                weaponEquipment.curWeapon[i].color = color;     
                weaponEquipment.curWeapon[i].sprite = managerDataWeaponSprites.weaponSprites[weaponEquipment.weaponSlot[i]];

                weaponEquipment.weaponStatusTxt[weaponEquipment.weaponSlot[i]].text = "UNEQUIP";
            }              
        }
    }   

    public void OnClickGadget()
    {
        gadgetEquipment.OnClickGadgetInventory(0);
        gadgetEquipment.OnClickGadgetEquipment(0);

        /* 가젯 화면에 들어갈 시, 인벤토리에 보여줄 가젯 */
        for (int i = 0; i <= 4; i++)
        {
            if (gadgetEquipment.isGadgetUsed[i])
            {
                gadgetEquipment.gadgetStatusTxt[i].text = "UNEQUIP";
            }
            else
            {
                gadgetEquipment.gadgetStatusTxt[i].text = "EQUIP";
            }
        }

        /* 무기 화면에 들어갈 시, 장비창에 보여줄 무기 이미지 */
        for (int i = 0; i <= 2; i++)
        {
            gadgetEquipment.gadgetSlot[i] = gadgetEquipment.gadgetSlot[i];
            if (gadgetEquipment.gadgetSlot[i] == 100 )
            {
                gadgetEquipment.isEquipped[i] = false;
                
                Color color = gadgetEquipment.curGadgets[i].color;
                color.a = 0;
                gadgetEquipment.curGadgets[i].color = color;

                gadgetEquipment.gadgetNames[i].text = "";

                
            }
            else
            {
                gadgetEquipment.isEquipped[i] = true;
                gadgetEquipment.isGadgetUsed[gadgetEquipment.gadgetSlot[i]] = true;
                Color color = gadgetEquipment.curGadgets[i].color;
                color.a = 1;
                gadgetEquipment.curGadgets[i].color = color;     
                gadgetEquipment.curGadgets[i].sprite = managerDataGadgetSprites.gadgetSprites[gadgetEquipment.gadgetSlot[i]];

                gadgetEquipment.gadgetNames[i].text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetNames[gadgetEquipment.gadgetSlot[i]];

                gadgetEquipment.gadgetStatusTxt[gadgetEquipment.gadgetSlot[i]].text = "UNEQUIP";
                
            }              
        }
    }   

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadSpaceship()
    {
        if ( PlayerPrefs.HasKey("Spaceship") )
        {           
            spaceshipEquipment.isEquipped = true;

            /* 0번 우주선 해제 */
            spaceshipEquipment.isSpaceshipUsed[0] = false;
            spaceshipEquipment.spaceshipStatusTxt[0].text = "EQUIP";

            spaceshipEquipment.isSpaceshipUsed[PlayerPrefs.GetInt("Spaceship")] = true;
            spaceshipEquipment.spaceshipSlot = PlayerPrefs.GetInt("Spaceship");  
            spaceshipEquipment.spaceshipStatusTxt[PlayerPrefs.GetInt("Spaceship")].text = "UNEQUIP";
            ManagerDataSpaceship.instanceDataSpaceship.spaceshipId = PlayerPrefs.GetInt("Spaceship");

            /* 메인화면 우주선 이미지 로드 */
            Color color = spaceshipEquipment.mainSpaceship.color;
            color.a = 1;
            spaceshipEquipment.mainSpaceship.color = color;
            spaceshipEquipment.mainSpaceship.sprite = spaceshipSprite[PlayerPrefs.GetInt("Spaceship")];
            
            spaceshipEquipment.spaceshipMainName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[PlayerPrefs.GetInt("Spaceship")];
            spaceshipEquipment.spaceshipMainLavelHealth.text = "Health";
            spaceshipEquipment.spaceshipMainSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[PlayerPrefs.GetInt("Spaceship")].ToString();
            spaceshipEquipment.spaceshipMainLavelSpeed.text = "Speed";
            spaceshipEquipment.spaceshipMainSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[PlayerPrefs.GetInt("Spaceship")].ToString();
            spaceshipEquipment.spaceshipMainSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[PlayerPrefs.GetInt("Spaceship")];
            spaceshipEquipment.spaceshipMainSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[PlayerPrefs.GetInt("Spaceship")];
        }
    }


    public void LoadWeapon()
    {
        print("무기 로드");
        
        if ( PlayerPrefs.HasKey("WeaponFirst") )
        {
            print(PlayerPrefs.GetInt("WeaponFirst"));
            weaponEquipment.isEquipped[0] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponFirst")] = true;
            weaponEquipment.weaponSlot[0] = PlayerPrefs.GetInt("WeaponFirst");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponFirst")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[0] = PlayerPrefs.GetInt("WeaponFirst");

            Color color = weaponEquipment.mainWeapons[0].color;
            color.a = 1;
            weaponEquipment.mainWeapons[0].color = color;
            weaponEquipment.mainWeapons[0].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponFirst")];

            Color color2 = weaponEquipment.curWeapon[0].color;
            color2.a = 1;
            weaponEquipment.curWeapon[0].color = color2;
            weaponEquipment.curWeapon[0].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponFirst")];
        }

        if ( PlayerPrefs.HasKey("WeaponSecond") )
        {
            weaponEquipment.isEquipped[1] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponSecond")] = true;
            weaponEquipment.weaponSlot[1] = PlayerPrefs.GetInt("WeaponSecond");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponSecond")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[1] = PlayerPrefs.GetInt("WeaponSecond");

            Color color = weaponEquipment.mainWeapons[1].color;
            color.a = 1;
            weaponEquipment.mainWeapons[1].color = color;
            weaponEquipment.mainWeapons[1].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponSecond")];

            Color color2 = weaponEquipment.curWeapon[1].color;
            color2.a = 1;
            weaponEquipment.curWeapon[1].color = color2;
            weaponEquipment.curWeapon[1].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponSecond")];
        }

        if ( PlayerPrefs.HasKey("WeaponThird") )
        {
            weaponEquipment.isEquipped[2] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponThird")] = true;
            weaponEquipment.weaponSlot[2] = PlayerPrefs.GetInt("WeaponThird");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponThird")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[2] = PlayerPrefs.GetInt("WeaponThird");

            Color color = weaponEquipment.mainWeapons[2].color;
            color.a = 1;
            weaponEquipment.mainWeapons[2].color = color;
            weaponEquipment.mainWeapons[2].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponThird")];

            Color color2 = weaponEquipment.curWeapon[2].color;
            color2.a = 1;
            weaponEquipment.curWeapon[2].color = color2;
            weaponEquipment.curWeapon[2].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponThird")];
        }
        
        if ( PlayerPrefs.HasKey("WeaponFourth") )
        {
            weaponEquipment.isEquipped[3] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponFourth")] = true;
            weaponEquipment.weaponSlot[3] = PlayerPrefs.GetInt("WeaponFourth");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponFourth")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[3] = PlayerPrefs.GetInt("WeaponFourth");

            Color color = weaponEquipment.mainWeapons[3].color;
            color.a = 1;
            weaponEquipment.mainWeapons[3].color = color;
            weaponEquipment.mainWeapons[3].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponFourth")];
            
            Color color2 = weaponEquipment.curWeapon[3].color;
            color2.a = 1;
            weaponEquipment.curWeapon[3].color = color2;
            weaponEquipment.curWeapon[3].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponFourth")];
        }

        if ( PlayerPrefs.HasKey("WeaponFifth") )
        {
            weaponEquipment.isEquipped[4] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponFifth")] = true;
            weaponEquipment.weaponSlot[4] = PlayerPrefs.GetInt("WeaponFifth");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponFifth")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[4] = PlayerPrefs.GetInt("WeaponFifth");

            Color color = weaponEquipment.mainWeapons[4].color;
            color.a = 1;
            weaponEquipment.mainWeapons[4].color = color;
            weaponEquipment.mainWeapons[4].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponFifth")];

            Color color2 = weaponEquipment.curWeapon[4].color;
            color2.a = 1;
            weaponEquipment.curWeapon[4].color = color2;
            weaponEquipment.curWeapon[4].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponFifth")];
        }

        if ( PlayerPrefs.HasKey("WeaponSixth") )
        {
            weaponEquipment.isEquipped[5] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponSixth")] = true;
            weaponEquipment.weaponSlot[5] = PlayerPrefs.GetInt("WeaponSixth");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponSixth")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[5] = PlayerPrefs.GetInt("WeaponSixth");

            Color color = weaponEquipment.mainWeapons[5].color;
            color.a = 1;
            weaponEquipment.mainWeapons[5].color = color;
            weaponEquipment.mainWeapons[5].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponSixth")];
            
            Color color2 = weaponEquipment.curWeapon[5].color;
            color2.a = 1;
            weaponEquipment.curWeapon[5].color = color2;
            weaponEquipment.curWeapon[5].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponSixth")];
        }

        if ( PlayerPrefs.HasKey("WeaponSeventh") )
        {
            weaponEquipment.isEquipped[6] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponSeventh")] = true;
            weaponEquipment.weaponSlot[6] = PlayerPrefs.GetInt("WeaponSeventh");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponSeventh")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[6] = PlayerPrefs.GetInt("WeaponSeventh");

            Color color = weaponEquipment.mainWeapons[6].color;
            color.a = 1;
            weaponEquipment.mainWeapons[6].color = color;
            weaponEquipment.mainWeapons[6].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponSeventh")];
                        
            Color color2 = weaponEquipment.curWeapon[6].color;
            color2.a = 1;
            weaponEquipment.curWeapon[6].color = color2;
            weaponEquipment.curWeapon[6].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponSeventh")];
        }

        if ( PlayerPrefs.HasKey("WeaponEighth") )
        {
            weaponEquipment.isEquipped[7] = true;
            weaponEquipment.isWeaponUsed[PlayerPrefs.GetInt("WeaponEighth")] = true;
            weaponEquipment.weaponSlot[7] = PlayerPrefs.GetInt("WeaponEighth");  
            weaponEquipment.weaponStatusTxt[PlayerPrefs.GetInt("WeaponEighth")].text = "UNEQUIP";
            ManagerDataWeapon.instanceDataWeapon.weaponId[7] = PlayerPrefs.GetInt("WeaponEighth");

            Color color = weaponEquipment.mainWeapons[7].color;
            color.a = 1;
            weaponEquipment.mainWeapons[7].color = color;
            weaponEquipment.mainWeapons[7].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponEighth")];
                                    
            Color color2 = weaponEquipment.curWeapon[7].color;
            color2.a = 1;
            weaponEquipment.curWeapon[7].color = color2;
            weaponEquipment.curWeapon[7].sprite = managerDataWeaponSprites.weaponSprites[PlayerPrefs.GetInt("WeaponEighth")];
        }
    }


    public void LoadGadget()
    {
        print("가젯 로드");
        
        if ( PlayerPrefs.HasKey("GadgetFirst") )
        {
            gadgetEquipment.isEquipped[0] = true;
            gadgetEquipment.isGadgetUsed[PlayerPrefs.GetInt("GadgetFirst")] = true;
            gadgetEquipment.gadgetSlot[0] = PlayerPrefs.GetInt("GadgetFirst");  
            gadgetEquipment.gadgetStatusTxt[PlayerPrefs.GetInt("GadgetFirst")].text = "UNEQUIP";
            ManagerDataGadget.instanceDataGadget.gadgetId[0] = PlayerPrefs.GetInt("GadgetFirst");

            Color color = gadgetEquipment.mainGadgets[0].color;
            color.a = 1;
            gadgetEquipment.mainGadgets[0].color = color;
            gadgetEquipment.mainGadgets[0].sprite = managerDataGadgetSprites.gadgetSprites[PlayerPrefs.GetInt("GadgetFirst")];

            Color color2 = gadgetEquipment.curGadgets[0].color;
            color2.a = 1;
            gadgetEquipment.curGadgets[0].color = color2;
            gadgetEquipment.curGadgets[0].sprite = managerDataGadgetSprites.gadgetSprites[PlayerPrefs.GetInt("GadgetFirst")];

            gadgetEquipment.gadgetNames[0].text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetNames[PlayerPrefs.GetInt("GadgetFirst")];
        }

        if ( PlayerPrefs.HasKey("GadgetSecond") )
        {
            gadgetEquipment.isEquipped[1] = true;
            gadgetEquipment.isGadgetUsed[PlayerPrefs.GetInt("GadgetSecond")] = true;
            gadgetEquipment.gadgetSlot[1] = PlayerPrefs.GetInt("GadgetSecond");  
            gadgetEquipment.gadgetStatusTxt[PlayerPrefs.GetInt("GadgetSecond")].text = "UNEQUIP";
            ManagerDataGadget.instanceDataGadget.gadgetId[1] = PlayerPrefs.GetInt("GadgetSecond");

            Color color = gadgetEquipment.mainGadgets[1].color;
            color.a = 1;
            gadgetEquipment.mainGadgets[1].color = color;
            gadgetEquipment.mainGadgets[1].sprite = managerDataGadgetSprites.gadgetSprites[PlayerPrefs.GetInt("GadgetSecond")];

            Color color2 = gadgetEquipment.curGadgets[1].color;
            color2.a = 1;
            gadgetEquipment.curGadgets[1].color = color2;
            gadgetEquipment.curGadgets[1].sprite = managerDataGadgetSprites.gadgetSprites[PlayerPrefs.GetInt("GadgetSecond")];

            gadgetEquipment.gadgetNames[1].text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetNames[PlayerPrefs.GetInt("GadgetSecond")];
        }

        if ( PlayerPrefs.HasKey("GadgetThird") )
        {
            gadgetEquipment.isEquipped[2] = true;
            gadgetEquipment.isGadgetUsed[PlayerPrefs.GetInt("GadgetThird")] = true;
            gadgetEquipment.gadgetSlot[2] = PlayerPrefs.GetInt("GadgetThird");  
            gadgetEquipment.gadgetStatusTxt[PlayerPrefs.GetInt("GadgetThird")].text = "UNEQUIP";
            ManagerDataGadget.instanceDataGadget.gadgetId[2] = PlayerPrefs.GetInt("GadgetThird");

            Color color = gadgetEquipment.mainGadgets[2].color;
            color.a = 1;
            gadgetEquipment.mainGadgets[2].color = color;
            gadgetEquipment.mainGadgets[2].sprite = managerDataGadgetSprites.gadgetSprites[PlayerPrefs.GetInt("GadgetThird")];

            Color color2 = gadgetEquipment.curGadgets[2].color;
            color2.a = 1;
            gadgetEquipment.curGadgets[2].color = color2;
            gadgetEquipment.curGadgets[2].sprite = managerDataGadgetSprites.gadgetSprites[PlayerPrefs.GetInt("GadgetThird")];

            gadgetEquipment.gadgetNames[2].text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetNames[PlayerPrefs.GetInt("GadgetThird")];
        }
    }
}

