using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyBehavior : MonoBehaviour
{
    bool spawnStratIsOpen = true;

    public GameObject spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnStratIsOpen) // Location based
        {
            spawnKeyOpen();
        }
        else
        {
            print("object based");
        }
    }
    
    void spawnKeyOpen()
    {
        Transform randChild = getRandomChild();
        transform.position = new Vector3(randChild.position.x, randChild.position.y + 2, randChild.position.z);
    }

    Transform getRandomChild()
    {
        int childRange = spawnPoints.transform.childCount;
        int childIndex = Random.Range(0, childRange);
        Transform childChoice = spawnPoints.transform.GetChild(childIndex);
        return childChoice;
    }
}
