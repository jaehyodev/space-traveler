using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField]
    private Vector2 limitMin;
    [SerializeField]
    private Vector2 limitMax;

    public Vector2 LimitMin { get { return limitMin; } }
    public Vector2 LimitMax { get { return limitMax; } }
}

/*
 * File : StageData.cs
 * Desc
 *  : 현재 스테이지의 화면 내 범위
 *  : 에셋 데이터로 저장해두고 정보를 불러와서 사용
 * 부모클래스로 ScriptableOject를 사용하면 해당 클래스를 에셋 파일의 형태로 저장
 * 3번째 줄과 같이 클래스 상단에 [CreateAssetMenu]를 붙이면
 * Project View의 Create("+") 메뉴에 메뉴로 등록된다.
*/

/*
LimitMin, LimitMax에 빨간 줄 오류...
스크립트 런타임 버전 오류입니다.
현재 사용하는 유니티 버전이 몇인지 모르겠지만
Edit - Project Settings - Player - Other Settings - Configure에 있는 Scripting Runtime Version을 .NET 3.5가 아닌 .NET 4.x로 바꿔주세요.
이 메뉴가 없으면
public Vector2 LimitMax => limitMax;
대신
public Vector2 LimitMax { get { return limitMax; } }
와 같이 작성해 주세요

=> 이 표현이 C# 6.0 이상에서 메소드나 프로퍼티의 Body가 간단할 때 람다식처럼 표현하는 기능입니다.

https://www.youtube.com/watch?v=Bi-IK4uTBTg
*/