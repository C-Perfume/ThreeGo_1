using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    float rotX;
    float rotY;

    public float rotSpeed = 200;


    void Update()
    {
        if (Input.GetMouseButton(1))
        { 
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            rotX += x * rotSpeed * Time.deltaTime;
            rotY += y * rotSpeed * Time.deltaTime;

            transform.localEulerAngles = new Vector3(-rotY, rotX, 0);
        }
        
    }
}
