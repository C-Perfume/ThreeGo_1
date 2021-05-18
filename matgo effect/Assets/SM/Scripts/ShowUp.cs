using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUp : MonoBehaviour
{
    //사라지는 시간
    public float DestroyTime = 5.0f;

    // 오브젝트 풀 크기
    public int poolsize;
       // 오브젝트 풀 배열
    public GameObject[] gwangObject;
    GameObject[] gwangs;


    private void Start()
    {
        gwangs = new GameObject[poolsize];
        //// 오브젝트 풀을 오브젝트 담을 수 있는 크기로 만들어준다.

           }

    private void Update()
    {// 버튼을 눌렀으니까.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            // 오브젝트 풀 안에 있는 오브젝트 중에서

            // 비활성화 된 오브젝트를
            // 활성화하고싶다.

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

        if (Input.GetKeyDown(KeyCode.F4))
        {
            for (int i = 13; i <= 13;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            for (int i = 14; i <= 14;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            for (int i = 15; i <= 15;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            for (int i = 16; i <= 16;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            for (int i = 17; i <= 17;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            for (int i = 18; i <= 18;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            for (int i = 19; i <= 19;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            for (int i = 20; i <= 20;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            for (int i = 21; i <= 21;)
            {
                gwangs[i] = Instantiate(gwangObject[i]);
                gwangs[i].SetActive(true);
                Destroy(gwangs[i], DestroyTime);
                gwangs[i].transform.position = transform.position;
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 22; i <= 22;)
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
