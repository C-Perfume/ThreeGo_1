using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class ChoiceCard : MonoBehaviour
{
    public static ChoiceCard instance;
    
    public GameObject gukjin;
    public GameObject gukjin_canvas;

     Transform pee_pos;
     Transform yeal_pos;
    public Transform pee1_pos;
    public Transform yeal1_pos;
    public Transform pee2_pos;
    public Transform yeal2_pos;

    int index;

    public GameObject canvas;

     GameObject choice1;
     GameObject choice2;

    public GameObject a;
    public GameObject b;


    public bool selec = false; 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        canvas.SetActive(false);
        gukjin_canvas.SetActive(false);

    }

    void Update()
    {
        if (!selec)
        {
            canvas.SetActive(false);
            return;
        }
        else 
        {
              canvas.SetActive(true);

            a.GetComponent<SpriteRenderer>().sprite = choice1.GetComponent<SpriteRenderer>().sprite;
            b.GetComponent<SpriteRenderer>().sprite = choice2.GetComponent<SpriteRenderer>().sprite;

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
                              print("A 선택");
                              canvas.SetActive(false);
                              GoStopRule.instance.chosen = choice1;
                              selec = false;
                         }
                         else if (obj.name.Contains("B"))
                         {
                              print("B 선택");
                              canvas.SetActive(false);
                              GoStopRule.instance.chosen = choice2;
                              selec = false;
                         }
                     }
                 }
            }
        }

        /*
        if (CardList.instance.Player1_Score.Contains(gukjin) ||
            CardList.instance.Player2_Score.Contains(gukjin))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) )
                {
                    GameObject obj = hit.transform.gameObject;
                    if (obj == gukjin)
                    {
                        gukjin_canvas.SetActive(true);
                    }
                }
            }
        }
        */
    }

    public void Sand_card(GameObject choa, GameObject chob)
    {
        choice1 = choa;
        choice2 = chob;

        selec = true;
    }
    public void Ques_Gukjin(int a)
    {
        index = a;
        gukjin_canvas.SetActive(true);
    }
    public void Ques_Gukjin()
    {
        gukjin_canvas.SetActive(false);
    }

    public void OnClickA()
    {
        if (index == 0)
        {
            yeal_pos = yeal1_pos;
        }
        else if (index == 1)
        {
            yeal_pos = yeal2_pos;
        }

        gukjin.GetComponent<Card>().type = Card.Card_Type.YEOL;
        //state를 쌍피로 바꾸고 obj 전달해준다. 
        gukjin.GetComponent<Card>().state = Card.CARD_STATE.KOOKJIN;
        iTween.MoveTo(gukjin, yeal_pos.position + new Vector3(-0.3f, 0, 0), 1);
        gukjin_canvas.SetActive(false);
    }

    public void OnClickB()
    {
        if (index == 0)
        {
            pee_pos = pee1_pos;
        }
        else if (index == 1)
        {
            pee_pos = pee2_pos;
        }
        //쌍피를 선택하면 type를 피로 바꾸로 
        gukjin.GetComponent<Card>().type = Card.Card_Type.PEE;
        //state를 쌍피로 바꾸고 obj 전달해준다. 
        gukjin.GetComponent<Card>().state = Card.CARD_STATE.TWO_PEE;
        iTween.MoveTo(gukjin, pee_pos.position + new Vector3(-0.3f, 0, 0), 1);
        
        gukjin_canvas.SetActive(false);
    }

    public void Shake()
    {
        
    }

}
