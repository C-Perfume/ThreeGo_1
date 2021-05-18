using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardS : MonoBehaviour
{
    public enum PAE_TYPE 
    {
        PEE,
        GWANG,
        YEOL,
        TEE ,
    }
     // 카드 속성.
    public enum CARD_STATUS
    {
        NONE,
        GODORI,         // 고도리
        TWO_PEE,        // 쌍피
        CHEONG_DAN,     // 청단
        HONG_DAN,       // 홍단
        CHO_DAN,        // 초단
        KOOKJIN,         // 국진
        BGWANG //비광
    }
    public enum PLAYER
    { 
     NONE,
    P1,
    P2,
    P3
    }
    public int num;
    public PLAYER pNum;
    public PAE_TYPE type;
    public CARD_STATUS state;

    public bool same(int idx) {
       return idx == num;
    }
    public bool sameS(CARD_STATUS gDR)
    {
        return gDR == state;
    }

}
