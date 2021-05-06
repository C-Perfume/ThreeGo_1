using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AR_Manager : MonoBehaviour
{
    //public GameObject ground;
    public GameObject testCam;
    public GameObject aRsession;
    public GameObject aRSOrigin;

    private void Awake()
    {
#if UNITY_EDITOR
        aRsession.SetActive(false);
        aRSOrigin.SetActive(false);
        testCam.SetActive(true);
        //ground.SetActive(true);
#else
 aRsession.SetActive(true);
        aRSOrigin.SetActive(true);
        testCam.SetActive(false);
        //ground.SetActive(false);
#endif
    }

}
