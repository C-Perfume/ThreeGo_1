using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowe : MonoBehaviour
{
    public float speed = 10;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject target = GameObject.Find("Player");  // �Ѿư� ��븦 ã����
        dir = target.transform.position - transform.position; // �Ѿư� ��� ������ ã��.
        dir.Normalize(); // ��ġ�� 1��
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += dir * speed * Time.deltaTime;

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //Vector3 dir = new Vector3(h, v, 0);

        //transform.position = transform.position + dir * speed * Time.deltaTime;


        
    }
}
