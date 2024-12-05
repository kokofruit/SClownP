using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string newGameScene;
    public string settingScene;
    public string menuScene;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ButtonSettings()
    {
        SceneManager.LoadScene(settingScene);
    }

    public void ButtonReturn()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
