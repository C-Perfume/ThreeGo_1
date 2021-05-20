using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static Effect instance;


    public enum EFT
    {
        End, // 최종점수
        Choose,// 카드 두겹 중 어떤거?
        GoStop,

        Bomb, // 폭탄
        ShakingCard,// 흔들꺼냐?
        Shake, //흔들었다 알려주기
        
        Shit, // 뻑 싼거
        GetShit, //뻑 먹기
        MyShit, //자뻑
        
        Kiss, // 쪽
        Kiss2, //따닥
        
        KJ,//국진선택 열끗? 쌍피?

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
    public GameObject[] cards;
    
    public EFT type;

    private void Awake()
    {
        instance = this;
    }

    
    //그냥 활성화만 해준다. 0, 1, 9번 hint용
    public void PlayEFT(int eftType)
    {
        eftL[eftType].SetActive(true);
    }

        // 이펙트만 복제 3, 6, 7, 8 번용
    public void PlayEFT(int eftType, GameObject obj2, float up)
    {
        GameObject eftA = Instantiate(eftL[eftType]);
        eftA.transform.SetParent(gameObject.transform);
        eftA.transform.localScale = new Vector3(.01f, .01f, .01f);
        eftA.transform.position = obj2.transform.position + Vector3.up * up;
        eftA.SetActive(true);
        Destroy(eftA, 2);
    }

    // 카드 복제용 이펙트 함수 1, 4, 5용
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

    //카드 선택용 함수
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
    
    //흔들기
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

    //카드 복사용
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