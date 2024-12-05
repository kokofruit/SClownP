using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitBehavior : MonoBehaviour
{
    GameObject player;
    CapsuleCollider playerBody;
    playerBehavior playerScript;

    public CanvasGroup winScreen;
    public CanvasGroup hud;

    private void Start()
    {
        playerScript = playerBehavior.instance;
        player = playerScript.gameObject;
        playerBody = player.GetComponent<CapsuleCollider>();
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
