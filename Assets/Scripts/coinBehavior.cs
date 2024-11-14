using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    private Rigidbody rb;
    SphereCollider coinRad;

    public float existTimer = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinRad = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        if (rb.transform.position.y > -5)
        {
            if (existTimer > 0)
            {
                existTimer -= Time.deltaTime;
            }
            else
            {
                //rb.transform.position = new Vector3(0, -10, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "surface")
        {

            //int amount = collision.contactCount;
            coinRad.radius = 8f;
        }
    }
}
