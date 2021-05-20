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
    public Text result;

    public int currPee;

    int totalCnt;
    public int currScore;
    public int finalScore;

    // 고도리 홍초청단 이펙트용
    public int gdr = 0;
    public int r = 0;
    public int g = 0;
    public int b = 0;

    //광이펙트용
    int g138 = 0;
    int g1311 = 0;
    int g1811 = 0;
    int g3811 = 0;
    int g13811 = 0;
    int g13812 = 0;
    int g131112 = 0;
    int g181112 = 0;
    int g381112 = 0;
    int g5 = 0;

    public int goScore;
    public int goCnt;
    public bool gc = false;
    public List<GameObject> tripleC;


    PlayerM pm;
    Score p2s;
    Effect eft;
    GameManager gm;
    void Start()
    {
        eft = Effect.instance;
        pm = gameObject.GetComponent<PlayerM>();
        if (gameObject.name == "P1") { p2s = GameObject.Find("P2").GetComponent<Score>(); }
        if (gameObject.name == "P2") { p2s = GameObject.Find("P1").GetComponent<Score>(); }
    }

    void Update()
    {
    }

    public void Reset()
    {
        currPee = 0;
        totalCnt = 0;
        currScore = 0;
        finalScore = 0;
        goScore = 0;
        goCnt = 0;
        tripleC.Clear();
        result.text = "졌당";

        gdr = 0;
        r = 0;
        g = 0;
        b = 0;

        g138 = 0;
        g1311 = 0;
        g1811 = 0;
        g3811 = 0;
        g13811 = 0;
        g13812 = 0;
        g131112 = 0;
        g181112 = 0;
        g381112 = 0;
        g5 = 0;

        AddP();
        AddS();
        FinalS();

        //go.text = "";
        //score.text = "";
        //pee.text = "";
        //finalS.text = "";


    }

    public void AddG()
    {

        if (goCnt == 0 && currScore >= 7)
        {
            // 묻기
            eft.PlayEFT(2);
            gc = true;

        }

        if (goCnt > 0 && goScore < currScore)
        {

            finalScore++;
            print("고 할꺼냐?");

            // 고하는 경우
            eft.PlayEFT(2);
            gc = true;


            //3고부터 2배씩 늘어나기
            for (int i = 3; i < 20; i++)
            {
                if (goCnt == i)
                { finalScore *= 2 * i - 1; }

            }

        }



    }

    public void AddP()
    {

        int j = 0;
        if (pm.p1FPosL.Count >0 && pm.p1FPosL[0].occupy.Count > 0)
        {

            for (int i = 0; i < pm.p1FPosL[0].occupy.Count; i++)
            {

                if (pm.p1FPosL[0].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.TWO_PEE)
                { j++; }
                if (pm.p1FPosL[0].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
                { j++; }
            }
        }

        if (pm.p1FPosL.Count > 0) { currPee = pm.p1FPosL[0].occupy.Count + j; }
        pee.text = "획득 피 : " + currPee.ToString();

    }

    public void AddS()
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
            if (pm.p1FPosL[1].occupy.Count >= 5) { i = 15; g5++;  }
            if (g5 == 1) { eft.PlayEFT(23, gameObject, .05f); }
            

            //4광 경우의 수
            if (pm.p1FPosL[1].occupy.Count == 4)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 1)
                    { g13811++; g13812++; g131112++; g181112++; break; }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 3)
                    { g13811++; g13812++; g131112++; g381112++; break; }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 8)
                    { g13811++; g13812++; g181112++; g381112++; break; }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 11)
                    { g13811++; g131112++; g181112++; g381112++; break; }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 12)
                    { g13812++; g131112++; g181112++; g381112++; break; }
                }

               
                if (g13811 == 4) { eft.PlayEFT(18, gameObject, .1f); g13811 = 100; }
                if (g13812 == 4) { eft.PlayEFT(19, gameObject, .1f); g13812 = 100; }
                if (g131112 == 4) { eft.PlayEFT(20, gameObject, .1f); g131112 = 100; }
                if (g181112 == 4) { eft.PlayEFT(21, gameObject, .1f); g181112 = 100; }
                if (g381112 == 4) { eft.PlayEFT(22, gameObject, .1f); g381112 = 100; }

            }

            //3광 경우의 수
            if (pm.p1FPosL[1].occupy.Count == 3)
            {

                for (int j = 0; j < 2; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 1)
                    { g138++; g1311++; g1811++; break; }
                }

                for (int j = 0; j < 2; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 3)
                    { g138++; g1311++; g3811++; break; }
                }

                for (int j = 0; j < 2; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 8)
                    { g138++; g1811++; g3811++; break; }
                }

                for (int j = 0; j < 2; j++)
                {
                    if (pm.p1FPosL[1].occupy[j].GetComponent<CardS>().num == 11)
                    { g1311++; g1811++; g3811++; break; }
                }

                if (g138 == 4) { eft.PlayEFT(14, gameObject, .1f); g138 = 100; }
                if (g1311 == 4) { eft.PlayEFT(15, gameObject, .1f); g1311 = 100; }
                if (g1811 == 4) { eft.PlayEFT(16, gameObject, .1f); g1811 = 100; }
                if (g3811 == 4) { eft.PlayEFT(17, gameObject, .1f); g3811 = 100; }

            }

            totalCnt += i;

        }

        //열끗
        if (pm.p1FPosL[2].occupy.Count >= 5)
        {
            int i = pm.p1FPosL[2].occupy.Count - 4;
            totalCnt += i;
        }

        //띠
        if (pm.p1FPosL[3].occupy.Count >= 5)
        {
            int i = pm.p1FPosL[3].occupy.Count - 4;
            totalCnt += i;
        }


        //고도리
        States(2, 4, "고도리", 5, gdr);

        //홍단
        States(3, 1, "홍단", 3, r);

        //초단
        States(3, 13, "초단", 3, g);

        //청단
        States(3, 21, "청단", 3, b);


        


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
        if (p2s.currScore > 6 && currPee > 4 && currPee < 7)
        {

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

    public void Card4()
    {
        //대통령
        for (int i = 0; i < pm.p1HandL.Count; i++)
        {
            tripleC = pm.p1HandL.FindAll(obj => obj.GetComponent<CardS>().same(
                    pm.p1HandL[i].GetComponent<CardS>().num));
            if (tripleC.Count == 4)
            {
                finalScore = 10;
                print("대통령");
                result.text = "이겼당!";
                finalS.text = "합계 : " + finalScore.ToString() + "+ 대통령";
                eft.PlayEFT(0);
                pm.reset = true;
                break;
            }
        }
    }

    //고도리, 홍청초
    public void States(int tY, int childIdx, string prints, int add, int plus)
    {

        if (pm.p1FPosL[tY].occupy.Count >= 3)
        {

            if (pm.p1FPosL[tY].occupy.Contains
                 (GameObject.Find("cards").transform.GetChild(childIdx).gameObject))
            {
                //s는 state의 줄임
                List<GameObject> s = pm.p1FPosL[tY].occupy.FindAll(obj => obj.GetComponent<CardS>().sameS(
                 GameObject.Find("cards").transform.GetChild(childIdx).GetComponent<CardS>().state));

                if (s.Count >= 3)
                {
                    print(prints + " : " + s[0]);
                    totalCnt += add;
                    plus++;
                }

                if (gdr == 1) { eft.PlayEFT(24, gm.zero.gameObject, .1f); }
                if (r == 1) { eft.PlayEFT(25, gm.zero.gameObject, .1f); }
                if (g == 1) { eft.PlayEFT(26, gm.zero.gameObject, .1f); }
                if (b == 1) { eft.PlayEFT(27, gm.zero.gameObject, .1f); }
            }


        }
    }


    public void FinalS()
    {
        finalScore = currScore;
        string p = null;
        string g = null;
        string m = null;
        string go = null;
        string s = null;
        string b = null;
        string s3 = null;


        if (currScore > 6)
        {
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

                if (pm.p2FPosL[0].occupy.Count + j < 7)
                {
                    finalScore *= 2;
                    print("피박");
                    p = " + 피박";
                }

            }

            //광박
            if (pm.p1FPosL[1].occupy.Count >= 3 &&
                pm.p2FPosL[1].occupy.Count == 0)
            {
                finalScore *= 2;
                print("광박");
                g = " + 광박";
            }


            //멍박
            if (pm.p1FPosL[2].occupy.Count >= 7)
            {
                finalScore *= 2;
                print("멍박");
                m = " + 멍박";
            }

            //고박
            if (p2s.goCnt > 0)
            {
                finalScore *= 2;
                print("고박");
                go = " + 고박";
            }
        }

        //흔들
        if (pm.shakeC > 0)
        {
            finalScore *= 2 * pm.shakeC;
            print("흔들두배 x" + pm.shakeC);
            s = " + 흔들기" + pm.shakeC + "번";
        }

        //폭탄
        if (pm.bombC > 0)
        {
            finalScore *= 2 * pm.bombC;
            print("폭탄두배 x" + pm.bombC);
            b = " + 폭탄" + pm.bombC + "번";
        }

        //3번 싼 경우
        if (pm.shitL.Count > 2)
        {
            finalScore = 10;
            print("3뻑");
            result.text = "이겼당!";
            eft.PlayEFT(0);
            pm.reset = true;
            s3 = " 삼연뻑";
        }

        finalS.text = "합계 : " + finalScore.ToString() + s3 + p + g + m + go + s + b;

    }






}
