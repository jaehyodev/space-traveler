using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 마우스 이벤트를 받기 위함
using System;

// Ctrl + ','을 누르면 자동완성으로 멤버를 구현
// public class VirtualJoystick : MonoBehaviour, 
//                                IDragHandler, IPointerDownHandler, IPointerUpHandler
// {
//     private Image bgImg;
//     private Image joystickImg;
//     private Vector3 inputVector;
    

//     public GameObject weapon;
//     private float speed = 3f; // 캐릭터 스피드
//     private float rotateSpeed = 5f; // 회전 속도
//     private Coroutine runningCoroutine; // 부드러운 회전 코루틴

//     private void Start()
//     {
//         runningCoroutine = StartCoroutine(RotateAngle(180, -1));
//         // 시작하면 charactor를 180도 오른쪽으로 회전
//         bgImg = GetComponent<Image>();
//         joystickImg = transform.GetChild(0).GetComponent<Image>();
//     }
//     public virtual void OnDrag(PointerEventData ped)
//     {
//         Vector2 pos;
//         if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,
//                 ped.position,
//                 ped.pressEventCamera,
//                 out pos))
//         {
//             pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
//             pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

//             inputVector = new Vector3(pos.x * 2 , 0, pos.y * 2);
//             inputVector = (inputVector.magnitude > 1.0f) ? 
//                           inputVector.normalized : inputVector;
//             TurnAngle(inputVector); 
            
//             // Move JoyStick Img
//             joystickImg.rectTransform.anchoredPosition = 
//                 new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)   
//                 //조이스틱 안에 원 최대치
//                 , inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));
//         }
        
//     }
//     public virtual void OnPointerDown(PointerEventData ped)
//     {
//         OnDrag(ped);
//     }

//     public virtual void OnPointerUp(PointerEventData ped)
//     {
//         inputVector = Vector3.zero;
//         joystickImg.rectTransform.anchoredPosition = Vector3.zero;
//     }
//     public float Horizontal()
//     {
//         if(inputVector.x != 0)
//         {
//             return inputVector.x;
//         }
//         else
//         {
//             return Input.GetAxis("Horizontal");
//         }
//     }
//     public float Vertical()
//     {
//         if (inputVector.z != 0)
//         {
//             return inputVector.z;
//         }
//         else
//         {
//             return Input.GetAxis("Vertical");
//         }
//     }



//     private void TurnAngle(Vector3 currentJoystickVec)
//     {
//         Vector3 originJoystickVec = weapon.transform.up;
//         // character가 바라보고 있는 벡터
 
//         float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
//         int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
//         // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
//         // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-
 
//         if (runningCoroutine != null)
//         {
//             StopCoroutine(runningCoroutine);
//         }
//         runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
//         // 코루틴이 실행중이면 실행 중인 코루틴 중단 후 코루틴 실행 
//         // 코루틴이 한 개만 존재하도록.
//         // => 회전 중에 새로운 회전이 들어왔을 경우, 회전 중이던 것을 멈추고 새로운 회전을 함.
//     }



