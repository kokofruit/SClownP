using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehavior : MonoBehaviour
{

    private float tossCooldown = 0f;
    public GameObject coin;
    public GameObject player;

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
        coin.transform.position = player.transform.position;
        tossCooldown = 30f;
    }
}
