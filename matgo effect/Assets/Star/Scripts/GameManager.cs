using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class floor
{
    public List<GameObject> occupy = new List<GameObject>();
    public Vector3 pos;
}
public class GameManager : MonoBehaviour
{
    //** floorL �߰��ϴ°� �ٽ� Ȯ���ϱ� / ��ũ��Ʈ �ΰ��� �ٲ㺸��

    #region ��������
    public Sprite[] imgs; // ȭ���̹���48��
    public Sprite back; // �޸��̳� ���� �ƹ��ų� �������
    public Transform[] deckPos; // �ٴ�����ġ
    public Transform zero; // ī�嵦 0,0,0�̴�.
    public Transform[] p1HandPos;
    public Transform[] p2HandPos;
    public Transform[] p1FPos; //��������
    public Transform[] p2FPos; //��������

    public List<GameObject> cardL = new List<GameObject>(); //��� �и� ��� ����Ʈ
                                                            //(num�� ���� ������ �ο��� ���� ���������� ������ ã�� �� �ִ� ����Ʈ�� �ϴ°� �ʿ��ߴ�..
                                                            //�迭�� ������ ������ �� �ִ� �Լ�?�� ��ã������.
                                                            //������� ī��Ӽ� ��ũ��Ʈ�� ���� ���� ���� ������ ������ �ʿ䰡 ��������.
                                                            //�ƽ�..(cardL.IndexOf(hitObj);)
    public List<GameObject> p1HandL = new List<GameObject>();
    public List<GameObject> p2HandL = new List<GameObject>();
    public List<GameObject> floorL = new List<GameObject>(); // �ٴ��� ���� Ȯ�ο� emptyL.occupy ����Ʈ ������ ������ ����..


    public List<floor> emptyL = new List<floor>();
    //���� �ٴ� ������� ��ġ���� �����ϱ� ���� ����Ʈ..�� ������ٰ� ������� �������� Ŭ����>bool�Ӽ��� ���� ó�� ������
    // �迭���� ����Ʈ�� ���� ��..�߰� ���Ű� �ʿ��ϴ� �Ф�
    public List<floor> p1FPosL = new List<floor>();
    public List<floor> p2FPosL = new List<floor>();

    List<GameObject> tripleC;
    #endregion


    void Start()
    {
        SetCard();
       Shuffle();
        StartCoroutine(Distribute());
    }



    void Update()
    {
        
    }

    
    
    void SetCard()
    {
        for (int j = 0; j < 12; j++)
        {
            for (int i = 1; i < 5; i++)
            {
                cardL[i - 1 + (j * 4)].GetComponent<CardS>().num = 1 + j; // ���� ����
            }
        } // Card ��ũ��Ʈ�� ���� ����

        for (int i = 0; i < cardL.Count; i++)
        {
            //deckimg = new Sprite[cardL.Count];
            cardL[i].GetComponent<SpriteRenderer>().sprite = imgs[i]; // �̹��� ����
            //deckimg[i] = cardL[i].GetComponent<SpriteRenderer>().sprite;
            //cardL[i].GetComponent<SpriteRenderer>().sprite = back;
            cardL[i].transform.position += Vector3.right * 0.01f * i;
        } // ī���̹���

        for (int i = 0; i < deckPos.Length; i++)
        {
            floor info = new floor(); // �ѹ� ���Դٰ� ������� �ӽú��� for���� �������� ����>������ �ݺ��Ѵ�.
            info.pos = deckPos[i].position;
            emptyL.Add(info); // ���⿡ �߰��� ������ empty[i]�� ���� ���Եȴ�.
        } // �ٴ� �а��� 

        for (int i = 0; i < p1FPos.Length; i++)
        {
            floor info = new floor();
            info.pos = p1FPos[i].position;
            p1FPosL.Add(info);
        } // �ٴ� �÷��̾� 1�� �а��� 

        for (int i = 0; i < p1FPos.Length; i++)
        {
            floor info = new floor();
            info.pos = p2FPos[i].position;
            p2FPosL.Add(info);
        } // �ٴ� �÷��̾� 2�� �а��� 
    }

    
    
     void Shuffle() // �ΰ��� ������ �����ش�. �װ� �����̴�..
    {
        for (int i = 0; i < 100; i++)
        {
            int j = Random.Range(12,27);
            int k = Random.Range(12, 27);
            GameObject Save = cardL[j];
            cardL[j] = cardL[k];
            cardL[k] = Save;
        }

        //���ڼ���
        GameObject t = cardL[1];
        cardL[1] = cardL[5];
        cardL[5] = t;


        GameObject a = cardL[2];
        cardL[2] = cardL[29];
        cardL[29] = a;

        GameObject b = cardL[8];
        cardL[8] = cardL[2];
        cardL[2] = b;

    }




