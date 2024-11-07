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
    public GameObject coin;
    public GameObject player;
    public Camera cam;
    public float throwForce;
    public float throwUpwardForce;
    public TMP_Text output;

    private Rigidbody coinRb;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = coin.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tossCooldown > 0)
        {
            tossCooldown -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && nearGround())
        {
            output.text = nearGround().ToString();
            tossCoin();
        }
    }

    // Tossing code by Dave / GameDevelopment on Youtube
    // https://www.youtube.com/watch?v=F20Sr5FlUlE
    #region Tossing by Dave / GameDevelopment

    void tossCoin()
    {
        tossTimer = tossCooldown;

        // set spawnpoint for coin 
        Vector3 spawnPoint = player.transform.position + player.transform.forward;
        coin.transform.position = spawnPoint;


        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - spawnPoint).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        coin.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);


    }

    #endregion

    bool nearGround()
    {
        if (Mathf.Abs(coinRb.velocity.y) > 1f)
        {
            return false;
        }
        if (coinRb.transform.position.y > 0.6f)
        {
            return false;
        }
        return true;
    }

}
