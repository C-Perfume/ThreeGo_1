using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public float InvokeTest = 2f; 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InvokeTest", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Invoke Start !");
    }
}
