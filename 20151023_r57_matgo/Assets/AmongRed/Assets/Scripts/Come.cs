using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Come : MonoBehaviour
{

    public GameObject target;
    public float speed = 3;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      

        transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed);
    }
}

