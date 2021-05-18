using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Card card = cube1.GetComponent<Card>();
            Card target;

            target = cube2.GetComponent<Card>();
            if(card.type == target.type)
            {
                // target 으로 이동
                Vector3 pos = cube2.transform.position;
                pos.x = pos.x + 0.2f;
                pos.y = pos.y + 0.2f;
                cube1.transform.position = pos; 
            }

            target = cube3.GetComponent<Card>();
            if (card.type == target.type)
            {
                // target 으로 이동
                Vector3 pos = cube3.transform.position;
                pos.x = pos.x + 0.2f;
                pos.y = pos.y + 0.2f;
                card.transform.position = pos;
            }

            target = cube4.GetComponent<Card>();
            if (card.type == target.type)
            {
                // target 으로 이동
                Vector3 pos = cube4.transform.position;
                pos.x = pos.x + 0.2f;
                pos.y = pos.y + 0.2f;
                cube1.transform.position = pos;
            }

            card = cube2.GetComponent<Card>();

            target = cube1.GetComponent<Card>();
            if (card.type == target.type)
            {
                // target 으로 이동
                Vector3 pos = cube1.transform.position;
                pos.x = pos.x + 0.2f;
                pos.y = pos.y + 0.2f;
                cube2.transform.position = pos;
            }

            target = cube3.GetComponent<Card>();
            if (card.type == target.type)
            {
                // target 으로 이동
                Vector3 pos = cube3.transform.position;
                pos.x = pos.x + 0.2f;
                pos.y = pos.y + 0.2f;
                cube2.transform.position = pos;
            }

            target = cube4.GetComponent<Card>();
            if (card.type == target.type)
            {
                // target 으로 이동
                Vector3 pos = cube4.transform.position;
                pos.x = pos.x + 0.2f;
                pos.y = pos.y + 0.2f;
                cube2.transform.position = pos;
            }
        }
    }
}
