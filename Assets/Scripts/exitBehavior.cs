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
        playerScript = FindObjectOfType<playerBehavior>();
        player = playerScript.gameObject;
        playerBody = player.GetComponent<CapsuleCollider>();

        winScreen.gameObject.SetActive(false);
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
        hud.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
