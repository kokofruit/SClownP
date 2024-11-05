using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class distractiontesting : MonoBehaviour
{
    // Public var
    public GameObject distractionSource;
    public float distractionRadius;
    public float turnSpeed;
    public float moveSpeed;
    public float rotationMin;
    public float investigationRadius = 4f;
    public TMP_Text writeOut;
    Text output;

    // Enemy vars
    Rigidbody rb;
    Vector3 mv;
    Quaternion rt = Quaternion.identity;
    string st = "ambient";

    NavMeshAgent nma;

    // Distraction vars
    private float distractionDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        output = writeOut.GetComponent<Text>();
        nma = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        nma.destination = distractionSource.transform.position;
    }

    void notCalled()
    {
        // calculate distance b/w enemy and source
        distractionDistance = Vector3.Distance(rb.position, distractionSource.transform.position);

        // ambient behavior
        if (st == "ambient")
        {

            // If distraction is close enough, enter targeting mode
            if (distractionDistance <= distractionRadius)
            {
                st = "targeting";
            }
        }

        // targeting behavior
        if (st == "targeting")
        {
            // Rotate towards distraction
            Vector3 direction = (distractionSource.transform.position - rb.position).normalized;
            Quaternion targetRt = Quaternion.LookRotation(direction);
            rt = Quaternion.RotateTowards(transform.rotation, targetRt, turnSpeed * Time.deltaTime);
            rb.MoveRotation(rt);

            // Move towards distraction
            if (Quaternion.Angle(rt, targetRt) < rotationMin)
            {
                st = "pathing";
            }



        }

        // pathing behavior
        if (st == "pathing")
        {
            mv = Vector3.MoveTowards(rb.position, distractionSource.transform.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(mv);

            // Investigate if close enough
            if (distractionDistance <= investigationRadius)
            {
                st = "investigating";
            }
        }

        writeOut.text = distractionDistance.ToString("F2");
    }
}
