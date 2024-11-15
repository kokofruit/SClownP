using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class doorBehavior : MonoBehaviour
{
    Vector3 pivotPoint;
    GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doorOpen()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
    }
}
