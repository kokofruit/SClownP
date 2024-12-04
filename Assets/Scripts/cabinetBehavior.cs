using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetBehavior : MonoBehaviour, interactInterface
{
    public bool isOpen = false;

    public void search()
    {
        foreach (Transform child in transform)
        {
            float dir = isOpen ? 0.4f : -0.4f;
            child.position = child.position + (transform.forward * dir);
        }
        isOpen = !isOpen;
    }

    public void getLMBVal()
    {
        string displayText = isOpen ? "Close" : "Search";
        uiManager.instance.displayLMB(displayText);
    }
}
