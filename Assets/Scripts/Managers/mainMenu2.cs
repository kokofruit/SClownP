using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class mainMenu2 : MonoBehaviour
{
    public string newGameScene;

    [SerializeField] CanvasGroup startMenu;
    [SerializeField] CanvasGroup startGif;
    [SerializeField] Image startGifImage;

    float cutsceneTimer = -1f;

    public void ButtonStart()
    {
        startMenu.interactable = false;
        startGif.alpha = 1f;
        cutsceneTimer = 0f;
    }

    private void Update()
    {
        if (cutsceneTimer >= 0f)
        {
            playCutscene();
        }
    }

    void playCutscene()
    {
        float frame = Mathf.Floor(cutsceneTimer / 0.5f);

        if (frame > 12)
        {
            startMenu.interactable = true;
            StartGame();
            return;
        }

        string path = "Assets/UI/StartCutscene/frame_" + frame.ToString() + ".png";
        Sprite spr = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
        startGifImage.sprite = spr;

        cutsceneTimer = cutsceneTimer + Time.deltaTime;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ButtonSettings()
    {
        // TODO
    }

    public void ButtonQuit()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
