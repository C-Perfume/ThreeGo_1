using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ddadak : MonoBehaviour
{
    public float speed = 5;
    Vector3 dir;
    public GameObject particle;


    void Start()
    {
      
    }

    
    void Update()
    {
        Vector3 dir = Vector3.down;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = Instantiate(particle);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }

}
