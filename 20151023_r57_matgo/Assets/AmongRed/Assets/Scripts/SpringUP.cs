using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpringUP : MonoBehaviour
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
            "delay", 0.5,
            "scale", new Vector3(2, 2, 2),
            "time", 3,
            "easetype", iTween.EaseType. easeOutBounce));
    }
}
