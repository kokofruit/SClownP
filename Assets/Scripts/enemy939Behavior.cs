using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class enemy939Behavior : MonoBehaviour
{
    #region Declare variables

    // Enemy vars
    Rigidbody rb;
    NavMeshAgent nma;
    public enum states
    {
        idle,
        wandering,
        seeking,
        distracted,
        chasing
    }
    public states currState = states.idle;
    public float turnSpeed;

    // Navigation vars
    public LayerMask floormask;
    float waitTimer = 0.0f;

    // Player vars
    public GameObject player;
    playerBehavior playerScript;
    CapsuleCollider playerCap;

    // Coin vars
    public GameObject coin;

    // Output vars
    public TMPro.TMP_Text output;

    #endregion

    void Start()
    {
        #region Get Variables

        // Enemy vars
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();

        // Player vars
        playerScript = FindObjectOfType<playerBehavior>();
        player = playerScript.gameObject;
        playerCap = player.GetComponent<CapsuleCollider>();

        // Coin vars
        coin = FindObjectOfType<coinBehavior>().gameObject;
        
        #endregion
    }

    void Update()
    {
        switch (currState)
        {
            case states.idle:
                doIdle();
                break;
            case states.wandering:
                doWander();
                break;
            case states.seeking:
                doSeeking();
                break;
            case states.chasing:
                doChasing();
                break;
            case states.distracted:
                doDistracted();
                break;
        }
        //output.text = currState.ToString();
    }

    #region Object detection
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            currState = states.chasing;
            playerScript.currState = playerBehavior.states.locked;
        }
        else if (other.gameObject == coin && currState != states.chasing)
        {
            currState = states.seeking;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            currState = states.idle;
            playerScript.currState = playerBehavior.states.idle;
        }
        if (other.gameObject == coin && currState != states.chasing)
        {
            currState = states.idle;
        }
    }
    #endregion

    #region Behavior

    // Wandering AI written by Innocent Qwa on youtube
    // https://www.youtube.com/watch?v=K2yirE5W2aU
    #region Wandering AI by Innocent Qwa

    void doIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }
        else
        {
            nma.stoppingDistance = 0f; // added by me
            nma.speed = 1f;
            nma.SetDestination(RandomNavSphere(transform.position, 10.0f, floormask));

            currState = states.wandering;
        }
    }

    void doWander()
    {
        if (nma.pathStatus == NavMeshPathStatus.PathComplete)
        {
            waitTimer = UnityEngine.Random.Range(3.0f, 4.0f);

            currState = states.idle;
            
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }

    #endregion


    void doSeeking()
    {
        nma.SetDestination(coin.transform.position);
        nma.stoppingDistance = 2f;
        nma.speed = 2f;
        
        if (nma.pathStatus == NavMeshPathStatus.PathComplete)
        {
            currState = states.distracted;
        }
    }

    void doDistracted()
    {
        //Quaternion target = Quaternion.LookRotation(coin.transform.position);
        //var rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed);
        //transform.rotation = rotation;
    }

    void doChasing()
    {
        nma.SetDestination(player.transform.position);
        nma.stoppingDistance = 0f;
        nma.speed = 3f;
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == playerCap)
        {
            print("hit!");
        }
    }

}
