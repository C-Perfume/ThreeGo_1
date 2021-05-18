using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASDF : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Card card = card1.GetComponent<Card>();
            Card target;

            target = card2.GetComponent<Card>();
            if (card.type == target.type)
            {
                Vector3 pos = card2.transform.position;
                pos.x = pos.x * 0.2f;
                pos.y = pos.y * 0.2f;
                card.transform.position = pos;

            }



        }

























    }
}
