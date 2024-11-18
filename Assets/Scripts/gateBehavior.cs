using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateBehavior : MonoBehaviour
{
    // set og position
    Vector3 startPos;
    float height;

    // set state
    bool isMoving = false;
    bool isOpen = false;

    // for opening/closing
    Vector3 goal;
    public float openspeed = 7f;

    void Start()
    {
        startPos = transform.position;
        height = GetComponent<Renderer>().bounds.size.y;
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
            if (isOpen)
            {
                goal = startPos;
                isOpen = false;
            }
            else
            {
                goal = new Vector3(startPos.x, startPos.y + height, startPos.z);
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

        Vector3 pos = Vector3.MoveTowards(transform.position, goal, openspeed);
        transform.position = pos;
    }
}
