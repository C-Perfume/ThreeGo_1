using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGMove : MonoBehaviour
{
    public Image[] bgs;
    Color[] endColors = new Color[3];
    float dir = -1;
    float currT = 0;
    void Start()
    {
        for (int i = 0; i < endColors.Length; i++)
        { endColors[i] = bgs[8 + i].color; }

        for (int i = 0; i < 5; i++)
        { 

        iTween.ScaleFrom(bgs[i]. transform.gameObject,
            (iTween.Hash(
                "delay", i,
                "scale", new Vector3(1,0),
                "Time", 1
                )));
        }

        iTween.MoveFrom(bgs[7].transform.gameObject,
         (iTween.Hash(
             "delay", 5,
             "position", new Vector3(-200, -200),
             "Time", 0.2f,
             "easetype", iTween.EaseType.easeOutBack,
             "onstart", "Sound",
           "onstarttarget", gameObject
                                        )));
       
        iTween.MoveFrom(bgs[6].transform.gameObject,
       (iTween.Hash(
           "delay", 5.2f,
           "position", new Vector3(-200, -200),
           "Time", 0.2f,
           "easetype", iTween.EaseType.easeOutBack
                                )));

        iTween.ScaleFrom(bgs[5].transform.gameObject,
          (iTween.Hash(
              "delay", 6,
              "scale", new Vector3(1.3f, 0),
              "Time", 2
                          ))) ;
    }

    void Update()
    { currT += Time.deltaTime;
        if (currT >= 7) { Color(); }
    }

    void Sound() 
    {
              bgs[6].transform.gameObject.GetComponent<AudioSource>().Play(); }

    void Color() {
               for (int i = 0; i < endColors.Length; i++)
        {
            endColors[i].a += 0.005f * dir;
            bgs[8 + i].color = endColors[i];
            if (endColors[i].a >= 1 || endColors[i].a <= 0) dir *= -1;
           
        }
    }
    public void OnPlay() {
        SceneManager.LoadScene("play");
    }
}
