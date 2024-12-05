using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsScreen : MonoBehaviour
{
    public string mainMenuScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonReturn()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    // TODO: Disable/Enable chromatic Abberation, FOV slider, sound slider
}
