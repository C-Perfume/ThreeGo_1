using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEventManager : MonoBehaviour
{
    public static ScoreEventManager instance;

    bool godoriCount = true;
    bool HongCount = true;
    bool ChoungCount = true;
    bool ChoCount = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GodoriEFT()
    {
        if (godoriCount)
        {
            SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_godori);
            Debug.Log("Godori 5 score");
        }
        godoriCount = false;
    }

    public void HongEFT()
    {
        if (HongCount)
        {
            SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_hongdan);
            Debug.Log("Hongdan 3 score");
        }
        HongCount = false;
    }

    public void ChoungEFT()
    {
        if (ChoungCount)
        {
            SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_chongdan);
            Debug.Log("Cheongdan 3 score");
        }
        ChoungCount = false;
    }
    public void ChoEFT()
    {
        if (ChoCount)
        {
            SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_chodan);
            Debug.Log("Chodan 3 score");
        }
        ChoCount = false;
    }


}
