using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    GameObject hitObj;//���̷� ���� ���� ��� ����
    public int player;

    int idx; // ī�� �̵� �� ���� ���� ã�µ� ����. obj�� ��ġ�� ã�ƾ��ؼ� bool ���� ��Ʈ�� �����
    bool s = false; // ���� ī�� ������ ��� ������ �� ����.
    int idx2; // p2AI��

    //�����̽ð�
    float spd = 1f;


    public int bombC = 0;
    public int bombP = 0;
    public int shakeC = 0;
    //public int p1bomb = 0;
    //public int p2bomb = 0;
    //public int p1shake = 0;
    //public int p2shake = 0;

    GameManager gm;
    Score sc;
    Effect eft;

    List<GameObject> tripleC;
    List<GameObject> hitdoubleC;
    List<GameObject> doubleC;
    public List<GameObject> shitL = new List<GameObject>();

    public List<GameObject> p1HandL;
    public List<floor> p1FPosL;
    public List<floor> p2FPosL;

    //p2AI��
    List<GameObject> p2HandL;
    GameObject select = null;


    void Start()
    {
        sc = gameObject.GetComponent<Score>();
        gm = GameObject.Find("GM").GetComponent<GameManager>();
         eft = Effect.instance;

        if (player == 1)
        {
            p1HandL = gm.p1HandL;
            p1FPosL = gm.p1FPosL;
            p2FPosL = gm.p2FPosL;
        }
        else if (player == 2)
        {
            p1HandL = gm.p2HandL;
            p2HandL = gm.p1HandL;
            p1FPosL = gm.p2FPosL;
            p2FPosL = gm.p1FPosL;
        }
    }


    //�ٴڿ� ���� �� ī�� 4�� ������ ��� p1���� �Ա�
    public void Card4F()
    {

        for (int i = 0; i < gm.emptyL.Count; i++)
        {

            if (gm.emptyL[i].occupy.Count > 3)
            {
                StartCoroutine(Move3(i, p1FPosL, gm.emptyL[i].occupy[0]));
                StartCoroutine(Move3(i, p1FPosL, gm.emptyL[i].occupy[1]));
                StartCoroutine(Move3(i, p1FPosL, gm.emptyL[i].occupy[2]));
                StartCoroutine(Move3(i, p1FPosL, gm.emptyL[i].occupy[3]));
            }

        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bombP > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    hitObj = hit.transform.gameObject; //���̷� ���� obj�� hitObj

                    #region//p2AI ������
                    //{
                    //    {
                    //        StartCoroutine(Move2(p2FPosL, p1FPosL));
                    //        bombC--;
                    //    }
                    //}
                    #endregion

                    if (gm.cardL.Contains(hitObj)) // ������ �÷��̾�1�� �ڵ帮��Ʈ�� �ִٸ�
                    {
                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                        bombP--;
                    }
                }

            }
            else
            {
                StartCoroutine(Move());
            }

            //ī�� ���� �� ����
            if (s)
            {
                select = eft.Select();

                if (select != null)
                {

                    for (int i = 0; i < gm.emptyL[idx].occupy.Count; i++)
                    {

                        if (select.name.Contains(gm.emptyL[idx].occupy[i].name)) 
                        { 
                      
                            StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[i]));
                            StartCoroutine(Move2(p1FPosL, p2FPosL, select));
                            eft.eftL[(int)Effect.EFT.Choose].SetActive(false);
                            break;
                        }
                        else
                        {
                            
                            StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1]));
                            StartCoroutine(Move2(p1FPosL, p2FPosL, select));
                            eft.eftL[(int)Effect.EFT.Choose].SetActive(false);
                        }
                    }
                    s = false;
                }


            }
        }
    }


    public IEnumerator Move()
    {
        yield return new WaitForSeconds(.001f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {

            hitObj = hit.transform.gameObject; //���̷� ���� obj�� hitObj

            //p2AI ������
            //if (player == 2)
            //{
            //    if (p2HandL.Contains(hitObj)) // ������ �÷��̾�1�� �ڵ帮��Ʈ�� �ִٸ�
            //    {
            //        yield return new WaitForSeconds(4);
            //        idx = -1;

            //        for (int j = 0; j < p1HandL.Count; j++)
            //        {

            //            for (int i = 0; i < gm.emptyL.Count; i++)
            //            {

            //                //���� �����̸�
            //                if (gm.emptyL[i].occupy.Count > 0
            //                    && p1HandL[j].GetComponent<Card>().num == gm.emptyL[i].occupy[0].GetComponent<Card>().num)
            //                {
            //                    idx = i;
            //                    idx2 = j;

            //                    break;
            //                }

            //                if (idx != -1) { hitObj = p1HandL[idx2]; }
            //                else { hitObj = p1HandL[p1HandL.Count - 1]; }
            //            }
            //        }


            //    }
            //}

            if (p1HandL.Contains(hitObj)) // ������ �÷��̾�1�� �ڵ帮��Ʈ�� �ִٸ�
            {

                //��ź�� ����Ʈ 
                //���� �� �� hitobj�� ���� ���� ���� �͸� tripleC[0], [1], [2]..����Ʈ�� ���
                // �ѹ� 3�и� ���� �����ؾ� �Ǵ� �� �˾Ҵµ� �ڵ����� ��
                tripleC = p1HandL.FindAll(obj => obj.GetComponent<CardS>().same(
                hitObj.GetComponent<CardS>().num));
                //��, ���ڿ�..
                hitdoubleC = gm.floorL.FindAll(obj => obj.GetComponent<CardS>().same(
                         hitObj.GetComponent<CardS>().num));
                //  for (int j = 0; j < hitdoubleC.Count; j++) { print("hitobj���� " + hitdoubleC[j].name); }
                // for (int j = 0; j < tripleC.Count; j++) { print("Ʈ���� " + tripleC[j].name); }

                idx = -1;

                for (int i = 0; i < gm.emptyL.Count; i++)
                {

                    //���� �����̸�
                    if (gm.emptyL[i].occupy.Count > 0
                        && hitObj.GetComponent<CardS>().num == gm.emptyL[i].occupy[0].GetComponent<CardS>().num)
                    { idx = i; break; }

                }

                //¦�� ������
                if (idx != -1)
                {
                  
                    // ��ź
                    if (tripleC.Count == 3)
                    {
                        print("��ź");
                        bombC++;
                        bombP += 2;

                        gm.ActioniT(tripleC[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);
                        gm.ActioniT(tripleC[1], spd + 0.2f, gm.emptyL[idx].pos + (Vector3.right * 0.3f * 2), .1f);
                        gm.ActioniT(tripleC[2], spd + 0.4f, gm.emptyL[idx].pos + (Vector3.right * 0.3f * 3), .1f);

                        gm.floorL.Add(tripleC[0]); // ��ź0�� �ٴ��п� �߰�  
                        gm.emptyL[idx].occupy.Add(tripleC[0]);
                        gm.floorL.Add(tripleC[1]); // ��ź1 �߰�  
                        gm.emptyL[idx].occupy.Add(tripleC[1]);
                        gm.floorL.Add(tripleC[2]); //  ��ź2 �߰�
                        gm.emptyL[idx].occupy.Add(tripleC[2]);

                        if (hitObj.name != gm.emptyL[idx].occupy[3].name) //��ź�� �� ������ �����̱�
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[3])); }


                        for (int i = 0; i < tripleC.Count; i++)
                        {

                            if (hitObj.name != tripleC[i].name)
                            { p1HandL.Remove(tripleC[i]); } //���п��� ���ֱ�

                        }

                        //����� ��������
                        Take();
                    }

                    //�Ϲݻ�Ȳ ���� �ٴ����� �����̱�
                    else
                    {

                        gm.ActioniT(hitObj, spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);

                        gm.floorL.Add(hitObj); // ���а� �ٴ��а� �ƴ�.  

                        gm.emptyL[idx].occupy.Add(hitObj);

                    }


                    //��.. �� ��� hitobj
                    if (hitdoubleC.Count == 1 &&
                           hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                    {
                        print("��");
                        shitL.Add(hitObj);
                        print("��" + shitL.Count);

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                    }
                    //�ٴڿ� ���� ī�� 2���̸�
                    else if (hitdoubleC.Count == 2)
                    {
                        print("ī�弱��");
                        eft.PlayEFT1(Effect.EFT.Choose, gm.emptyL[idx].occupy[0], gm.emptyL[idx].occupy[1]);
                        s = true;
                        StartCoroutine(Move3(idx, p1FPosL, hitObj));
                    
                    }
                    else
                    { 

                        // �Ϲݻ�Ȳ ¦ ���� �� �� �ڸ��� �ű��
                        StartCoroutine(Move3(idx, p1FPosL, hitObj));

                        if (hitObj.name != gm.emptyL[idx].occupy[0].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0])); }

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                    }

                    // �� �Ա�
                    if (gm.emptyL[idx].occupy.Count > 3)
                    {
                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));

                        if (hitObj.name != gm.emptyL[idx].occupy[1].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1])); }
                        if (hitObj.name != gm.emptyL[idx].occupy[2].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[2])); }

                        for (int i = 0; i < shitL.Count; i++)
                        {

                            //���� �� ���� ���� Ŭ���� ���� ���� ������
                            if (hitObj.GetComponent<CardS>().num == shitL[i].GetComponent<CardS>().num)
                            {

                                print("�ڻ�");
                                //����� ��������
                                if (p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1].GetComponent<CardS>().state == CardS.CARD_STATUS.TWO_PEE)
                                {  
                                    break; }
                                else
                                {
                                    Take();
                                    break;
                                }
                              

                            }

                        }

                        //����� ��������
                        Take();
                    }

                }

                // ¦�� �ȸ��� ���
                else
                {

                    if (tripleC != null && tripleC.Count == 3)
                    {
                        print("����?");
                    }

                    for (int i = 0; i < gm.emptyL.Count; i++)
                    {

                        if (gm.emptyL[i].occupy.Count == 0)
                        {
                            //���ڸ��� ����.
                            gm.ActioniT(hitObj, spd, gm.emptyL[i].pos, .1f);
                            gm.emptyL[i].occupy.Add(hitObj);
                            idx = -1;

                           StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                            break;

                        }

                    }
                    gm.floorL.Add(hitObj); // ���а� �ٴ��а� �ƴ�.

                }
                p1HandL.Remove(hitObj); // ���а� �̵������� ������

            }

            gm.Rot();
        }
    }



    IEnumerator Move2(List<floor> p1FPosL, List<floor> p2FPosL, GameObject hitObj) // ������ ���̵�
    {
        yield return new WaitForSeconds(1.5f);
        idx = -1;

        for (int i = 0; i < gm.floorL.Count; i++)
        {
            //���� ���̸�
            if (gm.emptyL[i].occupy.Count > 0
            && gm.cardL[0].GetComponent<CardS>().num == gm.emptyL[i].occupy[0].GetComponent<CardS>().num)
            {

                idx = i;
                break;

            }
        }


        //���� ����Ʈ
        doubleC = gm.floorL.FindAll(obj => obj.GetComponent<CardS>().same(
                hitObj.GetComponent<CardS>().num));

        // for (int j = 0; j < doubleC.Count; j++) { print("���� " + doubleC[j].name); }


        if (idx != -1)
        {

            //��.. �� ���
            if (doubleC.Count > 1 && tripleC.Count > 0
               && hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
            {

                //��0���� Ŭ���� �� ���̱�
                gm.ActioniT(gm.cardL[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);
                gm.floorL.Add(gm.cardL[0]); // �̵��� �и� �ٴڸ���Ʈ�� �߰�
                gm.emptyL[idx].occupy.Add(gm.cardL[0]);

            }

            //�Ϲ� ��Ȳ
            else
            {


                //�� 0���� ¦�´� ī�忡 ���̱�
                gm.ActioniT(gm.cardL[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);
                gm.floorL.Add(gm.cardL[0]); // �̵��� �и� �ٴڸ���Ʈ�� �߰�

                //�÷��̾� �ڸ��� ȸ��
                StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0]));
                StartCoroutine(Move3(idx, p1FPosL, gm.cardL[0]));




                //��
                if (doubleC.Count == 1 && hitdoubleC.Count == 0 &&
                  hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("��");
                    //����� ��������
                    Take();

                }

                //����
                if (tripleC != null && tripleC.Count == 1 && doubleC.Count == 1 && hitdoubleC.Count == 2 &&
                     hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("����");
                    //����� ��������
                    Take();

                }




                // �� �Ա�
                if (gm.emptyL[idx].occupy.Count > 3)
                {

                    if (gm.cardL[0].name != gm.emptyL[idx].occupy[1].name)
                    { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1])); }
                    if (gm.cardL[0].name != gm.emptyL[idx].occupy[2].name)
                    { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[2])); }


                    //����� ��������
                    Take();
                }
            }
        }

        //¦ ���� ���
        else
        {

            for (int i = 0; i < gm.emptyL.Count; i++)
            {

                if (gm.emptyL[i].occupy.Count == 0)
                {
                    //���ڸ��� ����
                    gm.ActioniT(gm.cardL[0], spd, gm.emptyL[i].pos, .1f);
                    gm.emptyL[i].occupy.Add(gm.cardL[0]);
                    idx = -1;
                    break;

                }
            }

            gm.floorL.Add(gm.cardL[0]); // �̵��� �и� �ٴڸ���Ʈ�� �߰�
        }

        gm.cardL.RemoveAt(0);

        StartCoroutine(AddScore());
    }

    IEnumerator AddScore() {
        yield return new WaitForSeconds(2f);
        sc.AddP();
        sc.TotalS();
        sc.AddG();
        sc.FinalS();
    }


    IEnumerator Move3(int idx, List<floor> p1FPosL, GameObject obj)
    // ¦���� �� p1p2 �÷ξ���ġ�� �̵�
    { // ����Ʈ �̸��� p1FPosL�̶� �Ű������� ���� �ٲ�� �ֵ��� ����. p1fl / p2fl ��� ���� ����

        yield return new WaitForSeconds(1f);
        int TY = (int)obj.GetComponent<CardS>().type;
        
        //������ ���� ���
        if (obj.GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
            {
                // ����
                print("��������");
               
                //���Ƕ��
                //obj.transform.position = p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position;
                //sc.currPee++;
            }

        p1FPosL[TY].occupy.Add(obj);
        gm.floorL.Remove(obj);
        gm.emptyL[idx].occupy.Remove(obj);

        gm.ActioniT(obj, spd, p1FPosL[TY].pos + Vector3.right * p1FPosL[TY].occupy.Count * 0.3f, .1f);

        gm.Rot();


        //�Ͼ���
        if (gm.floorL.Count <= 0)
        {
            print("�Ͼ�");
            //����� ��������
            Take();
        }

    }
    public void Take()
    {

        if (p2FPosL[0].occupy.Count > 0)
        {

            if (p1FPosL[0].occupy.Count > 0)
            {
                p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1].transform.position
                    = p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position + Vector3.right * 0.3f;

                //gm.ActioniT(
                // p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1],
                // spd,
                // p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position + Vector3.right * 0.3f,
                // .2f);
            }
            else
            {

                p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1].transform.position
                    = p1FPosL[0].pos;

                //gm.ActioniT(
                // p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1],
                //spd,
                // p1FPosL[0].pos,
                // .2f);
            }

            p1FPosL[0].occupy.Add(
                p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1]);

            p2FPosL[0].occupy.RemoveAt(
                p2FPosL[0].occupy.Count - 1);
        }
    }


}

