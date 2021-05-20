using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getup : MonoBehaviour
{

    public float delay = 1;
    public float rot = 20;
    public float y = 180;

        void Start()
    {
        iTween.RotateTo(gameObject,
                   iTween.Hash(
                   "delay", delay,
                    "rotation", new Vector3(0, 0, 0),
                      "time", 1,
                       "easytype", iTween.EaseType.easeOutBack)); ;
        iTween.RotateTo(gameObject,
                  iTween.Hash(
                  "delay", 1+delay + delay,
                   "rotation", new Vector3(0, y, 0),
                     "time", 1,
                      "easytype", iTween.EaseType.easeOutBack)); ;
        iTween.RotateTo(gameObject,
                 iTween.Hash(
                 "delay", 2+delay + delay+ delay,
                  "rotation", new Vector3(0, y, rot),
                    "time", 1,
                     "easytype", iTween.EaseType.easeOutBack)); ;
    }

   
}
