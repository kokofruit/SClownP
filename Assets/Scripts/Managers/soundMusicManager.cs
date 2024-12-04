using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundMusicManager : MonoBehaviour
{
    // Written using code by Sasquatch B Studios
    // https://www.youtube.com/watch?v=DU7cgVsU2rM&ab_channel=SasquatchBStudios

    public static soundMusicManager instance;

    [SerializeField] AudioMixerGroup fxMixer;

    [SerializeField] AudioSource musicPlayer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySong(AudioClip song)
    {
        musicPlayer.clip = song;
        musicPlayer.outputAudioMixerGroup = fxMixer;
        musicPlayer.Play();
    }

    public void StopSong()
    {
        musicPlayer.Stop();
    }
}
