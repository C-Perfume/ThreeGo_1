using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    //GameObject Eft;
    //public GameObject E;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);

            }

        }






        //if (Input.GetMouseButtonDown(0))
        //{
          //  Eft = Instantiate(E);
          //  Eft.SetActive(true);
          //  Eft.transform.position = transform.position;
        //}

    }
}