//     IEnumerator RotateAngle(float angle, int sign)
//     {
//         float mod = angle % rotateSpeed; // 남은 각도 계산
//         for (float i = mod; i < angle; i += rotateSpeed)
//         {
//             weapon.transform.Rotate(0, 0, sign * rotateSpeed); // 캐릭터 rotateSpeed만큼 회전
//             yield return new WaitForSeconds(0.01f); // 0.01초 대기
//         }
//         weapon.transform.Rotate(0, 0, sign * mod); // 남은 각도 회전
//     }
// }

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler 
{
    public static Action trys;
    // 공개
    public Transform weapon;        // 플레이어.
    public Transform joystick;         // 조이스틱.
    public GameObject spaceship;
 
    // 비공개
    private Vector3 joystickFirstPos;  // 조이스틱의 처음 위치.
    private Vector3 joystickVec;// 조이스틱의 벡터(방향)
    private float joystickRadius;          // 조이스틱 배경의 반 지름.
    private bool isJoystickMoving;          // 플레이어 움직임 스위치.
    private float rotateSpeed = 6; // 회전 속도
    private Coroutine runningCoroutine; // 부드러운 회전 코루틴

    PlayerController playerController;

    [SerializeField]
    private Canvas mainCanvas;
    private RectTransform rectTransform;
    // private void Awake()
    // {
    //     Vector3 v3;
    //     trys = () => { TurnAngle(); };
    // }
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //해상도에 맞게 해보자.
    }
    void Start()
    {   
        joystickRadius = this.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
        print("너비 :" + this.gameObject.GetComponent<RectTransform>().sizeDelta.x);
        print("joystickRadius : " + joystickRadius);
        spaceship = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindWithTag("WeaponFirst").transform;
        playerController = spaceship.GetComponent<PlayerController>();
        // 처음 조이스틱 위치 = 현재 조이스틱의 위치
        joystickFirstPos = joystick.transform.position;
        // 조이스틱의 움직이는 중 = 거짓
        isJoystickMoving = false;
    } 
 
    // 드래그 함수
    public void OnDrag(PointerEventData eventData)
    {

        float distance = Vector3.Distance(rectTransform.position, eventData.position);
        float maxDistance = 150f;

        // 조이스틱이 움직이는 중 = 참
        isJoystickMoving = true;

        // 위치 = 터치 이벤트가 발동된 위치
        Vector3 joystickTouchPos = eventData.position;
        
        // 조이스틱을 이동시킬 방향을 구함. 크기는 1로 만듬
        joystickVec = (joystickTouchPos - joystickFirstPos).normalized;
 
        // 터치이벤트가 발동된 위치와 조이스틱의 처음 위치의 거리를 구한다.
        float dis = Vector3.Distance(joystickTouchPos, joystickFirstPos);
        
        // 그 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는 곳으로 이동
        if (dis < maxDistance * mainCanvas.scaleFactor)
            joystick.position = joystickFirstPos + joystickVec * dis;

        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만(조이스틱 가장자리로) 이동
        else
            //joystick.position = joystickFirstPos + joystickVec * joystickRadius;
            joystick.position = transform.position + ((Vector3)eventData.position - transform.position).normalized * (maxDistance * mainCanvas.scaleFactor);
 
        //Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        TurnAngle(joystickVec);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // 드래그 끝.
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.position = joystickFirstPos; // 스틱을 원래의 위치로.
        joystickVec = Vector3.zero;          // 방향을 0으로.
        isJoystickMoving = false;
        TurnAngle(Vector3.zero); // 스틱을 떼면 회전을 멈춰야함.
    }





    public void TurnAngle(Vector3 currentJoystickVec)
    {
        // if ( PlayManager.isOver )
        //     return;
        
        /* EMP 사용 도중 1번 무기 회전 불가 */
        if (playerController.isEMPCast)
            return;

        Vector3 originJoystickVec = weapon.transform.up;
        // character가 바라보고 있는 벡터
 
        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
        // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-
 
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
        // 코루틴이 실행중이면 실행 중인 코루틴 중단 후 코루틴 실행 
        // 코루틴이 한 개만 존재하도록.
        // => 회전 중에 새로운 회전이 들어왔을 경우, 회전 중이던 것을 멈추고 새로운 회전을 함.
    }

    
    IEnumerator RotateAngle(float angle, int sign)
    {
        // if ( PlayManager.isOver )
        //     yield break;

        float mod = angle % rotateSpeed; // 남은 각도 계산
        for (float i = mod; i < angle; i += rotateSpeed)
        {
            //weapon.transform.Rotate(0, 0, sign * rotateSpeed); // 캐릭터 rotateSpeed만큼 회전
            weapon.transform.RotateAround(spaceship.transform.position, new Vector3(0, 0, 1), sign * rotateSpeed);
            yield return new WaitForSeconds(0.01f); // 0.01초 대기
        }
        //weapon.transform.Rotate(0, 0, sign * mod); // 남은 각도 회전
        weapon.transform.RotateAround(spaceship.transform.position, new Vector3(0, 0, 1), sign * mod);
    }
    
    
    //가운데 중심으로 회전 ex) 워크 3개 돌아가는 것
    //void FixedUpdate ()
    //{
    //    transform.RotateAround(weapon.position, zAxis, rotateSpeed); 
    //}

}

