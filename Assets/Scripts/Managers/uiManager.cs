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

    
}
