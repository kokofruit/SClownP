using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distractiontesting : MonoBehaviour
{
    // Public var
    public GameObject distractionSource;
    public float distractionProxRequirement = 5;
    public float turnSpeed = 1f;
    public float moveSpeed = 1f;
    public float rotationMin = 1f;
    public float objectDistMin = 1f;

    // Enemy vars
    Rigidbody rb;
    Vector3 mv;
    Quaternion rt = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If distraction is close enough
        float distractionDistance = Vector3.Distance(rb.position, distractionSource.transform.position);
        if (objectDistMin <= distractionDistance && distractionDistance <= distractionProxRequirement) {
            
            // Rotate towards distraction
            Vector3 direction = distractionSource.transform.position - rb.position;
            Quaternion targetRt = Quaternion.LookRotation(direction);
            rt = Quaternion.RotateTowards(transform.rotation, targetRt, turnSpeed * Time.deltaTime);

            // Move towards distraction
            if (distractionDistance > objectDistMin)
            {
                mv = Vector3.MoveTowards(rb.position, distractionSource.transform.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(mv);
            }
        }
        rb.MoveRotation(rt);
    }

}
