using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public float speed = 1;
    public GameObject Flame;
    public GameObject dragon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = Vector3.back;
        transform.position += dragon.transform.position + dir * speed;

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Burn = Instantiate(Flame);


        Destroy(collision.gameObject);

    }
}
