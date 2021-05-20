
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    //������ �� �����ǿ����� �ѹ������� ������ �̺�Ʈ ���̶� ������, 
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

    public void GodoriEFT(int a)
    {
        if (godoriCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_godori);
            ScoreUI.instance.Get_Count("����" , a);
            godori.SetActive(true);
            print("����~~");
            Destroy(godori, 5);
        }
        godoriCount = false;
    }

    public void HongEFT(int a)
    {
        if (HongCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_hongdan);
            ScoreUI.instance.Get_Count("ȫ��", a);
            hong.SetActive(true);
            Debug.Log("ȫ�� �̿�~~");
            Destroy(hong, 5);
        }
        HongCount = false;
    }

    public void ChoungEFT(int a)
    {
        if (ChoungCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_chongdan);
            ScoreUI.instance.Get_Count("û��", a);
            choung.SetActive(true);
            Debug.Log("û���̿�");
            Destroy(choung, 5);
        }
        ChoungCount = false;
    }
    public void ChoEFT(int a)
    {
        if (ChoCount)
        {
            //SoundManager.instance.AddPointer(SoundManager.AddPoint.EFT_chodan);
            ScoreUI.instance.Get_Count("�ʴ�", a);
            cho.SetActive(true);
            Debug.Log("�ʴ�!");
            Destroy(cho, 5);
        }
        ChoCount = false;
    }
}











