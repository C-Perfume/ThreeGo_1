using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GO_STOP : MonoBehaviour
{
    public static GO_STOP instance;
    public GameObject canvas;
    bool gostop;
    int p1score;
    int p2score;

    int score;

    int preScore = 0;

    int goCount;
    public int p1GoCount;
    public int p2GoCount;

    //int pee_score;
    int pee1_score;
    int pee2_score;

    //int kwang_score;
    int kwang1_score;
    int kwang2_score;

    int yeal1_count;
    int yeal2_count;

    public GameObject finalCanvas;
    Text double_Event;
    Text score_text;

    int index = -1;

    int yealCount = 0;
    int kwang_score = 0;
    int pee_score = 0;
    List<GameObject> otherlist = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        canvas.SetActive(false);
        finalCanvas.SetActive(false);
    }


    void Update()
    {
        int scoreLine = 0;


        if (index == 0)
        {
            goCount = ScoreUI.instance.go1count;
            score = GoStopRule.instance.p1_Score;

            yealCount = yeal1_count;
            pee_score = pee1_score;
            kwang_score = kwang1_score;
        }
        else if (index == 1)
        {
            goCount = ScoreUI.instance.go2count;
            score = GoStopRule.instance.p2_Score;

            yealCount = yeal2_count;
            pee_score = pee2_score;
            kwang_score = kwang2_score;
        }

        if (!gostop)
        {
            return;
        }
        else
        { 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                GameObject obj = hit.transform.gameObject;
                if (obj.CompareTag("ChoiceCard"))
                {
                    if (obj.name.Contains("A"))
                    {
                        print("�� �ߴ�");
                        preScore = score;
                        goCount++;
                        canvas.SetActive(false);
                        ScoreUI.instance.GoCount(goCount, index);
                        SoundManager.instance.PlayGo(goCount);
                        gostop = false;
                    }
                    else if (obj.name.Contains("B"))
                    {
                        print("����");
                        finalCanvas.SetActive(true);
                        double_Event = finalCanvas.transform.GetChild(1).gameObject.GetComponent<Text>();
                        score_text = finalCanvas.transform.GetChild(2).gameObject.GetComponent<Text>();
                        double_Event.text = " ";
                        print("���ӳ�");
                        //�������� 
                        List<string> doubles = double_Check(otherlist, pee_score, kwang_score, yealCount);
                        int dou = 1;
                        for (int i = 0; i < doubles.Count; i++)
                        {
                            dou *= 2;
                        }
                        //���� ������  = (���� + ��� ���� ���� )* �ڿ� ���� ����.
                        // ���� ���� Ȯ�� , ������ ������ ����ٰ� ���� ���� ����ϱ�. 
                        scoreLine = (score + goCount) * dou;

                        for (int i = 0; i < doubles.Count; i++)
                        {
                            double_Event.text += " " + doubles[i] + " ";
                        }

                        score_text.text = " ( " + score.ToString() + "+" + goCount.ToString() + ") x "
                            + dou.ToString() + " = " + scoreLine.ToString();
                        gostop = false;
                    }
                }
            }

        }

        }

    
         


     }

    public void TurnOver(int index, List<GameObject> other)
    {
        this.index = index;
        otherlist = other;

        if (index == 0)
        {
            score = GoStopRule.instance.p1_Score;
        }
        else if (index == 1)
        {
            score = GoStopRule.instance.p2_Score;
        }


        //7�� �̻��̰ų� ���� ���� ���� ������ 
        if (score > 6)
        {
            if (score > preScore)
            {
                gostop = true;
                canvas.SetActive(true);
            }
        }
    }

    public List<string> double_Check(List<GameObject> otherscore, int pee_score, int kwang_score, int yeal_count)
    {
        List<string> double_event = new List<string>();

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

        //����
        if (otkwang.Count == 0 && kwang_score >= 3)
        {
            double_event.Add("����");
        }
        //�ǹ�
        if (otpee.Count >= 1 && otpee.Count <= 6 && pee_score >= 1)
        {
            double_event.Add("�ǹ�");
        }
        //�۹��ϋ�, 
        if (yeal_count >= 7)
        {
            double_event.Add("�۹�");
        }
        return double_event;
    }


    public void ReciveScore(int a, int yealCount, int pee_score, int kwang_score)
    {
        if (a == 0)
        {
            yeal1_count = yealCount;
            pee1_score = pee_score;
            kwang1_score = kwang_score;
        }
        else if (a == 1)
        {
            yeal2_count = yealCount;
            pee2_score = pee_score;
            kwang2_score = kwang_score;
        }
    }


}
