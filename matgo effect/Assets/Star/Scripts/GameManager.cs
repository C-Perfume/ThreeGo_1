using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class floor
{
    public List<GameObject> occupy = new List<GameObject>();
    public Vector3 pos;
}
public class GameManager : MonoBehaviour
{
    //** floorL 추가하는거 다시 확인하기 / 스크립트 두개로 바꿔보기

    #region 변수모음
    public Sprite[] imgs; // 화투이미지48개
    public Transform[] deckPos; // 바닥패위치
    public Transform zero; // 카드덱 0,0,0이다.
    public Transform[] p1HandPos;
    public Transform[] p2HandPos;
    public Transform[] p1FPos; //광열띠피
    public Transform[] p2FPos; //광열띠피

    public List<GameObject> saveCL = new List<GameObject>();
    public List<GameObject> cardL = new List<GameObject>(); //모든 패를 담는 리스트
                                                            //(num의 같은 순서로 부여된 값을 가져오려면 순번을 찾을 수 있는 리스트로 하는게 필요했다..
                                                            //배열은 순번을 가져올 수 있는 함수?가 안찾아진다.
                                                            //강사님이 카드속성 스크립트를 따로 만들어서 굳이 순번을 가져올 필요가 없어졌다.
                                                            //아쉽..(cardL.IndexOf(hitObj);)
    public List<GameObject> p1HandL = new List<GameObject>();
    public List<GameObject> p2HandL = new List<GameObject>();
    public List<GameObject> floorL = new List<GameObject>(); // 바닥패 갯수 확인용 emptyL.occupy 리스트 갯수를 셀수가 없다..


    public List<floor> emptyL = new List<floor>();
    //현재 바닥 빈공간의 위치값을 저장하기 위한 리스트..를 만들었다가 강사님의 도움으로 클래스>bool속성을 통해 처리 변경함
    // 배열에서 리스트로 변경 함..추가 제거가 필요하다 ㅠㅠ
    public List<floor> p1FPosL = new List<floor>();
    public List<floor> p2FPosL = new List<floor>();

    List<GameObject> tripleC;
    Effect eft;
    #endregion


    void Start()
    {
        GameStart();
        eft.PlayBGM(Effect.EFT_TYPE.BGM);
    }

    void Update()
    {
        
    }
        
    void GameStart() {
        eft = Effect.instance;
        SetCard();
        Shuffle();
        StartCoroutine(Distribute());
    }

    public void Reset()
    {
        cardL.Clear();
        p1HandL.Clear();
        p2HandL.Clear();
        floorL.Clear();
        emptyL.Clear();
        p1FPosL.Clear();
        p2FPosL.Clear();

        for (int i = 0; i < saveCL.Count; i++)
        {
          cardL.Add(saveCL[i]);
          cardL[i].transform.position = zero.position;
          cardL[i].transform.eulerAngles = new Vector3(90, 0, 0);
          cardL[i].transform.Rotate(180, 0, 0);
        }
        GameStart();
    }

    void SetCard()
    {

        for (int j = 0; j < 12; j++)
        {
            for (int i = 1; i < 5; i++)
            {
                cardL[i - 1 + (j * 4)].GetComponent<CardS>().num = 1 + j; // 월값 설정
            }
        } // Card 스크립트에 월값 설정

        for (int i = 0; i < cardL.Count; i++)
        {
            cardL[i].GetComponent<SpriteRenderer>().sprite = imgs[i]; // 이미지 설정
            cardL[i].transform.position += Vector3.right * 0.0005f * i;
        } // 카드이미지

        for (int i = 0; i < deckPos.Length; i++)
        {
            floor info = new floor(); // 한번 나왔다가 사라지는 임시변수 for문을 돌때마다 생성>삭제를 반복한다.
            info.pos = deckPos[i].position;
            emptyL.Add(info); // 여기에 추가된 인포는 empty[i]번 으로 남게된다.
        } // 바닥 패공간 

        for (int i = 0; i < p1FPos.Length; i++)
        {
            floor info = new floor();
            info.pos = p1FPos[i].position;
            p1FPosL.Add(info);
        } // 바닥 플레이어 1번 패공간 

        for (int i = 0; i < p1FPos.Length; i++)
        {
            floor info = new floor();
            info.pos = p2FPos[i].position;
            p2FPosL.Add(info);
        } // 바닥 플레이어 2번 패공간 
    }

    
    
     void Shuffle() // 두값을 여러번 섞어준다. 그게 셔플이다..
    {
        for (int i = 0; i < 100; i++)
        {
            int j = Random.Range(12, cardL.Count); // cardL.Count
            int k = Random.Range(12, cardL.Count);
            GameObject Save = cardL[j];
            cardL[j] = cardL[k];
            cardL[k] = Save;
        }

        //따닥설정
        GameObject t = cardL[1];
        cardL[1] = cardL[32];
        cardL[32] = t;


        GameObject a = cardL[2];
        cardL[2] = cardL[8];
        cardL[8] = a;

        //GameObject b = cardL[3];
        //cardL[3] = cardL[6];
        //cardL[6] = b;

        GameObject c = cardL[5];
        cardL[5] = cardL[33];
        cardL[33] = c;

    }



