using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCard : MonoBehaviour, interactInterface
{
    void interactInterface.getLMBVal()
    {
        string displayText = "Take";
        uiManager.instance.displayLMB(displayText);
    }
}
