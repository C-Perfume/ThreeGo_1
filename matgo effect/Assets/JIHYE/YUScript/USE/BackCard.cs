using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCard : MonoBehaviour
{
    List<GameObject> deck;
    public GameObject back;
       
    void Update()
    {
        deck = CardList.instance.pea;
        if (deck != null)
        {
            back.SetActive(true);
        }
        else
        {
            back.SetActive(false);
        }
        
    }
}
