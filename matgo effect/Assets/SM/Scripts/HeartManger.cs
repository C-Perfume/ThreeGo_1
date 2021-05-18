using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManger : MonoBehaviour

{
    public float speed = 1;
    public float createTime= 10;
    public GameObject heartFactory;
    public GameObject heartheart;


    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = Vector3.up;
        heartheart = Instantiate(heartFactory);
        heartheart.transform.position = transform.position;

        Come c = collision.gameObject.GetComponent<Come>();
        c.enabled = false;
    }
}
