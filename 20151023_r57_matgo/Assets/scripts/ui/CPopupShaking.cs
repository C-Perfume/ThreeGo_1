using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using FreeNet;

public class CPopupShaking : MonoBehaviour {

	CCard selected_card_info;
	byte card_slot;

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


	public void refresh(CCard selected_card_info, byte card_slot)
	{
		this.selected_card_info = selected_card_info;
		this.card_slot = card_slot;
	}


	void on_touch_01()
	{
		on_choice_shaking(1);
	}


	void on_touch_02()
	{
		on_choice_shaking(0);
	}


	void on_choice_shaking(byte is_shaking)
	{
		gameObject.SetActive(false);

		CPlayRoomUI.send_select_card(this.selected_card_info, this.card_slot, is_shaking);
	}
}
