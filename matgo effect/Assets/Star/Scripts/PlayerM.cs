using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerM : MonoBehaviour
{
    public int player;

    int num = -1;
    int idx; // 카드 이동 시 같은 값을 찾는데 쓴다. obj의 위치를 찾아야해서 bool 말고 인트로 사용함
    bool c = false; // 같은 카드 두장인 경우 선택할 때 쓴다.
    bool s = false; // 흔들여부
    bool t = false; // 자뻑용
    bool kj = false; // 국진
    public bool reset = false; // 게임 종료 후 다시할지 끝낼지 여부
    GameObject select = null;
    GameObject or = null;

    //딜레이시간
    float spd = 0.1f;

    GameObject hitObj;//레이로 맞은 놈을 담는 변수

    public int bombC = 0; //폭탄 카운트
    public int bombP = 0; // 폭탄 후 2피
    public int shakeC = 0; // 흔든 숫자

    GameManager gm;
    Score sc;
    PlayerM p2m;
    Effect eft;

    List<GameObject> tripleC; // 손패 같은카드 3장
    List<GameObject> hitdoubleC; //  손패와 바닥패 같은거
    List<GameObject> doubleC; // 바닥패만 같은거
    public List<GameObject> shitL = new List<GameObject>(); // 싼 횟수

    public List<GameObject> p1HandL; // p1지정용
    public List<floor> p1FPosL;
    public List<floor> p2FPosL;
    public Transform[] p1HandPos;

    //p2AI용
    int idx2; // p2AI용
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


    //바닥에 같은 월 카드 4장 놔지는 경우 p1에게 먹기
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

                        hitObj = hit.transform.gameObject; //레이로 맞은 obj는 hitObj

                        #region//p2AI 움직임
                        if (player == 2)
                        {
                            {
                                StartCoroutine(Move2(p2FPosL, p1FPosL, gm.cardL[0]));
                                bombP--;
                            }
                        }
                        #endregion

                        if (gm.cardL.Contains(hitObj)) // 맞은게 덱리스트에 있다면
                        {
                            StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                            bombP--;
                        }
                        // 폭탄피가 2장이라도 손패를 낼 수 있다.
                        else
                        {
                            StartCoroutine(Move());
                        }
                    }

                }
                // 일반 상황
                else
                {
                    StartCoroutine(Move());
                }


                //카드 두장 중 선택
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

                //흔들 중 선택
                if (s)
                {
                    or = eft.Or();
                    if (player == 2) {

                        {
                            shakeC++;
                            //5번 흔들었다 활성화 후 카드 복제
                            eft.PlayEFT(5,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject
                                );

                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_shaking);

                            //4번 비활성화 후 4번 복제카드 삭제 
                            eft.eftL[4].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 6).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 5).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 4).gameObject);
                            //5번 복제 카드도 2초 후 삭제
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject, 2);
                            //5번 비활성화
                            StartCoroutine(Invisible(eft.eftL[5], 2));


                            //손패와 덱카드 움직이기
                            for (int i = 0; i < gm.emptyL.Count; i++)
                            {

                                if (gm.emptyL[i].occupy.Count == 0)
                                {
                                    //빈자리로 간다.
                                    gm.ActioniT(hitObj, spd, gm.emptyL[i].pos, .1f);
                                    gm.emptyL[i].occupy.Add(hitObj);
                                    idx = -1;

                                    StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                                    break;

                                }
                            }

                        }
                        // 흔들기 비활성화
                        s = false;
                    
                }

                    if (or != null)
                    {
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_tuk);
                        // 아니다 선택
                        if (or.name.Contains("c2"))
                        {
                            //비활성화 후 복제카드 폭파
                            eft.eftL[4].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject);

                            // 손패 움직이고 덱카드 움직이기
                            for (int i = 0; i < gm.emptyL.Count; i++)
                            {

                                if (gm.emptyL[i].occupy.Count == 0)
                                {
                                    //빈자리로 간다.
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
                            //5번 흔들었다 활성화 후 카드 복제
                            eft.PlayEFT(5,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject,
                                eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject
                                );

                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_shaking);

                            //4번 비활성화 후 4번 복제카드 삭제 
                            eft.eftL[4].SetActive(false);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 6).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 5).gameObject);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 4).gameObject);
                            //5번 복제 카드도 2초 후 삭제
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 3).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 2).gameObject, 2);
                            Destroy(eft.gameObject.transform.GetChild(eft.gameObject.transform.childCount - 1).gameObject, 2);
                            //5번 비활성화
                            StartCoroutine(Invisible(eft.eftL[5], 2));


                            //손패와 덱카드 움직이기
                            for (int i = 0; i < gm.emptyL.Count; i++)
                            {

                                if (gm.emptyL[i].occupy.Count == 0)
                                {
                                    //빈자리로 간다.
                                    gm.ActioniT(hitObj, spd, gm.emptyL[i].pos, .1f);
                                    gm.emptyL[i].occupy.Add(hitObj);
                                    idx = -1;

                                    StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                                    break;

                                }
                            }

                        }
                        // 흔들기 비활성화
                        s = false;
                    }

                }

                //국진팝업
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
                        // 열끗 선택
                        if (or.name.Contains("c2"))
                        {
                            eft.eftL[11].SetActive(false);
                        }

                        //쌍피라면
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


                //고스톱묻기
                if (sc.gc)
                {
                    or = eft.Or();

                    if (player == 2) {
                        eft.eftL[2].SetActive(false);
                        //고 효과
                        sc.goCnt++;
                        eft.PlayEFT(12, gameObject, .05f);
                        eft.PlayEFT(6, gameObject, .05f, 5);
                        eft.PlayGo(sc.goCnt);
                        print(sc.goCnt + "고");
                        sc.goScore = sc.currScore;
                        sc.go.text = "Go : " + sc.goCnt.ToString();
                    
                    sc.gc = false;

                                                 }

                    if (or != null)
                    {
                        // 아니당 선택
                        if (or.name.Contains("c2"))
                        {
                            eft.eftL[2].SetActive(false);


                            sc.result.text = "이겼당!";
                            eft.PlayEFT(0, gameObject, .05f, 5);
                            eft.PlayEFTM(Effect.EFT_TYPE.win);

                            //스톱 이펙트
                            eft.PlayEFT(13, gameObject, .05f);
                            eft.PlayEFTM(Effect.EFT_TYPE.EFT_Stop);
                            // 최종점수txt
                            eft.PlayEFT(0);
                            reset = true;
                        }

                        //고하기
                        else
                        {
                            eft.eftL[2].SetActive(false);
                            //고 효과
                            sc.goCnt++;
                            eft.PlayEFT(12, gameObject, .05f);
                            eft.PlayEFT(6, gameObject, .05f, 5);
                            eft.PlayGo(sc.goCnt);
                            print(sc.goCnt + "고");
                            sc.goScore = sc.currScore;
                            sc.go.text = "Go : " + sc.goCnt.ToString();
                        }
                        sc.gc = false;
                    }
                }


                //게임종료? 다시하기?
                if (reset)
                {
                    or = eft.Or();

                    if (player == 2) {
                        eft.eftL[0].SetActive(false);
                        Reset();
                    }
                    
                    if (or != null)
                    {

                        //끝
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

            hitObj = hit.transform.gameObject; //레이로 맞은 obj는 hitObj

            #region  //p2AI 움직임
            if (player == 2)
            {
                if (p2HandL.Contains(hitObj)) // 맞은게 플레이어1번 핸드리스트에 있다면
                {
                    yield return new WaitForSeconds(4);
                    idx = -1;

                    for (int j = 0; j < p1HandL.Count; j++)
                    {

                        for (int i = 0; i < gm.emptyL.Count; i++)
                        {

                            //같은 월값이면
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

            if (p1HandL.Contains(hitObj)) // 맞은게 플레이어1번 핸드리스트에 있다면
            {

                //폭탄용 리스트 
                //가진 패 중 hitobj와 같은 값을 가진 것만 tripleC[0], [1], [2]..리스트로 담김
                // 한번 3패를 내고 리셋해야 되는 줄 알았는데 자동으로 됨
                tripleC = p1HandL.FindAll(obj => obj.GetComponent<CardS>().same(
                hitObj.GetComponent<CardS>().num));

                //뻑, 따닥용..
                hitdoubleC = gm.floorL.FindAll(obj => obj.GetComponent<CardS>().same(
                         hitObj.GetComponent<CardS>().num));

                //  for (int j = 0; j < hitdoubleC.Count; j++) { print("hitobj더블 " + hitdoubleC[j].name); }
                // for (int j = 0; j < tripleC.Count; j++) { print("트리플 " + tripleC[j].name); }

                idx = -1;

                for (int i = 0; i < gm.emptyL.Count; i++)
                {

                    //같은 월값이면
                    if (gm.emptyL[i].occupy.Count > 0
                        && hitObj.GetComponent<CardS>().num == gm.emptyL[i].occupy[0].GetComponent<CardS>().num)
                    { idx = i; break; }

                }

                //짝이 맞으면
                if (idx != -1)
                {
                    eft.PlayEFTM(Effect.EFT_TYPE.EFT_jjak);
                    // 폭탄
                    if (tripleC.Count == 3)
                    {
                        eft.PlayEFT(3, gm.emptyL[idx].occupy[0], 0.05f);
                        eft.PlayEFT(2, gm.emptyL[idx].occupy[0], 0.05f, 5);
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_bomb);
                        print("폭탄");
                        bombC++;
                        bombP += 2;

                        gm.ActioniT(tripleC[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.005f), .1f);
                        gm.ActioniT(tripleC[1], spd + 0.2f, gm.emptyL[idx].pos + (Vector3.right * 0.005f * 2), .1f);
                        gm.ActioniT(tripleC[2], spd + 0.4f, gm.emptyL[idx].pos + (Vector3.right * 0.005f * 3), .1f);
                     

                        gm.floorL.Add(tripleC[0]); // 폭탄0이 바닥패에 추가  
                        gm.emptyL[idx].occupy.Add(tripleC[0]);
                        gm.floorL.Add(tripleC[1]); // 폭탄1 추가  
                        gm.emptyL[idx].occupy.Add(tripleC[1]);
                        gm.floorL.Add(tripleC[2]); //  폭탄2 추가
                        gm.emptyL[idx].occupy.Add(tripleC[2]);

                        if (hitObj.name != gm.emptyL[idx].occupy[3].name) //폭탄패 중 남은거 움직이기
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[3])); }


                        for (int i = 0; i < tripleC.Count; i++)
                        {

                            if (hitObj.name != tripleC[i].name)
                            { p1HandL.Remove(tripleC[i]); } //손패에서 빼주기

                        }

                        //상대피 가져오기
                        Take();
                    }

                    else
                    {
                        //일반상황 손패 바닥으로 움직이기

                        gm.ActioniT(hitObj, spd, gm.emptyL[idx].pos + (Vector3.right * 0.005f), .1f);

                        gm.floorL.Add(hitObj); // 손패가 바닥패가 됐다.  

                        gm.emptyL[idx].occupy.Add(hitObj);
                    }

                    //뻑.. 싼 경우 hitobj
                    if (hitdoubleC.Count == 1 &&
                            hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                    {

                        shitL.Add(hitObj);
                        print("뻑" + shitL.Count);
                        eft.PlayEFT(6, gm.emptyL[idx].occupy[0], 0.05f);
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_shit);
                        

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                    }
                    //바닥에 같은 카드 2장이면
                    else if (hitdoubleC.Count == 2)
                    {
                        print("카드선택");
                        eft.PlayEFT(1, gm.emptyL[idx].occupy[0], gm.emptyL[idx].occupy[1], null);
                        c = true;
                        num = idx;

                    }
                    else
                    {

                        // 일반상황 짝 맞은 패 내 자리로 옮기기
                        StartCoroutine(Move3(idx, p1FPosL, hitObj));

                        if (hitObj.name != gm.emptyL[idx].occupy[0].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0])); }

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));

                        //같은 카드 3장
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

                // 짝이 안맞은 경우
                else
                {
                   
                    if (tripleC != null && tripleC.Count == 3)
                    {
                        print("흔들기?");
                        eft.PlayEFT(4, tripleC[0], tripleC[1], tripleC[2]);
                        s = true;
                    }
                    //일반상황 빈자리
                    else
                    {
                        eft.PlayEFTM(Effect.EFT_TYPE.EFT_tuk);
                        for (int i = 0; i < gm.emptyL.Count; i++)
                        {

                            if (gm.emptyL[i].occupy.Count == 0)
                            {
                                //빈자리로 간다.
                                gm.ActioniT(hitObj, spd, gm.emptyL[i].pos, .1f);
                                gm.emptyL[i].occupy.Add(hitObj);
                                idx = -1;

                                StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                                break;

                            }
                        }

                    }
                    gm.floorL.Add(hitObj); // 손패가 바닥패가 됐다.

                }
                p1HandL.Remove(hitObj); // 손패가 이동했으니 빼주자
                gm.Sort(p1HandL, p1HandPos, spd);
            }

            gm.Rot();

        }
    }

    // 뻑 먹기
    void GetShit(GameObject hitObj)
    {

        t = false;

        if (hitObj.name != gm.emptyL[idx].occupy[1].name)
        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1])); }
        if (hitObj.name != gm.emptyL[idx].occupy[2].name)
        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[2])); }

        for (int i = 0; i < shitL.Count; i++)
        {

            //내가 싼 뻑의 값과 클릭한 놈의 값이 같으면
            if (hitObj.GetComponent<CardS>().num == shitL[i].GetComponent<CardS>().num)
            {
                t = true;
                print("자뻑");
                eft.PlayEFT(8, gm.emptyL[idx].occupy[0], 0.1f);
                eft.PlayEFTM(Effect.EFT_TYPE.EFT_getshit);


                doubleC = p2FPosL[0].occupy.FindAll(obj => obj.GetComponent<CardS>().sameS(CardS.CARD_STATUS.TWO_PEE));
                print("투피" + doubleC.Count);
                tripleC = p2FPosL[0].occupy.FindAll(obj => obj.GetComponent<CardS>().sameS(CardS.CARD_STATUS.KOOKJIN));
                print("국진" + tripleC.Count);

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
            //상대피 가져오기
            Take();
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_getshit);
            
        }

    }

    IEnumerator Move2(List<floor> p1FPosL, List<floor> p2FPosL, GameObject hitObj) // 덱에서 피이동
    {
        yield return new WaitForSeconds(1.5f);
        idx = -1;

        for (int i = 0; i < gm.floorL.Count; i++)
        {
            //같은 월이면
            if (gm.emptyL[i].occupy.Count > 0
            && gm.cardL[0].GetComponent<CardS>().num == gm.emptyL[i].occupy[0].GetComponent<CardS>().num)
            {

                idx = i;
                break;

            }
        }


        //뻑용 리스트 수정필요 같은카드 내는거 될 때랑 안될때 확인
        doubleC = gm.floorL.FindAll(obj => obj.GetComponent<CardS>().same(
                gm.cardL[0].GetComponent<CardS>().num));

        for (int j = 0; j < doubleC.Count; j++) { print("더블 " + doubleC[j].name); }


        if (idx != -1)
        {
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_jjak);
            //뻑.. 싼 경우
            if (hitdoubleC.Count == 1 &&
               hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
            {

                //덱0번을 클릭한 놈에 붙이기
                gm.ActioniT(gm.cardL[0], spd, hitObj.transform.position + (Vector3.right * 0.005f), .1f);
                gm.floorL.Add(gm.cardL[0]); // 이동된 패를 바닥리스트에 추가
                gm.emptyL[idx].occupy.Add(gm.cardL[0]);

            }
            //바닥에 같은 카드 2장이면
            else if (gm.emptyL[idx].occupy.Count == 2
                && hitObj.GetComponent<CardS>().num != gm.cardL[0].GetComponent<CardS>().num)
            {
                print("카드선택");
                eft.PlayEFT(1, gm.emptyL[idx].occupy[0], gm.emptyL[idx].occupy[1], null);
                c = true;
                num = idx;

            }
            //일반 상황
            else
            {


                //덱 0번을 짝맞는 카드에 붙이기
                gm.ActioniT(gm.cardL[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.005f), .1f);
                gm.floorL.Add(gm.cardL[0]); // 이동된 패를 바닥리스트에 추가

                //플레이어 자리로 회수
                StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0]));
                StartCoroutine(Move3(idx, p1FPosL, gm.cardL[0]));




                //쪽
                if (doubleC.Count == 1 && hitdoubleC.Count == 0 &&
                  hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("쪽");
                    eft.PlayEFT(9, gm.emptyL[idx].occupy[0], 0.05f);
                    eft.PlayEFTM(Effect.EFT_TYPE.EFT_kiss);
                    eft.PlayEFT(4, gm.emptyL[idx].occupy[0], 0.05f, 5);
                    //상대패 가져오기
                    Take();
                }

                //따닥
                if (tripleC != null && tripleC.Count == 1 && doubleC.Count == 1 && hitdoubleC.Count == 2 &&
                     hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("따닥");
                    eft.PlayEFT(10, gm.emptyL[idx].occupy[0], 0.05f);
                    eft.PlayEFTM(Effect.EFT_TYPE.EFT_kiss2);
                    eft.PlayEFT(5, gm.emptyL[idx].occupy[0], 0.05f, 5);
                    //상대패 가져오기
                    Take();
                }

                //뻑 먹기 + 자뻑
                if (gm.emptyL[idx].occupy.Count > 2) { GetShit(gm.cardL[0]); }
            }
        }

        //짝 없는 경우
        else
        {
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_tuk);
            for (int i = 0; i < gm.emptyL.Count; i++)
            {

                if (gm.emptyL[i].occupy.Count == 0)
                {
                    //빈자리로 가기
                    gm.ActioniT(gm.cardL[0], spd, gm.emptyL[i].pos, .1f);
                    gm.emptyL[i].occupy.Add(gm.cardL[0]);
                    idx = -1;
                    break;

                }
            }

            gm.floorL.Add(gm.cardL[0]); // 이동된 패를 바닥리스트에 추가
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
    // 짝맞은 패 p1p2 플로어위치로 이동
    { // 리스트 이름이 p1FPosL이라도 매개변수로 빼서 바뀔수 있도록 해줌. p1fl / p2fl 모두 적용 가능

        yield return new WaitForSeconds(1f);
        int TY = (int)obj.GetComponent<CardS>().type;
        eft.PlayEFTM(Effect.EFT_TYPE.EFT_take);
        //국진을 먹은 경우
        if (obj.GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
        {

            eft.PlayEFT(11);
            kj = true;
            print("국진선택");
        }

        p1FPosL[TY].occupy.Add(obj);
        gm.floorL.Remove(obj);
        gm.emptyL[idx].occupy.Remove(obj);

        gm.ActioniT(obj, spd, p1FPosL[TY].pos + Vector3.right * p1FPosL[TY].occupy.Count * 0.005f, .1f);

        //싹쓸이
        if (gm.floorL.Count <= 0)
        {
            print("싹쓸");
            eft.PlayEFT(28, gm.cardL[0], .05f);
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_clean);
            eft.PlayEFT(22,  gameObject, .1f, 5);
            //상대피 가져오기
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

