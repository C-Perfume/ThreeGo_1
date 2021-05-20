using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static Effect instance;


    public enum EFT
    {
        End, // ��������
        Choose,// ī�� �ΰ� �� ���?
        GoStop,

        Bomb, // ��ź
        ShakingCard,// ��鲨��?
        Shake, //������ �˷��ֱ�
        
        Shit, // �� �Ѱ�
        GetShit, //�� �Ա�
        MyShit, //�ڻ�
        
        Kiss, // ��
        Kiss2, //����
        
        KJ,//�������� ����? ����?

        Go,
        Stop,

        G138,
        G1311,
        G1811,
        G3811,
        G13811,
        G13812,
        G131112,
        G181112,
        G381112,
        G5,

        GDR,
        Red,
        Green,
        Blue,

        Clear,
        BombHint

    }

    public List<GameObject> eftL;
    public List<GameObject> eftF;
    public GameObject[] cards;
    
    public EFT type;

    // EFT ����
    public enum EFT_TYPE // ȿ����
    {
        EFT_bomb,//
        EFT_shaking,//
        EFT_shit,//
        EFT_getshit,//
        EFT_take,//
        EFT_kiss,//
        EFT_kiss2,//
        EFT_Stop,
        EFT_G3,
        EFT_G4,
        EFT_G5,
        EFT_GDR,
        EFT_hongdan,
        EFT_chodan,
        EFT_chongdan,
        EFT_clean,//
        EFT_ssg,//
        EFT_jjak,//
        EFT_tuk,//
        TitleM,
        BGM,
        win
    }

    public enum Go// ����� 
    {
        EFT_onego,//
        EFT_twogo,//
        EFT_threego,//
        EFT_fourgo,//
        EFT_sixgo,//
        EFT_fivego,//
        EFT_sevengo,//
    }


    //EFT �÷����ϴ� AudioSource
    public AudioSource bgmAudio;
    public AudioSource eftAudio;
    public AudioSource goAudio;

    // eft ����
    public AudioClip[] efts;
    public AudioClip[] go;



    private void Awake()
    {
        instance = this;
    }

    public void PlayBGM(EFT_TYPE type)
    {
        bgmAudio.PlayOneShot(efts[(int)type]);
    }
    
    public void PlayEFTM(EFT_TYPE type)
    {
        eftAudio.PlayOneShot(efts[(int)type]);
    }
    
    public void PlayGo(int gocount)
    {
        print(gocount + "��");
        goAudio.PlayOneShot(go[gocount - 1]);
    }

    
    //�׳� Ȱ��ȭ�� ���ش�. 0, 1, 9�� hint��
    public void PlayEFT(int eftType)
    {
        eftL[eftType].SetActive(true);
    }

        // ����Ʈ�� ���� 3, 6, 7, 8 ����
    public void PlayEFT(int eftType, GameObject obj2, float up)
    {
        GameObject eftA = Instantiate(eftL[eftType]);
        eftA.transform.SetParent(gameObject.transform);
        eftA.transform.localScale = new Vector3(.01f, .01f, .01f);
        eftA.transform.position = obj2.transform.position + Vector3.up * up;
        eftA.SetActive(true);
        Destroy(eftA, 2);
    }

    //���� ����Ʈ
    public void PlayEFT(int eftFn, GameObject obj2, float up, float destroy)
    {
        GameObject eftA = Instantiate(eftF[eftFn]);
        eftA.transform.SetParent(gameObject.transform);
        //eftA.transform.localScale = new Vector3(.01f, .01f, .01f);
        eftA.transform.position = obj2.transform.position + Vector3.up * up;
        eftA.SetActive(true);
        Destroy(eftA, destroy);
    }

    // ī�� ������ ����Ʈ �Լ� 1, 4, 5��
    public void PlayEFT(int eftType, GameObject cardA, GameObject cardB, GameObject cardC)
    {
       
        eftL[eftType].SetActive(true);
      
            if (eftType == 1)
        {
            ins(cardA, cards[0]);
            ins(cardB, cards[1]);
            gameObject.transform.GetChild(gameObject.transform.childCount - 2).Rotate(0, 179, 0);
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).Rotate(0, 179, 0);
        }

        if (eftType == 4)
        {
            ins(cardA, cards[2]);
            ins(cardB, cards[3]);
            ins(cardC, cards[4]);
        }

        if (eftType == 5)
        {
            ins(cardA, cards[5]);
            ins(cardB, cards[6]);
            ins(cardC, cards[7]);
            gameObject.transform.GetChild(gameObject.transform.childCount - 3).Rotate(-130, 0, 0);
            gameObject.transform.GetChild(gameObject.transform.childCount - 2).Rotate(-130, 0, 0);
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).Rotate(-130, 0, 0);

        }

    }

    //ī�� ���ÿ� �Լ�
    public GameObject Select()
    {
        GameObject select = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.parent == gameObject.transform)
            { 
                select = hit.transform.gameObject;
                select.SetActive(false);
                
                if (select.name != gameObject.transform.GetChild(gameObject.transform.childCount - 1).name)
                {
                    Destroy(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                }
                else { Destroy(gameObject.transform.GetChild(gameObject.transform.childCount - 2).gameObject); }
            }
        }
        else
        {
            select = null;
        }
        

        return select;

    }
    
    //����
    public GameObject Or()
    {
        GameObject or = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name.Contains("cc"))
            { 
                or = hit.transform.gameObject;
            }
        }
        else
        {
            or = null;
        }
        

        return or;

    }

    //ī�� �����
    void ins(GameObject obj, GameObject obj2) {
        GameObject card = Instantiate(obj);
        card.transform.SetParent(gameObject.transform);
        card.transform.localScale = new Vector3(.008f, .01f);
        card.transform.position = obj2.transform.position;
        card.transform.eulerAngles = new Vector3(40, 0, 0);
    }



    void Start()
    {
    
    }
    void Update()
    {
        
    }

}