using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;


    void Awake()
    {
        if(backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // private void Start()
    // {
    //     // 음소거키에 값이 없으면 음소거를 거짓으로 세팅하고 로드
    //     if(!PlayerPrefs.HasKey("musicMuted"))
    //     {
    //         PlayerPrefs.SetInt("musicMuted", 0);
    //         Debug.Log("음소거세팅이 저장이 안되어있읍니다. 음소거는 하지 않습니다.");
    //         MutedLoad();
    //     }
    //     // 음소거키에 값이 있으면 로드
    //     else
    //     {
    //         Debug.Log("음소거세팅이 저장되잇읍니다. 로드하여 음소거 값을 확인합니다.");
    //         MutedLoad();
    //     }
        
    //     // 볼륨세팅이 저장이 안되있는 경우에 볼륨키값을 100퍼로 맞춰주고 키에 든것을 슬라이더에 적용시키기
    //     if(!PlayerPrefs.HasKey("musicVolume"))
    //     {
    //         Debug.Log("볼륨세팅 저장이 안되어있읍니다. 볼륨크기는 최대로 합니다. 로드하여 볼륨세팅을 합니다.");
    //         PlayerPrefs.SetFloat("musicVolume", 1);
    //         VolumeLoad();
    //     }
    //     // 볼륨세팅이 저장이 되있으면 로드
    //     else
    //     {
    //         Debug.Log("볼륨세팅 저장이 되어있읍니다. 로드합니다. 로드하여 볼륨세팅을 합니다.");
    //         VolumeLoad();
    //     }
    // }
    
    // public void MutedLoad()
    // {
    //     // 음소거키가 1이면 true로 하고 음소거 시킴, 0이면 false를 넣어
    //     // musicMuted = PlayerPrefs.GetInt("musicMuted") == 1;
    //     // Debug.Log("1이면 뮤트, 0이면 온" + musicMuted);
    //     if ( PlayerPrefs.GetInt("musicMuted") == 1)
    //     {
    //         AudioListener.pause = true;
    //         Debug.Log("음소거합니다.");
    //     }
    //     else if ( PlayerPrefs.GetInt("musicMuted") == 0 )
    //     {
    //         AudioListener.pause = false;
    //         Debug.Log("음소거하지않습니다.");
    //     }
    // }

    // public void VolumeLoad()
    // {
    //     AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
    //     Debug.Log("볼륨세팅 볼륨값은" + AudioListener.volume);
    // }
}
