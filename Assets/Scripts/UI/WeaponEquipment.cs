using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponEquipment: MonoBehaviour
{
    [SerializeField]
    private Button lightWeaponsBtn;
    [SerializeField]
    private Button mediumWeaponsBtn;
    [SerializeField]
    private Button heavyWeaponsBtn;
    /* 소형, 중형, 대형 무기 탭을 누르면 각각의 무기 인벤토리를 보여줘야함 */
    [SerializeField]
    private GameObject lightWeapons;
    [SerializeField]
    private GameObject mediumWeapons;
    [SerializeField]
    private GameObject heavyWeapons;
    [SerializeField]
    private int weaponInvenStatus; // 소형은 0, 중형은 1, 대형은 2
    [SerializeField]
    private int weaponEquipStatus; // 슬롯이 1~3이면 0, 슬롯이 4~5이면 1, 슬롯이 6~7이면 2, 유저슬롯은 3

    [SerializeField]
    private Button[] invenSlotButton = new Button [30]; // 인벤토리의 30개 무기
	[SerializeField]
    private Button[] equipSlotBtn = new Button [8]; // 장비창의 8개 무기
    [SerializeField]
    private Button[] equipButton = new Button [30]; // 인벤토리 무기 밑의 장착 버튼 5개 (총 무기는 30개)

    public GameObject selectedImageInInventory; // 인벤토리에서 셀렉된 무기 강조 표시
    public GameObject selectedImageInEquipment; // 장비창에서 셀렉된 무기 강조표시

    public int selectedInventorySlot = 0; // 인벤토리에서 셀렉된 무기의 번호 (0~4)
    public int selectedEquipmentSlot = 0; // 장비창에서 셀렉된 무기의 번호 (0~7)
    
    public bool[] isWeaponUsed = new bool [30]; // 인벤토리에서 각각의 무기는 장착되었는가? (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    public bool[] isEquipped = new bool [8]; // 장비창의 각각의 슬롯에 무기는 장착되었는가? (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public TextMeshProUGUI[] weaponStatusTxt = new TextMeshProUGUI [30]; // 인벤토리에서 장착 또는 장착 해제 버튼의 텍스트 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public Image[] curWeapon = new Image [8]; // 장비창의 현재 장착된 무기 이미지 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)

    public Image[] mainWeapons = new Image [8]; // 메인화면의 현재 장착된 무기 이미지 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    

    /* 무기 스프라이트 */
    public GameObject managerDataWeapon;
    public ManagerDataWeapon managerDataWeaponSprites;
   
    public int[] weaponSlot = new int [8]; // 8가지 슬롯 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)

    /* 무기 설명 */
    [SerializeField]
    Image weaponDescImg;
    [SerializeField]
    TextMeshProUGUI weaponDescName;
    [SerializeField]
    TextMeshProUGUI weaponDescContentLavel;
    [SerializeField]
    TextMeshProUGUI weaponDescContentFigure;

    /* 무기 데이터 */
    [SerializeField]
    private GameObject[] PrefabWeapon = new GameObject[30];
    [SerializeField]
    private GameObject managerGame;

    [SerializeField]
    private Button closeBtn; // 장비창 닫기 버튼, 닫으면 일단 메인무기대로 유지

    public GameObject warnWeaponCanvas;
    public GameObject warnSequence;

    // [SerializeField]
    // private Button lightWeaponBtn; // 소형무기 인벤토리 보는 버튼
    // [SerializeField]
    // private Button mediumWeaponBtn; // 중형무기 인벤토리 보는 버튼
    // [SerializeField]
    // private Button heavyWeaponBtn; // 대형무기 인벤토리 보는 버튼


    void Awake()
    {
        lightWeaponsBtn.onClick.AddListener(() => OnClickWeaponsTab(0));
        mediumWeaponsBtn.onClick.AddListener(() => OnClickWeaponsTab(1));
        heavyWeaponsBtn.onClick.AddListener(() => OnClickWeaponsTab(2));

        /* 인벤토리 슬롯 5개 버튼 변수 할당 */
        invenSlotButton[0].onClick.AddListener(() => OnClickInventoryWeapon(0));
        invenSlotButton[1].onClick.AddListener(() => OnClickInventoryWeapon(1));
        invenSlotButton[2].onClick.AddListener(() => OnClickInventoryWeapon(2));
        invenSlotButton[3].onClick.AddListener(() => OnClickInventoryWeapon(3));
        invenSlotButton[4].onClick.AddListener(() => OnClickInventoryWeapon(4));
        invenSlotButton[10].onClick.AddListener(() => OnClickInventoryWeapon(10));
        invenSlotButton[11].onClick.AddListener(() => OnClickInventoryWeapon(11));
        invenSlotButton[12].onClick.AddListener(() => OnClickInventoryWeapon(12));
        invenSlotButton[13].onClick.AddListener(() => OnClickInventoryWeapon(13));
        invenSlotButton[14].onClick.AddListener(() => OnClickInventoryWeapon(14));
        invenSlotButton[20].onClick.AddListener(() => OnClickInventoryWeapon(20));
        invenSlotButton[21].onClick.AddListener(() => OnClickInventoryWeapon(21));
        invenSlotButton[22].onClick.AddListener(() => OnClickInventoryWeapon(22));
        invenSlotButton[23].onClick.AddListener(() => OnClickInventoryWeapon(23));
        invenSlotButton[24].onClick.AddListener(() => OnClickInventoryWeapon(24));

        /* 인벤토리의 무기 장착 버튼 변수 할당 */
        // 소형, 중형, 대형무기까지 총 15개의 장착 버튼이 있음 (0~4, 10~14, 20~24)
        // 15개 장착 버튼마다 무기 번호를 할당하여 무기 번호 정보를 넘긴다.
        equipButton[0].onClick.AddListener(() => OnClickInventoryWeapon(0));
        equipButton[1].onClick.AddListener(() => OnClickInventoryWeapon(1));
        equipButton[2].onClick.AddListener(() => OnClickInventoryWeapon(2));
        equipButton[3].onClick.AddListener(() => OnClickInventoryWeapon(3));
        equipButton[4].onClick.AddListener(() => OnClickInventoryWeapon(4));
        equipButton[10].onClick.AddListener(() => OnClickInventoryWeapon(10));
        equipButton[11].onClick.AddListener(() => OnClickInventoryWeapon(11));
        equipButton[12].onClick.AddListener(() => OnClickInventoryWeapon(12));
        equipButton[13].onClick.AddListener(() => OnClickInventoryWeapon(13));
        equipButton[14].onClick.AddListener(() => OnClickInventoryWeapon(14));
        equipButton[20].onClick.AddListener(() => OnClickInventoryWeapon(20));
        equipButton[21].onClick.AddListener(() => OnClickInventoryWeapon(21));
        equipButton[22].onClick.AddListener(() => OnClickInventoryWeapon(22));
        equipButton[23].onClick.AddListener(() => OnClickInventoryWeapon(23));
        equipButton[24].onClick.AddListener(() => OnClickInventoryWeapon(24));

        equipButton[0].onClick.AddListener(() => EquipSystem(0));
        equipButton[1].onClick.AddListener(() => EquipSystem(1));
        equipButton[2].onClick.AddListener(() => EquipSystem(2));
        equipButton[3].onClick.AddListener(() => EquipSystem(3));
        equipButton[4].onClick.AddListener(() => EquipSystem(4));
        equipButton[10].onClick.AddListener(() => EquipSystem(10));
        equipButton[11].onClick.AddListener(() => EquipSystem(11));
        equipButton[12].onClick.AddListener(() => EquipSystem(12));
        equipButton[13].onClick.AddListener(() => EquipSystem(13));
        equipButton[14].onClick.AddListener(() => EquipSystem(14));
        equipButton[20].onClick.AddListener(() => EquipSystem(20));
        equipButton[21].onClick.AddListener(() => EquipSystem(21));
        equipButton[22].onClick.AddListener(() => EquipSystem(22));
        equipButton[23].onClick.AddListener(() => EquipSystem(23));
        equipButton[24].onClick.AddListener(() => EquipSystem(24));

        /* 장비 슬롯 8개 버튼 변수 할당 */    
        equipSlotBtn[0].onClick.AddListener(() => OnClickEquipmentWeapon(0));
        equipSlotBtn[1].onClick.AddListener(() => OnClickEquipmentWeapon(1));
        equipSlotBtn[2].onClick.AddListener(() => OnClickEquipmentWeapon(2));
        equipSlotBtn[3].onClick.AddListener(() => OnClickEquipmentWeapon(3));
        equipSlotBtn[4].onClick.AddListener(() => OnClickEquipmentWeapon(4));
        equipSlotBtn[5].onClick.AddListener(() => OnClickEquipmentWeapon(5));
        equipSlotBtn[6].onClick.AddListener(() => OnClickEquipmentWeapon(6));
        equipSlotBtn[7].onClick.AddListener(() => OnClickEquipmentWeapon(7));

        closeBtn.onClick.AddListener(() => Close());
    }
    

    void Start()
    {   
        managerDataWeaponSprites = managerDataWeapon.GetComponent<ManagerDataWeapon>();

        lightWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
        mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void LoadWeaponEquipment()
    {
        managerGame = GameObject.Find("ManagerGame");
        managerDataWeapon = GameObject.Find("ManagerDataWeapon");
    }

    public void OnClickWeaponsTab(int weaponsTab)
    {
        switch (weaponsTab)
        {
            case (0):
                lightWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                lightWeapons.SetActive(true);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 0;
                selectedInventorySlot = 0;
                break;
            case (1):
                lightWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                lightWeapons.SetActive(false);
                mediumWeapons.SetActive(true);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 1;
                selectedInventorySlot = 10;
                break;
            case (2):
                lightWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                lightWeapons.SetActive(false);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(true);
                weaponInvenStatus = 2;
                selectedInventorySlot = 20;
                break;
            case (3):
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                break;
        }
    }
    
    /* 인벤토리의 무기를 클릭했을 경우, 강조 표시 이미지 이동 */
    public void OnClickInventoryWeapon(int inventorySlot)
    {
        selectedInventorySlot = inventorySlot;

        Color color = weaponDescImg.color;
        color.a = 1;
        weaponDescImg.color = color;

        /* 장비창 투명도를 1로 하여 이미지가 보이게 한다. */
        weaponDescImg.sprite = managerGame.GetComponent<GameManager>().managerDataWeaponSprites.weaponSprites[selectedInventorySlot];
        
        weaponDescName.text = managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponName[selectedInventorySlot] + "\n"
                                        + "TYPE: " + managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponType[selectedInventorySlot];
        weaponDescContentFigure.text = managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponDamage[selectedInventorySlot] + "\n"
                                        + "unlimited\n"
                                        + managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponAttackRate[selectedInventorySlot] + "\n"
                                        + managerDataWeapon.GetComponent<ManagerDataWeapon>().weaponBulletSpeed[selectedInventorySlot] + "\n"
                                        + "---";
        weaponDescName.text = "MachineGun\nTYPE: Light";

        switch (selectedInventorySlot)
        {
            case (0):
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                break;
            case (1):
                selectedImageInInventory.transform.localPosition = new Vector3 (-220,0,0);
                break;
            case (2):
                selectedImageInInventory.transform.localPosition = new Vector3 (0,0,0);
                break;
            case (3):
                selectedImageInInventory.transform.localPosition = new Vector3 (220,0,0);
                break;
            case (4):
                selectedImageInInventory.transform.localPosition = new Vector3 (440,0,0);
                break;
            case (10):
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                break;
            case (11):
                selectedImageInInventory.transform.localPosition = new Vector3 (-220,0,0);
                break;
            case (12):
                selectedImageInInventory.transform.localPosition = new Vector3 (0,0,0);
                break;
            case (13):
                selectedImageInInventory.transform.localPosition = new Vector3 (220,0,0);
                break;
            case (14):
                selectedImageInInventory.transform.localPosition = new Vector3 (440,0,0);
                break;
            case (20):
                selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
                break;
            case (21):
                selectedImageInInventory.transform.localPosition = new Vector3 (-220,0,0);
                break;
            case (22):
                selectedImageInInventory.transform.localPosition = new Vector3 (0,0,0);
                break;
            case (23):
                selectedImageInInventory.transform.localPosition = new Vector3 (220,0,0);
                break;
            case (24):
                selectedImageInInventory.transform.localPosition = new Vector3 (440,0,0);
                break;
        }
    }

    /* 장비창의 무기를 클릭했을 경우, 강조 표시 이미지 이동 */
    public void OnClickEquipmentWeapon(int equipmentSlot)
    {
        selectedEquipmentSlot = equipmentSlot;
        switch (equipmentSlot)
        {
            case (0):
                selectedImageInEquipment.transform.localPosition = new Vector3 (-250,65,0);
                weaponEquipStatus = 3;
                break;
            case (1):
                selectedImageInEquipment.transform.localPosition = new Vector3 (-85,65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                lightWeapons.SetActive(true);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 0;
                weaponEquipStatus = 0;
                break;
            case (2):
                selectedImageInEquipment.transform.localPosition = new Vector3 (85,65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                lightWeapons.SetActive(true);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 0;
                weaponEquipStatus = 0;
                break;
            case (3):
                selectedImageInEquipment.transform.localPosition = new Vector3 (250,65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                lightWeapons.SetActive(true);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 0;
                weaponEquipStatus = 0;
                break;
            case (4):
                selectedImageInEquipment.transform.localPosition = new Vector3 (-250,-65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                lightWeapons.SetActive(false);
                mediumWeapons.SetActive(true);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 1;
                weaponEquipStatus = 1;
                break;
            case (5):
                selectedImageInEquipment.transform.localPosition = new Vector3 (-85,-65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                lightWeapons.SetActive(false);
                mediumWeapons.SetActive(true);
                heavyWeapons.SetActive(false);
                weaponInvenStatus = 1;
                weaponEquipStatus = 1;
                break;
            case (6):
                selectedImageInEquipment.transform.localPosition = new Vector3 (85,-65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                lightWeapons.SetActive(false);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(true);
                weaponInvenStatus = 2;
                weaponEquipStatus = 2;
                break;
            case (7):
                selectedImageInEquipment.transform.localPosition = new Vector3 (250,-65,0);
                lightWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                heavyWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
                lightWeapons.SetActive(false);
                mediumWeapons.SetActive(false);
                heavyWeapons.SetActive(true);
                weaponInvenStatus = 2;
                weaponEquipStatus = 2;
                break;
        }
    }

    /* 장착 */
    public void EquipSystem(int inventorySlot)
    {
        /* 장비창 슬롯에서는 각각의 소형, 중형, 대형무기 종류에 따라 다른 무기 종류는 착용할 수 없음 */
        if ( weaponStatusTxt[inventorySlot].text == "EQUIP" )
        {
            if ( weaponEquipStatus == 0 && ( weaponInvenStatus == 1 || weaponInvenStatus == 2 ) ) 
            {
                print("소형무기 슬롯에는 소형무기만 장착할 수 있습니다.");
                return;
            }
            if ( weaponEquipStatus == 1 && ( weaponInvenStatus == 0 || weaponInvenStatus == 2 ) ) 
            {
                print("중형무기 슬롯에는 중형무기만 장착할 수 있습니다.");
                return;
            }
            if ( weaponEquipStatus == 2 && ( weaponInvenStatus == 0 || weaponInvenStatus == 1 ) ) 
            {
                print("대형무기 슬롯에는 대형무기만 장착할 수 있습니다.");
                return;
            }
        }

        // 1. 장착 버튼을 누를 때, 현재 활성화된 장비 슬롯에 무기가 없는 경우 -> 장착을 시키고 문자는 장착 해제로 바꾼다.
        if ( weaponStatusTxt[inventorySlot].text == "EQUIP" && !isEquipped[selectedEquipmentSlot] ) 
        {
            isWeaponUsed[inventorySlot] = true; // 장착을 누른 무기는 사용 중으로 바뀜
            EquipWeapon(inventorySlot);
            weaponStatusTxt[inventorySlot].text = "UNEQUIP";
            PrintCurWeapon();
            return;
        }

        // 다른 무기 장착 버튼을 누를 때, 현재 장비 슬롯에 무기가 있는 경우 -> 장착 해제를 시키고, 장착을 시키고 문자는 장착 해제로 바꾼다. 그리고 다른 곳에 장착된 무기 문자는 장착으로 바꾼다.
        if ( weaponStatusTxt[inventorySlot].text == "EQUIP" && isEquipped[selectedEquipmentSlot] ) 
        {
            

            /* 현재 셀렉된 장비창의 슬롯의 무기 고유번호에 따라 인벤토리의 슬롯을 '사용가능' 상태로 변경 */
            switch ( weaponSlot[selectedEquipmentSlot] )
            {
                case 0: // 현재 장착된 무기가 머신건라면
                    isWeaponUsed[0] = false; // 인벤토리 무기 0번을 '사용가능' 상태로 변경
                    break;
                case 1: // 현재 장착된 무기가 소형캐논이라면
                    isWeaponUsed[1] = false; // 인벤토리 무기 1번을 '사용가능' 상태로 변경
                    break;
                case 2: // 현재 장착된 무기가 입자포라면
                    isWeaponUsed[2] = false; // 인벤토리 무기 2번을 '사용가능' 상태로 변경
                    break;
                case 3: // 현재 장착된 무기가 레이저건이라면
                    isWeaponUsed[3] = false; // 인벤토리 무기 3번을 '사용가능' 상태로 변경
                    break;
                case 10: // 현재 장착된 무기가 캐논M이라면
                    isWeaponUsed[10] = false; // 인벤토리 무기 10번을 '사용가능' 상태로 변경
                    break;
                case 11: // 현재 장착된 무기가 유탄발사기라면
                    isWeaponUsed[11] = false; // 인벤토리 무기 11번을 '사용가능' 상태로 변경
                    break;
                case 12: // 현재 장착된 무기가 미니건이라면
                    isWeaponUsed[12] = false; // 인벤토리 무기 12번을 '사용가능' 상태로 변경
                    break;
                case 20: // 현재 장착된 무기가 더블캐논이라면
                    isWeaponUsed[20] = false; // 인벤토리 무기 20번을 '사용가능' 상태로 변경
                    break;
                case 21: // 현재 장착된 무기가 화염방사기라면
                    isWeaponUsed[21] = false; // 인벤토리 무기 21번을 '사용가능' 상태로 변경
                    break;
                case 22: // 현재 장착된 무기가 발칸이라면
                    isWeaponUsed[22] = false; // 인벤토리 무기 22번을 '사용가능' 상태로 변경
                    break;
                case 23: // 현재 장착된 무기가 레일건이라면
                    isWeaponUsed[23] = false; // 인벤토리 무기 23번을 '사용가능' 상태로 변경
                    break;
            }  
            UnEquipWeapon();

            // 인벤토리의 장착하려는 무기 장착 텍스트를 '장착해제'으로 바꾼다.
            weaponStatusTxt[inventorySlot].text = "UNEQUIP";
            
            isWeaponUsed[inventorySlot] = true; // 인벤창 무기는 사용 중이으로 바뀜

            EquipWeapon(inventorySlot);

            // 인벤토리 사용 중이지 않는 무기들 텍스트 에게는 장착으로
            for ( int index = 0; index <= 29; index++ )
            {
                if ( isWeaponUsed[index] )
                {    
                    continue;
                }
                else if ( weaponStatusTxt[index] != null )
                {
                    weaponStatusTxt[index].text = "EQUIP";
                }
            }
            PrintCurWeapon();
            return;
        }

        // 장착 해제 버튼을 누를 때, 현재 장비 슬롯에 무기가 있는 경우 -> 장착 해제, 텍스트는 장착으로
        if ( weaponStatusTxt[inventorySlot].text == "UNEQUIP" && isEquipped[selectedEquipmentSlot] ) 
        {
            // 다른 무기 장착 해제 버튼을 누를 때 -> 그냥 리턴 시켜야함
            if ( inventorySlot != weaponSlot[selectedEquipmentSlot] )
            {
                PrintCurWeapon();
                return;
            }
            else
            {
                // 같은 무기 장착 해제 버튼을 누를 때
                isWeaponUsed[inventorySlot] = false;
                UnEquipWeapon();
                weaponStatusTxt[inventorySlot].text = "EQUIP";
                PrintCurWeapon();
                return;
            }
        }

        // 장착 해제 버튼을 누를 때, 현재 장비 슬롯에 무기가 없는 경우 -> 아무것도 하지 않는다.
        if ( weaponStatusTxt[inventorySlot].text == "UNEQUIP" && isEquipped[selectedEquipmentSlot] ) 
        {
            PrintCurWeapon();
            return;
        }
    }

    public void EquipWeapon(int inventorySlot)
    {
        // 장착 슬롯 번호에 장착됨을 설정
        isEquipped[selectedEquipmentSlot] = true;
        weaponSlot[selectedEquipmentSlot] = inventorySlot; // 무기슬롯에 무기번호 부여 (현재 무기번호는 0부터 29까지 존재)

        /* 장비창 투명도를 1로 하여 이미지가 보이게 한다. */
        Color color = curWeapon[selectedEquipmentSlot].color;
        color.a = 1;
        curWeapon[selectedEquipmentSlot].color = color;

        print(selectedEquipmentSlot + "번째 셀렉된 장비슬롯에 " + inventorySlot + " 번 무기를 장착합니다.");
        curWeapon[selectedEquipmentSlot].sprite = managerGame.GetComponent<GameManager>().managerDataWeaponSprites.weaponSprites[inventorySlot]; // '장비창'의 '1번 무기'의 이미지를 '인벤토리창'의 장착 무기로 바꾼다.

            /* 메인화면에 보여줄 무기 이미지 */
                Color mainColor = mainWeapons[selectedEquipmentSlot].color;
                mainColor.a = 1;
                mainWeapons[selectedEquipmentSlot].color = mainColor;     
                mainWeapons[selectedEquipmentSlot].sprite = managerGame.GetComponent<GameManager>().managerDataWeaponSprites.weaponSprites[weaponSlot[selectedEquipmentSlot]];
        Save(); // 메인화면 이미지도 조정함.
    }

    public void UnEquipWeapon()
    {
        // 장착 슬롯 번호에 장착 해제됨을 설정
        isEquipped[selectedEquipmentSlot] = false;  
        weaponSlot[selectedEquipmentSlot] = 100;
        
        /* 투명도를 0으로 하여 이미지가 안보이게 한다. */
        Color color = curWeapon[selectedEquipmentSlot].color;
        color.a = 0;
        curWeapon[selectedEquipmentSlot].color = color;
        
        /* 메인화면 무기 장착해제 */
        Color mainColor = mainWeapons[selectedEquipmentSlot].color;
        mainColor.a = 0;
        mainWeapons[selectedEquipmentSlot].color = mainColor;

        Save(); // 메인화면 이미지도 조정함.
    }

    public void PrintCurWeapon()
    {
        print("1번 무기는 " + weaponSlot[0]);
        print("2번 무기는 " + weaponSlot[1]);
        print("3번 무기는 " + weaponSlot[2]);
        print("4번 무기는 " + weaponSlot[3]);
        print("5번 무기는 " + weaponSlot[4]);
        print("6번 무기는 " + weaponSlot[5]);
        print("7번 무기는 " + weaponSlot[6]);
        print("8번 무기는 " + weaponSlot[7]);
    }

    void Close()
    {
        if (!isEquipped[0])
        {
            print("1번 무기를 장착해주세요");
            warnWeaponCanvas.SetActive(true);
            return;
        }

        for (int i = 0; i <= 6 ; i++) 
        {
            if (weaponSlot[i] == 100 && weaponSlot[i + 1] < 100)
            {
                print("무기를 1번부터 차례대로 장착해주세요.");
                warnSequence.SetActive(true);
                return;
            }
        }

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false); // 아래 방식을 한 줄로 쓰기
        //GameObject firstChild = transform. GetChild(0). gameObject;
        //firstChild. SetActive(false);
    }
    
    void Save()
    {
       // 다른 씬에 전달하기 위해 데이터를 저장해둔다.
        for (int i = 0; i <= 7; i++)
        {
            print("무기 데이터를 넘길게요");
            ManagerDataWeapon.instanceDataWeapon.weaponId[i] = weaponSlot[i]; // weaponId[슬롯번호] = 현재 슬롯 안의 무기 고유 번호
                    
            /* 세이브시스템 */
            switch (i)
            {
                case 0:
                    PlayerPrefs.SetInt("WeaponFirst", weaponSlot[i]);
                    print("1번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 1:
                    PlayerPrefs.SetInt("WeaponSecond", weaponSlot[i]);
                    print("2번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 2:
                    PlayerPrefs.SetInt("WeaponThird", weaponSlot[i]);
                    print("3번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 3:
                    PlayerPrefs.SetInt("WeaponFourth", weaponSlot[i]);
                    print("4번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 4:
                    PlayerPrefs.SetInt("WeaponFifth", weaponSlot[i]);
                    print("5번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 5:
                    PlayerPrefs.SetInt("WeaponSixth", weaponSlot[i]);
                    print("6번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 6:
                    PlayerPrefs.SetInt("WeaponSeventh", weaponSlot[i]);
                    print("7번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
                case 7:
                    PlayerPrefs.SetInt("WeaponEighth", weaponSlot[i]);
                    print("8번 무기 시스템 세이브 " + weaponSlot[i]);
                    break;
            }
                
            if (weaponSlot[i] == 100)    
            {   
                /* 무기가 비어있다면 그 세이브 키는 삭제합니다. */
                switch (i)
                {
                    case 0:
                        PlayerPrefs.DeleteKey("WeaponFirst");
                        break;
                    case 1:
                        PlayerPrefs.DeleteKey("WeaponSecond");
                        break;
                    case 2:
                        PlayerPrefs.DeleteKey("WeaponThird");
                        break;
                    case 3:
                        PlayerPrefs.DeleteKey("WeaponFourth");
                        break;
                    case 4:
                        PlayerPrefs.DeleteKey("WeaponFifth");
                        break;
                    case 5:
                        PlayerPrefs.DeleteKey("WeaponSixth");
                        break;
                    case 6:
                        PlayerPrefs.DeleteKey("WeaponSeventh");
                        break;
                    case 7:
                        PlayerPrefs.DeleteKey("WeaponEighth");
                        break;
                }
                        
                // /* 무기가 장착이 되어있지 않다면 메인화면에 보여줄 투명 이미지 */
                // Color mainColor = mainWeapons[i].color;
                // mainColor.a = 0;
                // mainWeapons[i].color = mainColor;
            }
            // else
            // {
            //     /* 메인화면에 보여줄 무기 이미지 */
            //     Color mainColor = mainWeapons[i].color;
            //     mainColor.a = 1;
            //     mainWeapons[i].color = mainColor;     
            //     mainWeapons[i].sprite = managerGame.GetComponent<GameManager>().managerDataWeaponSprites.weaponSprites[weaponSlot[i]];
            // }

            PlayerPrefs.SetInt("isWeaponSaved", 1);
            PlayerPrefs.Save();
            PrintCurWeapon();
            print("무기 장착 완료, 저장");
        } 
    }

    // void Reset()
    // {
    //     lightWeaponsBtn.GetComponent<Image>().color = new Color(1, 1, 1);
    //     mediumWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    //     heavyWeaponsBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);

    //     lightWeapons.SetActive(true);
    //     mediumWeapons.SetActive(false);
    //     heavyWeapons.SetActive(false);    

    //     weaponInvenStatus = 0; // 인벤토리의 슬롯을 소형으로 설정
    //     weaponEquipStatus = 3; // 장비창의 슬롯을 유저무기로 설정

    //     selectedInventorySlot = 0;
    //     selectedEquipmentSlot = 0;
    //     selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
    //     selectedImageInEquipment.transform.localPosition = new Vector3 (-250,65,0);

    //     for ( int i = 0; i <= 7; i++ )
    //     {
    //         weaponSlot[i] = 100;
    //         isEquipped[i] = false; // 장착 슬롯들은 모두 장착 해제
    //         /* 투명도를 0으로 하여 이미지가 안보이게 한다. */
    //         Color color = curWeapon[i].color;
    //         color.a = 0;
    //         curWeapon[i].color = color;

    //         // Color color2 = mainWeapons[i].color;
    //         // color2.a = 0;
    //         // mainWeapons[i].color = color2;
    //     }
        
    //     for ( int j = 0; j <= 29; j++ )
    //     {
    //         isWeaponUsed[j] = false;
    //         if ( weaponStatusTxt[j] != null )
    //             weaponStatusTxt[j].text = "EQUIP"; 
    //     }
    //     print("장비 슬롯을 초기화하였습니다.");
    //     PrintCurWeapon();
    // }
}
