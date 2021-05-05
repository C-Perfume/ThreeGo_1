using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CPlayerInfoSlot : MonoBehaviour {

	//TextMesh score_text;
	//TextMesh go_text;
	//TextMesh shake_text;
	//TextMesh ppuk_text;
	//TextMesh pee_count_text;

	//ㅂ
	Text score_text;
	Text go_text;
	Text shake_text;
	Text ppuk_text;
	Text pee_count_text;

	void Awake()
	{
		//this.score_text = gameObject.transform.Find("score").GetComponent<TextMesh>();
		//this.go_text = gameObject.transform.Find("go").GetComponent<TextMesh>();
		//this.shake_text = gameObject.transform.Find("shake").GetComponent<TextMesh>();
		//this.ppuk_text = gameObject.transform.Find("ppuk").GetComponent<TextMesh>();
		//this.pee_count_text = gameObject.transform.Find("pee").GetComponent<TextMesh>();

		//ㅂ
		this.score_text = gameObject.transform.Find("score").GetComponent<Text>();
		this.go_text = gameObject.transform.Find("go").GetComponent<Text>();
		this.shake_text = gameObject.transform.Find("shake").GetComponent<Text>();
		this.ppuk_text = gameObject.transform.Find("ppuk").GetComponent<Text>();
		this.pee_count_text = gameObject.transform.Find("pee").GetComponent<Text>();
	}


	public void update_score(short score)
	{
		this.score_text.text = score.ToString();
	}


	public void update_go(short go)
	{
		this.go_text.text = go.ToString();
	}


	public void update_shake(short shake)
	{
		this.shake_text.text = shake.ToString();
	}


	public void update_ppuk(short ppuk)
	{
		this.ppuk_text.text = ppuk.ToString();
	}


	public void update_peecount(byte count)
	{//ㅂ format ({0}) 변경
		this.pee_count_text.text = string.Format("{0}", count);
	}
}
