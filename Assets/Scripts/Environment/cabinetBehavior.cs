using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabinetBehavior : MonoBehaviour, interactInterface
{
    public bool isOpen = false;
    bool isMoving = false;

    [SerializeField] AudioClip[] drawerRummageFX;
    [SerializeField] AudioClip drawerCloseFX;

    public Vector3 goal;
    public float moveSpeed;

    private void Update()
    {
        if (isMoving)
        {
            doOpen();
        }
    }

    public void search()
    {
        if (!isMoving)
        {
            // sound fx
            if (!isOpen) soundFXManager.instance.PlayRandomPitch(drawerRummageFX[Random.Range(0, drawerRummageFX.Length)], transform, 0.2f, volume: 0.5f);
            else soundFXManager.instance.PlayRandomPitch(drawerCloseFX, transform, 0.2f);

            float dir = isOpen ? 0.4f : -0.4f;
            goal = transform.GetChild(0).position + (transform.forward * dir);

            isOpen = !isOpen;
            isMoving = true;
        }
    }

    void doOpen()
    {
        Transform kid1 = transform.GetChild(0);
        if (kid1.position == goal)
        {
            isMoving = false;
            return;
        }

        Vector3 pos = Vector3.MoveTowards(kid1.position, goal, moveSpeed * Time.deltaTime);
        foreach (Transform child in transform)
        {
            child.position = new Vector3(pos.x, child.position.y, pos.z);
        }
    }

    public void getLMBVal()
    {
        string displayText = isOpen ? "Close" : "Search";
        uiManager.instance.displayLMB(displayText);
    }
}