    IEnumerator Distribute()
    {

        for (int redo = 0; redo < 2; redo++)// 2���ݺ�
        {
            
            for (int i = 0; i <= 4; i++)//p1�� �й�
            {

                ActioniT(cardL[0], 1 + (i * 0.2f), p1HandPos[i + (redo * 5)].position, .15f);
                cardL[0].GetComponent<CardS>().pNum = CardS.PLAYER.P1;
                p1HandL.Add(cardL[0]);
                cardL.RemoveAt(0);

            }

            for (int i = 0; i <= 3; i++) // �ٴ��кй�
            {

                ActioniT(cardL[0], i * 0.2f, deckPos[i + (redo * 4)].position, .15f);
                emptyL[i + (redo * 4)].occupy.Add(cardL[0]);
                floorL.Add(cardL[0]);
                cardL.RemoveAt(0);
            
            }

            for (int i = 0; i <= 4; i++) // p2�кй�
            {

                //cardL[0].GetComponent<SpriteRenderer>().sprite = back;
                ActioniT(cardL[0], 2 + (i * 0.2f), p2HandPos[i + (redo * 5)].position, .15f);
                cardL[0].GetComponent<CardS>().pNum = CardS.PLAYER.P2;
                p2HandL.Add(cardL[0]);
                cardL.RemoveAt(0);
            
            }
        
            yield return new WaitForSeconds(3);
        }
        
        Rot();
        Sort();

        PlayerM pm = GameObject.Find("P1").GetComponent<PlayerM>();
        //PlayerM pm2 = GameObject.Find("P2").GetComponent<PlayerM>();
        Score sc = GameObject.Find("P1").GetComponent<Score>();
        Score sc2 = GameObject.Find("P2").GetComponent<Score>();
        sc.Card4();
        sc.Card4();
        pm.Card4F();
    }

  
    //ī������
    public void Sort()
    {
        //�ٴ��� ����
        for (int i = 0; i < floorL.Count; i++)
        {

            int k = 1;
            for (int j = i + 1; j < floorL.Count; j++)
            {

                if (floorL[i].GetComponent<CardS>().num == floorL[j].GetComponent<CardS>().num)
                {

                    floorL[j].transform.position = floorL[i].transform.position + Vector3.right * 0.3f * k;

                    if (emptyL[j].occupy.Count > 0) emptyL[i].occupy.Add(emptyL[j].occupy[0]);
                    if (emptyL[j].occupy.Count > 0) emptyL[j].occupy.RemoveAt(0);
                    k++;
                }

            }

            k = 1;
        }


        // p1 ���� ����
        // ���� ��Ʈ�� �ٽ� ����� �ϳ�?�̤�
        for (int j = 0; j < p1HandL.Count; j++)
        {

            for (int i = 0; i < p1HandL.Count - 1; i++)
            {

                if (p1HandL[i].GetComponent<CardS>().num > p1HandL[i + 1].GetComponent<CardS>().num)
                {

                    GameObject a = p1HandL[i];
                    p1HandL[i] = p1HandL[i + 1];
                    p1HandL[i + 1] = a;

                    Vector3 save = p1HandL[i].transform.position;
                    p1HandL[i].transform.position = p1HandL[i + 1].transform.position;
                    p1HandL[i + 1].transform.position = save;
                    // �� ����� ��ġ �� �ƴ϶� �п�����Ʈ�� �迭�� �����ؾߵȴ�. 

                }
            }
        }


        // p2 ���� ����
        for (int j = 0; j < p2HandL.Count; j++)
        {

            for (int i = 0; i < p2HandL.Count - 1; i++)
            {

                if (p2HandL[i].GetComponent<CardS>().num > p2HandL[i + 1].GetComponent<CardS>().num)
                {

                    GameObject a = p2HandL[i];
                    p2HandL[i] = p2HandL[i + 1];
                    p2HandL[i + 1] = a;

                    Vector3 save = p2HandL[i].transform.position;
                    p2HandL[i].transform.position = p2HandL[i + 1].transform.position;
                    p2HandL[i + 1].transform.position = save;
                    // �� ����� ��ġ �� �ƴ϶� �п�����Ʈ�� �迭�� �����ؾߵȴ�. 

                }
            }
        }
    }




    //ī��ȸ��
    public void Rot()
    {
        for (int i = 0; i < p1HandL.Count; i++)
        {
            p1HandL[i].transform.eulerAngles = new Vector3(30, 0);
        }

        for (int i = 0; i < p2HandL.Count; i++)
        {
            p2HandL[i].transform.eulerAngles = new Vector3(-30, 0);
        }

        for (int i = 0; i < floorL.Count; i++)
        {
            floorL[i].transform.eulerAngles = new Vector3(90, 0);
        }

    }

    //ī�幫��
    public void ActioniT(GameObject obj, float i, Vector3 pos, float j)
    {

        iTween.MoveTo(obj,
                    iTween.Hash(
                    "delay", i,
                      "position", pos,
                       "time", j,
                        "easytype", iTween.EaseType.easeOutBack));
    
    } 

  
}



