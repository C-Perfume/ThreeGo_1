using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUpTwo : MonoBehaviour
{
    //������� �ð�
    public float DestroyTime = 5.0f;

    // ������Ʈ Ǯ ũ��
    public int poolsize = 1;
    // ������Ʈ Ǯ �迭
    public GameObject[] gwangObject;
    // spawnPoint��
    public Transform[] gwangPoints;


    GameObject ilgwang;


    private void Start()
    {
        // ������Ʈ Ǯ�� ������Ʈ ���� �� �ִ� ũ��� ������ش�.
        ilgwang = Instantiate(gwangObject[0]);
        ilgwang.SetActive(false);
        Destroy(ilgwang, DestroyTime);

    }

    private void Update()
    {// ��ư�� �������ϱ�.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            // ������Ʈ Ǯ �ȿ� �ִ� ������Ʈ �߿���

            // ��Ȱ��ȭ �� ������Ʈ��
            // Ȱ��ȭ�ϰ�ʹ�.

            ilgwang.SetActive(true);

            ilgwang.transform.position = transform.position;


        }

    }




}
