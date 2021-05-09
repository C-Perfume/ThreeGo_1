using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploEFT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ��ƼŬ �÷���
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();

        // ���� �÷���
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        // 3�� �ִٰ� �ı�����
        Destroy(gameObject, 3);
        Invoke("DestroyObject", 3);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}