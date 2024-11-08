using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerBehavior : MonoBehaviour
{

    public float tossCooldown;
    private float tossTimer = 0f;
    
    
    public float throwForce;
    public float throwUpwardForce;
    public TMP_Text output;

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


    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRad = player.GetComponent<SphereCollider>();
        playerCam = player.GetComponent<Camera>();
        playerCam = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        currState = states.idle;

        coinRb = coin.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetKeyDown(KeyCode.LeftControl)){
            playerRad.radius = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerRad.radius = 8f;
        }

        output.text = currState.ToString();
    }

    private states stateTest()
    {
        bool isHorizontally = !Mathf.Approximately(playerRb.velocity.x, 0);
        bool isVertically = !Mathf.Approximately(playerRb.velocity.z, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            return states.sprinting;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            return states.crouching;
        }
        else if (isHorizontally || isVertically)
        {
            return states.walking;
        }
        else
        {
            return states.idle;
        }
    }
    

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

        coin.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);


    }

    #endregion

}
