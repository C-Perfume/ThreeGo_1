using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation; // ar관련
using UnityEngine.XR.ARSubsystems; // ar raycast 관련 trackableType.plane의 앞에 쓰거나 유징을 넣어준다

public class AR : MonoBehaviour
{
    public GameObject ground;
    public GameObject testCam;
    public GameObject aRsession;
    public GameObject aRSOrigin;

    public GameObject indi;
    public Text distance;
    public Text info;
    public GameObject obj;
    ARRaycastManager rayManager;

    private void Awake()
    {
#if UNITY_EDITOR
        aRsession.SetActive(false);
        aRSOrigin.SetActive(false);
        testCam.SetActive(true);
        ground.SetActive(true);
#else
        aRsession.SetActive(true);
        aRSOrigin.SetActive(true);
        testCam.SetActive(false);
        ground.SetActive(false);
#endif
    }
    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        int layer = 1 << LayerMask.NameToLayer("Ground");

#if UNITY_EDITOR

        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            DetectedGround(hit.point);
        }
        else { indi.SetActive(false); }

#else
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (rayManager.Raycast(ray, hits, TrackableType.Planes))
        {

            DetectedGround(hits[0].pose.position);
        }

#endif
        distance.text = string.Format("{0:N2}", Vector3.Distance(Camera.main.transform.position, indi.transform.position))+"m"; 

        if (Input.GetMouseButtonDown(0))
        {
            if (indi.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.SetPositionAndRotation(indi.transform.position+Vector3.forward*0.1f, indi.transform.rotation);
                indi.SetActive(false);
                ground.SetActive(false);
                info.rectTransform.gameObject.SetActive(false);
                enabled = false;
            }
        }
    }
    void DetectedGround(Vector3 hitPos)
    {
        indi.SetActive(true);
        indi.transform.position = hitPos;
        indi.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
