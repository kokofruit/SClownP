using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distractiontesting : MonoBehaviour
{
    // Public var
    public GameObject distractionSource;
    public float distractionProxRequirement = 5;
    public float turnSpeed = 20f;

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
        float distractionDistance = Vector3.Distance(rb.position, distractionSource.transform.position);
        if (distractionDistance <= distractionProxRequirement) {
            Vector3 direction = distractionSource.transform.position - rb.position;
            Quaternion targetRt = Quaternion.LookRotation(direction);
            rt = Quaternion.RotateTowards(transform.rotation, targetRt, turnSpeed * Time.deltaTime);
        }
        rb.MoveRotation(rt);
    }

}
