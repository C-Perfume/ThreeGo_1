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
        // v �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            print("1��Ű ����!!");
            // �������� 1��ŭ �̵� �ϰ� �ʹ�.
            Vector3 dir = transform.right;
            transform.position += dir * 2;
            currentTime = 0;
        }

    }
}
