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

    //int pee_score;
    int pee1_score;
    int pee2_score;

    //int kwang_score;
    int kwang1_score;
    int kwang2_score;

    int yeal1_count;
    int yeal2_count;

    bool Go_mind = true;
    bool OnClick = false;

    public GameObject finalCanvas;
    Text double_Event;
    Text score_text;

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
        p1score = GoStopRule.instance.p1_Score;
        p2score = GoStopRule.instance.p2_Score;




    }

    public void TurnOver(int index,List<GameObject> other)
    {
        
        StartCoroutine(Chack_Go(index,other));
    }


    public IEnumerator Chack_Go(int index,List<GameObject>otherScore)
    {
        int scoreLine = 0;
        int yealCount = 0;
        int kwang_score = 0;
        int pee_score = 0;
        if (index == 0)
        {
            score = p1score;
            goCount = p1GoCount;

            yealCount = yeal1_count;
            pee_score = pee1_score;
            kwang_score = kwang1_score; 
        }
        else if (index == 1)
        {
            score = p2score;
            goCount = p2GoCount;

            yealCount = yeal2_count;
            pee_score = pee2_score;
            kwang_score = kwang2_score;
        }

        //7�� �̻��̰ų� ���� ���� ���� ������ 
        if (score > 6)
        {
            if (score > preScore)
            {
                canvas.SetActive(true);
                //yield return new WaitForSeconds(5f);
                //�ð� ������ ���þ��ϸ� �ڵ���
                //������ �������� �����. 
                if (OnClick)
                { 
                //���Ѵٰ� �ϸ�.
                if (Go_mind)
                {
                    print("�� �ߴ�");
                    preScore = score;
                    goCount++;
                    canvas.SetActive(false);
                    ScoreUI.instance.GoCount(goCount,index);
                    SoundManager.instance.PlayGo(goCount);
                }
                else if(!Go_mind)
                {
                    finalCanvas.SetActive(true);
                    double_Event = finalCanvas.transform.GetChild(1).gameObject.GetComponent<Text>();
                    score_text = finalCanvas.transform.GetChild(2).gameObject.GetComponent<Text>();
                    double_Event.text = " ";
                    print("���ӳ�");
                    //�������� 
                    List<string> doubles = double_Check(otherScore, pee_score, kwang_score, yealCount);
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
                        yield return new WaitForSeconds(0.2f);
                    }

                    score_text.text = " ( " + score.ToString() + "+" + goCount.ToString() + ") x " 
                        + dou.ToString() + " = " + scoreLine.ToString(); 
                }
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

    public List<string> double_Check(List<GameObject> otherscore ,int pee_score,int kwang_score , int yeal_count)
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
        if (otpee.Count >= 1 && otpee.Count <=6 && pee_score >=1)
        {
            double_event.Add("�ǹ�");               
        }
        //�۹��ϋ�, 
        if ( yeal_count >= 7 )
        {
            double_event.Add("�۹�"); 
        }
        return double_event;
    }


    public void ReciveScore(int a,int yealCount,int pee_score,int kwang_score)
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

    public void OnClick_GO()
    {
        OnClick = true;
        Go_mind = true;
        readyGo.text = "Go";
        canvas.SetActive(false);
    }
    public void OnClick_Stop()
    {
        OnClick = true;
        Go_mind = false;
        readyGo.text = "Stop";
        canvas.SetActive(false);
    }
}
