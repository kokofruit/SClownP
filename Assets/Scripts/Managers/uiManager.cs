using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        playerScript = playerBehavior.instance;
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
        //TODO: first time only

        if (cutsceneTimer > 20)
        {
            return;
        }

        if (cutsceneTimer <= 0)
        {
            startGifImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/UI/StartCutscene/frame_00_delay-0.33s.png", typeof(Sprite)) as Sprite;
        }
        else if (cutsceneTimer <= 1)
        {
            startGifImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/UI/StartCutscene/frame_08_delay-0.33s.png", typeof(Sprite)) as Sprite;
        }

        cutsceneTimer = cutsceneTimer + Time.deltaTime;
    }
}
