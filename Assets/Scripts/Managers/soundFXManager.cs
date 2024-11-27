using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundFXManager : MonoBehaviour
{
    // Written using code by Sasquatch B Studios
    // https://www.youtube.com/watch?v=DU7cgVsU2rM&ab_channel=SasquatchBStudios

    public static soundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

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
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
