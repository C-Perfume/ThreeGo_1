using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static Effect instance;

  
    public enum EFT
    {
        End,
        Choose,// ī�� �ΰ� �� ���?
        
        GoStop,
        Bomb, // ��ź
        ShakingCard,// ��鲨��?
        Shake, //������ �˷��ֱ�
        Shit, // �� �Ѱ�
        Kiss, // ��
        Kiss2, //����
        KJ,//�������� ����? ����?
        GDR,

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

        Red,
        Blue,
        Green

    }

    public List<GameObject> eftL;
    public GameObject[] cards;
    public GameObject select;
    //public bool s = false;
    private void Awake()
    {
        instance = this;
    }

    // ����Ʈ�� Ȱ��ȭ
    public void PlayEFT(EFT type)
    {
        eftL[(int)type].SetActive(true);
    }

    //ī�� ����
    public void PlayEFT1(EFT type, GameObject cardA, GameObject cardB)
    {
        eftL[(int)type].SetActive(true);
        if ((int)type == 1)
        {
            ins(cardA, cards[0]);
            ins(cardB, cards[1]);
        }

    }
    
    //ī�� ����
    public void PlayEFT2(EFT type, GameObject cardA, GameObject cardB, GameObject cardC)
    {
        eftL[(int)type].SetActive(true);

        if ((int)type == 4)
        {
            ins(cardA, cards[2]);
            ins(cardB, cards[3]);
            ins(cardC, cards[4]);
        }

        if ((int)type == 5)
        {
            ins(cardA, cards[5]);
            ins(cardB, cards[6]);
            ins(cardC, cards[7]);
        }


    }

    public GameObject Select()
    {
        
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

    //ī�庹���
    void ins(GameObject obj, GameObject obj2) {
        GameObject card = Instantiate(obj);
        card.transform.position = obj2.transform.position;
        card.transform.eulerAngles = new Vector3(50, 0, 0);
        card.transform.SetParent(gameObject.transform);
    }

    void Start()
    {
    
    }
    void Update()
    {
        
    }

}