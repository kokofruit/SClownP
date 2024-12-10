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
    [SerializeField] AudioClip[] mulchSteps;
    [SerializeField] AudioClip[] scpSteps;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayFXClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = fxMixer;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomPitch(AudioClip audioClip, Transform spawnTransform, float range, float volume = 1f)
    {
        float pitch = Random.Range(-(range / 2), (range / 2));

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = fxMixer;
        audioSource.pitch = 1 + pitch;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayFootStep(string creature, string speed, AudioSource stepPlayer)
    {
        // Choose source
        if (!stepPlayer.isPlaying)
        {
            // Choose random clip from list
            var list = creature == "mulch" ? mulchSteps : scpSteps;
            int stepIndex = Random.Range(0, list.Length);
            stepPlayer.clip = list[stepIndex];

            // Match speed
            switch (speed)
            {
                case "sprint":
                    stepPlayer.pitch = 1.5f;
                    break;
                case "crouch":
                    stepPlayer.pitch = 0.7f;
                    break;
                default:
                    stepPlayer.pitch = 1f;
                    break;
            }

            // Add variation
            stepPlayer.pitch += Random.Range(-0.2f, 0.2f);

            // Play
            stepPlayer.Play();
        }
    }
}
