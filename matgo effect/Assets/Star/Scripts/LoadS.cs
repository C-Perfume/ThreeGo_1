using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadS : MonoBehaviour
{
    public static LoadS instance;

    public GameObject loading;
    public Image loadingBg;
    int fadeDir = 1;

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
        if (Input.GetMouseButtonDown(0))
        {

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

                }
            }
        }

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

        //loading.SetActive(false);
        fadeDir = -1;
        print(ao.progress);
    }
}
