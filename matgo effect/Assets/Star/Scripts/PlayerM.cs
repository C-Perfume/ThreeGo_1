using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    GameObject hitObj;//레이로 맞은 놈을 담는 변수
    public int player;

    int idx; // 카드 이동 시 같은 값을 찾는데 쓴다. obj의 위치를 찾아야해서 bool 말고 인트로 사용함
    bool s = false; // 같은 카드 두장인 경우 선택할 때 쓴다.
    int idx2; // p2AI용

    //딜레이시간
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

    //p2AI용
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
            if (bombP > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    hitObj = hit.transform.gameObject; //레이로 맞은 obj는 hitObj

                    #region//p2AI 움직임
                    //{
                    //    {
                    //        StartCoroutine(Move2(p2FPosL, p1FPosL));
                    //        bombC--;
                    //    }
                    //}
                    #endregion

                    if (gm.cardL.Contains(hitObj)) // 맞은게 플레이어1번 핸드리스트에 있다면
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

            //카드 두장 중 선택
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

            hitObj = hit.transform.gameObject; //레이로 맞은 obj는 hitObj

            //p2AI 움직임
            //if (player == 2)
            //{
            //    if (p2HandL.Contains(hitObj)) // 맞은게 플레이어1번 핸드리스트에 있다면
            //    {
            //        yield return new WaitForSeconds(4);
            //        idx = -1;

            //        for (int j = 0; j < p1HandL.Count; j++)
            //        {

            //            for (int i = 0; i < gm.emptyL.Count; i++)
            //            {

            //                //같은 월값이면
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
                  
                    // 폭탄
                    if (tripleC.Count == 3)
                    {
                        print("폭탄");
                        bombC++;
                        bombP += 2;

                        gm.ActioniT(tripleC[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);
                        gm.ActioniT(tripleC[1], spd + 0.2f, gm.emptyL[idx].pos + (Vector3.right * 0.3f * 2), .1f);
                        gm.ActioniT(tripleC[2], spd + 0.4f, gm.emptyL[idx].pos + (Vector3.right * 0.3f * 3), .1f);

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

                    //일반상황 손패 바닥으로 움직이기
                    else
                    {

                        gm.ActioniT(hitObj, spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);

                        gm.floorL.Add(hitObj); // 손패가 바닥패가 됐다.  

                        gm.emptyL[idx].occupy.Add(hitObj);

                    }


                    //뻑.. 싼 경우 hitobj
                    if (hitdoubleC.Count == 1 &&
                           hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                    {
                        print("뻑");
                        shitL.Add(hitObj);
                        print("뻑" + shitL.Count);

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                    }
                    //바닥에 같은 카드 2장이면
                    else if (hitdoubleC.Count == 2)
                    {
                        print("카드선택");
                        eft.PlayEFT1(Effect.EFT.Choose, gm.emptyL[idx].occupy[0], gm.emptyL[idx].occupy[1]);
                        s = true;
                        StartCoroutine(Move3(idx, p1FPosL, hitObj));
                    
                    }
                    else
                    { 

                        // 일반상황 짝 맞은 패 내 자리로 옮기기
                        StartCoroutine(Move3(idx, p1FPosL, hitObj));

                        if (hitObj.name != gm.emptyL[idx].occupy[0].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0])); }

                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));
                    }

                    // 뻑 먹기
                    if (gm.emptyL[idx].occupy.Count > 3)
                    {
                        StartCoroutine(Move2(p1FPosL, p2FPosL, hitObj));

                        if (hitObj.name != gm.emptyL[idx].occupy[1].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1])); }
                        if (hitObj.name != gm.emptyL[idx].occupy[2].name)
                        { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[2])); }

                        for (int i = 0; i < shitL.Count; i++)
                        {

                            //내가 싼 뻑의 값과 클릭한 놈의 값이 같으면
                            if (hitObj.GetComponent<CardS>().num == shitL[i].GetComponent<CardS>().num)
                            {

                                print("자뻑");
                                //상대피 가져오기
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

                        //상대피 가져오기
                        Take();
                    }

                }

                // 짝이 안맞은 경우
                else
                {

                    if (tripleC != null && tripleC.Count == 3)
                    {
                        print("흔들기?");
                    }

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
                    gm.floorL.Add(hitObj); // 손패가 바닥패가 됐다.

                }
                p1HandL.Remove(hitObj); // 손패가 이동했으니 빼주자

            }

            gm.Rot();
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


        //뻑용 리스트
        doubleC = gm.floorL.FindAll(obj => obj.GetComponent<CardS>().same(
                hitObj.GetComponent<CardS>().num));

        // for (int j = 0; j < doubleC.Count; j++) { print("더블 " + doubleC[j].name); }


        if (idx != -1)
        {

            //뻑.. 싼 경우
            if (doubleC.Count > 1 && tripleC.Count > 0
               && hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
            {

                //덱0번을 클릭한 놈에 붙이기
                gm.ActioniT(gm.cardL[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);
                gm.floorL.Add(gm.cardL[0]); // 이동된 패를 바닥리스트에 추가
                gm.emptyL[idx].occupy.Add(gm.cardL[0]);

            }

            //일반 상황
            else
            {


                //덱 0번을 짝맞는 카드에 붙이기
                gm.ActioniT(gm.cardL[0], spd, gm.emptyL[idx].pos + (Vector3.right * 0.3f), .1f);
                gm.floorL.Add(gm.cardL[0]); // 이동된 패를 바닥리스트에 추가

                //플레이어 자리로 회수
                StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[0]));
                StartCoroutine(Move3(idx, p1FPosL, gm.cardL[0]));




                //쪽
                if (doubleC.Count == 1 && hitdoubleC.Count == 0 &&
                  hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("쪽");
                    //상대패 가져오기
                    Take();

                }

                //따닥
                if (tripleC != null && tripleC.Count == 1 && doubleC.Count == 1 && hitdoubleC.Count == 2 &&
                     hitObj.GetComponent<CardS>().num == gm.cardL[0].GetComponent<CardS>().num)
                {
                    print("따닥");
                    //상대패 가져오기
                    Take();

                }




                // 뻑 먹기
                if (gm.emptyL[idx].occupy.Count > 3)
                {

                    if (gm.cardL[0].name != gm.emptyL[idx].occupy[1].name)
                    { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[1])); }
                    if (gm.cardL[0].name != gm.emptyL[idx].occupy[2].name)
                    { StartCoroutine(Move3(idx, p1FPosL, gm.emptyL[idx].occupy[2])); }


                    //상대피 가져오기
                    Take();
                }
            }
        }

        //짝 없는 경우
        else
        {

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
    // 짝맞은 패 p1p2 플로어위치로 이동
    { // 리스트 이름이 p1FPosL이라도 매개변수로 빼서 바뀔수 있도록 해줌. p1fl / p2fl 모두 적용 가능

        yield return new WaitForSeconds(1f);
        int TY = (int)obj.GetComponent<CardS>().type;
        
        //국진을 먹은 경우
        if (obj.GetComponent<CardS>().state == CardS.CARD_STATUS.KOOKJIN)
            {
                // 선택
                print("국진선택");
               
                //쌍피라면
                //obj.transform.position = p1FPosL[0].occupy[p1FPosL[0].occupy.Count - 1].transform.position;
                //sc.currPee++;
            }

        p1FPosL[TY].occupy.Add(obj);
        gm.floorL.Remove(obj);
        gm.emptyL[idx].occupy.Remove(obj);

        gm.ActioniT(obj, spd, p1FPosL[TY].pos + Vector3.right * p1FPosL[TY].occupy.Count * 0.3f, .1f);

        gm.Rot();


        //싹쓸이
        if (gm.floorL.Count <= 0)
        {
            print("싹쓸");
            //상대피 가져오기
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

