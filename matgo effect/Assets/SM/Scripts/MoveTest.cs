using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float createTime = 1;
    float currentTime = 0;

    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // v 시간이 흐르다가
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            print("1번키 누름!!");
            // 오른쪽을 1만큼 이동 하고 싶다.
            Vector3 dir = transform.right;
            transform.position += dir * 2;
            currentTime = 0;
        }

    }
}
