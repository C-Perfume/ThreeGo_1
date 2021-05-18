using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //���ھ �÷��̾ ���̶� �ΰ��� �ʿ��ϴ�.
    public Text go;
    public Text score;
    public Text pee;
    public Text finalS;

    public int currPee;
    
    int totalCnt;
    public int currScore;
    public int finalScore;
    int card4 = 0; // �����

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
        pee.text = "ȹ�� �� : " + currPee.ToString();

    }

    public void TotalS()
    {
        totalCnt = 0;

        //�� 10�忡 1����
        if (currPee >= 10)
        {
            int i = currPee - 9;
            totalCnt += i;

        }


        //��
        if (pm.p1FPosL[1].occupy.Count >= 3)
        {

            int i = pm.p1FPosL[1].occupy.Count;

            for (int j = 0; j < 3; j++)
            {
                //������
                if (pm.p1FPosL[1].occupy.Count == 3 &&
                    pm.p1FPosL[1].occupy[j].GetComponent<CardS>().state == CardS.CARD_STATUS.BGWANG)
                {

                    i = 2;
                }
            }

            //5��
            if (pm.p1FPosL[1].occupy.Count >= 5) { i = 15; }

            totalCnt += i;

        }

        //����
        if ( pm.p1FPosL[2].occupy.Count >= 5)
        {
            int i = pm.p1FPosL[2].occupy.Count - 4;
            totalCnt += i;
        }

        //��
        if ( pm.p1FPosL[3].occupy.Count >= 5)
        {
            int i = pm.p1FPosL[3].occupy.Count - 4;
            totalCnt += i;
        }


        //����
        States( 2, 4, "����", 5);

        //ȫ��
        States(3, 1, "ȫ��", 3);

        //�ʴ�
        States(3, 13, "�ʴ�", 3);

        //û��
        States(3, 21, "û��", 3);

        //3�� �� ���
        if (pm.shitL.Count > 2)
        {
            totalCnt = 10;
            print("3��");
        }


        // �����̵� �� 7���� �Ǵ� ���
        for (int i = 0; i < pm.p1FPosL[2].occupy.Count; i++)
        {

            // ������ �ְ�
            if (pm.p1FPosL[2].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
            {

                // �� �ǰ����� 9�� ������ 5��̸�, ������ 6���� ���
                if (currPee == 9 && pm.p1FPosL[3].occupy.Count > 5 && totalCnt == 6)
                {

                    print("������ ���Ƿ� - ����");
                    pm.p1FPosL[2].occupy[i].transform.position = pm.p1FPosL[0].occupy[pm.p1FPosL[0].occupy.Count - 1].transform.position;
                    currPee++;
                    break;
                }
            }

        }

        currScore = totalCnt;

        score.text = "���� : " + currScore.ToString();

        // �����̵� �� �ǹ� ���ϴ� ���
        //�ǹ��̸鼭 5~6���� ��
        if (p2s.currScore> 10 && currPee > 4 && currPee < 7) {

            for (int i = 0; i < pm.p1FPosL[2].occupy.Count; i++)
            {

                // ������ �ְ�
                if (pm.p1FPosL[2].occupy[i].GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
                {
      
                    print("������ ���Ƿ� �ǹڸ��ϱ�");
                    pm.p1FPosL[2].occupy[i].transform.position = pm.p1FPosL[0].occupy[pm.p1FPosL[0].occupy.Count - 1].transform.position;
                    currPee++;
                     break;
                   
                }
            }
        }

    }

    public void Card4() {
        //�����
        for (int i = 0; i < pm.p1HandL.Count; i++)
        {
            tripleC = pm.p1HandL.FindAll(obj => obj.GetComponent<CardS>().same(
                    pm.p1HandL[i].GetComponent<CardS>().num));
            if (tripleC.Count == 4)
            {
                card4++;
                finalScore = 10;
                print("�����");
                finalS.text = "�������� : " + finalScore.ToString();
                break;
            }
        }
    }
    
    //����, ȫû��
    public void States(int tY, int childIdx, string prints, int add)
    {

        if (pm.p1FPosL[tY].occupy.Count >= 3)
        {

            if (pm.p1FPosL[tY].occupy.Contains
                 (GameObject.Find("cards").transform.GetChild(childIdx).gameObject))
            {
                //s�� state�� ����
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
         
        //�ǹ�
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
                print("�ǹ�");
            }

        }


        //����
        if (pm.p1FPosL[1].occupy.Count >= 3 &&
            pm.p2FPosL[1].occupy.Count == 0)
        {
            finalScore *= 2;
            print("����");
        }


        //�۹�
        if (pm.p1FPosL[2].occupy.Count >= 7)
        {
            finalScore *= 2;
            print("�۹�");
        }

        //���
        if (p2s.currGo > 0 && currScore >= 7) {
            finalScore *= 2;
            print("���");
        }

        if (pm.shakeC > 0)
        { //���
            finalScore *= 1 + pm.shakeC;
            print("���ι�"); }
        
        if (pm.bombC > 0)
        {  //��ź
            finalScore *= 1 + pm.bombC; 
            print("��ź�ι�"); }
       
       

        finalS.text = "�������� : " + finalScore.ToString();

    }


    public void AddG()
    {

        if (goCnt == 0 && currScore >= 7 && card4 == 0)
        {
            print("���ߴ�");
            goCnt++;
            goScore = currScore;
        }

        if (goCnt > 1 && goScore < currScore)
        {

            finalScore++;
            print("�� �Ҳ���?");
            // ���ϴ� ���
            goCnt++;
            print(goCnt+"��"); 
            goScore = currScore; 


            //3����� 2�辿 �þ��
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
