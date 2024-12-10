using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class playerBehavior : MonoBehaviour
{
    
    #region Declare Variables

    // Player variables
    public static playerBehavior instance;
    public enum states
    {
        idle,
        walking,
        sprinting,
        crouching,
        locked
    }
    public states currState;
    Rigidbody playerRb;
    SphereCollider playerRad;
    Camera playerCam;

    // Coin variables
    [SerializeField] GameObject coinObj;

    // Toss variables
    public float tossCooldown;
    float tossTimer = 0f;
    float torqueForce = 18f;
    public float throwForce = 5f;
    public float throwUpwardForce = 8f;

    // UI variables
    [SerializeField] Image rmbRadial;

    // Sound
    [SerializeField] AudioClip coinFlickSound;
    [SerializeField] AudioClip dogAttack;
    [SerializeField] AudioClip keySound;
    public AudioSource stepPlayer;

    #endregion
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        #region Get Variables
        
        // Player variables
        playerRb = GetComponent<Rigidbody>();
        playerRad = GetComponent<SphereCollider>();
        playerCam = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        currState = states.idle;

        #endregion
    }

    void Update()
    {
        // Update cooldown or throw coin
        if (tossTimer >= 0)
        {
            tossTimer -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            soundFXManager.instance.PlayFXClip(coinFlickSound, transform);
            tossCoin();
        }
        uiManager.instance.updateRadial(tossTimer / tossCooldown);

        // Update state
        //if (currState != states.locked)
        //{
        currState = stateTest();
        //}

        // Follow state behavior
        switch (currState)
        {
            case states.idle:
                break;
            case states.walking:
                doWalk();
                break;
            case states.sprinting:
                doSprint();
                break;
            case states.crouching:
                doCrouch();
                break;
            case states.locked:
                break;
        }

        // Interact ability
        interact(Input.GetKeyDown(KeyCode.Mouse0));
    }

    #region Behavior

    // Set current state based on FPC script
    states stateTest()
    {
        FirstPersonController fpcScript = GetComponent<FirstPersonController>();
        
        if (fpcScript.isSprinting)
        {
            return states.sprinting;
        }
        else if (fpcScript.isCrouched)
        {
            return states.crouching;
        }
        else if (fpcScript.isWalking)
        {
            return states.walking;
        }
        else
        {
            return states.idle;
        }
    }

    void doWalk()
    {
        playerRad.radius = 8f;
    }

    void doSprint()
    {
        playerRad.radius = 10f;
    }

    void doCrouch()
    {
        playerRad.radius = 4f;
    }

    void doLocked()
    {
        playerRad.radius = 10f;
    }

    #endregion

    #region Interaction Ability

    void interact(bool mousePressed)
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 2.75f)/* && hit.transform.gameObject.GetComponent<interactInterface>() != null*/)
        {
            GameObject obj = hit.transform.gameObject;

            switch (obj.tag)
            {
                case "key":
                    if (mousePressed) {
                        obj.SetActive(false);
                        takeKeyCard();
                    }
                    break;
                case "rDoor":
                    if (mousePressed)
                    {
                        doorBehavior doorScript = obj.GetComponent<doorBehavior>();
                        doorScript.doorOpen();
                    }
                    break;
                case "uGate":
                    if (mousePressed)
                    {
                        gateBehavior gateScript = obj.GetComponent<gateBehavior>();
                        if (!gateScript.isLocked) gateScript.gateOpen();
                    }
                    break;
                case "eDoor":
                    if (mousePressed)
                    {
                        gateBehavior eDoorScript = obj.GetComponent<gateBehavior>();
                        eDoorScript.gateOpen();
                    }
                    break;
                case "cabinet":
                    // If drawers or handles are clicked, set object to parent cabinet
                    while (obj.transform.parent != null && obj.transform.parent.tag == "cabinet") obj = obj.transform.parent.gameObject;

                    cabinetBehavior cabScript = obj.GetComponent<cabinetBehavior>();

                    if (mousePressed)
                    {
                        cabScript.search();

                        keyManager keyMng = keyManager.instance;
                        if (!keyMng.playerHasKey && obj == keyMng.storedCabinet)
                        {
                            takeKeyCard();
                        }
                    }
                    break;
                default:
                    break;
            }
            
            //highlight(obj);
            
            try
            {
                textBoxManager.instance.initiateText(obj);
                obj.GetComponent<interactInterface>().getLMBVal();
            }
            catch (Exception)
            {
                uiManager.instance.hideLMB();
            }
        }
        else
        {
            uiManager.instance.hideLMB();
        }
    }

    void takeKeyCard()
    {
        keyManager.instance.playerHasKey = true;
        uiManager.instance.showKeyGot();
        soundFXManager.instance.PlayFXClip(keySound, transform);
        if (textBoxManager.instance.interactionTexts.ContainsKey("NeedKeycard")) textBoxManager.instance.interactionTexts.Remove("NeedKeycard");
        foreach (gateBehavior gate in FindObjectsOfType<gateBehavior>())
        {
            gate.isLocked = false;
        }
    }

    public void highlight(GameObject obj)
    {
        if (obj.GetComponent<Outline>() == null)
        {
            Outline outline = obj.AddComponent<Outline>();
            outline.OutlineWidth = 10;
        }
    }

    void deselect()
    {
        uiManager.instance.hideLMB();

        // for every object with outline, remove that shit
        foreach (Outline item in FindObjectsOfType(typeof(Outline)))
        {
            Destroy(item);
        }
    }

    #endregion

    #region Distraction Ability

    // Tossing function made using code by Dave / GameDevelopment on Youtube
    // https://www.youtube.com/watch?v=F20Sr5FlUlE
    #region Tossing by Dave / GameDevelopment

    void tossCoin()
    {
        // update timers
        tossTimer = tossCooldown;

        // set spawnpoint for coin and instantiate
        Vector3 spawnPoint = transform.position + playerCam.transform.forward * 1.5f;
        GameObject coin = GameObject.Instantiate(coinObj, position: spawnPoint, Quaternion.Euler(90, 0, 0));

        // get coin rigidbody
        Rigidbody coinRb = coin.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = playerCam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - spawnPoint).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        coinRb.AddForce(forceToAdd, ForceMode.Impulse);

        // add torque to flip - by me
        coinRb.AddTorque(transform.right * torqueForce);
    }

    #endregion
    #endregion

    #region Lose Game

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "gameEnd")
        {
            if (Vector3.Distance(other.transform.position, transform.position) < 1)
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

    #endregion
}
