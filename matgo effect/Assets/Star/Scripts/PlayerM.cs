using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerM : MonoBehaviour
{
    public int player;

    int num = -1;
    int idx; // ī�� �̵� �� ���� ���� ã�µ� ����. obj�� ��ġ�� ã�ƾ��ؼ� bool ���� ��Ʈ�� �����
    bool c = false; // ���� ī�� ������ ��� ������ �� ����.
    bool s = false; // ��鿩��
    bool t = false; // �ڻ���
    bool kj = false; // ����
    public bool reset = false; // ���� ���� �� �ٽ����� ������ ����
    GameObject select = null;
    GameObject or = null;

    //�����̽ð�
    float spd = 0.1f;

    GameObject hitObj;//���̷� ���� ���� ��� ����

    public int bombC = 0; //��ź ī��Ʈ
    public int bombP = 0; // ��ź �� 2��
    public int shakeC = 0; // ��� ����

    GameManager gm;
    Score sc;
    PlayerM p2m;
    Effect eft;

    List<GameObject> tripleC; // ���� ����ī�� 3��
    List<GameObject> hitdoubleC; //  ���п� �ٴ��� ������
    List<GameObject> doubleC; // �ٴ��и� ������
    public List<GameObject> shitL = new List<GameObject>(); // �� Ƚ��

    public List<GameObject> p1HandL; // p1������
    public List<floor> p1FPosL;
    public List<floor> p2FPosL;
    public Transform[] p1HandPos;

    //p2AI��
    int idx2; // p2AI��
    List<GameObject> p2HandL;


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
            p1HandPos = gm.p1HandPos;
            p2m = GameObject.Find("P2").GetComponent<PlayerM>();
        }
        else if (player == 2)
        {
            p1HandL = gm.p2HandL;
            p2HandL = gm.p1HandL;
            p1FPosL = gm.p2FPosL;
            p2FPosL = gm.p1FPosL;
            p1HandPos = gm.p2HandPos;
            p2m = GameObject.Find("P1").GetComponent<PlayerM>();
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
#if UNITY_EDITOR
            if (!EventSystem.current.IsPointerOverGameObject())
#else
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#endif
            {
                if (bombP > 0)
                {
                    eft.eftL[29].SetActive(false);
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {

                        hitObj = hit.transform.gameObject; //���̷� ���� obj�� hitObj

                        #region//p2AI ������
                        if (player == 2)
                        {
                            {
                                StartCoroutine(Move2(p2FPosL, p1FPosL, gm.cardL[0]));
                                bombP--;
                            }
                        }
                        #endregion

                        if (gm.cardL.Contains(hitObj)) // ������ ������Ʈ�� �ִٸ�
                        {
                            StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                            bombP--;
                        }
                        // ��ź�ǰ� 2���̶� ���и� �� �� �ִ�.
                        else
                        {
                            StartCoroutine(Move());
                        }
                    }

                }
                // �Ϲ� ��Ȳ
                else
                {
                    StartCoroutine(Move());
                }


                //ī�� ���� �� ����
                if (c)
                {
                    if (player == 2) {
                        StartCoroutine(Move3(num, p1FPosL, hitObj));
                        StartCoroutine(Move3(num, p1FPosL, gm.emptyL[num].occupy[0]));
                        StartCoroutine(Move2(p1FPosL, p2FPosL, gm.emptyL[num].occupy[0]));
                        eft.eftL[1].SetActive(false);
                        Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject);
                        Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject);
                        c = false;
                    }
                    select = eft.Select();

                    if (select != null)
                    {

                        //for (int i = 0; i < gm.emptyL[num].occupy.Count; i++)
                        //{

                        if (select.name.Contains(gm.emptyL[num].occupy[0].name))
                        {
                            StartCoroutine(Move3(num, p1FPosL, hitObj));
                            StartCoroutine(Move3(num, p1FPosL, gm.emptyL[num].occupy[0]));
                            StartCoroutine(Move2(p1FPosL, p2FPosL, gm.emptyL[num].occupy[0]));
                            eft.eftL[1].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject);
                            // break;
                        }
                        else
                        {
                            StartCoroutine(Move3(num, p1FPosL, hitObj));
                            StartCoroutine(Move3(num, p1FPosL, gm.emptyL[num].occupy[1]));
                            StartCoroutine(Move2(p1FPosL, p2FPosL, gm.emptyL[num].occupy[1]));
                            eft.eftL[1].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject);
                        }
                        //}
                        c = false;
                    }


                }

                //��� �� ����
                if (s)
                {
                    or = eft.Or();
                    if (player == 2) {

                        {
                            shakeC++;
                            //5�� ������ Ȱ��ȭ �� ī�� ����
                            eft.PlayEFT(5,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject
                                );

                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_shaking);

                            //4�� ��Ȱ��ȭ �� 4�� ����ī�� ���� 
                            eft.eftL[4].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 6).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 5).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 4).gameObject);
                            //5�� ���� ī�嵵 2�� �� ����
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject, 2);
                            //5�� ��Ȱ��ȭ
                            StartCoroutine(Invisible(eft.eftL[5], 2));


                            //���п� ��ī�� �����̱�
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

                        }
                        // ���� ��Ȱ��ȭ
                        s = false;
                    
                }

                    if (or != null)
                    {
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_tuk);
                        // �ƴϴ� ����
                        if (or.name.Contains("c2"))
                        {
                            //��Ȱ��ȭ �� ����ī�� ����
                            eft.eftL[4].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject);

                            // ���� �����̰� ��ī�� �����̱�
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
                        }
                        else
                        {
                            shakeC++;
                            //5�� ������ Ȱ��ȭ �� ī�� ����
                            eft.PlayEFT(5,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject
                                );

                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_shaking);

                            //4�� ��Ȱ��ȭ �� 4�� ����ī�� ���� 
                            eft.eftL[4].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 6).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 5).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 4).gameObject);
                            //5�� ���� ī�嵵 2�� �� ����
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject, 2);
                            //5�� ��Ȱ��ȭ
                            StartCoroutine(Invisible(eft.eftL[5], 2));


                            //���п� ��ī�� �����̱�
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

                        }
                        // ���� ��Ȱ��ȭ
                        s = false;
                    }

                }

                //�����˾�
                if (kj)
                {
                    or = eft.Or();
                    if (player == 2) {

                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_take);
                        gm.ActioniT(p1FPosL[2].occupy[p1FPosL[2].occupy.Count - 1], spd,
                            p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position, .1f);

                        p1FPosL[0].occupy.Add(p1FPosL[2].occupy[p1FPosL[2].occupy.Count - 1]);
                        p1FPosL[2].occupy.RemoveAt(p1FPosL[2].occupy.Count - 1);

                        eft.eftL[11].SetActive(false);
                    
                    kj = false;

                }


                    if (or != null)
                    {
                        // ���� ����
                        if (or.name.Contains("c2"))
                        {
                            eft.eftL[11].SetActive(false);
                        }

                        //���Ƕ��
                        else
                        {
                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_take);
                            gm.ActioniT(p1FPosL[2].occupy[p1FPosL[2].occupy.Count - 1], spd,
                                p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position, .1f);

                            p1FPosL[0].occupy.Add(p1FPosL[2].occupy[p1FPosL[2].occupy.Count - 1]);
                            p1FPosL[2].occupy.RemoveAt(p1FPosL[2].occupy.Count - 1);

                            eft.eftL[11].SetActive(false);
                        }
                        kj = false;
                    }

                }


                //���鹯��
                if (sc.gc)
                {
                    or = eft.Or();

                    if (player == 2) {
                        eft.eftL[2].SetActive(false);
                        //�� ȿ��
                        sc.goCnt++;
                        eft.PlayEFT(12, gameObject, .05f);
                        eft.PlayEFT(6, gameObject, .05f, 5);
                        eft.PlayGo(sc.goCnt);
                        print(sc.goCnt + "��");
                        sc.goScore = sc.currScore;
                        sc.go.text = "Go : " + sc.goCnt.ToString();
                    
                    sc.gc = false;

                                                 }

                    if (or != null)
                    {
                        // �ƴϴ� ����
                        if (or.name.Contains("c2"))
                        {
                            eft.eftL[2].SetActive(false);


                            sc.result.text = "�̰��!";
                            eft.PlayEFT(0, gameObject, .05f, 5);
                            eft.PlayEFTM(Effect.EFT_TYPE.win);

                            //���� ����Ʈ
                            eft.PlayEFT(13, gameObject, .05f);
                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_Stop);
                            // ��������txt
                            eft.PlayEFT(0);
                            reset = true;
                        }

                        //���ϱ�
                        else
                        {
                            eft.eftL[2].SetActive(false);
                            //�� ȿ��
                            sc.goCnt++;
                            eft.PlayEFT(12, gameObject, .05f);
                            eft.PlayEFT(6, gameObject, .05f, 5);
                            eft.PlayGo(sc.goCnt);
                            print(sc.goCnt + "��");
                            sc.goScore = sc.currScore;
                            sc.go.text = "Go : " + sc.goCnt.ToString();
                        }
                        sc.gc = false;
                    }
                }


                //��������? �ٽ��ϱ�?
                if (reset)
                {
                    or = eft.Or();

                    if (player == 2) {
                        eft.eftL[0].SetActive(false);
                        Reset();
                    }
                    
                    if (or != null)
                    {

                        //��
                        if (or.name.Contains("c2"))
                        {
                            eft.eftL[0].SetActive(false);
                        }
                        else
                        {
                            eft.eftL[0].SetActive(false);
                            Reset();
                        }

                    }
                }

                if (p2m.reset)
                {
                    eft.PlayEFT(1, gameObject, .05f, 5);
                    eft.PlayEFTM(Effect.EFT_TYPE.lose);
                    eft.PlayEFT(0);
                }


            }

            if (sc.gdr == 1)
            {
                eft.PlayEFT(24, gameObject, .1f);
                eft.PlayEFT(18, gameObject, 0.05f, 5);
                eft.PlayEFTM(Effect.EFT_TYPE.EFT_GDR); sc.gdr = 100;
            }
            if (sc.r == 1)
            {
                eft.PlayEFT(25, gameObject, .1f);
                eft.PlayEFT(19, gameObject, 0.05f, 5);
                eft.PlayEFTM(Effect.EFT_TYPE.EFT_hongdan); sc.r = 100;
            }
            if (sc.g == 1)
            {
                eft.PlayEFT(26, gameObject, .1f);
                eft.PlayEFT(20, gameObject, 0.05f, 5);
                eft.PlayEFTM(Effect.EFT_TYPE.EFT_chodan); sc.g = 100;
            }
            if (sc.b == 1)
            {
                eft.PlayEFT(27, gameObject, .1f);
                eft.PlayEFT(21, gameObject, 0.05f, 5);
                eft.PlayEFTM(Effect.EFT_TYPE.EFT_chongdan); sc.b = 100;
            }


            //if (Input.GetKeyDown(KeyCode.Alpha2)) Reset();
        }
    }

    private void Reset()
    {
        bombC = 0;
        bombP = 0;
        shakeC = 0;
        shitL.Clear();
        p1HandL.Clear();
        p1FPosL.Clear();
        p2FPosL.Clear();

        gm.Reset();
        sc.Reset();
    }
    public IEnumerator Invisible(GameObject obj, int t)
    {
        yield return new WaitForSeconds(t);
        obj.SetActive(false);
    }

    public IEnumerator Move()
    {
        yield return null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            hitObj = hit.transform.gameObject; //���̷� ���� obj�� hitObj

            #region  //p2AI ������
            if (player == 2)
            {
                if (p2HandL.Contains(hitObj)) // ������ �÷��̾�1�� �ڵ帮��Ʈ�� �ִٸ�
                {
                    yield return new WaitForSeconds(4);
                    idx = -1;

                    for (int j = 0; j < p1HandL.Count; j++)
                    {

                        for (int i = 0; i < gm.emptyL.Count; i++)
                        {

                            //���� �����̸�
                            if (gm.emptyL[i].occupy.Count > 0
                                && p1HandL[j].GetComponent<CardS>().num == gm.emptyL[i].occupy[0].GetComponent<CardS>().num)
                            {
                                idx = i;
                                idx2 = j;

                                break;
                            }

                            if (idx != -1) { hitObj = p1HandL[idx2]; }
                            else { hitObj = p1HandL[p1HandL.Count - 1]; }
                        }
                    }


                }
            }
            #endregion

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
                    eft.PlayEFTM(Effect.EFT_TYPE.EFT_jjak);
                    // ��ź
                    if (tripleC.Count == 3)
                    {
                        eft.PlayEFT(3, gm.emptyL[idx].occupy[0], 0.05f);
                        eft.PlayEFT(2, gm.emptyL[idx].occupy[0], 0.05f, 5);
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_bomb);
                        print("��ź");
                        bombC++;
                        bombP += 2;

                        gm.ActioniT(tripleC[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.005f), .1f);
                        gm.ActioniT(tripleC[1], spd + 0.2f, gm.emptyL[idx].pos + (Vector3.right * 0.005f * 2), .1f);
                        gm.ActioniT(tripleC[2], spd + 0.4f, gm.emptyL[idx].pos + (Vector3.right * 0.005f * 3), .1f);
                     

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

                    else
                    {
                        //�Ϲݻ�Ȳ ���� �ٴ����� �����̱�

                        gm.ActioniT(hitObj, spd, gm.emptyL[idx].pos + (Vector3.right * 0.005f), .1f);

                        gm.floorL.Add(hitObj); // ���а� �ٴ��а� �ƴ�.  

                        gm.emptyL[idx].occupy.Add(hitObj);
                    }

                    //��.. �� ��� hitobj
                    if (hitdoubleC.Count == 1 &&
                            hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                    {

                        shitL.Add(hitObj);
                        print("��" + shitL.Count);
                        eft.PlayEFT(6, gm.emptyL[idx].occupy[0], 0.05f);
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_shit);
                        

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                    }
                    //�ٴڿ� ���� ī�� 2���̸�
                    else if (hitdoubleC.Count == 2)
                    {
                        print("ī�弱��");
                        eft.PlayEFT(1, gm.emptyL[idx].occupy[0], gm.emptyL[idx].occupy[1], null);
                        c = true;
                        num = idx;

                    }
                    else
                    {

                        // �Ϲݻ�Ȳ ¦ ���� �� �� �ڸ��� �ű��
                        StartCoroutine(Move3(idx, p1FPosL, hitObj));

                        if (hitObj.name != gm.emptyL[idx].occupy[0].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0])); }

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));

                        //���� ī�� 3��
                        if (hitdoubleC.Count == 3)
                        {
                            if (
                                //gm.emptyL[idx].occupy.Count > 2 && 
                                tripleC.Count != 3)
                            {
                                GetShit(hitObj);
                            }

                        }

                    }

                }

                // ¦�� �ȸ��� ���
                else
                {
                   
                    if (tripleC != null && tripleC.Count == 3)
                    {
                        print("����?");
                        eft.PlayEFT(4, tripleC[0], tripleC[1], tripleC[2]);
                        s = true;
                    }
                    //�Ϲݻ�Ȳ ���ڸ�
                    else
                    {
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_tuk);
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

                    }
                    gm.floorL.Add(hitObj); // ���а� �ٴ��а� �ƴ�.

                }
                p1HandL.Remove(hitObj); // ���а� �̵������� ������
                gm.Sort(p1HandL, p1HandPos, spd);
            }

            gm.Rot();

        }
    }

    // �� �Ա�
    void GetShit(GameObject hitObj)
    {

        t = false;

        if (hitObj.name != gm.emptyL[idx].occupy[1].name)
        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1])); }
        if (hitObj.name != gm.emptyL[idx].occupy[2].name)
        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[2])); }

        for (int i = 0; i < shitL.Count; i++)
        {

            //���� �� ���� ���� Ŭ���� ���� ���� ������
            if (hitObj.GetComponent<CardS>().num == shitL[i].GetComponent<CardS>().num)
            {
                t = true;
                print("�ڻ�");
                eft.PlayEFT(8, gm.emptyL[idx].occupy[0], 0.1f);
                eft.PlayEFTM(Effect.EFT_TYPE.EFT_getshit);


                doubleC = p2FPosL[0].occupy.FindAll(obj => obj.GetComponent<CardS>().sameS(CardS.CARD_STATUS.TWO_PEE));
                print("����" + doubleC.Count);
                tripleC = p2FPosL[0].occupy.FindAll(obj => obj.GetComponent<CardS>().sameS(CardS.CARD_STATUS.KOOKJIN));
                print("����" + tripleC.Count);

                if (doubleC.Count > 0 || tripleC.Count > 0)
                {
                    if (doubleC.Count > 0)
                    {
                        TakeM(doubleC[0]);
                    }
                    else {
                        TakeM(tripleC[0]);
                    }
                    

                  //  for (int j = 0; j < p2FPosL[0].occupy.Count; j++)
                    //{

                    //    if (p2FPosL[0].occupy[j].GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN &&
                    //        p2FPosL[0].occupy[j].GetComponent<CardS>().state == CardS.CARD_STATUS.TWO_PEE)
                    //    {
                            
                    //        break;
                    //    }
                    //}

                }
                else { Take(); Take(); }

                break;



            }

        }

        if (!t)
        {
            eft.PlayEFT(7, gm.emptyL[idx].occupy[0], 0.05f);
            //����� ��������
            Take();
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_getshit);
            
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


        //���� ����Ʈ �����ʿ� ����ī�� ���°� �� ���� �ȵɶ� Ȯ��
        doubleC = gm.floorL.FindAll(obj => obj.GetComponent<CardS>().same(
                gm.cardL[0].GetComponent<CardS>().num));

        for (int j = 0; j < doubleC.Count; j++) { print("���� " + doubleC[j].name); }


        if (idx != -1)
        {
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_jjak);
            //��.. �� ���
            if (hitdoubleC.Count == 1 &&
               hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
            {

                //��0���� Ŭ���� �� ���̱�
                gm.ActioniT(gm.cardL[0], spd, hitObj.transform.position + (Vector3.right * 0.005f), .1f);
                gm.floorL.Add(gm.cardL[0]); // �̵��� �и� �ٴڸ���Ʈ�� �߰�
                gm.emptyL[idx].occupy.Add(gm.cardL[0]);

            }
            //�ٴڿ� ���� ī�� 2���̸�
            else if (gm.emptyL[idx].occupy.Count == 2
                && hitObj.GetComponent<CardS>().num != gm.cardL[0].GetComponent<CardS>().num)
            {
                print("ī�弱��");
                eft.PlayEFT(1, gm.emptyL[idx].occupy[0], gm.emptyL[idx].occupy[1], null);
                c = true;
                num = idx;

            }
            //�Ϲ� ��Ȳ
            else
            {


                //�� 0���� ¦�´� ī�忡 ���̱�
                gm.ActioniT(gm.cardL[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.005f), .1f);
                gm.floorL.Add(gm.cardL[0]); // �̵��� �и� �ٴڸ���Ʈ�� �߰�

                //�÷��̾� �ڸ��� ȸ��
                StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0]));
                StartCoroutine(Move3(idx, p1FPosL, gm.cardL[0]));




                //��
                if (doubleC.Count == 1 && hitdoubleC.Count == 0 &&
                  hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("��");
                    eft.PlayEFT(9, gm.emptyL[idx].occupy[0], 0.05f);
                    eft.PlayEFTM(Effect.EFT_TYPE.EFT_kiss);
                    eft.PlayEFT(4, gm.emptyL[idx].occupy[0], 0.05f, 5);
                    //����� ��������
                    Take();
                }

                //����
                if (tripleC != null && tripleC.Count == 1 && doubleC.Count == 1 && hitdoubleC.Count == 2 &&
                     hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("����");
                    eft.PlayEFT(10, gm.emptyL[idx].occupy[0], 0.05f);
                    eft.PlayEFTM(Effect.EFT_TYPE.EFT_kiss2);
                    eft.PlayEFT(5, gm.emptyL[idx].occupy[0], 0.05f, 5);
                    //����� ��������
                    Take();
                }

                //�� �Ա� + �ڻ�
                if (gm.emptyL[idx].occupy.Count > 2) { GetShit(gm.cardL[0]); }
            }
        }

        //¦ ���� ���
        else
        {
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_tuk);
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
        gm.Rot();
        StartCoroutine(AddScore());
    }

    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(2f);
        sc.AddP();
        sc.AddS();
        sc.AddG();
        sc.FinalS();
    }


    IEnumerator Move3(int idx, List<floor> p1FPosL, GameObject obj)
    // ¦���� �� p1p2 �÷ξ���ġ�� �̵�
    { // ����Ʈ �̸��� p1FPosL�̶� �Ű������� ���� �ٲ�� �ֵ��� ����. p1fl / p2fl ��� ���� ����

        yield return new WaitForSeconds(1f);
        int TY = (int)obj.GetComponent<CardS>().type;
        eft.PlayEFTM(Effect.EFT_TYPE.EFT_take);
        //������ ���� ���
        if (obj.GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
        {

            eft.PlayEFT(11);
            kj = true;
            print("��������");
        }

        p1FPosL[TY].occupy.Add(obj);
        gm.floorL.Remove(obj);
        gm.emptyL[idx].occupy.Remove(obj);

        gm.ActioniT(obj, spd, p1FPosL[TY].pos + Vector3.right * p1FPosL[TY].occupy.Count * 0.005f, .1f);

        //�Ͼ���
        if (gm.floorL.Count <= 0)
        {
            print("�Ͼ�");
            eft.PlayEFT(28, gm.cardL[0], .05f);
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_clean);
            eft.PlayEFT(22,  gameObject, .1f, 5);
            //����� ��������
            Take();
        }

        gm.Rot();

        if (bombP > 0) { eft.PlayEFT(eft.eftL.Count - 1); }

    }
    public void Take()
    {
        int n = -1;

        if (p2FPosL[0].occupy.Count > 0)
        {

            if (p2FPosL[0].occupy.Count == 1)
            {
                TakeM(p2FPosL[0].occupy[0]);
            }
            else
            {

                for (int i = 0; i < p2FPosL[0].occupy.Count; i++)
                {

                    if (p2FPosL[0].occupy[i].GetComponent<CardS>().state != CardS.CARD_STATUS.TWO_PEE
                     && p2FPosL[0].occupy[i].GetComponent<CardS>().state != CardS.CARD_STATUS.KOOKJIN)
                    {
                        n = i;
                        break;
                    }

                }

                if (n != -1)
                {
                    TakeM(p2FPosL[0].occupy[n]);
                }
                else
                {
                    TakeM(p2FPosL[0].occupy[n - 1]);
                }
            }
        }
    }

    void TakeM(GameObject obj) {

        if (p1FPosL[0].occupy.Count > 0)
        {
           obj.transform.position
                = p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position + Vector3.right * 0.005f;
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_take);
            #region
            //gm.ActioniT(
            // p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1],
            // spd,
            // p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position + Vector3.right * 0.3f,
            // .2f);
            #endregion
        }
        else
        {

           obj.transform.position
                = p1FPosL[0].pos;
            #region
            //gm.ActioniT(
            // p2FPosL[0].occupy[p2FPosL[0].occupy.Count - 1],
            //spd,
            // p1FPosL[0].pos,
            // .2f);
          
            #endregion 
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_take);
        }


        p1FPosL[0].occupy.Add(obj);

        p2FPosL[0].occupy.Remove(obj);

    }

}

