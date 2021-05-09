using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // BGM 종류
    public enum BGM_TYPE
    {
        BGM_1,
        BGM_2,
        BGM_3
    }

    // EFT 종류
    public enum EFG_TYPE
    {
        EFT_ssg,//
        EFT_jjak,//
        EFT_tuk,//

        EFT_eatpack,//
        /*
        EFT_kiss,//
        EFT_bomb,//
        EFT_ddadak,//
        EFT_shaking,//
        EFT_pack,//
        EFT_clean,//
        */
        EFT_takecakd,//
        EFT_nagari
    }

    //불필요한 이넘 들 하지만 나중에 파티클을위해 !!3광의 각각을 관리할수도
    //a: 1광 , b : 3광, c :8광, d :똥광, e : 비광
    public enum GoState
    {
        gang_abc,
        gang_abd,
        gang_acd,
        gang_bcd,
        gang_abcd,
        gang_abce,
        gang_abde,
        gang_acde,
        gang_bcde,
        gang_abcde,
    }


    public enum GoCounter
    {
        EFT_onego,//
        EFT_twogo,//
        EFT_threego,//
        EFT_fourgo,//
        EFT_sixgo,//
        EFT_fivego,//
        EFT_sevengo,//
        EFT_eightgo//
    }

    public enum AddPoint
    {
        EFT_godori,
        EFT_hongdan,
        EFT_chongdan,
        EFT_chodan,
        
        EFT_threegang,
        EFT_fourgang,
        EFT_fivegang,
    }

    //BGM 플레이하는 AudioSource
    public AudioSource bgmAudio;
    //EFT 플레이하는 AudioSource
    public AudioSource eftAudio;

    public AudioSource goAudio;

    public AudioSource eventsAudio;

    // bgm 파일
    public AudioClip[] bgms;
    // eft 파일
    public AudioClip[] efts;

    public AudioClip[] go;

    public AudioClip[] events;


    private void Awake()
    {
        instance = this;
    }

    public void PlayBGM(BGM_TYPE type)
    {
        bgmAudio.clip = bgms[(int)type];
        bgmAudio.Play();
    }

    public void PlayEFT(EFG_TYPE type)
    {
        //eftAudio.clip = efts[(int)type];
        eftAudio.PlayOneShot(efts[(int)type]);
        //print(type.ToString());
    }

    public void PlayGo(byte gocount)
    { 
        print(gocount + "고");
        goAudio.PlayOneShot(go[(int)gocount-1]);
    }

    public void AddPointer(AddPoint point)
    {
        eventsAudio.PlayOneShot(events[(int)point]);
        print(point.ToString());
    }
}