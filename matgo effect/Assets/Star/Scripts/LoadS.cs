using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadS : MonoBehaviour
{
    public static LoadS instance;

    public GameObject loading;
    public Image loadingBg;
    int fadeDir = 1;

    public bool canClick = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    void Update()
    {
        if (loading.activeSelf == true)
        {
            Color bgColor = loadingBg.color;
            bgColor.a += Time.deltaTime * fadeDir;
            loadingBg.color = bgColor;
            if (bgColor.a <= 0)
            {
                loading.SetActive(false);
            }
        }
        return;
        if (canClick == true && Input.GetMouseButtonDown(0))
        {
            //canClick = false;
            //if (Input.mousePosition.x < Screen.width * 0.5f)
            //{
            //    StartCoroutine(ChangeScene("StarMatgo"));
            //}
            //else
            //{
            //    StartCoroutine(ChangeScene("YUGameScene"));
            //}
            //#if UNITY_EDITOR
            //          if (!EventSystem.current.IsPointerOverGameObject())
            //#else
            //          if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //#endif
            //          {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitobj = hit.transform.gameObject;

                if (hitobj.name.Contains("pivot"))
                {

                    if (hitobj.name.Contains("A"))
                    {
                        StartCoroutine(ChangeScene("StarMatgo"));
                    }
                    else
                    {
                        StartCoroutine(ChangeScene("YUGameScene"));
                    }

                    //                }
                }
            }
        }

        
    }


    public void OnClickL()
    {
        StartCoroutine(ChangeScene("StarMatgo"));
    }

    public void OnClickR()
    {
        StartCoroutine(ChangeScene("YUGameScene"));
    }

    IEnumerator ChangeScene(string sceneName)
    {
        loading.SetActive(true);
        fadeDir = 1;
        // ���� �ε� �Ѵ�(�񵿱� �ε�)
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        ao.allowSceneActivation = false;

        // �ε尡 �Ϸᰡ �Ǿ��ٸ�                
        while (ao.isDone == false)
        {
            // �����Ȳ ǥ��
            print(ao.progress + "");

            if (ao.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2);
                ao.allowSceneActivation = true;
            }

            yield return null;
        }

        fadeDir = -1;
        print(ao.progress);
    }
}
