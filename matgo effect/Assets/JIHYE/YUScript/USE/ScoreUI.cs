using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance;

    public GameObject player1_ScoreUI;
    public GameObject player2_ScoreUI;

    Text go1;
    Text go2;
    Text score1UI;
    Text score2UI;
    Text score1event;
    Text score2event;

    int go1count = 0;
    int go2count = 0;

    int score1;
    int score2;



    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //0번 은 GO / 1번은 스코어 / 3 번은 이벤트?
        go1 =  player1_ScoreUI.transform.GetChild(0).gameObject.GetComponent<Text>();
        score1UI = player1_ScoreUI.transform.GetChild(1).gameObject.GetComponent<Text>();
        score1event = player1_ScoreUI.transform.GetChild(2).gameObject.GetComponent<Text>();
        score1event.text = " ";

        go2 = player2_ScoreUI.transform.GetChild(0).gameObject.GetComponent<Text>();
        score2UI = player2_ScoreUI.transform.GetChild(1).gameObject.GetComponent<Text>();
        score2event = player2_ScoreUI.transform.GetChild(2).gameObject.GetComponent<Text>();
        score2event.text = " ";
    }

    // 점수를 받아서 계속 업데이트 해줘야한다. 
    void Update()
    {
        go1.text = go1count.ToString() + " 고";
        go2.text = go2count.ToString() + " 고";

        score1UI.text = score1.ToString() + ": 점"; 
        score2UI.text = score2.ToString() + ": 점"; 
    }

    // 흔들 폭판 뻑 카운트 

    // 점수 업데이트 
    public void Get_Count(int score ,int a)
    {
        if (a == 0)
        {
           score1 = score;
        }
        else if (a == 1)
        {
           score2 = score;
        }
    }

    // 특별 이벤트 점수 추가 
    public void Get_Count(string events, int a)
    {
        if (a == 0)
        {
            score1event.text += "/" + events;
        }
        else if (a == 1)
        {
            score2event.text += "/" +events;
        }
    }

    // 고 업데이트 
    public void GoCount(int go, int a)
    {
        if (a == 0)
        {
            go1count = go;
        }
        else if (a == 1)
        {
            go2count = go;
        }
    }
}
