using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUpTwo : MonoBehaviour
{
    //사라지는 시간
    public float DestroyTime = 5.0f;

    // 오브젝트 풀 크기
    public int poolsize = 1;
    // 오브젝트 풀 배열
    public GameObject[] gwangObject;
    // spawnPoint들
    public Transform[] gwangPoints;


    GameObject ilgwang;


    private void Start()
    {
        // 오브젝트 풀을 오브젝트 담을 수 있는 크기로 만들어준다.
        ilgwang = Instantiate(gwangObject[0]);
        ilgwang.SetActive(false);
        Destroy(ilgwang, DestroyTime);

    }

    private void Update()
    {// 버튼을 눌렀으니까.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            // 오브젝트 풀 안에 있는 오브젝트 중에서

            // 비활성화 된 오브젝트를
            // 활성화하고싶다.

            ilgwang.SetActive(true);

            ilgwang.transform.position = transform.position;


        }

    }




}
