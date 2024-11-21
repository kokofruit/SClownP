using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetBehavior : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            child.position = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
