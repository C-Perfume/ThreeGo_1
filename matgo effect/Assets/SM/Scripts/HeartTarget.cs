using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTarget : MonoBehaviour
{
    public float speed = 1;
    GameObject Chicken;
    public GameObject explosionFactory; //익스플로팩토리에서 오브젝트를 따와서
    public GameObject exploPos;


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = transform.position;
        GameObject exploPos = Instantiate(explosionFactory);
        dir = exploPos.transform.position;
        //Destroy(explosionFactory.gameObject);
    }



    //아래 스크립트처럼 작성하면 폭발효과가 다른곳에서 생성된다.
    //public float speed = 1;
    //GameObject Chicken;
    //public GameObject explosionFactory; //익스플로팩토리에서 오브젝트를 따와서
    //public GameObject exploPos;


    //private void OnCollisionEnter(Collision collision)
    //{
    //
    //    GameObject explo = Instantiate(explosionFactory);
    //    explo.transform.position = exploPos.transform.position;
    //    Destroy(explosionFactory.gameObject);
    //}

}
