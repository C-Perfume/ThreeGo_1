using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    float currentTime;
    public float createTime = 1f;
    public GameObject ParticleFactory;
    public GameObject[] P;
    public float speed = 5;
    public float destroyTime = 3;


    // Start is called before the first frame update
    void Start()
    {
        ParticleFactory = Instantiate(P[0]);
        ParticleFactory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        ParticleFactory.SetActive(true);
        ParticleFactory.transform.position = transform.position;
        Vector3 dir = Vector3.up;
        transform.position += dir * speed * Time.deltaTime;
        Destroy(ParticleFactory, destroyTime);

        //currentTime += Time.deltaTime;
        //if (currentTime > createTime)
        //{
        //    ParticleFactory.SetActive(true);
        //    ParticleFactory.transform.position = transform.position;
        //    currentTime = 0;
        //    break;
        //}



    }
}