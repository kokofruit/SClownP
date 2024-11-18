using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class playerBehavior : MonoBehaviour
{
    #region Declare Variables

    // Player variables
    public GameObject player;
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
    GameObject coin;
    Rigidbody coinRb;
    coinBehavior coinScript;
    SphereCollider coinRad;

    // Toss variables
    public float tossCooldown;
    float tossTimer = 0f;
    float torqueForce = 18f;
    float throwForce = 5f;
    float throwUpwardForce = 8f;

    // Key variables
    public bool hasKey = false;

    // UI variables
    public Image rmbRadial;
    public TMP_Text output;

    #endregion

    void Start()
    {
        #region Get Variables
        
        // Player variables
        playerRb = GetComponent<Rigidbody>();
        playerRad = player.GetComponent<SphereCollider>();
        playerCam = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        currState = states.idle;

        // Coin variables
        coinScript = FindObjectOfType<coinBehavior>();
        coin = coinScript.gameObject;
        coinRb = coin.GetComponent<Rigidbody>();
        coinRad = coin.GetComponent<SphereCollider>();

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
            tossCoin();
        }
        updateRadial();

        // Update state
        if (currState != states.locked)
        {
            currState = stateTest();
        }

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            interact();
        }
    }

    #region Behavior

    // Set current state based on FPC script
    states stateTest()
    {
        FirstPersonController fpcScript = player.GetComponent<FirstPersonController>();
        
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

    void interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 2.75f))
        {
            GameObject obj = hit.transform.gameObject;
            print(obj.name);
            if (obj.name == "keycard")
            {
                takeKeyCard(obj);
            }
            if (obj.tag == "door")
            {
                doorBehavior doorScript = obj.GetComponent<doorBehavior>();
                doorScript.doorOpen();
            }
            if (obj.tag == "gate")
            {
                gateBehavior gateScript = obj.GetComponent<gateBehavior>();
                gateScript.gateOpen();
            }
        }
    }

    void takeKeyCard(GameObject obj)
    {
        //Destroy(obj);
        obj.SetActive(false);
        hasKey = true;
    }

    void openDoor(GameObject obj)
    {
        print("Image the door opened");
    }

    #endregion

    #region Distraction Ability

    // Tossing code by Dave / GameDevelopment on Youtube
    // https://www.youtube.com/watch?v=F20Sr5FlUlE
    #region Tossing by Dave / GameDevelopment

    void tossCoin()
    {

        // update timers
        tossTimer = tossCooldown;
        coinScript.existTimer = tossCooldown;

        // set spawnpoint for coin  - by me
        Vector3 spawnPoint = player.transform.position + playerCam.transform.forward * 1.2f;
        coin.transform.position = spawnPoint;

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


        // set horizontal and then add torque to flip - by me
        coin.transform.eulerAngles = new Vector3(90,0,0);
        coinRb.AddTorque(transform.right * torqueForce);

        // set coin detection radius
        coinRad.radius = 0.5f;

    }

    #endregion

    // Update cooldown radial UI
    void updateRadial()
    {
        if (tossCooldown > 0)
        {
            float cooldownPercent = tossTimer / tossCooldown;
            rmbRadial.fillAmount = 1 - cooldownPercent;
        }
    }

    #endregion
}
