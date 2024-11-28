using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundFXManager : MonoBehaviour
{
    // Written using code by Sasquatch B Studios
    // https://www.youtube.com/watch?v=DU7cgVsU2rM&ab_channel=SasquatchBStudios

    public static soundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] AudioMixerGroup fxMixer;

    public float volFX = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayFXClip(AudioClip audioClip, Transform spawnTransform)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volFX;
        audioSource.outputAudioMixerGroup = fxMixer;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
