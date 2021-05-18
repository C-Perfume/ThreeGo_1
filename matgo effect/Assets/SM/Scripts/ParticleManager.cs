using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    float currentTime;
    public float createTime = 1f;
    public GameObject ParticleFactory;
    public float speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        ParticleFactory = Instantiate(ParticleFactory);
        ParticleFactory.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ParticleFactory.SetActive(false);

        ParticleFactory.transform.position = transform.position;
        Vector3 dir = Vector3.up;
        transform.position += dir * speed * Time.deltaTime;

   

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