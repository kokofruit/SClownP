using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    private Rigidbody rb;

    public float existTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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

    void resetCoin()
    {
        rb.transform.rotation = new Quaternion(90,0,0,0);
    }

}
