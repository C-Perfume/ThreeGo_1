using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elastic : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iTween.ScaleTo(target, iTween.Hash(
            "delay", 0,
            "scale", new Vector3(4, 4, 4),
            "time", 3,
            "easetype", iTween.EaseType.easeOutElastic));
    }
}

//easeInSine 작아져버림
