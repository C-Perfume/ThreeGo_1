using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTarget : MonoBehaviour
{
    public float speed = 1;
    GameObject Chicken;
    public GameObject explosionFactory; //�ͽ��÷����丮���� ������Ʈ�� ���ͼ�
    public GameObject exploPos;


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = transform.position;
        GameObject exploPos = Instantiate(explosionFactory);
        dir = exploPos.transform.position;
        //Destroy(explosionFactory.gameObject);
    }



    //�Ʒ� ��ũ��Ʈó�� �ۼ��ϸ� ����ȿ���� �ٸ������� �����ȴ�.
    //public float speed = 1;
    //GameObject Chicken;
    //public GameObject explosionFactory; //�ͽ��÷����丮���� ������Ʈ�� ���ͼ�
    //public GameObject exploPos;


    //private void OnCollisionEnter(Collision collision)
    //{
    //
    //    GameObject explo = Instantiate(explosionFactory);
    //    explo.transform.position = exploPos.transform.position;
    //    Destroy(explosionFactory.gameObject);
    //}

}
