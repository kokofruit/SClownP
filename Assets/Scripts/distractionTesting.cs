using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class distractiontesting : MonoBehaviour
{
    // Public vars
    public float moveSpeed;
    public LayerMask floormask;

    public GameObject distraction;
    public GameObject player;

    public TMPro.TMP_Text output;

    // Enemy vars
    Rigidbody rb;
    NavMeshAgent nma;

    enum states
    {
        idle,
        wandering,
        seeking,
        distracted,
        chasing
    }
    
    states currState = states.idle;

    // Timer vars
    private float waitTimer = 0.0f;
    private float distractedTimer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();

        nma.speed = moveSpeed;
    }

    // Update function
    private void Update()
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
            case states.distracted:
                doDistracted();
                break;
        }

        output.text = currState.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            nma.SetDestination(player.transform.position);
            currState = states.chasing;
        }
        else if (other.gameObject == distraction)
        {
            if (currState == states.idle || currState == states.wandering)
            {
                //nma.stoppingDistance = 2f;
                nma.SetDestination(distraction.transform.position);
                currState = states.seeking;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            currState = states.idle;
        }
    }

    // Wandering AI written by Innocent Qwa on youtube
    // https://www.youtube.com/watch?v=K2yirE5W2aU
    // Code section below v

    private void doIdle()
    {

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        //nma.stoppingDistance = 1f; // added by me
        nma.SetDestination(RandomNavSphere(transform.position, 10.0f, floormask));
        currState = states.wandering;

    }

    private void doWander()
    {
        if (nma.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return;
        }

        waitTimer = UnityEngine.Random.Range(1.0f, 4.0f);
        currState = states.idle;
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }

    // Innocent Qwa's Code section above ^

    

    private void doSeeking()
    {
        if (nma.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return;
        }

        distractedTimer = 30f;
        currState = states.idle;
    }

    private void doDistracted()
    {
        if (distractedTimer > 0)
        {
            distractedTimer -= Time.deltaTime;
            return;
        }

        currState = states.idle;
    }
}
