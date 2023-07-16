using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerInGame : MonoBehaviour
{

    //배경음 크기
    public int bgmVolume = 50;

    //효과음 크기
    public int sfxVolume = 50;

    //보이스 크기
    //public int voiceVolume = 50;
    /*
    public Dictionary<string,AudioClip> bgmAudioClipDict = new Dictionary<string,AudioClip>();

    public Dictionary<string, AudioClip> sfxAudioClipDict = new Dictionary<string, AudioClip>();

    public Dictionary<string, AudioClip> voiceAudioClipDict = new Dictionary<string, AudioClip>();
    */
    string bgmAudioSourceFolderName = "BGM";

    string sfxAudioSourceFolderName = "SFX";

    string voiceAudioSourceFolderName = "Voice";

    //public List<AudioClip> sfxAudioClipList = new List<AudioClip>();

    //public List<AudioClip> voiceAudioClipList = new List<AudioClip>();

    [ReadOnly] public AudioSource bgmAudioSource;

    public List<AudioClip> bgmAudioClipList = new List<AudioClip>();

    //private AudioSource sfxAudioSource;

    [ReadOnly] public AudioSource sfxAudioSource;

    public List<AudioClip> sfxAudioClipList = new List<AudioClip>();


    public enum SFXname
    {
        none,
        StoneBurn,
        AlarmPositive,
    }

    public enum SFXType
    {
        normal,
        rise,
    }


    private void Awake()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;

        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource.loop = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ChangeBGMAndPlay(0);
    }

    //특정 번호의 음성을 재생
    public void ChangeBGMAndPlay(int tempNum)
    {
        if (tempNum == -1)
        {
            bgmAudioSource.Stop();
        }
        //혹시라도 너무 큰 값을 받르면 작동하지 않게 만든다.
        else if (tempNum < bgmAudioClipList.Count)
        {

            bgmAudioSource.Stop();

            bgmAudioSource.clip = bgmAudioClipList[tempNum];

            bgmAudioSource.loop = true;

            bgmAudioSource.volume = 0.01f * bgmVolume;

            bgmAudioSource.Play();
        }
    }

    //소리가 해당 시간동안 천천히 사라짐
    public void FadeOutBGM()
    {
        StartCoroutine(FadeOutBGMCor());

    }

    IEnumerator FadeOutBGMCor()
    {
        while (bgmAudioSource.volume > 0f)
        {
            bgmAudioSource.volume -= (Time.deltaTime / 2);

            yield return null;
        }

        yield break;
    }

    public void ChangeSFXAndPlay(SFXname tempSFXname)
    {

        if (tempSFXname == SFXname.none)
        {
            sfxAudioSource.Stop();
        }
        else if((int)tempSFXname < sfxAudioClipList.Count)
        {
            sfxAudioSource.Stop();

            sfxAudioSource.clip = sfxAudioClipList[(int)tempSFXname];

            sfxAudioSource.loop = false;

            sfxAudioSource.volume = 0.01f * bgmVolume;

            sfxAudioSource.Play();

        }
        else
        {
            sfxAudioSource.Stop();
        }
        
        /*
        else if (tempNum < sfxAudioClipList.Count)
        {
            sfxAudioSource.Stop();

            sfxAudioSource.clip = sfxAudioClipList[tempNum];

            sfxAudioSource.loop = false;

            sfxAudioSource.volume = 0.01f * bgmVolume;

            sfxAudioSource.Play();
        }
        */
    }

    /*
    public void ChangeSFXAndPlay(int tempNum)
    {
        if (tempNum == -1)
        {
            sfxAudioSource.Stop();
        }
        else if (tempNum < sfxAudioClipList.Count)
        {
            sfxAudioSource.Stop();

            sfxAudioSource.clip = sfxAudioClipList[tempNum];

            sfxAudioSource.loop = false;

            sfxAudioSource.volume = 0.01f * bgmVolume;

            sfxAudioSource.Play();
        }
    }
    */

    /*
    public void ChooseSFXAndPlay(int tempNum)
    {
        //기존 audio를 정지
        sfxAudioSource.Stop();

        sfxAudioSource.volume = 0.01f * sfxVolume;

        //새롭게 audio를 설정

    }
    */

    //다음씬으로 넘어가기 위한 
    public void StopAllAudio()
    {
        bgmAudioSource.Stop();

        sfxAudioSource.Stop();

    }



    //폴더에 있는 Sound들을 불러온다.
    public void LoadSounds()
    {
        ///bgmAudioSource.
        //AudioClip
        //bgmAudioSource.        
    }

}
