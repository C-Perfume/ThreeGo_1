using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    void Start()
    {
        invoke("Active_false",2);
        
    }

    private void invoke(string v1, int v2)
    {
        throw new NotImplementedException();
    }

    void Active_false()
    {
        gameObject.SetActive(false);
    }
}
