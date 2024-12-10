using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settingsScreen : MonoBehaviour
{
    public string mainMenuScene;

    [SerializeField] Toggle fxTogObj;
    [SerializeField] Toggle musTogObj;
    
    void Start()
    {
        fxTogObj.isOn = AudioManagerScript.instance.fxEnabled;
        musTogObj.isOn = AudioManagerScript.instance.musicEnabled;
    }


    public void fxToggle()
    {
        AudioManagerScript.instance.ToggleFX();
    }

    public void musToggle()
    {
        AudioManagerScript.instance.ToggleMusic();
    }

    public void ButtonReturn()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
