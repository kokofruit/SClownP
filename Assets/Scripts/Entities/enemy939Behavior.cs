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
    NavMeshAgent nma;
    Animator ant;
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

    // Output vars
    public TMPro.TMP_Text output;

    // sound
    [SerializeField] AudioClip distractedGrowl;
    [SerializeField] AudioClip chasingGrowl;

    #endregion

    void Start()
    {
        #region Get Variables

        // Enemy vars
        nma = GetComponent<NavMeshAgent>();
        ant = GetComponent<Animator>();

        // Player vars
        playerScript = FindObjectOfType<playerBehavior>();
        player = playerScript.gameObject;
        playerCap = player.GetComponent<CapsuleCollider>();
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
        //print();
        //output.text = currState.ToString();
    }

    #region Object detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (currState != states.chasing)
            {
                soundFXManager.instance.PlayFXClip(chasingGrowl, transform);
            }
        }
        if (other.gameObject.GetComponent<coinBehavior>() is coinBehavior)
        {
            soundFXManager.instance.PlayFXClip(distractedGrowl, transform);
            ant.SetInteger("stateA", 1);
            currState = states.seeking;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            ant.SetInteger("stateA", 2);
            currState = states.chasing;
            playerScript.currState = playerBehavior.states.locked;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            ant.SetInteger("stateA", 0);
            currState = states.idle;
            playerScript.currState = playerBehavior.states.idle;
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
            nma.stoppingDistance = 0f;
            nma.speed = 1f;
            nma.SetDestination(RandomNavSphere(transform.position, 10.0f, floormask));

            ant.SetInteger("stateA", 1);
            currState = states.wandering;
        }
    }

    void doWander()
    {
        if (!nma.pathPending && !nma.hasPath)
        {
            waitTimer = UnityEngine.Random.Range(3.0f, 4.0f);

            ant.SetInteger("stateA", 0);
            currState = states.idle;
        }
        soundFXManager.instance.PlayFootStep("939", "crouch");
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
        GameObject coin = FindObjectOfType<coinBehavior>().gameObject;
        nma.SetDestination(coin.transform.position);
        nma.stoppingDistance = 2f;
        nma.speed = 2f;
        soundFXManager.instance.PlayFootStep("939", "walk");

        if (Vector3.Distance(transform.position, nma.destination) < 2f)
        {
            ant.SetInteger("stateA", 0);
            currState = states.distracted;
        }
    }

    void doDistracted()
    {
        //Quaternion target = Quaternion.LookRotation(coin.transform.position);
        //var rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed);
        //transform.rotation = rotation;
        if (FindObjectOfType<coinBehavior>() == null)
        {
            ant.SetInteger("stateA", 0);
            currState = states.idle;
        }
    }

    void doChasing()
    {
        nma.SetDestination(player.transform.position);
        nma.stoppingDistance = 0f;
        nma.speed = 3f;
        soundFXManager.instance.PlayFootStep("939", "sprint");
    }

    #endregion

}
