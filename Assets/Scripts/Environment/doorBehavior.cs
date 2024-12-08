using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class doorBehavior : MonoBehaviour, interactInterface
{
    // set og rotation
    Vector3 startRot;

    // set state
    bool isMoving = false;
    bool isOpen = false;

    // for opening/closing
    Quaternion goal;
    public float openspeed = 7f;

    [SerializeField] AudioClip doorOpenFX;

    void Start()
    {
        startRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            openingAction();
        }
    }

    public void doorOpen()
    {
        if (!isMoving)
        {
            soundFXManager.instance.PlayRandomPitch(doorOpenFX, transform, 0.2f);
            //soundFXManager.instance.PlayFXClip(doorOpenFX, transform);
            if (isOpen)
            {
                goal = Quaternion.Euler(startRot);
            }
            else
            {
                goal = Quaternion.Euler(new Vector3(0, (startRot.y + 90), 0));
            }
            isMoving = true;
            isOpen = !isOpen;
        }
    }

    void openingAction()
    {
        if (transform.rotation == goal)
        {
            isMoving = false;
            return;
        }
        Quaternion rot = Quaternion.RotateTowards(transform.rotation, goal, openspeed * Time.deltaTime);
        transform.rotation = rot;
    }

    public void getLMBVal()
    {
        string displayText = isOpen ? "Close" : "Open";
        uiManager.instance.displayLMB(displayText);
    }
}
