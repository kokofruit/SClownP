using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class exitBehavior : MonoBehaviour
{
    GameObject player;
    CapsuleCollider playerBody;
    playerBehavior playerScript;

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
            SceneManager.LoadScene("WinScreen");
        }
    }

}
