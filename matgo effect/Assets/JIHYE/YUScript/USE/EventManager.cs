
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    //어차피 한 게임판에서는 한법ㄴ씩만 나오는 이벤트 들이라 괜찮음, 
    bool godoriCount = true;
    bool HongCount = true;
    bool ChoungCount = true;
    bool ChoCount = true;


    public GameObject godori;
    public GameObject choung;
    public GameObject hong;
    public GameObject cho;

    public GameObject poop;
    public GameObject kiss;
    public GameObject eating;


    //public GameObject player_event;
    //public GameObject deck_event;
    private void Awake()
    {
        instance = this;
    }

    public enum Play_Event
    {
        kiss,
        ddong,
        eatting_ddong,
        ddadack,
        choice
    }

    void Start()
    {
        //player_event.SetActive(false);
        //deck_event.SetActive(false);
        godori.SetActive(false);
        choung.SetActive(false);
        cho.SetActive(false);
        hong.SetActive(false);
        kiss.SetActive(false);
        poop.SetActive(false);
        eating.SetActive(false);
    }

    public void PlayerEvent(Play_Event events)
    {
        //ShowUI(player_event, events);
    }

    public void DeckEvent(Play_Event events)
    {
        //ShowUI(deck_event, events);
    }

   
    IEnumerable ShowUI(GameObject tex, Play_Event eve)
    {
        tex.SetActive(true);
        tex.GetComponent<Text>().text = eve.ToString();
        yield return new WaitForSeconds(1);
        tex.SetActive(false);
    }


    public void EventsReset()
    {
        godoriCount = true;
        ChoCount = true;
        HongCount = true;
        ChoungCount = true;
    }

    public void GodoriEFT()
    {
        if (godoriCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_godori);
            godori.SetActive(true);
            print("고도리~~");
            Destroy(godori, 5);
        }
        godoriCount = false;
    }

    public void HongEFT()
    {
        if (HongCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_hongdan);
            hong.SetActive(true);
            Debug.Log("홍단 이요~~");
            Destroy(hong, 5);
        }
        HongCount = false;
    }

    public void ChoungEFT()
    {
        if (ChoungCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_chongdan);
            choung.SetActive(true);
            Debug.Log("청단이요");
            Destroy(choung, 5);
        }
        ChoungCount = false;
    }
    public void ChoEFT()
    {
        if (ChoCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_chodan);
            cho.SetActive(true);
            Debug.Log("초단!");
            Destroy(cho, 5);
        }
        ChoCount = false;
    }
}











