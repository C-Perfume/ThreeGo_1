using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoStopRule : MonoBehaviour
{
    public static GoStopRule instance;
    Vector3 addpos = new Vector3(0.05f,0,0);
    public List<GameObject> floor;
    public List<GameObject> pea;
    //public List<GameObject> player1;

    public List<GameObject> playerhand = new List<GameObject>();//�����
    public List<GameObject> player1hand;
    public List<GameObject> player2hand;

    public List<GameObject> player_score = new List<GameObject>();//�����
    public List<GameObject> player1_score;
    public List<GameObject> player2_score;

    public int p1_Score;
    public int p2_Score;

    public List<Transform> floor_slot;
    public List<Transform> empty_floor_slot;

    public List<Transform> player_scorebord = new List<Transform>();//������ ����
    public List<Transform> player1_scorebord;
    public List<Transform> player2_scorebord;

    public List<int> p1_toilet;
    public List<int> p2_toilet;

    public GameObject chosen;


    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        //GameObject����Ʈ
        floor = CardList.instance.floor;
        pea = CardList.instance.pea;
        player1hand= CardList.instance.player1;
        player1_score = CardList.instance.Player1_Score;
        player2hand= CardList.instance.player2;
        player2_score = CardList.instance.Player2_Score;
        //Transform����Ʈ
        floor_slot = CardList.instance.floor_Slot;
        empty_floor_slot = CardList.instance.Empty_floor_Slot;
        player1_scorebord = CardList.instance.player1_ScoreBord;
        player2_scorebord = CardList.instance.player2_ScoreBord;
    }

    /*
    //������ ī��Ʈ ���� ������ 
    //���߿��� ����Ʈ�� �����ϸ� ���ڴ� ����Ʈ ���;�(���� ī��). 
    public int Count_SameFloor(GameObject obj)
    {
        int Count = 0;
        for (int i = 0; i < floor.Count; i++)
        {
            if (floor[i] == obj)
            {
                Count++;
            }
        }
        return Count;
    }*/


    public List<GameObject> LiST_SameFloor(GameObject obj)
    {
        List<GameObject> samefloor = new List<GameObject>();
        for (int i = 0; i < floor.Count; i++)
        { 
            if (floor[i].GetComponent<Card>().moon == obj.GetComponent<Card>().moon)
            {
                samefloor.Add(floor[i]);
            }
        }
        return samefloor;
    }

    //ī�� ��ġ �̵� �Լ� 
     void Move_to_pos(GameObject card ,Vector3 pos)
     { 
        iTween.MoveTo(card,pos,0.5f);
        SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_jjak);
        /*
        iTween.MoveTo(card,iTween.Hash(
            "delay",0.2f,
            "position",pos,
            "time",1,
            "easetype",iTween.EaseType.easeOutBack));
        */
     }
    public void Move_to_Emptyfloor(GameObject card)
    {
        iTween.MoveTo(card, iTween.Hash(
            "delay", 0.2f,
            "position", empty_floor_slot[0].position,
            "time", 0.5f,
            "easetype", iTween.EaseType.easeOutBack));
        SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_tuk);
        floor_slot.Add(empty_floor_slot[0]);
        empty_floor_slot.RemoveAt(0);
    }

    public void Add_emptyfloor(GameObject obj)
    {
        //�ٴڿ��� ���������� �̵��� ģ���� �ִٸ� --�������� �Լ� ��������� 
        for (int i = 0; i < floor_slot.Count; i++)
        {
            //�ٴ�ģ���� �ڸ� �� ������ �־����� Ȯ�� �ϰ� 
            if (obj.transform.position == floor_slot[i].position)
            {
                //�׷��ٸ� ������ �ִ� �ڸ��� ���ڸ��� �ٲ��ش�.
                empty_floor_slot.Add(floor_slot[i]);
                floor_slot.RemoveAt(i);
            }

        }
        //print("���� �ڸ��� ������ ����");
    }
    //����Ʈ �̵� �Լ�
    public void Move_to_List(GameObject card, List<GameObject> target,List<GameObject> origin)
    {
        //������ ����Ʈ���� �ٲ︮��Ʈ�� �ٲ� �ش�.
        target.Add(card);
        origin.Remove(card);
    }
    public void Move_to_List(GameObject card,List<GameObject> origin,int a)
    {
        if(a == 0)//1�� �÷��̾� �ϋ�
        {
            player_scorebord = player1_scorebord;
            player_score = player1_score;
            
        }
        else if (a == 1)
        {
            player_scorebord = player2_scorebord;
            player_score = player2_score;
        }

        if (origin == floor)
        {
            for (int i = 0; i < floor_slot.Count; i++)
            {
                //�ٴ�ģ���� �ڸ� �� ������ �־����� Ȯ�� �ϰ� 
                if (card.transform.position == floor_slot[i].position)
                {
                    //�׷��ٸ� ������ �ִ� �ڸ��� ���ڸ��� �ٲ��ش�.
                    empty_floor_slot.Add(floor_slot[i]);
                    floor_slot.RemoveAt(i);
                }

            }
        }
        //������ ����Ʈ���� �ٲ︮��Ʈ�� �ٲ� �ش�.
        player_score.Add(card);
        origin.Remove(card);

        S_Calculator(player_score, card, a);
    }
    //���� ����Ʈ�� ���� �� ������Ʈ�� �й��ϱ�
    public List<GameObject> Division_Score(List<GameObject> scorelist,Card.Card_Type type)
    {
        List<GameObject> ScoreType = new List<GameObject>(); 
        for (int i = 0; i < scorelist.Count; i++)
        {
            Card.Card_Type t = scorelist[i].GetComponent<Card>().type;
            if (t == type)
            {
                ScoreType.Add(scorelist[i]);
            }
        }
        return ScoreType;
   
    }

    public void Take_Pee(List<GameObject> otherList,int want,int a)
    {
        List<GameObject> compare = new List<GameObject>();
        //� �̺�Ʈ�� ������ -- ���� ��� 
        //���� ���� �Ǹ� �����´�. 
        compare = otherList.FindAll(obj => obj.GetComponent<Card>().type == Card.Card_Type.PEE);
        //������ �ǰ� �ִ� �� Ȯ�� �ϰ� 
        if (compare.Count == 0)
        {
            //������ �� �������� �� 
            return;
        }
        else
        {
            print("�ǰ���������~");
            List<GameObject> twopee = new List<GameObject>();
            twopee = compare.FindAll(obj => obj.GetComponent<Card>().state == Card.CARD_STATE.TWO_PEE);
            //������ ���� ���ǰ� �ִ��� Ȯ�� �ϰ� 
            if (twopee.Count != 0)
            {
                if (twopee.Count == compare.Count || want == 2)//�ǰ� ��� �����ϋ�
                {
                    // ���Ǹ� �ִٸ� ���ǰ������� 
                    // �� �ΰ� ���������� ���ǰ� ������ 
                    //���ǰ�������
                    Move_to_List(twopee[0], otherList, a);
                    return;
                }

                //�׳��� ��������
                for (int i = 0; i < twopee.Count; i++)
                {
                    compare.Remove(twopee[i]);
                }
                Move_to_List(compare[0],otherList, a);
                return;
            }
            else
            {
                for (int i = 0; i < twopee.Count; i++)
                {
                        compare.Remove(twopee[i]);
                }

                if (want == 2)
                {
                   //���� ���������� �Ǿտ� ������� �� �Ǿտ� ���廩��
                   Move_to_List(compare[0], otherList, a);
                   Move_to_List(compare[0], otherList, a);
                }
                else
                { 
                    // �׳� �� ��������. 
                   Move_to_List(compare[0], otherList, a);
                }
                return;
            }
        }
    }

    //���� ����Ʈ�� ���� ����ϱ�. 
    public void S_Calculator(List<GameObject> scorelist ,GameObject addobj,int a)
    {
        List<GameObject> GangList =  Division_Score( scorelist,Card.Card_Type.KWANG );
        List<GameObject> TeeList = Division_Score( scorelist,Card.Card_Type.TEE);
        List<GameObject> YealList = Division_Score( scorelist,Card.Card_Type.YEOL);
        List<GameObject> PeeList = Division_Score( scorelist,Card.Card_Type.PEE);

        int running_score=0;
        int gang_score=0;
        int tee_score=0;
        int yeal_score=0;
        int pee_score=0;

        //������ ���
        { 
        switch (GangList.Count)
        {
            case 3:
                gang_score = 3;//�� 3���� 3��, ������ 
                for (int i = 0; i < 3; i++)
                {
                    if (GangList[i].GetComponent<Card>().name == "Pea (45)")
                    {
                        gang_score = 2;//�� ���Ե� 3���� 2��
                        break;               
                    }
                }
                break;
            case 4:
                gang_score = 4;
                break;
            case 5:
                gang_score = 15;
                break;
            default:
                gang_score = 0 ;
                break;
        }
            if (gang_score != 0) print("��" + gang_score + "��~");

        }

        //������ ���
        {
            // ������ �ϴ� 5�� �̻� ���� ���� �̰� �ܿϼ��� ������ 3���� �߰�. 
         if (TeeList.Count >= 5) tee_score = TeeList.Count - 4;
         else tee_score = 0;

            int cheong = 0;
            int hong = 0;
            int cho = 0;
            //������ ���� �������� ����
            for (int i = 0; i < TeeList.Count; i++)
            {
                int state = 0;
                state =  (int)TeeList[i].GetComponent<Card>().state;
                //û�� 3 //ȫ�� 4//�ʴ� 5
                switch (state)
                {
                    case 0:
                        break;
                    case 3: 
                        cheong++;
                        break;
                    case 4:
                        hong++;
                        break;
                    case 5:
                        cho++;
                        break;
                    default:
                        break;
                }
            }
            //û���� 3���̸� 3�� �߰� 
            if (cheong == 3)
            {
                tee_score += 3;
                EventManager.instance.ChoungEFT(a);
            }
            //ȫ���� 3���̸� 3�� �߰� 
            if (hong == 3)
            { 
                tee_score += 3;
                EventManager.instance.HongEFT(a);
            }
            //�ʴ��� 3���̸� 3�� �߰�
            if (cho == 3)
            {
               tee_score += 3;
                EventManager.instance.ChoEFT(a);
            }

        }

        //���� ���� ���. 
        {
            //���� 5�夷 �̻���� 1���� 
            if (YealList.Count > 4)
            {
                yeal_score = YealList.Count - 4;
            }
            else
            { 
                yeal_score = 0; 
            }
            
            int godori = 0;
            for (int i = 0; i < YealList.Count; i++)
            {
                if (Card.CARD_STATE.GODORI == YealList[i].GetComponent<Card>().state)
                {
                    godori++;
                }
            }
            if (godori == 3)
            { 
                yeal_score += 5; //���� 3���̸� 5��
                EventManager.instance.GodoriEFT(a);
            }
        }

        //�� ���� ���
        {
            int twopee = 0;
            for (int i = 0; i < PeeList.Count; i++)
            {
                if (Card.CARD_STATE.TWO_PEE == PeeList[i].GetComponent<Card>().state)
                {
                    twopee++;
                }
            }
            //pee_score += twopee;
            
            if (PeeList.Count + twopee >= 10)
            { 
                pee_score = PeeList.Count - 9;
                pee_score += twopee;
                //print("�� " + pee_score + "��");
            }
            else pee_score = 0;
        }

        //���� ���� �ջ�. 
        {
            running_score += gang_score;
            running_score += tee_score;
            running_score += yeal_score;
            running_score += pee_score;
            //�߰� ������ �߰� ����. 
        }

        //��ġ ��ġ  
        StartCoroutine( ScoreBord(a, addobj, GangList, TeeList, YealList, PeeList) );


        if (a == 0)
        {
            p1_Score = running_score;
        }
        else if (a == 1)
        {
            p2_Score = running_score;
        }

        ScoreUI.instance.Get_Count(running_score , a);
        GO_STOP.instance.ReciveScore(a,YealList.Count, pee_score, gang_score);
        //return running_score;
    }
    //������ ��ġ ��ġ �Լ� �����. 
    public IEnumerator ScoreBord(int a,GameObject obj, List<GameObject> gang, List<GameObject> tee, List<GameObject> yeal, List<GameObject> pee)
    {
        yield return new WaitForSeconds(1f);

        if (a == 0)//1�� �÷��̾� �ϋ�
        {
            player_scorebord = player1_scorebord;
        }
        else if (a == 1)
        {
            player_scorebord = player2_scorebord;
        }

        if (gang.Contains(obj))
        {
            Move_to_pos(obj, player_scorebord[0].position + addpos * (gang.Count-1));
        }
        else if (tee.Contains(obj))
        {
            Move_to_pos(obj, player_scorebord[1].position + addpos * (tee.Count - 1));
        }
        else if (yeal.Contains(obj))
        {
            Move_to_pos(obj, player_scorebord[2].position + addpos * (yeal.Count - 1));
        }
        else if (pee.Contains(obj))
        {
            Move_to_pos(obj, player_scorebord[3].position + addpos * (pee.Count - 1));
        }
    }


    //������ ī�尡 ��ī���� �̵��� �̺�Ʈ �߻� �Լ���. 
    /// <summary>
    /// �÷��̾ ��ī��� �ٴ�ī���� �� �� ���� ��ī�� �� ���� ��ģ�� �� ī���� �����Ӱ� ����Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="samefloorCard"></�÷��̾ �� ī��� ���� ���� �ٴ�ī�� ����Ʈ param>
    /// <param name="player"></�÷��̾ ��ī��param>
    /// <param name="deck"></������ �� ī��param>
    public void Turnplayer1(List<GameObject> samefloorCard, GameObject player, GameObject deck)
    {
        StartCoroutine(Turn_Result(samefloorCard, player, deck,0));
    }
    public void Turnplayer2(List<GameObject> samefloorCard, GameObject player, GameObject deck)
    {
        StartCoroutine(Turn_Result(samefloorCard, player, deck,1));
    }

    public void Boming(GameObject back ,GameObject deck,int a)
    {
        Move_to_Emptyfloor(back);
        List<GameObject> otherscore = new List<GameObject>();
        if (a == 0)
        {
            otherscore = player2_score;
        }
        else if (a == 1)
        {
            otherscore = player1_score;
        }

        StartCoroutine(DeckTurn(deck, otherscore, a));

    }
    public void Boming(List<GameObject> bomb,GameObject same,int a, GameObject deck)
    {
        List<GameObject> otherscore = new List<GameObject>();
        if (a == 0)
        {
            otherscore = player2_score;
        }
        else if (a == 1)
        {
            otherscore = player1_score;
        }
        StartCoroutine( bombTurn(bomb, same, a,deck));
        GO_STOP.instance.TurnOver(0, otherscore);
    }


    bool Is_GukJin(GameObject a, GameObject b)
    {
        if (a.GetComponent<Card>().state == Card.CARD_STATE.KOOKJIN || 
            b.GetComponent<Card>().state == Card.CARD_STATE.KOOKJIN)
        {
            //print("������ ���");
            return true;
        }
        return false;
    }
    public int WhosPooping(int a,GameObject poop)
    {
        List<int> toilet = new List<int>();
        if (a == 0)
        {
            toilet = p1_toilet;
        }
        else if (a == 1)
        {
            toilet = p2_toilet;
        }

        for (int i = 0; i < toilet.Count; i++)
        {
            if (poop.GetComponent<Card>().moon == toilet[i])
            {
                print("���� ����!������!");
                return 2;
            }
        }
        return 1;
    }

    IEnumerator bombTurn (List<GameObject> bomb,GameObject same, int a,GameObject deck)
    {
        List<GameObject> otherscore = new List<GameObject>();
        if (a == 0)
        {
            player_score = player1_score;
            playerhand = player1hand;
            otherscore = player2_score;
        }
        else if (a == 1)
        {
            player_score = player2_score;
            playerhand = player2hand;
            otherscore = player1_score;
        }


        Move_to_pos(bomb[0], same.transform.position + addpos);
        yield return new WaitForSeconds(1);
        Move_to_pos(bomb[1], bomb[0].transform.position + addpos);
        yield return new WaitForSeconds(1);
        Move_to_pos(bomb[2], bomb[1].transform.position + addpos);
        //��ź�̿�
        print("��ź�̿�~!");
        yield return new WaitForSeconds(1);
        

       StartCoroutine( DeckTurn(deck, otherscore, a));
        //ī�� ��������.
        Move_to_List(bomb[0], playerhand, a);
        Move_to_List(bomb[1], playerhand, a);
        Move_to_List(bomb[2], playerhand, a);
        Move_to_List(same, floor, a);
        yield return new WaitForSeconds(1);
        //�ߖP�����
        Take_Pee(otherscore, 1, a);
        if (Is_GukJin(bomb[0], bomb[1]) || Is_GukJin(bomb[2], same)) 
        {
            yield return new WaitForSeconds(1.5f);
            //ChoiceCard.instance.gukjin_selec = true;
            ChoiceCard.instance.Ques_Gukjin(a);
            //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
            //ChoiceCard.instance.Ques_Gukjin(1);
        }

        GO_STOP.instance.TurnOver(0, otherscore);
    }

    public IEnumerator DeckTurn(GameObject deck,List<GameObject> other_score,int index)
    {
        //��Ÿ��� �ٴ�ī�� �� 
        List<GameObject> decksamecard = LiST_SameFloor(deck);
        //�ٴ��� ���� ī�� ����� ���� �ൿ
        switch (decksamecard.Count)
        {
            case 0:
                // ��ī�� ��ġ �ٴ��� ��������� �̵� 
                Move_to_Emptyfloor(deck);
                // ��Ÿ�� �ٴ�ī�� ����Ʈ�� �̵� , 
                Move_to_List(deck, floor, pea);
                break;
            case 1:
                //��ī�� ��ġ ���� �� ī�� ��ġ�� �̵� �̵�1
                Move_to_pos(deck, decksamecard[0].transform.position + addpos);
                yield return new WaitForSeconds(1);
                // �Ѵ� ����Ʈ ���� ����Ʈ�� ����
                Move_to_List(deck, pea, index);
                Move_to_List(decksamecard[0], floor, index);
                if (Is_GukJin(deck, decksamecard[0])) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);
                    //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                    //ChoiceCard.instance.Ques_Gukjin(1);
                }
                 
                
                break;
            case 2:
                //��ī�� ��ġ ���� ���� ��ġ�� �̵� 
                Move_to_pos(deck, decksamecard[1].transform.position + addpos);
                //==   //�ٴ�ī���� �ΰ� �� ���� â �ߵ� //�ϴ� ����ī��� ��� 2���� ī���ΰɷ�
                //EventManager.instance.DeckEvent(EventManager.Play_Event.choice);
                chosen = decksamecard[1];
                ChoiceCard.instance.Sand_card(decksamecard[0], decksamecard[1]);
                yield return new WaitForSeconds(3);//�� �ð� 3�� 3�ʾȿ� �Ȱ��� �׳� [1]�� ���ش�.
                ChoiceCard.instance.selec = false;
                //���� �ȵ� ī��� �ٴ�ī��� ��ġ �̵� ����߰���. 

                // ���õ� �ٴ�ī��� ��ī�� ����Ʈ �̵�
                Move_to_List(deck, pea, index);
                Move_to_List(chosen, floor, index);
                if (Is_GukJin(deck, chosen)) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);

                }
                break;
            case 3:
                //��ī�带 ������ �ٴ�ī��� ��ġ���� 
                Move_to_pos(deck, decksamecard[2].transform.position + addpos);
                //==  //�˸Ա� �̺�Ʈ �߻� 
                //EventManager.instance.DeckEvent(EventManager.Play_Event.eatting_ddong);
                print("�� ���� ������������ ");
                SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_eatpack);
                yield return new WaitForSeconds(1);
                //�ٴ�ī�� 3��� ��ī�� ���� ���������� �����Ͽ� �̵�
                //����Ʈ �̵�
                Move_to_List(deck, pea, index);
                Move_to_List(decksamecard[0], floor, index);
                Move_to_List(decksamecard[1], floor, index);
                Move_to_List(decksamecard[2], floor, index);
                yield return new WaitForSeconds(1);
                //�ЖP����� 
                Take_Pee(other_score, WhosPooping(index, deck), index);

                if (Is_GukJin(deck, decksamecard[0]) || Is_GukJin(decksamecard[1], decksamecard[2])) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);
                    //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                    //ChoiceCard.instance.Ques_Gukjin(1);
                }
                break;
        }
        yield return new WaitForSeconds(1);
    }

    /// <summary>
    /// ���� ������ �ൿ�� ���� ���ִ� �Լ� 
    /// </summary>
    /// <param name="samefloorCard"></param>
    /// <param name="player"></param>
    /// <param name="deck"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public IEnumerator Turn_Result(List<GameObject> samefloorCard, GameObject player,GameObject deck,int index)
    {
        List<int> toilet = new List<int>();
        List<GameObject> other_score = new List<GameObject>();
        if (index == 0)
        {
            playerhand = player1hand;
            player_score = player1_score;
            other_score = player2_score;
            toilet = p1_toilet;
        }
        else if (index == 1)
        {
            playerhand = player2hand;
            player_score = player2_score;
            other_score = player1_score;
            toilet = p2_toilet;
        }

        //�÷��̾ ��ī�尡 1���ϋ�.. 
        switch (samefloorCard.Count)
        {
            case 0:/// �ٴڿ� �´� �а� ������ 
                //�÷��̾� ī�� ��ġ �ٴ��� ����ġ�� �̵� /��ġ�� �̵�
                Move_to_Emptyfloor(player);
                yield return new WaitForSeconds(1);
                if (player.GetComponent<Card>().moon == deck.GetComponent<Card>().moon)
                {
                    //��ī�� ��ġ ���� �� ī�� ���� ������.
                    Move_to_pos(deck, player.transform.position + addpos);
                    //==//     //���̺�Ʈ �߻�
                    //EventManager.instance.PlayerEvent(EventManager.Play_Event.kiss);
                    print("��~~~");//���߿� �ڸ������ϱ� 
                    SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_kiss);
                    yield return new WaitForSeconds(1);
                    //��ī�� ��������Ʈ�� �̵� �� �� �ڸ��� ���еǾ� �̵� 
                    Move_to_List(player, playerhand,index);
                    Move_to_List(deck, pea,index);
                    //�ЖP����� 
                    Take_Pee(other_score, 1, index);
                    if (Is_GukJin(deck, player)) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }
                else
                {                    
                    StartCoroutine(DeckTurn(deck, other_score, index));
                    //�÷��̾ ��ī��� �ٴ� ī�帮��Ʈ�� �߰��ϰ� ��ī�忡�� �����.-����Ʈ �̵� 
                    Move_to_List(player, floor, playerhand);
                }
                break;
            case 1: ///�ٴڿ� �´� ī�尡 �ϳ��� ��
                //��ġ�� �ٴ��� ���� ī������ �̵� 
                Move_to_pos(player, samefloorCard[0].transform.position + addpos);
                yield return new WaitForSeconds(1);
                if (player.GetComponent<Card>().moon == deck.GetComponent<Card>().moon)
                {
                    //��ī�� ��ġ ��ī�� ���� �̵� 
                    Move_to_pos(deck, player.transform.position + addpos);
                    //==  //�˽α� �̺�Ʈ �߻�
                    SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_pack);
                    //EventManager.instance.DeckEvent(EventManager.Play_Event.ddong);
                    print("�մ�..");
                    toilet.Add(deck.GetComponent<Card>().moon);
                    //��ī�� ��ī�� �� ��  �ٴ�ī��� ����Ʈ �߰�
                    Move_to_List(player, floor, playerhand);
                    Move_to_List(deck, floor, pea);
                }
                else
                {                   
                    StartCoroutine(DeckTurn(deck, other_score, index));
                    // ��ī�� ���� �ٴ�ī�� ����Ʈ ���� ����Ʈ�� ����
                    Move_to_List(player,  playerhand,index);
                    Move_to_List(samefloorCard[0], floor,index);
                    if (Is_GukJin( player,samefloorCard[0])) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }

                 break;
            case 2: //�ٴڿ� �´� ī�尡 2�� ��
                //��ġ�� �ٴ��� ���� ī�� �� �ణ ������ �̵� 
                Move_to_pos(player, samefloorCard[1].transform.position + addpos);
                yield return new WaitForSeconds(1);
                if (player.GetComponent<Card>().moon == deck.GetComponent<Card>().moon)
                {
                    //��ī�� ��ī������ ��ġ ���� 
                    Move_to_pos(deck, player.transform.position + addpos);
                    //==  //���� �̺�Ʈ �߻� 
                    //EventManager.instance.PlayerEvent(EventManager.Play_Event.ddadack);
                    print("�̰� �������̾�~ ����! ");
                    SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_ddadak);
                    //���� ��ī�� �ٴ��� ī�� 2�� ��ī�� ��� ���������� ��ġ ���� �̵�
                    yield return new WaitForSeconds(1);
                    //��ī�� �ٴ� 2�� ��ī�� ��� ������ ����Ʈ�� �̵�
                    Move_to_List(player,playerhand,index);
                    Move_to_List(deck,  pea,index);
                    Move_to_List(samefloorCard[0],  floor,index);
                    Move_to_List(samefloorCard[1],  floor,index);
                    yield return new WaitForSeconds(1);
                    //�ЖP����� 
                    Take_Pee(other_score, 1, index);

                    if (Is_GukJin(player, deck) || Is_GukJin(samefloorCard[0], samefloorCard[1])) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }
                else
                {
                    StartCoroutine(DeckTurn(deck, other_score, index));
                    // �ٴ�ī�� ������ �� ����â Ȱ��ȭ [�ϴ� 1������]
                    //EventManager.instance.PlayerEvent(EventManager.Play_Event.choice);
                    print("���� �����");
                    chosen = samefloorCard[1];
                    ChoiceCard.instance.Sand_card(samefloorCard[0], samefloorCard[1]);
                    yield return new WaitForSeconds(3);
                    ChoiceCard.instance.selec = false;
                    // ����ī��� �÷��̾� ī�� ���� ����Ʈ�� �̵�
                    Move_to_List(player,playerhand,index);
                    Move_to_List(chosen, floor,index);

                    if (Is_GukJin(player, chosen)) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }
                break;
            case 3: //�ٴڿ� �´� ī�尡 3�� ��
                //��ġ�� �ٴ��� ���� ī������ �̵� 
                Move_to_pos(player, samefloorCard[2].transform.position + addpos);
                //== //�˸Ա� �̺�Ʈ �߻�
                //EventManager.instance.PlayerEvent(EventManager.Play_Event.eatting_ddong);
                print("�̶� �������̿�");
                SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_eatpack);
                yield return new WaitForSeconds(1);
                StartCoroutine(DeckTurn(deck, other_score, index));
                //�÷��̾� 3�� ī��� ��ī�� ����Ʈ ���� ����Ʈ�� �̵�
                Move_to_List(player, playerhand,index);
                Move_to_List(samefloorCard[0],  floor,index);
                Move_to_List(samefloorCard[1], floor,index);
                Move_to_List(samefloorCard[2],  floor,index);
                yield return new WaitForSeconds(1);
                //�ЖP����� 
                Take_Pee(other_score, WhosPooping(index,player), index);

                if (Is_GukJin(player, samefloorCard[0]) ||  Is_GukJin(samefloorCard[1], samefloorCard[2])) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);
                    //yield return new WaitForSeconds(3);//3�ʾȿ� ���þ��ϸ� �׳� ���� 
                    //ChoiceCard.instance.Ques_Gukjin(1);
                }

                break;
            default:
                break;
        }


        { 
        if (index == 0)
        {
            p1_toilet = toilet;
            player1hand = playerhand;
            player1_score = player_score;
            player2_score = other_score;
        }
        else if (index == 1)
        {
            p2_toilet = toilet;
            player2hand = playerhand;
            player2_score = player_score;
            player1_score = other_score;
        }
        GO_STOP.instance.TurnOver(0, other_score);
        }//���� ��� ����
    }
}
