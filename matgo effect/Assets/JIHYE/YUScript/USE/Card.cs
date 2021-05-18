using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static Card instance;
    private void Awake()
    {
        instance = this;
    }
    public enum Card_Type
    {
        PEE,
        KWANG,
        TEE,
        YEOL
    }
    public enum CARD_STATE
    {
        NONE,
        GODORI,         // 고도리
        TWO_PEE,        // 쌍피
        CHEONG_DAN,     // 청단
        HONG_DAN,       // 홍단
        CHO_DAN,        // 초단
        KOOKJIN         // 국진
    }

    public enum POS
    {
        deck,
        floor,
        p1hand,
        p1score,
        p2hand,
        p2score
    }

    public int moon;
    public string name;
    public int floor_index;
    public int player_index;
    public Card_Type type;
    public CARD_STATE state;
    public POS pos;
    
    

    private void Start()
    {
        name = gameObject.name;
    }

    public bool Same(int mon)
    {
        for (int i = 0; i < CardList.instance.floor.Count; i++)
        {
            if (this.moon == mon)
            {
                return true;
            }
        }
        return false;
    }

    public bool Same(GameObject a, GameObject b)
    {
        return a.GetComponent<Card>().moon == b.GetComponent<Card>().moon;
    }
    public bool OtherPee(GameObject obj)
    {
        return obj.GetComponent<Card>().type == Card_Type.PEE;
    }





}
