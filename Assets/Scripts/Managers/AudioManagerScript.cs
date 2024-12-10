using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;
    public bool isSoundEnabled = true;
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    [SerializeField] AudioMixer masterMixer;
    public bool fxEnabled = true;
    public bool musicEnabled = true;

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

        //audioSource1 = GetComponent<AudioSource>();
        //audioSource2 = GetComponent<AudioSource>();
    }

    public void Toggle(bool v)
    {
        //if (!isSoundEnabled)
        //{
        //    const float V = (float)1f;
        //    isSoundEnabled = v;
        //}
        //else
        //{
        //    const float V = 0f;
        //    isSoundEnabled = v;
        //}
        
    }

    public void ToggleFX()
    {
        if (fxEnabled)
        {
            masterMixer.SetFloat("fxVol", -80f);
            fxEnabled = false;
        }
        else
        {
            masterMixer.SetFloat("fxVol", 0f);
            fxEnabled = true;
        }
    }

    public void ToggleMusic()
    {
        if (musicEnabled)
        {
            masterMixer.SetFloat("musVol", -80f);
            musicEnabled = false;
        }
        else
        {
            masterMixer.SetFloat("musVol", 0f);
            musicEnabled = true;
        }
    }
}
