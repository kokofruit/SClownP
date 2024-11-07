using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehavior : MonoBehaviour
{

    public float tossCooldown;
    private float tossTimer = 0f;
    public GameObject coin;
    public GameObject player;
    public Camera cam;
    public float throwForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tossCooldown > 0)
        {
            tossCooldown -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            tossCoin();
        }
    }

    void tossCoin()
    {
        //tossTimer = tossCooldown;
        //coin.transform.position = player.transform.position;
        // Get coin rigidbody
        Rigidbody coinRb = coin.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;
        //RaycastHit hit;

        // add force
       

    }
}
