using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    // Player
    GameObject player;
    CapsuleCollider playerBody;
    playerBehavior playerScript;

    // Canvas Groups
    [SerializeField] CanvasGroup winScreen;
    [SerializeField] CanvasGroup hud;
    [SerializeField] CanvasGroup startGif;
    Image startGifImage;

    // Timer
    float cutsceneTimer = 0f;
    string state = "cutscene";

    void Start()
    {
        playerScript = playerBehavior.instance;
        player = playerScript.gameObject;
        playerBody = player.GetComponent<CapsuleCollider>();

        startGifImage = startGif.transform.GetChild(1).GetComponent<Image>();

        
    }

    void Update()
    {
        if (state == "cutscene")
        {
            startGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerBody && keyManager.instance.playerHasKey)
        {
            winGame();
        }
    }

    void winGame()
    {
        hud.alpha = 0;
        winScreen.alpha = 1;
        Time.timeScale = 0;
    }

    void startGame()
    {
        float frame = Mathf.Floor(cutsceneTimer/0.5f);

        if (frame > 13)
        {
            state = "hud";
            return;
        }

        string path = "Assets/UI/StartCutscene/frame_" + frame.ToString() + ".png";
        Sprite spr = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
        startGifImage.sprite = spr;

        cutsceneTimer = cutsceneTimer + Time.deltaTime;
    }
}
