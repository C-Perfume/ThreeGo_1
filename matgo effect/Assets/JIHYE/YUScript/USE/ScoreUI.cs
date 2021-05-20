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
        //0�� �� GO / 1���� ���ھ� / 3 ���� �̺�Ʈ?
        go1 =  player1_ScoreUI.transform.GetChild(0).gameObject.GetComponent<Text>();
        score1UI = player1_ScoreUI.transform.GetChild(1).gameObject.GetComponent<Text>();
        score1event = player1_ScoreUI.transform.GetChild(2).gameObject.GetComponent<Text>();
        score1event.text = " ";

        go2 = player2_ScoreUI.transform.GetChild(0).gameObject.GetComponent<Text>();
        score2UI = player2_ScoreUI.transform.GetChild(1).gameObject.GetComponent<Text>();
        score2event = player2_ScoreUI.transform.GetChild(2).gameObject.GetComponent<Text>();
        score2event.text = " ";
    }

    // ������ �޾Ƽ� ��� ������Ʈ ������Ѵ�. 
    void Update()
    {
        go1.text = go1count.ToString() + " ��";
        go2.text = go2count.ToString() + " ��";

        score1UI.text = score1.ToString() + ": ��"; 
        score2UI.text = score2.ToString() + ": ��"; 
    }

    // ��� ���� �� ī��Ʈ 

    // ���� ������Ʈ 
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

    // Ư�� �̺�Ʈ ���� �߰� 
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

    // �� ������Ʈ 
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
