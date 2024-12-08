using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceshipEquipment: MonoBehaviour
{
    [SerializeField]
    private Button[] invenSlotButton = new Button [5]; // 인벤토리의 5개 우주선
    [SerializeField]
    private Button[] equipButton = new Button [5]; // 인벤토리 우주선 밑의 장착 버튼 5개 (총 우주선 5개)

    public GameObject selectedImageInInventory; // 인벤토리에서 셀렉된 무기 강조 표시

    public int selectedInventorySlot = 0; // 인벤토리에서 셀렉된 무기의 번호 (0~4)
    
    public bool[] isSpaceshipUsed = new bool [5]; // 인벤토리에서 각각의 무기는 장착되었는가? (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    public bool isEquipped; // 장비창의 각각의 슬롯에 무기는 장착되었는가? (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public TextMeshProUGUI[] spaceshipStatusTxt = new TextMeshProUGUI [5]; // 인벤토리에서 장착 또는 장착 해제 버튼의 텍스트 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public Image curSpaceship; // 장비창의 현재 장착된 무기 이미지 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)

    public Image mainSpaceship; // 메인화면의 현재 장착된 무기 이미지 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    

    /* 무기 스프라이트 */
    //public Sprite[] weaponSprite = new Sprite [30]; // 무기 이미지에 사용할 스프라이트 (매니저게임 스크립트에서 무기 인벤토리 버튼 클릭시 무기를 부르기위해 퍼블릭으로 한다.)

   
    public int spaceshipSlot; // 8가지 슬롯 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)

    /* 무기 설명 */
    [SerializeField]
    Image spaceshipDescImg;
    public TextMeshProUGUI spaceshipMainName;
    public TextMeshProUGUI spaceshipMainLavelHealth;
    public TextMeshProUGUI spaceshipMainSpecHealth;
    public TextMeshProUGUI spaceshipMainLavelSpeed;
    public TextMeshProUGUI spaceshipMainSpecSpeed;
    public TextMeshProUGUI spaceshipMainSpellName;
    public TextMeshProUGUI spaceshipMainSpellContent;
    [SerializeField] TextMeshProUGUI spaceshipDescName;
    [SerializeField] TextMeshProUGUI spaceshipDescLavelHealth;
    [SerializeField] TextMeshProUGUI spaceshipDescSpecHealth;
    [SerializeField] TextMeshProUGUI spaceshipDescLavelSpeed;
    [SerializeField] TextMeshProUGUI spaceshipDescSpecSpeed;
    [SerializeField] TextMeshProUGUI spaceshipDescSpellName;
    [SerializeField] TextMeshProUGUI spaceshipDescSpellContent;
    public TextMeshProUGUI spaceshipName;
    public TextMeshProUGUI spaceshipLavelHealth;
    public TextMeshProUGUI spaceshipSpecHealth;
    public TextMeshProUGUI spaceshipLavelSpeed;
    public TextMeshProUGUI spaceshipSpecSpeed;
    public TextMeshProUGUI spaceshipSpellName;
    public TextMeshProUGUI spaceshipSpellContent;

    /* 무기 데이터 */
    [SerializeField]
    private GameObject[] PrefabSpaceship = new GameObject[5];
    [SerializeField]
    private GameObject managerDataSpaceship;
    [SerializeField]
    private GameObject managerGame;

    [SerializeField]
    private Button closeBtn; // 장비창 닫기 버튼, 닫으면 일단 메인무기대로 유지

    //public bool isSave;

    public GameObject spaceshipCanvas; // 우주선 인벤토리창
    public GameObject warnSpaceshipCanvas; // 우주선 세이브 경고창

    void Awake()
    {
        /* 인벤토리 슬롯 5개 버튼 변수 할당 */
        invenSlotButton[0].onClick.AddListener(() => OnClickSpaceshipInventory(0));
        invenSlotButton[1].onClick.AddListener(() => OnClickSpaceshipInventory(1));
        invenSlotButton[2].onClick.AddListener(() => OnClickSpaceshipInventory(2));
        invenSlotButton[3].onClick.AddListener(() => OnClickSpaceshipInventory(3));
        invenSlotButton[4].onClick.AddListener(() => OnClickSpaceshipInventory(4));

        /* 인벤토리의 무기 장착 버튼 변수 할당 */
        // 소형, 중형, 대형무기까지 총 15개의 장착 버튼이 있음 (0~4, 10~14, 20~24)
        // 15개 장착 버튼마다 무기 번호를 할당하여 무기 번호 정보를 넘긴다.
        equipButton[0].onClick.AddListener(() => OnClickSpaceshipInventory(0));
        equipButton[1].onClick.AddListener(() => OnClickSpaceshipInventory(1));
        equipButton[2].onClick.AddListener(() => OnClickSpaceshipInventory(2));
        equipButton[3].onClick.AddListener(() => OnClickSpaceshipInventory(3));
        equipButton[4].onClick.AddListener(() => OnClickSpaceshipInventory(4));

        equipButton[0].onClick.AddListener(() => EquipSystem(0));
        equipButton[1].onClick.AddListener(() => EquipSystem(1));
        equipButton[2].onClick.AddListener(() => EquipSystem(2));
        equipButton[3].onClick.AddListener(() => EquipSystem(3));
        equipButton[4].onClick.AddListener(() => EquipSystem(4));

        closeBtn.onClick.AddListener(() => Close());
    }

    public void LoadSpaceshipEquipment()
    {
        managerGame = GameObject.Find("ManagerGame");
        managerDataSpaceship = GameObject.Find("ManagerDataSpaceship");
    }
    
    /* 인벤토리의 무기를 클릭했을 경우, 강조 표시 이미지 이동 */
    public void OnClickSpaceshipInventory(int inventorySlot)
    {
        selectedInventorySlot = inventorySlot;

        Color color = spaceshipDescImg.color;
        color.a = 1;
        spaceshipDescImg.color = color;

        /* 장비창 투명도를 1로 하여 이미지가 보이게 한다. */
        spaceshipDescImg.sprite = managerGame.GetComponent<GameManager>().spaceshipSprite[selectedInventorySlot];
        //weaponDescName.text = "MachineGun\nTYPE: Light";
        spaceshipDescName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[selectedInventorySlot];
        spaceshipDescLavelHealth.text = "Health";
        spaceshipDescSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[selectedInventorySlot].ToString();
        spaceshipDescLavelSpeed.text = "Speed";
        spaceshipDescSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[selectedInventorySlot].ToString();
        spaceshipDescSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[selectedInventorySlot];
        spaceshipDescSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[selectedInventorySlot];

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
            // case (10):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
            //     break;
            // case (11):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (-220,0,0);
            //     break;
            // case (12):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (0,0,0);
            //     break;
            // case (13):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (220,0,0);
            //     break;
            // case (14):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (440,0,0);
            //     break;
            // case (20):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
            //     break;
            // case (21):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (-220,0,0);
            //     break;
            // case (22):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (0,0,0);
            //     break;
            // case (23):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (220,0,0);
            //     break;
            // case (24):
            //     selectedImageInInventory.transform.localPosition = new Vector3 (440,0,0);
            //     break;
        }
    }


    /* 장착 */
    public void EquipSystem(int inventorySlot)
    {
        // 우주선 장착 버튼을 누를 때, 현재 우주선 슬롯에 우주선이 없는 경우 -> 장착을 시키고 문자는 장착 해제로 바꾼다.
        if (spaceshipStatusTxt[inventorySlot].text == "EQUIP" && !isEquipped)
        {
            isSpaceshipUsed[inventorySlot] = true; // 장착을 누른 무기는 사용 중으로 바뀜
            EquipSpaceship(inventorySlot);
            spaceshipStatusTxt[inventorySlot].text = "UNEQUIP";
            return;
        }

        // 다른 우주선 장착 버튼을 누를 때, 현재 우주선 슬롯에 우주선이 있는 경우
        // 현재 장착된 우주선을 장착 해제시키고, 새로 선택한 우주선을 장착시키고 문자는 장착 해제로 바꾼다. 그리고 다른 곳에 장착된 우주선 문자는 장착으로 바꾼다.
        if (spaceshipStatusTxt[inventorySlot].text == "EQUIP" && isEquipped) 
        {
            

            /* 현재 장착된 우주선의 고유 번호에 따라 인벤토리의 슬롯을 '사용가능' 상태로 변경 */
            switch (spaceshipSlot)
            {
                case 0: // 현재 장착된 무기가 검볼이라면
                    isSpaceshipUsed[0] = false; // 인벤토리 우주선 0번 검볼을 '사용가능' 상태로 변경
                    break;
                case 1: // 현재 장착된 무기가 스프라이트라면
                    isSpaceshipUsed[1] = false; // 인벤토리 우주선 1번 스프라이트를 '사용가능' 상태로 변경
                    break;
            }  

            UnEquipSpaceship(); // 장착해제 함수를 나중에 해야함 왜냐 스페이스쉽슬롯을 위에서 써야하니까

            // 인벤토리의 장착하려는 우주선 장착 텍스트를 '장착해제'으로 바꾼다.
            spaceshipStatusTxt[inventorySlot].text = "UNEQUIP";
            
            isSpaceshipUsed[inventorySlot] = true; // 인벤창 무기는 사용 중이으로 바뀜

            EquipSpaceship(inventorySlot);

            // 인벤토리 사용 중이지 않는 무기들 텍스트 에게는 장착으로
            for (int i = 0; i <= 1; i++)
            {
                if (isSpaceshipUsed[i])
                {    
                    continue;
                }
                else if (spaceshipStatusTxt[i] != null)
                {
                    spaceshipStatusTxt[i].text = "EQUIP";
                }
            }
            return;
        }

        // 현재 장착된 우주선을 장착 해제 버튼을 누를 때, 현재 장비 슬롯에 우주선이 있는 경우 -> 장착 해제, 텍스트는 장착으로
        if (spaceshipStatusTxt[inventorySlot].text == "UNEQUIP" && isEquipped) 
        {
            // 다른 무기 장착 해제 버튼을 누를 때 -> 그냥 리턴 시켜야함
            if (inventorySlot != spaceshipSlot)
            {
                return;
            }
            else
            {
                // 같은 무기 장착 해제 버튼을 누를 때
                isSpaceshipUsed[inventorySlot] = false;
                UnEquipSpaceship();
                spaceshipStatusTxt[inventorySlot].text = "EQUIP";
                return;
            }
        }

        // 장착 해제 버튼을 누를 때, 현재 우주선 슬롯에 우주선이 없는 경우 -> 아무것도 하지 않는다.
        if (spaceshipStatusTxt[inventorySlot].text == "UNEQUIP" && isEquipped) 
        {
            return;
        }
    }

    public void EquipSpaceship(int inventorySlot)
    {
        // 장착 슬롯 번호에 장착됨을 설정
        isEquipped = true;
        spaceshipSlot = inventorySlot;

        /* 장비창 투명도를 1로 하여 이미지가 보이게 한다. */
        Color curColor = curSpaceship.color;
        curColor.a = 1;
        curSpaceship.color = curColor;

        curSpaceship.sprite = managerGame.GetComponent<GameManager>().spaceshipSprite[spaceshipSlot]; // '장비창'의 '1번 무기'의 이미지를 '인벤토리창'의 장착 무기로 바꾼다.

        spaceshipName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[spaceshipSlot];
        spaceshipLavelHealth.text = "Health";
        spaceshipSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[spaceshipSlot].ToString();
        spaceshipLavelSpeed.text = "Speed";
        spaceshipSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[spaceshipSlot].ToString();
        spaceshipSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[spaceshipSlot];
        spaceshipSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[spaceshipSlot];
    
        /* 메인화면에 보여줄 무기 이미지 */
        Color mainColor = mainSpaceship.color;
        mainColor.a = 1;
        mainSpaceship.color = mainColor;     
        mainSpaceship.sprite = managerGame.GetComponent<GameManager>().spaceshipSprite[spaceshipSlot];

        spaceshipMainName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[spaceshipSlot];
        spaceshipMainLavelHealth.text = "Health";
        spaceshipMainSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[spaceshipSlot].ToString();
        spaceshipMainLavelSpeed.text = "Speed";
        spaceshipMainSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[spaceshipSlot].ToString();
        spaceshipMainSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[spaceshipSlot];
        spaceshipMainSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[spaceshipSlot];
    
        // 다른 씬에 전달하기 위해 데이터를 저장해둔다.
        print("우주선 장착 완료, 저장");
        print("우주선 데이터를 넘깁니다.");
        ManagerDataSpaceship.instanceDataSpaceship.spaceshipId = spaceshipSlot;

        PlayerPrefs.SetInt("Spaceship", spaceshipSlot);
        PlayerPrefs.SetInt("isSpaceshipSaved", 1);
        PlayerPrefs.Save();
        PrintCurSpaceship();
    }

    public void UnEquipSpaceship()
    {
        // 장착 슬롯 번호에 장착 해제됨을 설정
        isEquipped = false;  
        spaceshipSlot = 100; // 장착해제 하므로 우주선은 없는 번호 100을 부여함

        /* 투명도를 0으로 하여 이미지가 안보이게 한다. */
        Color color = curSpaceship.color;
        color.a = 0;
        curSpaceship.color = color;

        spaceshipName.text = "";
        spaceshipLavelHealth.text = "";
        spaceshipSpecHealth.text = "";
        spaceshipLavelSpeed.text = "";
        spaceshipSpecSpeed.text = "";
        spaceshipSpellName.text = "";
        spaceshipSpellContent.text = "";
    }

    public void PrintCurSpaceship()
    {
        print("현재 장착된 우주선번호는 " + spaceshipSlot + " 입니다.");
    }

    void Close()
    {
        if (!isEquipped)
        {
            print("우주선을 장착해주세요");
            warnSpaceshipCanvas.SetActive(true); // 우주선 장착하라는 경고창 열기
            return;
        }
        else
        {
            spaceshipCanvas.SetActive(false); // 우주선 인벤토리 닫기
        }         
    }

    // void Reset()
    // {
    //     isSave = false;
    //     isEquipped = false;

    //     spaceshipSlotTemp = 100;

    //     selectedInventorySlot = 0;
    //     selectedImageInInventory.transform.localPosition = new Vector3 (-440,0,0);
        
    //     spaceshipDescName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipName[selectedInventorySlot];
    //     spaceshipDescLavelHealth.text = "Health";
    //     spaceshipDescSpecHealth.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipHealth[selectedInventorySlot].ToString();
    //     spaceshipDescLavelSpeed.text = "Speed";
    //     spaceshipDescSpecSpeed.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpeed[selectedInventorySlot].ToString();
    //     spaceshipDescSpellName.text = managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellName[selectedInventorySlot];
    //     spaceshipDescSpellContent.text = " " + managerDataSpaceship.GetComponent<ManagerDataSpaceship>().spaceshipSpellContent[selectedInventorySlot];
        
    //     /* 투명도를 0으로 하여 이미지가 안보이게 한다. */
    //     Color color = curSpaceship.color;
    //     color.a = 0;
    //     curSpaceship.color = color;
        
    //     spaceshipName.text = "";
    //     spaceshipLavelHealth.text = "";
    //     spaceshipSpecHealth.text = "";
    //     spaceshipLavelSpeed.text = "";
    //     spaceshipSpecSpeed.text = "";
    //     spaceshipSpellName.text = "";
    //     spaceshipSpellContent.text = "";
        
    //     for ( int i = 0; i <= 4; i++ )
    //     {
    //         isSpaceshipUsed[i] = false;
            
    //         if ( spaceshipStatusTxt[i] != null )
    //             spaceshipStatusTxt[i].text = "EQUIP"; 
    //     }
    //     print("우주선 슬롯을 초기화하였습니다.");
    //     PrintCurSpaceship();
    // }                     
}
