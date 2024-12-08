using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GadgetEquipment: MonoBehaviour
{
    [SerializeField]
    private Button[] invenSlotButton = new Button [5]; // 인벤토리의 30개 무기
	[SerializeField]
    private Button[] equipSlotBtn = new Button [3]; // 장비창의 8개 무기
    [SerializeField]
    private Button[] equipButton = new Button [5]; // 인벤토리 무기 밑의 장착 버튼 5개 (총 무기는 30개)

    public GameObject selectedImageInInventory; // 인벤토리에서 셀렉된 무기 강조 표시
    public GameObject selectedImageInEquipment; // 장비창에서 셀렉된 무기 강조표시

    public int selectedInventorySlot = 0; // 인벤토리에서 셀렉된 무기의 번호 (0~4)
    public int selectedEquipmentSlot = 0; // 장비창에서 셀렉된 무기의 번호 (0~7)
    
    public bool[] isGadgetUsed = new bool [5]; // 인벤토리에서 각각의 무기는 장착되었는가? (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    public bool[] isEquipped = new bool [3]; // 장비창의 각각의 슬롯에 무기는 장착되었는가? (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public TextMeshProUGUI[] gadgetStatusTxt = new TextMeshProUGUI [5]; // 인벤토리에서 장착 또는 장착 해제 버튼의 텍스트 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public Image[] curGadgets = new Image [3]; // 장비창의 현재 장착된 무기 이미지 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    public Image[] mainGadgets = new Image [3]; // 메인화면의 현재 장착된 무기 이미지 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)
    
    public TextMeshProUGUI[] gadgetNames = new TextMeshProUGUI [3]; // 장비창에서 나오는 이름

    /* 무기 스프라이트 */
    public GameObject managerDataGadget;
    public ManagerDataGadget managerDataGadgetSprites;
   
    public int[] gadgetSlot = new int [3]; // 8가지 슬롯 (매니저게임 스크립트에서 로드를 위해 퍼블릭으로 한다.)

    /* 무기 설명 */
    [SerializeField]
    Image gadgetDescImg;
    [SerializeField]
    TextMeshProUGUI gadgetDescName;
    [SerializeField]
    TextMeshProUGUI gadgetDescContent;

    /* 무기 데이터 */
    [SerializeField]
    private GameObject managerGame;

    [SerializeField]
    private Button closeBtn; // 장비창 닫기 버튼, 닫으면 일단 메인무기대로 유지

    public GameObject warnSequence;

    void Awake()
    {
        /* 인벤토리 슬롯 5개 버튼 변수 할당 */
        invenSlotButton[0].onClick.AddListener(() => OnClickGadgetInventory(0));
        invenSlotButton[1].onClick.AddListener(() => OnClickGadgetInventory(1));
        invenSlotButton[2].onClick.AddListener(() => OnClickGadgetInventory(2));
        invenSlotButton[3].onClick.AddListener(() => OnClickGadgetInventory(3));
        invenSlotButton[4].onClick.AddListener(() => OnClickGadgetInventory(4));

        /* 인벤토리의 무기 장착 버튼 변수 할당 */
        // 소형, 중형, 대형무기까지 총 15개의 장착 버튼이 있음 (0~4, 10~14, 20~24)
        // 15개 장착 버튼마다 무기 번호를 할당하여 무기 번호 정보를 넘긴다.
        equipButton[0].onClick.AddListener(() => OnClickGadgetInventory(0));
        equipButton[1].onClick.AddListener(() => OnClickGadgetInventory(1));
        equipButton[2].onClick.AddListener(() => OnClickGadgetInventory(2));
        equipButton[3].onClick.AddListener(() => OnClickGadgetInventory(3));
        equipButton[4].onClick.AddListener(() => OnClickGadgetInventory(4));

        equipButton[0].onClick.AddListener(() => EquipSystem(0));
        equipButton[1].onClick.AddListener(() => EquipSystem(1));
        equipButton[2].onClick.AddListener(() => EquipSystem(2));
        equipButton[3].onClick.AddListener(() => EquipSystem(3));
        equipButton[4].onClick.AddListener(() => EquipSystem(4));

        /* 장비 슬롯 8개 버튼 변수 할당 */    
        equipSlotBtn[0].onClick.AddListener(() => OnClickGadgetEquipment(0));
        equipSlotBtn[1].onClick.AddListener(() => OnClickGadgetEquipment(1));
        equipSlotBtn[2].onClick.AddListener(() => OnClickGadgetEquipment(2));

        closeBtn.onClick.AddListener(() => Close()); // 저장 버튼을 누르면 세이브 함수 실행(?)
    }
    

    void Start()
    {   
        managerDataGadgetSprites = managerDataGadget.GetComponent<ManagerDataGadget>();
    }
    
    public void LoadGadgetEquipment()
    {
        managerGame = GameObject.Find("ManagerGame");
        managerDataGadget = GameObject.Find("ManagerDataGadget");
    }

    /* 인벤토리의 무기를 클릭했을 경우, 강조 표시 이미지 이동 */
    public void OnClickGadgetInventory(int inventorySlot)
    {
        selectedInventorySlot = inventorySlot;

        Color color = gadgetDescImg.color;
        color.a = 1;
        gadgetDescImg.color = color;

        /* 장비창 투명도를 1로 하여 이미지가 보이게 한다. */
        gadgetDescImg.sprite = managerGame.GetComponent<GameManager>().managerDataGadgetSprites.gadgetSprites[selectedInventorySlot];
        //gadgetDescName.text = "MachineGun\nTYPE: Light";
        gadgetDescName.text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetNames[selectedInventorySlot];
        gadgetDescContent.text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetContents[selectedInventorySlot];
        
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
        }
    }

    /* 장비창의 무기를 클릭했을 경우, 강조 표시 이미지 이동 */
    public void OnClickGadgetEquipment(int equipmentSlot)
    {
        selectedEquipmentSlot = equipmentSlot;
        switch (equipmentSlot)
        {
            case (0):
                selectedImageInEquipment.transform.localPosition = new Vector3 (-200,50,0);
                break;
            case (1):
                selectedImageInEquipment.transform.localPosition = new Vector3 (0,50,0);
                break;
            case (2):
                selectedImageInEquipment.transform.localPosition = new Vector3 (200,50,0);
                break;
        }
    }

    /* 장착 */
    public void EquipSystem(int inventorySlot)
    {
        // 장착 버튼을 누를 때, 현재 활성화된 장비 슬롯에 무기가 없는 경우 -> 장착을 시키고 문자는 장착 해제로 바꾼다.
        if ( gadgetStatusTxt[inventorySlot].text == "EQUIP" && !isEquipped[selectedEquipmentSlot] ) 
        {
            isGadgetUsed[inventorySlot] = true; // 장착을 누른 무기는 사용 중으로 바뀜
            EquipGadget(inventorySlot);
            gadgetStatusTxt[inventorySlot].text = "UNEQUIP";
            PrintCurGadget();
            return;
        }

        // 다른 무기 장착 버튼을 누를 때, 현재 장비 슬롯에 무기가 있는 경우 -> 장착 해제를 시키고, 장착을 시키고 문자는 장착 해제로 바꾼다. 그리고 다른 곳에 장착된 무기 문자는 장착으로 바꾼다.
        if ( gadgetStatusTxt[inventorySlot].text == "EQUIP" && isEquipped[selectedEquipmentSlot] ) 
        {
            /* 현재 셀렉된 장비창의 슬롯의 무기 고유번호에 따라 인벤토리의 슬롯을 '사용가능' 상태로 변경 */
            switch ( gadgetSlot[selectedEquipmentSlot] )
            {
                case 0: // 현재 장착된 무기가 머신건라면
                    isGadgetUsed[0] = false; // 인벤토리 무기 0번을 '사용가능' 상태로 변경
                    break;
                case 1: // 현재 장착된 무기가 소형캐논이라면
                    isGadgetUsed[1] = false; // 인벤토리 무기 1번을 '사용가능' 상태로 변경
                    break;
                case 2: // 현재 장착된 무기가 입자포라면
                    isGadgetUsed[2] = false; // 인벤토리 무기 2번을 '사용가능' 상태로 변경
                    break;
                case 3: // 현재 장착된 무기가 레이저건이라면
                    isGadgetUsed[3] = false; // 인벤토리 무기 3번을 '사용가능' 상태로 변경
                    break;
                case 4: // 현재 장착된 무기가 레이저건이라면
                    isGadgetUsed[4] = false; // 인벤토리 무기 3번을 '사용가능' 상태로 변경
                    break;
            }  

            UnEquipGadget();
            

            // 인벤토리의 장착하려는 무기 장착 텍스트를 '장착해제'으로 바꾼다.
            gadgetStatusTxt[inventorySlot].text = "UNEQUIP";
            
            isGadgetUsed[inventorySlot] = true; // 인벤창 무기는 사용 중이으로 바뀜

            EquipGadget(inventorySlot);

            // 인벤토리 사용 중이지 않는 무기들 텍스트 에게는 장착으로
            for (int i = 0; i <= 4; i++)
            {
                if ( isGadgetUsed[i] )
                {    
                    continue;
                }
                else if ( gadgetStatusTxt[i] != null )
                {
                    gadgetStatusTxt[i].text = "EQUIP";
                }
            }
            PrintCurGadget();
            return;
        }

        // 장착 해제 버튼을 누를 때, 현재 장비 슬롯에 무기가 있는 경우 -> 장착 해제, 텍스트는 장착으로
        if ( gadgetStatusTxt[inventorySlot].text == "UNEQUIP" && isEquipped[selectedEquipmentSlot] ) 
        {
            // 다른 무기 장착 해제 버튼을 누를 때 -> 그냥 리턴 시켜야함
            if ( inventorySlot != gadgetSlot[selectedEquipmentSlot] )
            {
                PrintCurGadget();
                return;
            }
            else
            {
                // 같은 무기 장착 해제 버튼을 누를 때
                isGadgetUsed[inventorySlot] = false;
                UnEquipGadget();
                gadgetStatusTxt[inventorySlot].text = "EQUIP";
                PrintCurGadget();
                return;
            }
        }

        // 장착 해제 버튼을 누를 때, 현재 장비 슬롯에 무기가 없는 경우 -> 아무것도 하지 않는다.
        if ( gadgetStatusTxt[inventorySlot].text == "UNEQUIP" && isEquipped[selectedEquipmentSlot] ) 
        {
            PrintCurGadget();
            return;
        }
    }

    public void EquipGadget(int inventorySlot)
    {
        // 장착 슬롯 번호에 장착됨을 설정
        isEquipped[selectedEquipmentSlot] = true;
        gadgetSlot[selectedEquipmentSlot] = inventorySlot; // 무기슬롯에 무기번호 부여 (현재 무기번호는 0부터 29까지 존재)

        /* 장비창 투명도를 1로 하여 이미지가 보이게 한다. */
        Color color = curGadgets[selectedEquipmentSlot].color;
        color.a = 1;
        curGadgets[selectedEquipmentSlot].color = color;

        print(selectedEquipmentSlot + "번째 셀렉된 장비슬롯에 " + inventorySlot + " 번 무기를 장착합니다.");
        curGadgets[selectedEquipmentSlot].sprite = managerGame.GetComponent<GameManager>().managerDataGadgetSprites.gadgetSprites[inventorySlot]; // '장비창'의 '1번 무기'의 이미지를 '인벤토리창'의 장착 무기로 바꾼다.
        gadgetNames[selectedEquipmentSlot].text = managerDataGadget.GetComponent<ManagerDataGadget>().gadgetNames[inventorySlot];

         /* 메인화면에 보여줄 무기 이미지 */
        Color mainColor = mainGadgets[selectedEquipmentSlot].color;
        mainColor.a = 1;
        mainGadgets[selectedEquipmentSlot].color = mainColor;     
        mainGadgets[selectedEquipmentSlot].sprite = managerGame.GetComponent<GameManager>().managerDataGadgetSprites.gadgetSprites[gadgetSlot[selectedEquipmentSlot]];
        
        // 다른 씬에 전달하기 위해 데이터를 저장해둔다.
        for (int i = 0; i <= 2; i++)
        {
            print("가젯 데이터를 넘길게요");
            ManagerDataGadget.instanceDataGadget.gadgetId[i] = gadgetSlot[i]; // gadgetId[슬롯번호] = 현재 슬롯 안의 무기 고유 번호
    
            /* 세이브시스템 */
            switch (i)
            {
                case 0:
                    PlayerPrefs.SetInt("GadgetFirst", gadgetSlot[i]);
                    print("1번 가젯 시스템 세이브 " + gadgetSlot[i]);
                    break;
                case 1:
                    PlayerPrefs.SetInt("GadgetSecond", gadgetSlot[i]);
                    print("2번 가젯 시스템 세이브 " + gadgetSlot[i]);
                    break;
                case 2:
                    PlayerPrefs.SetInt("GadgetThird", gadgetSlot[i]);
                    print("3번 가젯 시스템 세이브 " + gadgetSlot[i]);
                    break;
            }
                
            if (gadgetSlot[i] == 100)    
            {   
                /* 무기가 비어있다면 그 세이브 키는 삭제합니다. */
                switch (i)
                {
                    case 0:
                        PlayerPrefs.DeleteKey("GadgetFirst");
                        break;
                    case 1:
                        PlayerPrefs.DeleteKey("GadgetSecond");
                        break;
                    case 2:
                        PlayerPrefs.DeleteKey("GadgetThird");
                        break;
                }
                        
                // /* 무기가 장착이 되어있지 않다면 메인화면에 보여줄 투명 이미지 */
                // Color color2 = mainGadgets[i].color;
                // color2.a = 0;
                // mainGadgets[i].color = color2;
            }
            // else
            // {
            //     /* 메인화면에 보여줄 무기 이미지 */
            //     Color mainColor = mainGadgets[i].color;
            //     mainColor.a = 1;
            //     mainGadgets[i].color = mainColor;     
            //     mainGadgets[i].sprite = managerGame.GetComponent<GameManager>().managerDataGadgetSprites.gadgetSprites[gadgetSlot[i]];
            // }

            PlayerPrefs.SetInt("IsGadgetSaved", 1);
            PlayerPrefs.Save();    
            PrintCurGadget();
            print("가젯 장착 완료, 저장");   
        }
    }

    public void UnEquipGadget()
    {
        // 장착 슬롯 번호에 장착 해제됨을 설정
        isEquipped[selectedEquipmentSlot] = false;  
        gadgetSlot[selectedEquipmentSlot] = 100; // 장착해제 하므로 그 자리에 무기는 없는 번호 100을 부여함
        /* 투명도를 0으로 하여 이미지가 안보이게 한다. */
        Color color = curGadgets[selectedEquipmentSlot].color;
        color.a = 0;
        curGadgets[selectedEquipmentSlot].color = color;

        gadgetNames[selectedEquipmentSlot].text = "";

                /* 무기가 장착이 되어있지 않다면 메인화면에 보여줄 투명 이미지 */
                Color color2 = mainGadgets[selectedEquipmentSlot].color;
                color2.a = 0;
                mainGadgets[selectedEquipmentSlot].color = color2;
    }

    public void PrintCurGadget()
    {
        print("1번 무기는 " + gadgetSlot[0]);
        print("2번 무기는 " + gadgetSlot[1]);
        print("3번 무기는 " + gadgetSlot[2]);
    }

    void Close()
    {
        for (int i = 0; i <= 1 ; i++) 
        {
            if (gadgetSlot[i] == 100 && gadgetSlot[i + 1] < 100)
            {
                print("가젯을 1번부터 차례대로 장착해주세요.");
                warnSequence.SetActive(true);
                return;
            }
        }

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false); // 아래 방식을 한 줄로 쓰기
        //GameObject firstChild = transform. GetChild(0). gameObject;
        //firstChild. SetActive(false);     
    } 
}