    IEnumerator Distribute()
    {

        for (int redo = 0; redo < 2; redo++)// 2번반복
        {
            eft.PlayEFTM(Effect.EFT_TYPE.EFT_ssg);

            for (int i = 0; i <= 4; i++)//p1패 분배
            {
                
                ActioniT(cardL[0], 1 + (i * 0.2f), p1HandPos[i + (redo * 5)].position, .15f);
                p1HandL.Add(cardL[0]);
                cardL.RemoveAt(0);

            }

            for (int i = 0; i <= 3; i++) // 바닥패분배
            {
                //eft.PlayEFTM(Effect.EFT_TYPE.EFT_ssg);
                ActioniT(cardL[0], i * 0.2f, deckPos[i + (redo * 4)].position, .15f);
                emptyL[i + (redo * 4)].occupy.Add(cardL[0]);
                floorL.Add(cardL[0]);
                cardL.RemoveAt(0);
            
            }

            for (int i = 0; i <= 4; i++) // p2패분배
            {
                //eft.PlayEFTM(Effect.EFT_TYPE.EFT_ssg);
                //cardL[0].GetComponent<SpriteRenderer>().sprite = back;
                ActioniT(cardL[0], 2 + (i * 0.2f), p2HandPos[i + (redo * 5)].position, .15f);
                p2HandL.Add(cardL[0]);
                cardL.RemoveAt(0);
            
            }
        
            yield return new WaitForSeconds(3);
        }
        
        Rot();
        Sort();

        PlayerM pm = GameObject.Find("P1").GetComponent<PlayerM>();
        //PlayerM pm2 = GameObject.Find("P2").GetComponent<PlayerM>();
        Score sc = GameObject.Find("P1").GetComponent<Score>();
        Score sc2 = GameObject.Find("P2").GetComponent<Score>();
        sc.Card4();
        pm.Card4F();
    }

  
    //카드정렬
    public void Sort()
    {
        //바닥패 정렬
        for (int i = 0; i < floorL.Count; i++)
        {

            int k = 1;
            for (int j = i + 1; j < floorL.Count; j++)
            {

                if (floorL[i].GetComponent<CardS>().num == floorL[j].GetComponent<CardS>().num)
                {

                    floorL[j].transform.position = floorL[i].transform.position + Vector3.right * 0.005f * k;

                    if (emptyL[j].occupy.Count > 0) emptyL[i].occupy.Add(emptyL[j].occupy[0]);
                    if (emptyL[j].occupy.Count > 0) emptyL[j].occupy.RemoveAt(0);
                    k++;
                }

            }

            k = 1;
        }


        // p1 손패 정렬
        // 버블 솔트를 다시 배워야 하나?ㅜㅜ
        for (int j = 0; j < p1HandL.Count; j++)
        {

            for (int i = 0; i < p1HandL.Count - 1; i++)
            {

                if (p1HandL[i].GetComponent<CardS>().num > p1HandL[i + 1].GetComponent<CardS>().num)
                {

                    GameObject a = p1HandL[i];
                    p1HandL[i] = p1HandL[i + 1];
                    p1HandL[i + 1] = a;

                    Vector3 save = p1HandL[i].transform.position;
                    p1HandL[i].transform.position = p1HandL[i + 1].transform.position;
                    p1HandL[i + 1].transform.position = save;
                    // 패 모양의 위치 뿐 아니라 패오브젝트의 배열도 수정해야된다. 

                }
            }
        }


        // p2 손패 정렬
        for (int j = 0; j < p2HandL.Count; j++)
        {

            for (int i = 0; i < p2HandL.Count - 1; i++)
            {

                if (p2HandL[i].GetComponent<CardS>().num > p2HandL[i + 1].GetComponent<CardS>().num)
                {

                    GameObject a = p2HandL[i];
                    p2HandL[i] = p2HandL[i + 1];
                    p2HandL[i + 1] = a;

                    Vector3 save = p2HandL[i].transform.position;
                    p2HandL[i].transform.position = p2HandL[i + 1].transform.position;
                    p2HandL[i + 1].transform.position = save;
                    // 패 모양의 위치 뿐 아니라 패오브젝트의 배열도 수정해야된다. 

                }
            }
        }
    }

    public void Sort(List<GameObject> p1HandL , Transform[] p1HandPos, float spd) {
      
        for (int i = 0; i < p1HandL.Count; i++)
        {
            ActioniT(p1HandL[i], spd, p1HandPos[i].position, .1f);
        }
    }


    //카드회전
    public void Rot()
    {
        for (int i = 0; i < p1HandL.Count; i++)
        {
            p1HandL[i].transform.eulerAngles = new Vector3(30, 0);
        }

        for (int i = 0; i < p2HandL.Count; i++)
        {
            p2HandL[i].transform.eulerAngles = new Vector3(150, 0, 180);
        }

        for (int i = 0; i < floorL.Count; i++)
        {
            floorL[i].transform.eulerAngles = new Vector3(90, 0);
        }

    }

    //카드무빙
    public void ActioniT(GameObject obj, float i, Vector3 pos, float j)
    {
      
        iTween.MoveTo(obj,
                    iTween.Hash(
                    "delay", i,
                      "position", pos,
                       "time", j,
                        "easytype", iTween.EaseType.easeOutBack));
    
    } 

  
}




