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
    // Public var
    public float moveSpeed;
    public LayerMask floormask;

    public GameObject distraction;
    public GameObject player;

    public TMPro.TextMeshPro output;

    // Enemy vars
    Rigidbody rb;
    NavMeshAgent nma;

    enum states
    {
        idle,
        wandering,
        investigating,
        seeking
    }
    
    states currState = states.idle;
    private float waitTimer = 0.0f;
    private GameObject target;
    

    

    // Distraction vars
    private float distractionDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();

        nma.speed = moveSpeed;
    }

    private void Update()
    {
        switch (currState)
        {
            case states.idle:
                testProx();
                doIdle();
                break;
            case states.wandering:
                testProx();
                doWander();
                break;
            case states.investigating:
                doInvestigating();
                break;
        }

        output.text = currState.ToString();
    }

    // Wandering AI written by Innocent Qwa on youtube
    // https://www.youtube.com/watch?v=K2yirE5W2aU

    private void doIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

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

    private void testProx()
    {
        if (Vector3.Distance(transform.position, distraction.transform.position) < 5f)
        {
            nma.SetDestination(distraction.transform.position);
            currState = states.investigating;
            
        }
    }

    private void doInvestigating()
    {
        if (nma.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return;
        }
    }
}
