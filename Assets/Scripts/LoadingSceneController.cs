using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image loadingBar;
    [SerializeField]
    Image loadingPoint;
    //public GameObject hh;

    private void Start()
    {   
        // StartCoroutine(LoadSceneProcess()); 또는 이렇게도 사용하는 듯   
        StartCoroutine(LoadSceneProcess()); 
    }

    private void Update()
    {
        //hh.transform.Translate(1,0,0);
        
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        Debug.Log(nextScene + sceneName);
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadSceneProcess()
    {
        // LoadSceneAsync함수는 비동기 방식으로 씬을 불러오는 중간에 작업이 가능
        // 하지만 LoadScene함수는 동기 방식으로 씬을 다 불러오기 전까지는 작업이 불가
        // 씬을 불러오는 진행 상황은 LoadSceneAsync 함수가 AsyncOperation 타입으로 반환해준다.
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        // AsyncOperation 클래스의 객체인 op를 통해서 allowSceneActivation을 false로 바꿔준다.
        // 이 옵션은 씬을 비동기로 불러올 때, 씬의 로딩이 끝나면 자동으로 불러온 씬으로 이동할 것인지 설정
        // 이 값을 false로 설정하면 씬을 90%까지만 로드한 상태로 다음 씬으로 가지않고 기다린다.
        // 이 값을 다시 true로 설정하면 그 때 남은 부분을 로딩하고 씬을 불러온다.
        // 원래 true로 해도 상관은 없지만 false로 하는 이유가 2가지 정도가 있다.
        // 첫째, 생각보다 씬 로딩 속도가 빠를 수 있다. 너무 빠르면 안 보이니까...
        // 둘째, 로딩화면에서 불러와야하는게 씬만은 아니다. 에셋 번들로 나눠서 빌드하니까 이것도 불러와야하니까... 다 안 들어와있으면 꺠질테니까
        op.allowSceneActivation = false;

        float timer = -2f;
        while(!op.isDone)
        {
            // 반복문이 한 번 반복될때마다 유니티 엔진에 제어권을 넘기도록 한다.
            // 제어구너을 넘기지 않으면 반복문이 끝나기 전에는 화면이 갱신되지 않아서
            // 진행바가 차오르는게 보이지 않는다.
            yield return null;
            loadingPoint.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(-350f, 350f, op.progress), 155);
            if (op.progress < 0.9f)
            {
                
                loadingBar.fillAmount = op.progress;
            }
            else
            {
                // 90퍼센트가 넘는다면 그 이후는 1초에 걸쳐 채우게 한다 (이게 왜 1초야?)
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                loadingPoint.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(300f, 350f, timer), 155);
                if (loadingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

// 함수와 변수를 static으로 선언해두면 LoadingScene으로 넘어오지 않아서
// 로딩씬컨트롤러가 부착된 게임오브젝트가 생성되지 않은 상태이더라도
// 로딩씬컨트롤러의 클래스 이름으로 호출해서 사용할 수 있게 됩니다.
// static으로 선언된 함수의 내부에서는 static으로 선언되지 않은
// 일반멤버변수나 함수는 바로 호출할 수 없음