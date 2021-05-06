using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // BGM ����
    public enum BGM_TYPE
    {
        BGM_1,
        BGM_2,
        BGM_3
    }

    // EFT ����
    public enum EFG_TYPE
    {
        EFT_ssg,
        EFT_jjak,
        EFT_tuk,

        EFT_godori,
        EFT_hongdan,
        EFT_chongdan,
        EFT_chodan,

        EFT_kiss,
        EFT_bomb,
        EFT_ddadak,
        EFT_shaking,
        EFT_pack,
        EFT_eatpack,
        EFT_clean,
        EFT_takecakd,
        EFT_nagari,

        EFT_onego,
        EFT_twogo,
        EFT_threego,
        EFT_fourgo,
        EFT_fivego,
        EFT_sixgo
    }

    //BGM �÷����ϴ� AudioSource
    public AudioSource bgmAudio;
    //EFT �÷����ϴ� AudioSource
    public AudioSource eftAudio;

    // bgm ����
    public AudioClip[] bgms;
    // eft ����
    public AudioClip[] efts;


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
        print(type.ToString());
    }
}