using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoGo : MonoBehaviour
{
    public float speed = 1;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.right;
        transform.position += dir * speed * Time.deltaTime;

    }
}
