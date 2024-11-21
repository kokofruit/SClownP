using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetBehavior : MonoBehaviour
{
    bool isSearched = false;

    public void search()
    {
        if (!isSearched)
        {
            foreach (Transform child in transform)
            {
                child.position = child.position - (transform.forward * 0.4f);
            }
            isSearched = true;
        }
    }
}
