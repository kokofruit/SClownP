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

    // UI variables
    [SerializeField] Image rmbRadial;

    // Sound
    [SerializeField] AudioClip coinFlickSound;

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
            soundFXManager.instance.PlayFXClip(coinFlickSound, transform);
            tossCoin();
        }
        uiManager.instance.updateRadial(tossTimer / tossCooldown);

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
                    if (mousePressed) takeKeyCard(obj);
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
                        gateScript.gateOpen();
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
                    if (obj.transform.parent != null && obj.transform.parent.tag == "cabinet") obj = obj.transform.parent.gameObject;

                    cabinetBehavior cabScript = obj.GetComponent<cabinetBehavior>();

                    if (mousePressed)
                    {
                        cabScript.search();

                        keyManager keyMng = keyManager.instance;
                        if (!keyMng.playerHasKey && obj == keyMng.storedCabinet)
                        {
                            keyMng.playerHasKey = true;
                            uiManager.instance.showKeyGot();
                        }
                    }
                    break;
                default:
                    break;
            }
            
            //highlight(obj);
            print(obj.name);
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

    void takeKeyCard(GameObject obj)
    {
        //Destroy(obj);
        obj.SetActive(false);
        keyManager.instance.playerHasKey = true;
        uiManager.instance.showKeyGot();
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

    // Tossing code by Dave / GameDevelopment on Youtube
    // https://www.youtube.com/watch?v=F20Sr5FlUlE
    #region Tossing by Dave / GameDevelopment

    void tossCoin()
    {

        // update timers
        tossTimer = tossCooldown;
        //coinScript.existTimer = tossCooldown;

        // set spawnpoint for coin  - by me
        Vector3 spawnPoint = transform.position + playerCam.transform.forward * 1.2f;
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
    #endregion

    #region Lose Game

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            loseGame();
        }
    }

    private void loseGame()
    {

    }

    #endregion
}
