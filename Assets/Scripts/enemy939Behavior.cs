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
    // Public vars
    public float moveSpeed;
    public LayerMask floormask;

    public GameObject distraction;
    
    public GameObject player;

    public TMPro.TMP_Text output;

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

    // Timer vars
    private float waitTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();

        nma.speed = moveSpeed;

        
    }

    // Update function
    void FixedUpdate()
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
        }
        output.text = currState.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            currState = states.chasing;
        }
        else if (other.gameObject == distraction && currState != states.distracted && currState != states.chasing)
        {
            nma.stoppingDistance = 2f;
            nma.SetDestination(distraction.transform.position);
            currState = states.seeking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player || other.gameObject == distraction)
        {
            currState = states.idle;
        }
    }

    // Wandering AI written by Innocent Qwa on youtube
    // https://www.youtube.com/watch?v=K2yirE5W2aU
    #region Wandering AI by Innocent Qwa

    private void doIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }
        else
        {
            nma.stoppingDistance = 0f; // added by me
            nma.SetDestination(RandomNavSphere(transform.position, 10.0f, floormask));

            currState = states.wandering;
        }
    }

    private void doWander()
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


    private void doSeeking()
    {
        if (nma.pathStatus == NavMeshPathStatus.PathComplete)
        {
            //distractedTimer = 30f;
            currState = states.distracted;
        }
    }

    private void doChasing()
    {
        nma.SetDestination(player.transform.position);
    }
}
