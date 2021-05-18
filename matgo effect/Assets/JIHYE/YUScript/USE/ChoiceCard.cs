using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class ChoiceCard : MonoBehaviour
{
    public static ChoiceCard instance;
    
    public GameObject gukjin;
    public GameObject gukjin_canvas;

    bool twopee = false;

    public GameObject canvas;

    public GameObject choice1;
    public GameObject choice2;

    public GameObject a;
    public GameObject b;


    public bool selec = false; 
    public bool gukjin_selec = false; 

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
                        print("A ����");
                        canvas.SetActive(false);
                        GoStopRule.instance.chosen = choice1;
                        selec = false;
                    }
                    else if (obj.name.Contains("B"))
                    {
                        print("B ����");
                        canvas.SetActive(false);
                        GoStopRule.instance.chosen = choice2;
                        selec = false;
                    }
                }
            }
        }
        }

        if (gukjin_selec)
        {
            if (twopee)
            {
                //���Ǹ� �����ϸ� type�� �Ƿ� �ٲٷ� 
                gukjin.GetComponent<Card>().type = Card.Card_Type.PEE;
                //state�� ���Ƿ� �ٲٰ� obj �������ش�. 
                gukjin.GetComponent<Card>().state = Card.CARD_STATE.TWO_PEE;

                gukjin_selec = false;
            }
        }

    }

    public GameObject Gukjin_Type(GameObject obj)
    {
        if (obj.GetComponent<Card>().state == Card.CARD_STATE.KOOKJIN)
        {
            //������ ���� �Ͽ� Ÿ���� �������ִ� ���� �Լ� �ʿ�.

        }

        return obj;
    }


    public void Sand_card(GameObject choa, GameObject chob)
    {
        choice1 = choa;
        choice2 = chob;

        selec = true;
    }


    public void OnClickA()
    {
        twopee = false;
        gukjin_canvas.SetActive(false);
    }

    public void OnClickB()
    {
        twopee = true;
        gukjin_canvas.SetActive(false);
    }

}
