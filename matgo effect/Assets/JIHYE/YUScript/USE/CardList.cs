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

    public List<GameObject> player1 = new List<GameObject>();//손에 들고있는패
    public List<GameObject> player2 = new List<GameObject>();
    public List<GameObject> floor = new List<GameObject>();//바닥에 깔린패
                                                           //바닥에 깔렸는데 같은 패라서 위에 겹쳐진 


    public List<GameObject> Player1_Score = new List<GameObject>();//접수판으로가져온패
    public List<GameObject> Player2_Score = new List<GameObject>();

    public List<Transform> floor_Slot = new List<Transform>(); //카드가 위치한 자리 
    public List<Transform> Empty_floor_Slot = new List<Transform>();

    public List<Transform> player1_Slot = new List<Transform>();//손의 위치
    public List<Transform> player2_Slot = new List<Transform>();

    public List<Transform> player1_ScoreBord = new List<Transform>();//점수판 위치 
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
        //카드 선택
        Selected();
        
    }


    //============================================

    IEnumerator Start_Setting()
    {
        Suff_Pea();//카드섞고
        Card_Share();//카드배치 동시에 
        yield return new WaitForSeconds(1);
        FloorArray();//바닥정렬
        PlayerArray(0);//플레이어1 정렬
        PlayerArray(1);//플레이어2 정렬
    }
    void Suff_Pea()
    {
        /*국진 확인용
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

        /* 폭탄 위한 설정  */
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
                //pea[0].transform.position = floorSlot[i + a * 4].pos; //이동시키고
                floor_Slot.Add(Empty_floor_Slot[0]);
                Empty_floor_Slot.RemoveAt(0);
                floor.Add(pea[0]);//바닥패에 추가
                pea.RemoveAt(0);
            }
            for (int i = 0; i < 5; i++)
            {

                Card c = pea[0].GetComponent<Card>();
                c.player_index = 0;
                iTween.MoveTo(pea[0], player1_Slot[ (a * 5) + i].position, 1);
                //pea[0].transform.position = player1_Slot[i + a * 4].position;//1 플레이어손 위치
                player1.Add(pea[0]);
                pea.RemoveAt(0);
            }
            for (int i = 0; i < 5; i++)
            {
                Card c = pea[0].GetComponent<Card>();
                c.player_index = 1;
                iTween.MoveTo(pea[0], player2_Slot[i + a * 5].position, 1);
                //pea[0].transform.position = player1_Slot[i + a * 4].position;//2 플레이어손 위치
                player2.Add(pea[0]);
                pea.RemoveAt(0);
            }
        }
    }
    void FloorArray()
    {
        // 바닥에 깔린 카드중에 같은 달카드가 있으면 같은 자리에 놓는다. 
        for (int i = 0; i < floor.Count - 1; i++)
        {
            Card cardi = floor[i].GetComponent<Card>();
            for (int j = floor.Count-1; j > i ; j--)
            {
                Card cardj = floor[j].GetComponent<Card>();
                if (cardi.moon == cardj.moon)//같은 달이면 
                {
                    for (int k = 0; k < floor_Slot.Count; k++)
                    {
                        Vector3 pos = floor[j].transform.position;
                        //floor[j]의 위치가 floor_Slot의 포함 되어있다면
                        if (pos == floor_Slot[k].position)
                        {
                            //floor[j]의 기존 위치는 빈바닥 리스트에 포함하고 바닥 위치 리스트에서 뺸다.  
                            Empty_floor_Slot.Add(floor_Slot[k]);
                            floor_Slot.Remove(floor_Slot[k]);
                            break;
                        }
                    }
                    //비교 오브젝트가 같으면 기준 오브젝트 위치보다 조금 옆에 둔다.
                    floor[j].transform.position = floor[i].transform.position + new Vector3(0.2f, 0, 0);
                    print(j + "는" + cardi.moon + "같은달");
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

        //플레이어의 손패게임오브젝트를 순서대로 정렬한다. 
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
        //정렬된 아이를 1대1대응으로 자리에 둔다.
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
                        //print("1플레이어" + GoStopRule.instance.p1_Score + "점");
                        //GO_STOP.instance.TurnOver(0,player2);
                    }
                    else if (samehand.Count == 3)
                    {
                        //바닥에 카드가있으면 폭탄이구 / 없으면 흠들이야 헤헷  
                        List<GameObject> samefloor = GoStopRule.instance.LiST_SameFloor(obj);
                        if (samefloor.Count != 0)//폭탄
                        {
                            //폭탄 
                            print("폭탄이요~");
                            //3장 한번에 다내기. 폭탄 함수. 
                             GoStopRule.instance.Boming(samehand, samefloor[0], 0, pea[0]);
                            for (int i = 0; i < 2; i++)
                            {
                                GameObject back = Instantiate(back_card);
                                back.transform.position = player1_Slot[10+i].position;
                                player1.Add(back);
                            }
                        }
                        else//흔들
                        {
                            //흔들 이벤트 보여주고 
                            print("흔들어써요오");
                            //선택됨 카드한장 내고 게임 진행
                            GoStopRule.instance.Turnplayer1(samefloor, obj, pea[0]);
                        }
                    }
                    else if (samehand.Count == 0 && obj.GetComponent<Card>().moon == 13)
                    {
                        GoStopRule.instance.Boming(obj, pea[0], 0);
                        Destroy(obj);
                    }
                    //GO_STOP.instance.TurnOver(0, player2);
                    print("1플레이어" + GoStopRule.instance.p1_Score + "점");
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
                        //print("2플레이어" + GoStopRule.instance.p2_Score + "점");

                    }
                    else if (samehand.Count == 3)
                    {
                        //바닥에 카드가있으면 폭탄이구 / 없으면 흠들이야 헤헷  
                        List<GameObject> samefloor = GoStopRule.instance.LiST_SameFloor(obj);
                        if (samefloor.Count != 0)//폭탄
                        {
                            //폭탄 
                            print("폭탄이요~");
                            //3장 한번에 다내기. 폭탄 함수. 
                            GoStopRule.instance.Boming(samehand, samefloor[0], 1, pea[0]);
                            for (int i = 0; i < 2; i++)
                            {
                                GameObject back = Instantiate(back_card);
                                back.transform.position = player2_Slot[10 + i].position;
                            }
                        }
                        else//흔들
                        {
                            int x = samehand[0].GetComponent<Card>().moon;
                            //흔들 이벤트 보여주고 
                            print(x + "흔들었어요");
                            //선택됨 카드한장 내고 게임 진행
                            GoStopRule.instance.Turnplayer2(samefloor, obj, pea[0]);
                        }
                    }
                    else if (samehand.Count == 0 && obj.GetComponent<Card>().moon == 13)
                    {
                        GoStopRule.instance.Boming(obj, pea[0], 1);
                        Destroy(obj);
                    }
                    //GO_STOP.instance.TurnOver(1, player1);
                    //print("2플레이어" + GoStopRule.instance.p1_Score + "점");
                }
            }
        }
    }
    
}

