using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUp : MonoBehaviour
{
    //������� �ð�
    public float DestroyTime = 5.0f;

    // ������Ʈ Ǯ ũ��
    public int poolsize;
       // ������Ʈ Ǯ �迭
    public GameObject[] gwangObject;
    GameObject[] gwangs;


    private void Start()
    {
        gwangs = new GameObject[poolsize];
        //// ������Ʈ Ǯ�� ������Ʈ ���� �� �ִ� ũ��� ������ش�.

           }

    private void Update()
    {// ��ư�� �������ϱ�.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            // ������Ʈ Ǯ �ȿ� �ִ� ������Ʈ �߿���

            // ��Ȱ��ȭ �� ������Ʈ��
            // Ȱ��ȭ�ϰ�ʹ�.

            //ilgwang.SetActive(true);
            //ilgwang.transform.position = transform.position;

            gwangs[0] = Instantiate(gwangObject[0]);
            gwangs[0].SetActive(true);
            Destroy(gwangs[0], DestroyTime);
            gwangs[0].transform.position = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //gwang2.SetActive(true);
            //gwang2.transform.position = transform.position;
            for (int i = 1; i <= 1;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //gwang3.SetActive(true);
            //gwang3.transform.position = transform.position;
            for (int i = 2; i <= 2;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 3; i <= 3;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 4; i <= 4;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for (int i = 5; i <= 5;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            for (int i = 6; i <= 6;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            for (int i = 7; i <= 7;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            for (int i = 8; i <= 8;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            for (int i = 9; i <= 9;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 10; i <= 10;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 11; i <= 11;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }


        if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 12; i <= 12;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }



    }




}
