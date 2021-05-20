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

    public List<GameObject> playerhand = new List<GameObject>();//빈공간
    public List<GameObject> player1hand;
    public List<GameObject> player2hand;

    public List<GameObject> player_score = new List<GameObject>();//빈공간
    public List<GameObject> player1_score;
    public List<GameObject> player2_score;

    public int p1_Score;
    public int p2_Score;

    public List<Transform> floor_slot;
    public List<Transform> empty_floor_slot;

    public List<Transform> player_scorebord = new List<Transform>();//빈점수 공간
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
        //GameObject리스트
        floor = CardList.instance.floor;
        pea = CardList.instance.pea;
        player1hand= CardList.instance.player1;
        player1_score = CardList.instance.Player1_Score;
        player2hand= CardList.instance.player2;
        player2_score = CardList.instance.Player2_Score;
        //Transform리스트
        floor_slot = CardList.instance.floor_Slot;
        empty_floor_slot = CardList.instance.Empty_floor_Slot;
        player1_scorebord = CardList.instance.player1_ScoreBord;
        player2_scorebord = CardList.instance.player2_ScoreBord;
    }

    /*
    //지금은 카운트 수만 세지만 
    //나중에는 리스트도 리턴하면 좋겠다 리스트 리터언(같은 카드). 
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

    //카드 위치 이동 함수 
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
        //바닥에서 점수판으로 이동한 친구가 있다면 --수동으로 함수 적어줘야함 
        for (int i = 0; i < floor_slot.Count; i++)
        {
            //바닥친구가 자리 를 가지고 있었는지 확인 하고 
            if (obj.transform.position == floor_slot[i].position)
            {
                //그렇다면 가지고 있던 자리를 빈자리로 바꿔준다.
                empty_floor_slot.Add(floor_slot[i]);
                floor_slot.RemoveAt(i);
            }

        }
        //print("나는 자리가 없었숴 ㅎㅎ");
    }
    //리스트 이동 함수
    public void Move_to_List(GameObject card, List<GameObject> target,List<GameObject> origin)
    {
        //원래의 리스트에서 바뀐리스트로 바꿔 준다.
        target.Add(card);
        origin.Remove(card);
    }
    public void Move_to_List(GameObject card,List<GameObject> origin,int a)
    {
        if(a == 0)//1번 플레이어 일떄
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
                //바닥친구가 자리 를 가지고 있었는지 확인 하고 
                if (card.transform.position == floor_slot[i].position)
                {
                    //그렇다면 가지고 있던 자리를 빈자리로 바꿔준다.
                    empty_floor_slot.Add(floor_slot[i]);
                    floor_slot.RemoveAt(i);
                }

            }
        }
        //원래의 리스트에서 바뀐리스트로 바꿔 준다.
        player_score.Add(card);
        origin.Remove(card);

        S_Calculator(player_score, card, a);
    }
    //점수 리스트를 각각 의 스테이트로 분배하기
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
        //어떤 이벤트가 있을떄 -- 직접 등록 
        //상대방 패의 피를 가져온다. 
        compare = otherList.FindAll(obj => obj.GetComponent<Card>().type == Card.Card_Type.PEE);
        //상대방이 피가 있는 지 확인 하고 
        if (compare.Count == 0)
        {
            //없으면 피 못가져감 ㅜ 
            return;
        }
        else
        {
            print("피가져갈꼐요~");
            List<GameObject> twopee = new List<GameObject>();
            twopee = compare.FindAll(obj => obj.GetComponent<Card>().state == Card.CARD_STATE.TWO_PEE);
            //상대방의 피중 쌍피가 있는지 확인 하고 
            if (twopee.Count != 0)
            {
                if (twopee.Count == compare.Count || want == 2)//피가 모두 쌍피일떄
                {
                    // 쌍피만 있다면 쌍피가져가기 
                    // 피 두개 가져갈껀대 쌍피가 있으면 
                    //쌍피가져가기
                    Move_to_List(twopee[0], otherList, a);
                    return;
                }

                //그냥피 가져가기
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
                   //두장 가져갈껀데 맨앞에 한장뺴고 또 맨앞에 한장빼기
                   Move_to_List(compare[0], otherList, a);
                   Move_to_List(compare[0], otherList, a);
                }
                else
                { 
                    // 그냥 피 가져가기. 
                   Move_to_List(compare[0], otherList, a);
                }
                return;
            }
        }
    }

    //점수 리스트로 점수 계산하기. 
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

        //광점수 계산
        { 
        switch (GangList.Count)
        {
            case 3:
                gang_score = 3;//광 3장은 3점, 하지만 
                for (int i = 0; i < 3; i++)
                {
                    if (GangList[i].GetComponent<Card>().name == "Pea (45)")
                    {
                        gang_score = 2;//비광 포함된 3장은 2점
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
            if (gang_score != 0) print("광" + gang_score + "점~");

        }

        //띠점수 계산
        {
            // 띠계수가 일단 5개 이상 부터 점수 이고 단완성이 있으면 3점씩 추가. 
         if (TeeList.Count >= 5) tee_score = TeeList.Count - 4;
         else tee_score = 0;

            int cheong = 0;
            int hong = 0;
            int cho = 0;
            //각각의 단이 몇장인지 세고
            for (int i = 0; i < TeeList.Count; i++)
            {
                int state = 0;
                state =  (int)TeeList[i].GetComponent<Card>().state;
                //청단 3 //홍단 4//초단 5
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
            //청단이 3장이면 3점 추가 
            if (cheong == 3)
            {
                tee_score += 3;
                EventManager.instance.ChoungEFT(a);
            }
            //홍단이 3장이면 3점 추가 
            if (hong == 3)
            { 
                tee_score += 3;
                EventManager.instance.HongEFT(a);
            }
            //초단이 3장이면 3점 추가
            if (cho == 3)
            {
               tee_score += 3;
                EventManager.instance.ChoEFT(a);
            }

        }

        //동물 점수 계산. 
        {
            //동물 5장ㅇ 이상부터 1점씩 
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
                yeal_score += 5; //고도리 3장이면 5점
                EventManager.instance.GodoriEFT(a);
            }
        }

        //피 점수 계산
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
                //print("피 " + pee_score + "점");
            }
            else pee_score = 0;
        }

        //최종 점수 합산. 
        {
            running_score += gang_score;
            running_score += tee_score;
            running_score += yeal_score;
            running_score += pee_score;
            //추가 고점수 추가 예정. 
        }

        //위치 배치  
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
    //점수판 위치 배치 함수 만들기. 
    public IEnumerator ScoreBord(int a,GameObject obj, List<GameObject> gang, List<GameObject> tee, List<GameObject> yeal, List<GameObject> pee)
    {
        yield return new WaitForSeconds(1f);

        if (a == 0)//1번 플레이어 일떄
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


    //선택한 카드가 덱카드의 이동과 이벤트 발생 함수임. 
    /// <summary>
    /// 플레이어가 낸카드와 바닥카드의 비교 후 다음 덱카드 비교 까지 마친후 각 카드의 움직임과 리스트를 정리하는 함수
    /// </summary>
    /// <param name="samefloorCard"></플레이어가 낸 카드와 같은 달의 바닥카드 리스트 param>
    /// <param name="player"></플레이어가 낸카드param>
    /// <param name="deck"></덱에서 깐 카드param>
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
            //print("국진이 골라");
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
                print("내가 쌌지!두장줘!");
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
        //폭탄이요
        print("폭탄이요~!");
        yield return new WaitForSeconds(1);
        

       StartCoroutine( DeckTurn(deck, otherscore, a));
        //카드 가져오고.
        Move_to_List(bomb[0], playerhand, a);
        Move_to_List(bomb[1], playerhand, a);
        Move_to_List(bomb[2], playerhand, a);
        Move_to_List(same, floor, a);
        yield return new WaitForSeconds(1);
        //삐뻇어오고
        Take_Pee(otherscore, 1, a);
        if (Is_GukJin(bomb[0], bomb[1]) || Is_GukJin(bomb[2], same)) 
        {
            yield return new WaitForSeconds(1.5f);
            //ChoiceCard.instance.gukjin_selec = true;
            ChoiceCard.instance.Ques_Gukjin(a);
            //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
            //ChoiceCard.instance.Ques_Gukjin(1);
        }

        GO_STOP.instance.TurnOver(0, otherscore);
    }

    public IEnumerator DeckTurn(GameObject deck,List<GameObject> other_score,int index)
    {
        //덱타드와 바닥카드 비교 
        List<GameObject> decksamecard = LiST_SameFloor(deck);
        //바닥의 같은 카드 장수에 따른 행동
        switch (decksamecard.Count)
        {
            case 0:
                // 덱카드 위치 바닥의 빈공간으로 이동 
                Move_to_Emptyfloor(deck);
                // 덱타드 바닥카드 리스트로 이동 , 
                Move_to_List(deck, floor, pea);
                break;
            case 1:
                //덱카드 위치 같은 달 카드 위치로 이동 이동1
                Move_to_pos(deck, decksamecard[0].transform.position + addpos);
                yield return new WaitForSeconds(1);
                // 둘다 리스트 점수 리스트로 변경
                Move_to_List(deck, pea, index);
                Move_to_List(decksamecard[0], floor, index);
                if (Is_GukJin(deck, decksamecard[0])) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);
                    //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
                    //ChoiceCard.instance.Ques_Gukjin(1);
                }
                 
                
                break;
            case 2:
                //덱카드 위치 같은 달의 위치로 이동 
                Move_to_pos(deck, decksamecard[1].transform.position + addpos);
                //==   //바닥카드중 두개 중 선택 창 발동 //일단 선택카드는 모두 2번쨰 카드인걸루
                //EventManager.instance.DeckEvent(EventManager.Play_Event.choice);
                chosen = decksamecard[1];
                ChoiceCard.instance.Sand_card(decksamecard[0], decksamecard[1]);
                yield return new WaitForSeconds(3);//고를 시간 3초 3초안에 안고르면 그냥 [1]로 해준다.
                ChoiceCard.instance.selec = false;
                //선택 안된 카드는 바닥카드로 위치 이동 해줘야겠음. 

                // 선택된 바닥카드와 덱카드 리스트 이동
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
                //덱카드를 같은달 바닥카드로 위치변경 
                Move_to_pos(deck, decksamecard[2].transform.position + addpos);
                //==  //똥먹기 이벤트 발생 
                //EventManager.instance.DeckEvent(EventManager.Play_Event.eatting_ddong);
                print("이 똥은 내가가져간다 ");
                SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_eatpack);
                yield return new WaitForSeconds(1);
                //바닥카드 3장과 덱카드 전부 점수판으로 구분하여 이동
                //리스트 이동
                Move_to_List(deck, pea, index);
                Move_to_List(decksamecard[0], floor, index);
                Move_to_List(decksamecard[1], floor, index);
                Move_to_List(decksamecard[2], floor, index);
                yield return new WaitForSeconds(1);
                //패뻇어오기 
                Take_Pee(other_score, WhosPooping(index, deck), index);

                if (Is_GukJin(deck, decksamecard[0]) || Is_GukJin(decksamecard[1], decksamecard[2])) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);
                    //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
                    //ChoiceCard.instance.Ques_Gukjin(1);
                }
                break;
        }
        yield return new WaitForSeconds(1);
    }

    /// <summary>
    /// 턴이 끝날때 행동을 정리 해주는 함수 
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

        //플레이어가 낸카드가 1장일떄.. 
        switch (samefloorCard.Count)
        {
            case 0:/// 바닥에 맞는 패가 없을때 
                //플레이어 카드 위치 바닥의 빈위치로 이동 /위치만 이동
                Move_to_Emptyfloor(player);
                yield return new WaitForSeconds(1);
                if (player.GetComponent<Card>().moon == deck.GetComponent<Card>().moon)
                {
                    //덱카드 위치 내가 낸 카드 위로 움직임.
                    Move_to_pos(deck, player.transform.position + addpos);
                    //==//     //쪽이벤트 발생
                    //EventManager.instance.PlayerEvent(EventManager.Play_Event.kiss);
                    print("쪽~~~");//나중에 자리선정하기 
                    SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_kiss);
                    yield return new WaitForSeconds(1);
                    //각카드 점수리스트로 이동 및 각 자리로 구분되어 이동 
                    Move_to_List(player, playerhand,index);
                    Move_to_List(deck, pea,index);
                    //패뻇어오기 
                    Take_Pee(other_score, 1, index);
                    if (Is_GukJin(deck, player)) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }
                else
                {                    
                    StartCoroutine(DeckTurn(deck, other_score, index));
                    //플레이어가 낸카드는 바닥 카드리스트에 추가하고 내카드에서 지운다.-리스트 이동 
                    Move_to_List(player, floor, playerhand);
                }
                break;
            case 1: ///바닥에 맞는 카드가 하나일 떄
                //위치를 바닥의 같은 카드위로 이동 
                Move_to_pos(player, samefloorCard[0].transform.position + addpos);
                yield return new WaitForSeconds(1);
                if (player.GetComponent<Card>().moon == deck.GetComponent<Card>().moon)
                {
                    //덱카드 위치 내카드 위로 이동 
                    Move_to_pos(deck, player.transform.position + addpos);
                    //==  //똥싸기 이벤트 발생
                    SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_pack);
                    //EventManager.instance.DeckEvent(EventManager.Play_Event.ddong);
                    print("쌌다..");
                    toilet.Add(deck.GetComponent<Card>().moon);
                    //내카드 덱카드 둘 다  바닥카드로 리스트 추가
                    Move_to_List(player, floor, playerhand);
                    Move_to_List(deck, floor, pea);
                }
                else
                {                   
                    StartCoroutine(DeckTurn(deck, other_score, index));
                    // 낸카드 맞은 바닥카드 리스트 점수 리스트로 변경
                    Move_to_List(player,  playerhand,index);
                    Move_to_List(samefloorCard[0], floor,index);
                    if (Is_GukJin( player,samefloorCard[0])) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }

                 break;
            case 2: //바닥에 맞는 카드가 2일 떄
                //위치를 바닥의 같은 카드 위 약간 옆으로 이동 
                Move_to_pos(player, samefloorCard[1].transform.position + addpos);
                yield return new WaitForSeconds(1);
                if (player.GetComponent<Card>().moon == deck.GetComponent<Card>().moon)
                {
                    //덱카드 내카드위로 위치 변경 
                    Move_to_pos(deck, player.transform.position + addpos);
                    //==  //따닥 이벤트 발생 
                    //EventManager.instance.PlayerEvent(EventManager.Play_Event.ddadack);
                    print("이게 무슨일이야~ 따닥! ");
                    SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_ddadak);
                    //내가 낸카드 바닥의 카드 2장 덱카드 모두 점수판으로 위치 구분 이동
                    yield return new WaitForSeconds(1);
                    //낸카드 바닥 2장 덱카드 모두 점수판 리스트로 이동
                    Move_to_List(player,playerhand,index);
                    Move_to_List(deck,  pea,index);
                    Move_to_List(samefloorCard[0],  floor,index);
                    Move_to_List(samefloorCard[1],  floor,index);
                    yield return new WaitForSeconds(1);
                    //패뻇어오기 
                    Take_Pee(other_score, 1, index);

                    if (Is_GukJin(player, deck) || Is_GukJin(samefloorCard[0], samefloorCard[1])) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }
                else
                {
                    StartCoroutine(DeckTurn(deck, other_score, index));
                    // 바닥카드 두장중 비교 선택창 활성화 [일단 1번선택]
                    //EventManager.instance.PlayerEvent(EventManager.Play_Event.choice);
                    print("낸거 골라유");
                    chosen = samefloorCard[1];
                    ChoiceCard.instance.Sand_card(samefloorCard[0], samefloorCard[1]);
                    yield return new WaitForSeconds(3);
                    ChoiceCard.instance.selec = false;
                    // 선택카드와 플레이어 카드 점수 리스트로 이동
                    Move_to_List(player,playerhand,index);
                    Move_to_List(chosen, floor,index);

                    if (Is_GukJin(player, chosen)) 
                    {
                        yield return new WaitForSeconds(1.5f);
                        //ChoiceCard.instance.gukjin_selec = true;
                        ChoiceCard.instance.Ques_Gukjin(index);
                        //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
                        //ChoiceCard.instance.Ques_Gukjin(1);
                    }
                }
                break;
            case 3: //바닥에 맞는 카드가 3일 떄
                //위치를 바닥의 같은 카드위로 이동 
                Move_to_pos(player, samefloorCard[2].transform.position + addpos);
                //== //똥먹기 이벤트 발생
                //EventManager.instance.PlayerEvent(EventManager.Play_Event.eatting_ddong);
                print("이똥 누구똥이여");
                SoundManager.instance.PlayEFT(SoundManager.EFG_TYPE.EFT_eatpack);
                yield return new WaitForSeconds(1);
                StartCoroutine(DeckTurn(deck, other_score, index));
                //플레이어 3장 카드와 낸카드 리스트 점수 리스트로 이동
                Move_to_List(player, playerhand,index);
                Move_to_List(samefloorCard[0],  floor,index);
                Move_to_List(samefloorCard[1], floor,index);
                Move_to_List(samefloorCard[2],  floor,index);
                yield return new WaitForSeconds(1);
                //패뻇어오기 
                Take_Pee(other_score, WhosPooping(index,player), index);

                if (Is_GukJin(player, samefloorCard[0]) ||  Is_GukJin(samefloorCard[1], samefloorCard[2])) 
                {
                    yield return new WaitForSeconds(1.5f);
                    //ChoiceCard.instance.gukjin_selec = true;
                    ChoiceCard.instance.Ques_Gukjin(index);
                    //yield return new WaitForSeconds(3);//3초안에 선택안하면 그냥 꺼짐 
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
        }//점수 결과 전달
    }
}
