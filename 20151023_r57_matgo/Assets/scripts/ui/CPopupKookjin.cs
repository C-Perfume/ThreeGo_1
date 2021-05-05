using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using FreeNet;

public class CPopupKookjin : MonoBehaviour {

	List<Button> slots;

	void Awake()
	{
		this.slots = new List<Button>();
		for (int i = 0; i < 2; ++i)
		{
			Transform obj = transform.Find(string.Format("slot{0:D2}", (i + 1)));
			this.slots.Add(obj.GetComponent<Button>());
		}

		this.slots[0].onClick.AddListener(this.on_touch_01);
		this.slots[1].onClick.AddListener(this.on_touch_02);
	}


	void on_touch_01()
	{
		on_choice_kookjin(1);
	}


	void on_touch_02()
	{
		on_choice_kookjin(0);
	}


	void on_choice_kookjin(byte use_kookjin)
	{
		gameObject.SetActive(false);

		CPacket msg = CPacket.create((short)PROTOCOL.ANSWER_KOOKJIN_TO_PEE);
		msg.push(use_kookjin);
		CNetworkManager.Instance.send(msg);
	}
}
