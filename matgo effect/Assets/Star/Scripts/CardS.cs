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
     // ī�� �Ӽ�.
    public enum CARD_STATUS
    {
        NONE,
        GODORI,         // ����
        TWO_PEE,        // ����
        CHEONG_DAN,     // û��
        HONG_DAN,       // ȫ��
        CHO_DAN,        // �ʴ�
        KOOKJIN,         // ����
        BGWANG //��
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
