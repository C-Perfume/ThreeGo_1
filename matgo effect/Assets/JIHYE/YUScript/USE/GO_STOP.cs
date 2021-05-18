using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GO_STOP : MonoBehaviour
{
    public static GO_STOP instance;
    public GameObject canvas;
    public Text readyGo;
    int p1score;
    int p2score;

    int score;

    int preScore = 0;

    int goCount;
    public int p1GoCount;
    public int p2GoCount;

    public bool ask_go = false; 
    bool Go_mind = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        p1score = GoStopRule.instance.p1_Score;
        p2score = GoStopRule.instance.p2_Score;
        if (ask_go)
        {
            
        }
    }

    public void TurnOver(int index,List<GameObject> other)
    {
        
        StartCoroutine(Chack_Go(index,other));
    }


    public IEnumerator Chack_Go(int index,List<GameObject>otherScore)
    {
        if (index == 0)
        {
            score = p1score;
            goCount = p1GoCount;
        }
        else if (index == 1)
        {
            score = p2score;
            goCount = p2GoCount;
        }

        //7점 이상이거나 이전 점수 보다 높을떄 
        if (score >= 7)
        {
            if (score > preScore)
            {
                canvas.SetActive(true);
                yield return new WaitForSeconds(5f);
                //시간 지나도 선택안하면 자동고
                //고할지 스돕할지 물어본다. 
                //고한다고 하면.
                if (Go_mind)
                {
                    print("고 했다");
                    preScore = score;
                    goCount++;
                    canvas.SetActive(false);
                }
                else
                {
                    print("게임끝");
                    //게임종료 

                    // 최종 점수 확인 , 곱하지 점수들 끌어다가 죄종 점수 계산하기. 
                    canvas.SetActive(false);
                }
            }
        }

        if (index == 0)
        {
            p1score = score  ;
            p1GoCount =  goCount;
        }
        else if (index == 1)
        {
            p2score = score;
            p2GoCount = goCount;
        }
    }

    public int double_Check(List<GameObject> otherscore ,int pee_score,int kwang_score , List<GameObject> myYeal)
    {
        int dou = 1;
     
        List<GameObject> otpee = new List<GameObject>();
        List<GameObject> otkwang = new List<GameObject>();

        for (int i = 0; i < otherscore.Count; i++)
        {
            Card.Card_Type type = otherscore[i].GetComponent<Card>().type;
            switch (type)
            {
                case Card.Card_Type.PEE:
                    otpee.Add(otherscore[i]);
                    break;
                case Card.Card_Type.KWANG:
                    otkwang.Add(otherscore[i]);
                    break;
                default:
                    break;
            }
        }

        //피박
        if (otpee.Count >= 1 && otpee.Count <=6 && pee_score >=1)
        {
            dou *= 2;                
        }
        //광박
        if (otkwang.Count == 0 && kwang_score >= 3)
        {
            dou *= 2;
        }
        //멍박일떄, 
        if ( myYeal.Count >= 7 )
        {
            dou *= 2;
        }
        return dou;
    }




    public void OnClick_GO()
    {
        Go_mind = true;
        readyGo.text = "Go";
    }
    public void OnClick_Stop()
    {
        Go_mind = false;
        readyGo.text = "Stop";
    }
}
