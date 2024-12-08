using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    private float fadeTime = 0.1f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine("TwinkleLoop");  // 코루틴 함수 실행
    }

    private IEnumerator TwinkleLoop()
    {
        while ( true )
        {
            // Alpha 값을 1에서 0으로 : Fade Out
            yield return StartCoroutine(FadeEffect(1,0));
            // Alpha 값을 0에서 1로 : Fade In
            yield return StartCoroutine(FadeEffect(0, 1));
            // 코루틴 스타트 함수를 실행 시, 코루틴 스타트 함수가 모두 끝나야 다음 문항으로 넘어감 
        }
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while ( percent < 1 )
        {
            // fadeTime 시간동안 while 반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime; // 시간 끝나면 1

            // 유니티의 클래스에 설정되어있는 spriteRender.color, transform.position은 프로퍼티로
            // spriteRenderer.color.a = 1.0f와 같이 설정이 불가능
            // spriteRenderer.color = new Color(spriteRenderer.color.r ..., ..., 1.0f);와 같이 설정해야함
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent); // 컬러변수의 알파값을 러프함수로 변화시키군
            // float result = Mathf.Lerp(start, end, percent);
            // start와 end 사이의 값 중 percent 위치에 있는 값을 반환
            // 예를 들어 start가 0, end가 100일 때, percent가 0.3이면 30을 반환
            spriteRenderer.color = color;

            yield return null;

        }

    }
}
