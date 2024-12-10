using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateBehavior : MonoBehaviour, interactInterface
{
    bool isGate;

    // set og position
    Vector3 startPos;
    float height;

    // set state
    bool isMoving = false;
    bool isOpen = false;

    // for opening/closing
    Vector3 goal;
    public float openspeed = 7f;

    [SerializeField] AudioClip gateOpenFX;
    [SerializeField] AudioClip elevatorFX;

    public bool isLocked;

    void Start()
    {
        isGate = tag == "uGate";
        startPos = transform.position;
        height = GetComponent<Renderer>().bounds.size.y;
        print(height);
    }

    void Update()
    {
        if (isMoving)
        {
            openingAction();
        }
    }

    public void gateOpen()
    {
        if (!isMoving)
        {
            if (isGate) soundFXManager.instance.PlayRandomPitch(gateOpenFX, transform, 0.2f);
            else soundFXManager.instance.PlayFXClip(elevatorFX,transform);

            if (isOpen)
            {
                goal = startPos;
                isOpen = false;
            }
            else
            {
                if (isGate) goal = new Vector3(startPos.x, startPos.y + height, startPos.z);
                else goal = new Vector3(startPos.x, startPos.y, startPos.z - 2.5f);
            }
            isMoving = true;
        }
    }

    void openingAction()
    {
        if (transform.position == goal)
        {
            if (goal != startPos)
            {
                isOpen = true;
            }
            isMoving = false;
            return;
        }

        Vector3 pos = Vector3.MoveTowards(transform.position, goal, openspeed * Time.deltaTime);
        transform.position = pos;
    }

    public void getLMBVal()
    {
        //string displayText = isOpen ? (isGate ? "Close" : "") : "Open";
        string displayText = isGate ? (isLocked ? "Locked" : (isOpen ? "Close" : "Open")) : "Open";
        
        uiManager.instance.displayLMB(displayText);
    }
}
