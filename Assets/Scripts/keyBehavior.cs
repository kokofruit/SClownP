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
        Transform randChild = getRandomChild(spawnPoints.transform);
        transform.position = new Vector3(randChild.position.x, randChild.position.y + 2, randChild.position.z);
    }

    Transform getRandomChild(Transform parent)
    {
        int childRange = parent.transform.childCount;
        int childIndex = Random.Range(0, childRange);
        Transform childChoice = parent.transform.GetChild(childIndex);
        return childChoice;
    }
}
