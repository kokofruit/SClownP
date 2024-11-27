using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetBehavior : MonoBehaviour
{
    bool isSearched = false;

    public void search()
    {
        foreach (Transform child in transform)
        {
            float dir = isSearched ? 0.4f : -0.4f;
            child.position = child.position + (transform.forward * dir);
        }
        isSearched = !isSearched;
    }
}
