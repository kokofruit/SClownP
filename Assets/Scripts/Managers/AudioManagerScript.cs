using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;
    public bool isSoundEnabled = true;
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Awake()
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

        audioSource1 = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
    }

    public void Toggle(bool v)
    {
        if (!isSoundEnabled)
        {
            const float V = (float)1f;
            isSoundEnabled = v;
        }
        else
        {
            const float V = 0f;
            isSoundEnabled = v;
        }
    }
}
