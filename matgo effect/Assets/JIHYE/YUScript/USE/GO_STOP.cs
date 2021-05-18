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

        //7�� �̻��̰ų� ���� ���� ���� ������ 
        if (score >= 7)
        {
            if (score > preScore)
            {
                canvas.SetActive(true);
                yield return new WaitForSeconds(5f);
                //�ð� ������ ���þ��ϸ� �ڵ���
                //������ �������� �����. 
                //���Ѵٰ� �ϸ�.
                if (Go_mind)
                {
                    print("�� �ߴ�");
                    preScore = score;
                    goCount++;
                    canvas.SetActive(false);
                }
                else
                {
                    print("���ӳ�");
                    //�������� 

                    // ���� ���� Ȯ�� , ������ ������ ����ٰ� ���� ���� ����ϱ�. 
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

        //�ǹ�
        if (otpee.Count >= 1 && otpee.Count <=6 && pee_score >=1)
        {
            dou *= 2;                
        }
        //����
        if (otkwang.Count == 0 && kwang_score >= 3)
        {
            dou *= 2;
        }
        //�۹��ϋ�, 
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
