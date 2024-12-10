using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsScreen : MonoBehaviour
{
    public string mainMenuScene;
    
    void Start()
    {
       
    }

    
    void Update()
    {

    }

    public void ButtonReturn()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
