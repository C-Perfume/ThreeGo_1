using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using FreeNet;

public class CPopupGoStop : MonoBehaviour {

	List<Image> slots;

	void Awake()
	{
		this.slots = new List<Image>();
		for (int i = 0; i < 2; ++i)
		{
			Transform obj = transform.Find(string.Format("slot{0:D2}", (i + 1)));
			this.slots.Add(obj.GetComponentInChildren<Image>());
		}

		this.slots[0].GetComponent<Button>().onClick.AddListener(this.on_touch_01);
		this.slots[1].GetComponent<Button>().onClick.AddListener(this.on_touch_02);
	}


	void on_touch_01()
	{
		on_choice_go_or_stop(1);
	}


	void on_touch_02()
	{
		on_choice_go_or_stop(0);
	}


	void on_choice_go_or_stop(byte is_go)
	{
		gameObject.SetActive(false);

		CPacket choose_msg = CPacket.create((short)PROTOCOL.ANSWER_GO_OR_STOP);
		choose_msg.push(is_go);
		CNetworkManager.Instance.send(choose_msg);
	}
}
