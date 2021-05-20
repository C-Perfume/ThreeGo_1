using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour
{
    float currentTime;
    public float createTime = 1;
    public GameObject BreatheFactory;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > createTime)
        {
            GameObject enemy = Instantiate(BreatheFactory);
            enemy.transform.position = transform.position;
            currentTime = 0;
        }

    }


}
