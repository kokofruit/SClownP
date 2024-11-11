using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerBehavior : MonoBehaviour
{

    // Player variables
    public GameObject player;
    private Rigidbody playerRb;
    private SphereCollider playerRad;
    private Camera playerCam;
    public enum states
    {
        idle,
        walking,
        sprinting,
        crouching
    }
    public states currState;

    // Coin variables
    public GameObject coin;
    private Rigidbody coinRb;

    // Enemy variables
    public GameObject enemy;
    private enemy939Behavior enemyScript;

    // Toss variables
    public float tossCooldown;
    private float tossTimer = 0f;
    public float torqueForce;
    public float throwForce;
    public float throwUpwardForce;

    public TMP_Text output;



    // Start is called before the first frame update
    void Start()
    {
        #region Get Variables

        // Player variables
        playerRb = GetComponent<Rigidbody>();
        playerRad = player.GetComponent<SphereCollider>();
        playerCam = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        currState = states.idle;

        // Coin variables
        coinRb = coin.GetComponent<Rigidbody>();

        // Enemy variables
        enemyScript = enemy.GetComponent<enemy939Behavior>();

        #endregion
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currState = stateTest();
        if (tossCooldown > 0)
        {
            tossCooldown -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && coin.GetComponent<coinBehavior>().nearGround())
        {
            tossCoin();
        }

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
        }

        output.text = currState.ToString();
    }

    // Set current state based on FPC script
    private states stateTest()
    {
        //bool isHorizontally = !Mathf.Approximately(playerRb.velocity.x, 0);
        //bool isVertically = !Mathf.Approximately(playerRb.velocity.z, 0);

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    return states.sprinting;
        //}
        //else if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    return states.crouching;
        //}
        //else if (isHorizontally || isVertically)
        //{
        //    return states.walking;
        //}
        //else
        //{
        //    return states.idle;
        //}
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

    #region Behavior Functions
    private void doWalk()
    {
        playerRad.radius = 8f;
    }

    private void doSprint()
    {
        playerRad.radius = 10f;
    }

    private void doCrouch()
    {
        if (enemyScript.currState != enemy939Behavior.states.chasing)
        {
            playerRad.radius = 4f;
        }
    }
    #endregion

    // Tossing code by Dave / GameDevelopment on Youtube
    // https://www.youtube.com/watch?v=F20Sr5FlUlE
    #region Tossing by Dave / GameDevelopment

    void tossCoin()
    {
        tossTimer = tossCooldown;

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

        coinRb.AddTorque(coinRb.transform.up * torqueForce);

    }

    #endregion

}
