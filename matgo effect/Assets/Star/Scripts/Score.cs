using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //스코어도 플레이어가 둘이라 두개가 필요하다.
    public Text go;
    public Text score;
    public Text pee;
    public Text finalS;

    public int currPee;
    
    int totalCnt;
    public int currScore;
    public int finalScore;
    int card4 = 0; // 대통령

    int goScore;
    int currGo;
    int goCnt;
    public List<GameObject> tripleC;


    PlayerM pm;
    Score p2s;

    void Start()
    {
        pm = gameObject.GetComponent<PlayerM>();
        if (gameObject.name == "P1") { p2s = GameObject.Find("P2").GetComponent<Score>(); }
        if (gameObject.name == "P2") { p2s = GameObject.Find("P1").GetComponent<Score>(); }
    }

    void Update()
    {

    }

    public void AddP()
    {

        int j = 0;
        if (pm.p1FPosL[0].occupy.Count > 0)
        {

            for (int i = 0; i < pm.p1FPosL[0].occupy.Count; i++)
            {

                if (pm.p1FPosL[0].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.TWO_PEE)
                { j++; }
            }
        }

        currPee = pm.p1FPosL[0].occupy.Count + j;
        pee.text = "획득 피 : " + currPee.ToString();

    }

    public void TotalS()
    {
        totalCnt = 0;

        //피 10장에 1점씩
        if (currPee >= 10)
        {
            int i = currPee - 9;
            totalCnt += i;

        }


        //광
        if (pm.p1FPosL[1].occupy.Count >= 3)
        {

            int i = pm.p1FPosL[1].occupy.Count;

            for (int j = 0; j < 3; j++)
            {
                //비광포함
                if (pm.p1FPosL[1].occupy.Count == 3 &&
                    pm.p1FPosL[1].occupy[j].GetComponent<CardS>().state == CardS.CARD_STATUS.BGWANG)
                {

                    i = 2;
                }
            }

            //5광
            if (pm.p1FPosL[1].occupy.Count >= 5) { i = 15; }

            totalCnt += i;

        }

        //열끗
        if ( pm.p1FPosL[2].occupy.Count >= 5)
        {
            int i = pm.p1FPosL[2].occupy.Count - 4;
            totalCnt += i;
        }

        //띠
        if ( pm.p1FPosL[3].occupy.Count >= 5)
        {
            int i = pm.p1FPosL[3].occupy.Count - 4;
            totalCnt += i;
        }


        //고도리
        States( 2, 4, "고도리", 5);

        //홍단
        States(3, 1, "홍단", 3);

        //초단
        States(3, 13, "초단", 3);

        //청단
        States(3, 21, "청단", 3);

        //3번 싼 경우
        if (pm.shitL.Count > 2)
        {
            totalCnt = 10;
            print("3뻑");
        }


        // 국진이동 시 7점이 되는 경우
        for (int i = 0; i < pm.p1FPosL[2].occupy.Count; i++)
        {

            // 국진이 있고
            if (pm.p1FPosL[2].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
            {

                // 총 피갯수가 9장 열끗은 5장미만, 점수는 6점인 경우
                if (currPee == 9 && pm.p1FPosL[3].occupy.Count > 5 && totalCnt == 6)
                {

                    print("국진을 쌍피로 - 점수");
                    pm.p1FPosL[2].occupy[i].transform.position = pm.p1FPosL[0].occupy[pm.p1FPosL[0].occupy.Count - 1].transform.position;
                    currPee++;
                    break;
                }
            }

        }

        currScore = totalCnt;

        score.text = "점수 : " + currScore.ToString();

        // 국진이동 시 피박 면하는 경우
        //피박이면서 5~6장일 때
        if (p2s.currScore> 10 && currPee > 4 && currPee < 7) {

            for (int i = 0; i < pm.p1FPosL[2].occupy.Count; i++)
            {

                // 국진이 있고
                if (pm.p1FPosL[2].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
                {
      
                    print("국진을 쌍피로 피박면하기");
                    pm.p1FPosL[2].occupy[i].transform.position = pm.p1FPosL[0].occupy[pm.p1FPosL[0].occupy.Count - 1].transform.position;
                    currPee++;
                     break;
                   
                }
            }
        }

    }

    public void Card4() {
        //대통령
        for (int i = 0; i < pm.p1HandL.Count; i++)
        {
            tripleC = pm.p1HandL.FindAll(obj => obj.GetComponent<CardS>().same(
                    pm.p1HandL[i].GetComponent<CardS>().num));
            if (tripleC.Count == 4)
            {
                card4++;
                finalScore = 10;
                print("대통령");
                finalS.text = "최종점수 : " + finalScore.ToString();
                break;
            }
        }
    }
    
    //고도리, 홍청초
    public void States(int tY, int childIdx, string prints, int add)
    {

        if (pm.p1FPosL[tY].occupy.Count >= 3)
        {

            if (pm.p1FPosL[tY].occupy.Contains
                 (GameObject.Find("cards").transform.GetChild(childIdx).gameObject))
            {
                //s는 state의 줄임
                List<GameObject> s =pm.p1FPosL[tY].occupy.FindAll(obj => obj.GetComponent<CardS>().sameS(
                GameObject.Find("cards").transform.GetChild(childIdx).GetComponent<CardS>().state));

                if (s.Count >= 3)
                {
                    print(prints + " : " + s[0]);
                    totalCnt += add;
                }


            }


        }
    }
    

    public void FinalS()
    {
        finalScore = currScore;
         
        //피박
        if (currPee >= 10 && pm.p2FPosL[0].occupy.Count > 0)
        {
            int j = 0;

            for (int i = 0; i < pm.p2FPosL[0].occupy.Count; i++)
            {

                if (pm.p2FPosL[0].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.TWO_PEE)
                {
                    j++;
                }

            }

            if (pm.p2FPosL[0].occupy.Count + j < 7) { finalScore *= 2;
                print("피박");
            }

        }


        //광박
        if (pm.p1FPosL[1].occupy.Count >= 3 &&
            pm.p2FPosL[1].occupy.Count == 0)
        {
            finalScore *= 2;
            print("광박");
        }


        //멍박
        if (pm.p1FPosL[2].occupy.Count >= 7)
        {
            finalScore *= 2;
            print("멍박");
        }

        //고박
        if (p2s.currGo > 0 && currScore >= 7) {
            finalScore *= 2;
            print("고박");
        }

        if (pm.shakeC > 0)
        { //흔들
            finalScore *= 1 + pm.shakeC;
            print("흔들두배"); }
        
        if (pm.bombC > 0)
        {  //폭탄
            finalScore *= 1 + pm.bombC; 
            print("폭탄두배"); }
       
       

        finalS.text = "최종점수 : " + finalScore.ToString();

    }


    public void AddG()
    {

        if (goCnt == 0 && currScore >= 7 && card4 == 0)
        {
            print("고했다");
            goCnt++;
            goScore = currScore;
        }

        if (goCnt > 1 && goScore < currScore)
        {

            finalScore++;
            print("고 할꺼냐?");
            // 고하는 경우
            goCnt++;
            print(goCnt+"고"); 
            goScore = currScore; 


            //3고부터 2배씩 늘어나기
                for (int i = 3; i < 20; i++)
                {
                    if (goCnt == i)
                { finalScore *= 2*i-1; }
                 
                  }


        }

        currGo += goCnt;
        go.text = "Go : " + currGo.ToString();
      
    }




}
