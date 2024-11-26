using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class uiBehavior : MonoBehaviour
{
    // Player
    GameObject player;
    CapsuleCollider playerBody;
    playerBehavior playerScript;

    // Canvas Groups
    public CanvasGroup winScreen;
    public CanvasGroup hud;
    public CanvasGroup startGif;
    Image startGifImage;

    // Timer
    float cutsceneTimer = 0f;

    void Start()
    {
        playerScript = FindObjectOfType<playerBehavior>();
        player = playerScript.gameObject;
        playerBody = player.GetComponent<CapsuleCollider>();

        startGifImage = startGif.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        startGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerBody && playerScript.hasKey)
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
        //TODO: first time only

        if (cutsceneTimer > 20)
        {
            return;
        }

        if (cutsceneTimer == 0)
        {
            startGifImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/UI/Start GIF/frame_00_delay-0.33s.jpg", typeof(Sprite)) as Sprite;
        }
        if (cutsceneTimer == 10)
        {
            startGifImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/UI/Start GIF/frame_08_delay-0.33s.jpg", typeof(Sprite)) as Sprite;
        }

        cutsceneTimer = cutsceneTimer + Time.deltaTime;
    }
}
