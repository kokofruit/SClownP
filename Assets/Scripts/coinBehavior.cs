using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    public bool isGrounded;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = nearGround();
    }

    public bool nearGround()
    {
        if (Mathf.Abs(rb.velocity.y) > 1f)
        {
            return false;
        }
        if (rb.transform.position.y > 0.6f)
        {
            return false;
        }
        return true;
    }

}
