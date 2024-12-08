using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiManager : MonoBehaviour
{
    public static uiManager instance;

    // Player
    GameObject player;
    CapsuleCollider playerBody;
    playerBehavior playerScript;

    // Canvas Groups
    [SerializeField] CanvasGroup winScreen;
    [SerializeField] CanvasGroup hud;
    [SerializeField] CanvasGroup lmbGroup;
    Image lmbIcon;
    TextMeshProUGUI lmbText;
    [SerializeField] Image rmb;
    [SerializeField] CanvasGroup keygot;

    // Timer
    float cutsceneTimer = 0f;
    string state = "cutscene";

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerScript = playerBehavior.instance;
        player = playerScript.gameObject;
        playerBody = player.GetComponent<CapsuleCollider>();

        lmbIcon = lmbGroup.transform.GetComponentInChildren<Image>();
        lmbText = lmbIcon.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();
        lmbGroup.alpha = 0f;      
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

    public void displayLMB(string displayText)
    {
        lmbGroup.alpha = 1f;
        lmbText.text = displayText;
    }

    public void hideLMB()
    {
        lmbGroup.alpha = 0f;
    }

    public void updateRadial(float amount)
    {
        if (amount <= 1)
        {
            float cooldownPercent = amount;
            rmb.fillAmount = 1 - cooldownPercent;
        }
        else rmb.fillAmount = 1;
    }

    public void showKeyGot()
    {
        keygot.alpha = 1f;
    }
}
