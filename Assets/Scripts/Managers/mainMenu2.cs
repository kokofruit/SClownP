using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Audio;

public class mainMenu2 : MonoBehaviour
{
    public string newGameScene;
    public string settingsScene;
    public string loseScene;

    [SerializeField] CanvasGroup startMenu;
    [SerializeField] CanvasGroup startGif;
    [SerializeField] Image startGifImage;
    [SerializeField] Sprite[] sprites;

    float cutsceneTimer = -1f;

    [SerializeField] AudioClip MulchSong;
    public static bool songHasPlayed = false;
    [SerializeField] AudioClip CutsceneSound;
    [SerializeField] AudioClip selectSFX;


    private void Start()
    {
        if (!songHasPlayed)
        {
            soundMusicManager.instance.PlaySong(MulchSong);
            songHasPlayed=true;
        }
    }

    public void ButtonStart()
    {
        startMenu.interactable = false;
        startGif.alpha = 1f;
        cutsceneTimer = 0f;
        soundFXManager.instance.PlayFXClip(selectSFX,transform);
        soundMusicManager.instance.PlaySong(CutsceneSound);
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

        if (frame > 14)
        {
            startMenu.interactable = true;
            StartGame();
            return;
        }

        //string path = "Assets/UI/StartCutscene/frame_" + frame.ToString() + ".png";
        //Sprite spr = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
        Sprite spr = sprites[(int)frame];
        startGifImage.sprite = spr;

        cutsceneTimer = cutsceneTimer + Time.deltaTime;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ButtonSettings()
    {
        SceneManager.LoadScene(settingsScene);
    }

    public void ButtonQuit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
