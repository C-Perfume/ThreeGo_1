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
        
        GameObject target = GameObject.Find("Player");  // 쫓아갈 상대를 찾아줘
        dir = target.transform.position - transform.position; // 쫓아갈 상대 방향을 찾음.
        dir.Normalize(); // 수치를 1로
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
