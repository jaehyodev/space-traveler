using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundEffectsSlider;
    [SerializeField] Button soundOnBtn;
    [SerializeField] Button soundOffBtn;
    [SerializeField] Image musicOnImg;
    [SerializeField] Image musicOffImg;
    //[SerializeField] Button soundEffectOnBtn;
    //[SerializeField] Button soundEffectOffBtn;
    private bool volumeMuted = false;
    private bool loading = true;
    

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener (delegate {ChangeMusicVolume();});
        soundEffectsSlider.onValueChanged.AddListener (delegate {ChangeSoundEffectsVolume();});

        // 음소거키에 값이 없으면 음소거를 거짓으로 세팅하고 로드
        if(!PlayerPrefs.HasKey("volumeMuted"))
        {
            PlayerPrefs.SetInt("volumeMuted", 0);
        }
        
        // 볼륨세팅이 저장이 안되있는 경우에 볼륨키값을 100퍼로 맞춰주고 키에 든것을 슬라이더에 적용시키기
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        // 볼륨세팅이 저장이 안되있는 경우에 볼륨키값을 100퍼로 맞춰주고 키에 든것을 슬라이더에 적용시키기
        if(!PlayerPrefs.HasKey("soundEffectsVolume"))
        {
            PlayerPrefs.SetFloat("soundEffectsVolume", 1);
        }
        
        Load();
        UpdateButtonIcon();
        AudioListener.pause = volumeMuted;
    }

    // 씬이 로드될 때 슬라이더에 담긴 함수가 처음에 발동된다...
    public void ChangeMusicVolume()
    {
        print("배경음악 볼륨 바꿉니다.");
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(musicSlider.value) * 20);
        //AudioListener.volume = musicSlider.value;
        UpdateButtonIcon();
        if(!loading)
            Save();
    }

    public void ChangeSoundEffectsVolume()
    {
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(soundEffectsSlider.value) * 20);
        UpdateButtonIcon();
        if(!loading)
            Save();
    }

    public void OnButtonPress()
    {
        volumeMuted = false;
        AudioListener.pause = false;
        Save();
        UpdateButtonIcon();
    }

    public void OffButtonPress()
    {
        volumeMuted = true;
        AudioListener.pause = true;
        Save();
        UpdateButtonIcon();
    }

    void UpdateButtonIcon()
    {
        /* 뮤트이면 아이콘 변경 */
        if(volumeMuted)
        {
            soundOnBtn.gameObject.SetActive(true);
            soundOffBtn.gameObject.SetActive(false);       
        }
        else
        {
            soundOnBtn.gameObject.SetActive(false);
            soundOffBtn.gameObject.SetActive(true);
        }

        /* 배경음악 아이콘 변경 */
        if(musicSlider.value <= 0.001f)
        {
            musicOnImg.enabled = false;
            musicOffImg.enabled = true;
        }
        else
        {
            musicOnImg.enabled = true;
            musicOffImg.enabled = false;  
        }
    }

    public void Load()
    {
        Debug.Log("로드함수");
        // 음소거키가 1이면 true로 하고 음소거 시킴, 0이면 false를 넣어
        volumeMuted = PlayerPrefs.GetInt("volumeMuted") == 1;

                if(PlayerPrefs.HasKey("soundEffectsVolume"))
        {
            Debug.Log("soundEffectsSlider.value" + PlayerPrefs.GetFloat("soundEffectsVolume"));
            soundEffectsSlider.value = PlayerPrefs.GetFloat("soundEffectsVolume");
            Debug.Log("효과음slider : " + soundEffectsSlider.value);
        }
        else
        {
            soundEffectsSlider.value = 1;
        }
        // 뮤직볼륨키에 들어있는 실수값을 볼륨슬라이더에 적용
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = 1;
        }

        loading = false;
        BackgroundMusic backgroundMusic = GameObject.Find("BGM").GetComponent<BackgroundMusic>();
        backgroundMusic.GetComponent<AudioSource>().Play();

        //ChangeMusicVolume();
        //ChangeSoundEffectsVolume();
    }

    public void Save()
    {
        // 음소거키에 음소거 참이면 1을 저장, 거짓이면 0을 저장
        PlayerPrefs.SetInt("volumeMuted", volumeMuted ? 1: 0);
        // 볼륨슬라이더값을 뮤직볼륨키에 저장하여 게임을 켤때마다 적용시킬 수 있도록 해야지
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsSlider.value);
        Debug.Log("효과음세팅 저장이 되어있읍니다. 로드합니다." + PlayerPrefs.GetFloat("soundEffectsVolume"));
    }
}

// PlayerPrefs로 실수와 정수 문자는 저장가능, 불은 저장 불가