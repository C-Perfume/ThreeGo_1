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
    public enum EFG_TYPE // ȿ����
    {
        EFT_ssg,//
        EFT_jjak,//
        EFT_tuk,//

        EFT_eatpack,//
        
        EFT_kiss,//
        EFT_bomb,//
        EFT_ddadak,//
        EFT_shaking,//
        EFT_pack,//
        EFT_clean,//
        
        EFT_takecakd, //
        EFT_nagari,//

        EFT_tictok // 
       
    }

    //���ʿ��� �̳� �� ������ ���߿� ��ƼŬ������ !!3���� ������ �����Ҽ���
    //a: 1�� , b : 3��, c :8��, d :�˱�, e : ��
    public enum GoState
    {
        //3��
        gang_abc,
        gang_abd,
        gang_acd,
        gang_bcd,
        //4��
        gang_abcd,
        gang_abce,
        gang_abde,
        gang_acde,
        gang_bcde,
        //5��
        gang_abcde,
    }


    public enum GoCounter // ����� 
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

    public enum AddPoint // �̺�Ʈ ���� 
    {
        EFT_godori,
        EFT_hongdan,
        EFT_chongdan,
        EFT_chodan,

        EFT_threegang,
        EFT_fourgang,
        EFT_fivegang,
    }

    //BGM �÷����ϴ� AudioSource
    public AudioSource bgmAudio;
    //EFT �÷����ϴ� AudioSource
    public AudioSource eftAudio;

    public AudioSource goAudio;

    public AudioSource eventsAudio;

    // bgm ����
    public AudioClip[] bgms;
    // eft ����
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
        eftAudio.PlayOneShot(efts[(int)type]);
    }

    public void PlayGo(int gocount)
    {
        print(gocount + "��");
        goAudio.PlayOneShot(go[gocount - 1]);
    }

    public void AddPointer(AddPoint point)
    {
        eventsAudio.PlayOneShot(events[(int)point]);
        print(point.ToString());
    }
}