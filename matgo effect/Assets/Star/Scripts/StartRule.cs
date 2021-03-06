using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartRule : MonoBehaviour
{
    public GameObject[] rules;
    public GameObject setting;
    public GameObject muteSet;
    public AudioSource bGM;
    void Start()
    {
        for(int i = 0; i<rules.Length; i++) { rules[i].SetActive(false); }
           }

    void Update()
    {

    }

    public void Wait (){
        rules[0].SetActive(true);
        rules[1].SetActive(true);
        bGM.Pause();
        Time.timeScale = 0;
    }
    public void SeeRule() {
        rules[2].SetActive(true);
    }
    public void SKipRule()
    {
        rules[0].SetActive(false);
        bGM.UnPause();
        Time.timeScale = 1;
    }
        public void pre()
    {
        rules[2].SetActive(false);
    }

    public void next()
    {
        rules[3].SetActive(true);
    }

    public void pre2()
    {
        rules[3].SetActive(false);
    }

    public void Pause()
    {
        setting.SetActive(false);
         bGM.Pause();
        Time.timeScale = 0;
        rules[4].SetActive(true);
    }

    public void Mute()
    {
        Camera.main.GetComponent<AudioListener>().enabled = false;
        rules[5].SetActive(true); rules[6].SetActive(true);
        muteSet.SetActive(false);
    }
        public void UnMute()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;
        rules[5].SetActive(false); rules[6].SetActive(false);
        muteSet.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void UnPause()
    {
        setting.SetActive(true);
        bGM.UnPause();
        Time.timeScale = 1;
        rules[4].SetActive(false);
    }

    public void Retry() {
        LoadS.instance.canClick = true;

        Time.timeScale = 1;
        SceneManager.LoadScene("title");
    }
}
