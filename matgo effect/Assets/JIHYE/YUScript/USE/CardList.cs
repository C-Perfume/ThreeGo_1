using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class NumSort : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (x <= y) return 1;
        return -1;
    }
}



public class CardList : MonoBehaviour
{
    public static CardList instance;
    public GameObject back_card;
    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> pea = new List<GameObject>(48);
    public List<Sprite> images = new List<Sprite>();

    public List<GameObject> player1 = new List<GameObject>();//�տ� ����ִ���
    public List<GameObject> player2 = new List<GameObject>();
    public List<GameObject> floor = new List<GameObject>();//�ٴڿ� ����
                                                           //�ٴڿ� ��ȴµ� ���� �ж� ���� ������ 


    public List<GameObject> Player1_Score = new List<GameObject>();//���������ΰ�������
    public List<GameObject> Player2_Score = new List<GameObject>();

    public List<Transform> floor_Slot = new List<Transform>(); //ī�尡 ��ġ�� �ڸ� 
    public List<Transform> Empty_floor_Slot = new List<Transform>();

    public List<Transform> player1_Slot = new List<Transform>();//���� ��ġ
    public List<Transform> player2_Slot = new List<Transform>();

    public List<Transform> player1_ScoreBord = new List<Transform>();//������ ��ġ 
    public List<Transform> player2_ScoreBord = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < pea.Count; i++)
        {
            pea[i].GetComponent<SpriteRenderer>().sprite = images[i];
        }
        StartCoroutine(Start_Setting());


    }

    void Update()
    {
        //ī�� ����
        Selected();
        
    }


    //============================================

    IEnumerator Start_Setting()
    {
        Suff_Pea();//ī�弯��
        Card_Share();//ī���ġ ���ÿ� 
        yield return new WaitForSeconds(1);
        FloorArray();//�ٴ�����
        PlayerArray(0);//�÷��̾�1 ����
        PlayerArray(1);//�÷��̾�2 ����
    }
    void Suff_Pea()
    {
        /*���� Ȯ�ο�
        GameObject y = pea[0];
        pea[0] = pea[32];
        pea[32] = y;
        */

        for (int i = 0; i < 100; i++)
        {
            int a = UnityEngine.Random.Range(4, pea.Count);
            int b = UnityEngine.Random.Range(4, pea.Count);

            GameObject x = pea[a];
            pea[a] = pea[b];
            pea[b] = x;
        }

        /* ��ź ���� ����  */
        for(int i = 0; i < 3; i++)
        {
            GameObject t = pea[i];
            pea[i] = pea[i + 5];
            pea[i + 5] = t;
        }
        
    }
    void Card_Share()
    {
        for (int a = 0; a < 2; a++)
        {
            for (int i = 0; i < 4; i++)
            {
                iTween.MoveTo(pea[0], Empty_floor_Slot[0].position,1);
                //pea[0].transform.position = floorSlot[i + a * 4].pos; //�̵���Ű��
                floor_Slot.Add(Empty_floor_Slot[0]);
                Empty_floor_Slot.RemoveAt(0);
                floor.Add(pea[0]);//�ٴ��п� �߰�
                pea.RemoveAt(0);
            }
            for (int i = 0; i < 5; i++)
            {

                Card c = pea[0].GetComponent<Card>();
                c.player_index = 0;
                iTween.MoveTo(pea[0], player1_Slot[ (a * 5) + i].position, 1);
                //pea[0].transform.position = player1_Slot[i + a * 4].position;//1 �÷��̾�� ��ġ
                player1.Add(pea[0]);
                pea.RemoveAt(0);
            }
            for (int i = 0; i < 5; i++)
            {
                Card c = pea[0].GetComponent<Card>();
                c.player_index = 1;
                iTween.MoveTo(pea[0], player2_Slot[i + a * 5].position, 1);
                //pea[0].transform.position = player1_Slot[i + a * 4].position;//2 �÷��̾�� ��ġ
                player2.Add(pea[0]);
                pea.RemoveAt(0);
            }
        }
    }
    void FloorArray()
    {
        // �ٴڿ� �� ī���߿� ���� ��ī�尡 ������ ���� �ڸ��� ���´�. 
        for (int i = 0; i < floor.Count - 1; i++)
        {
            Card cardi = floor[i].GetComponent<Card>();
            for (int j = floor.Count-1; j > i ; j--)
            {
                Card cardj = floor[j].GetComponent<Card>();
                if (cardi.moon == cardj.moon)//���� ���̸� 
                {
                    for (int k = 0; k < floor_Slot.Count; k++)
                    {
                        Vector3 pos = floor[j].transform.position;
                        //floor[j]�� ��ġ�� floor_Slot�� ���� �Ǿ��ִٸ�
                        if (pos == floor_Slot[k].position)
                        {
                            //floor[j]�� ���� ��ġ�� ��ٴ� ����Ʈ�� �����ϰ� �ٴ� ��ġ ����Ʈ���� �A��.  
                            Empty_floor_Slot.Add(floor_Slot[k]);
                            floor_Slot.Remove(floor_Slot[k]);
                            break;
                        }
                    }
                    //�� ������Ʈ�� ������ ���� ������Ʈ ��ġ���� ���� ���� �д�.
                    floor[j].transform.position = floor[i].transform.position + new Vector3(0.2f, 0, 0);
                    print(j + "��" + cardi.moon + "������");
                }
            }
        }
    }


    void PlayerArray(int index)
    {
        List<GameObject> player = new List<GameObject>();
        List<Transform> player_slot = new List<Transform>();
        if (index == 0)
        {
            player = player1;
            player_slot = player1_Slot;
        }
        else if (index == 1)
        {
            player = player2;
            player_slot = player2_Slot;
        }

        //�÷��̾��� ���а��ӿ�����Ʈ�� ������� �����Ѵ�. 
        for (int i = 0; i < player.Count; i++)
        {
            for (int j =0; j <player.Count-i-1 ; j++)
            {
                if (player[j].GetComponent<Card>().moon > player[j + 1].GetComponent<Card>().moon)
                {
                    GameObject phand = player[j];
                    player[j] = player[j + 1];
                    player[j + 1] = phand;
                }
            }
        }
        //���ĵ� ���̸� 1��1�������� �ڸ��� �д�.
        for (int i = 0; i < player.Count; i++)
        {
            iTween.MoveTo(player[i], player_slot[i].position, 1);
        }
    }
    public bool Same(Card c)
    {
        for (int i = 0; i < floor.Count; i++)
        {
            if (c.moon == floor[i].GetComponent<Card>().moon)
            {
                return true;
            }
        }
        return false;
    }

    private void Selected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.transform.gameObject;
                if (player1.Contains(obj))
                {
                    PlayerArray(0);
                    List<GameObject> samehand = player1.FindAll(a => a.GetComponent<Card>().Same(
                       obj.GetComponent<Card>().moon));

                    if (samehand.Count == 1 || samehand.Count == 2)
                    {
                        List<GameObject> samefloor1card = GoStopRule.instance.LiST_SameFloor(obj);
                        GoStopRule.instance.Turnplayer1(samefloor1card, obj, pea[0]);
                        //print("1�÷��̾�" + GoStopRule.instance.p1_Score + "��");
                        //GO_STOP.instance.TurnOver(0,player2);
                    }
                    else if (samehand.Count == 3)
                    {
                        //�ٴڿ� ī�尡������ ��ź�̱� / ������ ����̾� ����  
                        List<GameObject> samefloor = GoStopRule.instance.LiST_SameFloor(obj);
                        if (samefloor.Count != 0)//��ź
                        {
                            //��ź 
                            print("��ź�̿�~");
                            //3�� �ѹ��� �ٳ���. ��ź �Լ�. 
                             GoStopRule.instance.Boming(samehand, samefloor[0], 0, pea[0]);
                            for (int i = 0; i < 2; i++)
                            {
                                GameObject back = Instantiate(back_card);
                                back.transform.position = player1_Slot[10+i].position;
                                player1.Add(back);
                            }
                        }
                        else//���
                        {
                            //��� �̺�Ʈ �����ְ� 
                            print("�������");
                            //���õ� ī������ ���� ���� ����
                            GoStopRule.instance.Turnplayer1(samefloor, obj, pea[0]);
                        }
                    }
                    else if (samehand.Count == 0 && obj.GetComponent<Card>().moon == 13)
                    {
                        GoStopRule.instance.Boming(obj, pea[0], 0);
                        Destroy(obj);
                    }
                    //GO_STOP.instance.TurnOver(0, player2);
                    print("1�÷��̾�" + GoStopRule.instance.p1_Score + "��");
                }
                if (player2.Contains(obj))
                {
                    PlayerArray(1);
                    List<GameObject> samehand = player2.FindAll(a => a.GetComponent<Card>().Same(
                       obj.GetComponent<Card>().moon));

                    if (samehand.Count == 1 || samehand.Count == 2)
                    {
                        List<GameObject> samefloor2card = GoStopRule.instance.LiST_SameFloor(obj);
                        GoStopRule.instance.Turnplayer2(samefloor2card, obj, pea[0]);
                        //print("2�÷��̾�" + GoStopRule.instance.p2_Score + "��");

                    }
                    else if (samehand.Count == 3)
                    {
                        //�ٴڿ� ī�尡������ ��ź�̱� / ������ ����̾� ����  
                        List<GameObject> samefloor = GoStopRule.instance.LiST_SameFloor(obj);
                        if (samefloor.Count != 0)//��ź
                        {
                            //��ź 
                            print("��ź�̿�~");
                            //3�� �ѹ��� �ٳ���. ��ź �Լ�. 
                            GoStopRule.instance.Boming(samehand, samefloor[0], 1, pea[0]);
                            for (int i = 0; i < 2; i++)
                            {
                                GameObject back = Instantiate(back_card);
                                back.transform.position = player2_Slot[10 + i].position;
                            }
                        }
                        else//���
                        {
                            int x = samehand[0].GetComponent<Card>().moon;
                            //��� �̺�Ʈ �����ְ� 
                            print(x + "�������");
                            //���õ� ī������ ���� ���� ����
                            GoStopRule.instance.Turnplayer2(samefloor, obj, pea[0]);
                        }
                    }
                    else if (samehand.Count == 0 && obj.GetComponent<Card>().moon == 13)
                    {
                        GoStopRule.instance.Boming(obj, pea[0], 1);
                        Destroy(obj);
                    }
                    //GO_STOP.instance.TurnOver(1, player1);
                    //print("2�÷��̾�" + GoStopRule.instance.p1_Score + "��");
                }
            }
        }
    }
    
}

