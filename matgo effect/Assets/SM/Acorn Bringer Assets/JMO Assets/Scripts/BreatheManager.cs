using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheManager : MonoBehaviour
{
    GameObject FireBreathe;
    public GameObject ExplosionFactory;
    public float speed = 1;
    Vector3 dir;
    Component rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        // ������ 0�� �����ؼ� if���� ������ ��´�. ������ ������ Ground�� ���Ѵ�.
        float Value = 0;
        if(Value<1)
        {
            GameObject target = GameObject.Find("Bottom");
            dir = target.transform.position - transform.position;
            dir.Normalize();

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        //rigidbody.velocity = Vector3.zero;
        //rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = Instantiate(ExplosionFactory);

       
        

    }

}
