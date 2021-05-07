using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FreeNet;

public class CPopupGameResult : MonoBehaviour {

	Sprite win_sprite;
	Sprite lose_sprite;

	Image win_lose;
	Text money;
	Text score;
	Text double_val;
	Text final_score;

	void Awake()
	{
		this.win_sprite = Resources.Load<Sprite>("images/win");
		this.lose_sprite = Resources.Load<Sprite>("images/lose");

		transform.Find("bg").GetComponent<Button>().onClick.AddListener(this.on_touch);
		this.win_lose = transform.Find("title").GetComponent<Image>();
		this.money = transform.Find("money").GetComponent<Text>();
		this.score = transform.Find("score").GetComponent<Text>();
		this.double_val = transform.Find("double").GetComponent<Text>();
		this.final_score = transform.Find("final_score").GetComponent<Text>();
	}


	void on_touch()
	{
		CUIManager.Instance.hide(UI_PAGE.POPUP_GAME_RESULT);

		CPacket send = CPacket.create((short)PROTOCOL.READY_TO_START);
		CNetworkManager.Instance.send(send);
	}


	public void refresh(byte is_win,
		int score,
		int double_val,
		int final_score)
	{
		if (is_win == 1)
		{
			this.win_lose.sprite = this.win_sprite;
			this.money.text = "오잉? 타짜세요?";
		}
		else
		{
			this.win_lose.sprite = this.lose_sprite;
			this.money.text = "... 지갑이 싸늘하다..";
		}

		this.score.text = score.ToString();
		this.double_val.text = double_val.ToString();
		this.final_score.text = final_score.ToString();
	}
}
