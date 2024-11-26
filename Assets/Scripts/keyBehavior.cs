using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class keyBehavior : MonoBehaviour
{
    float spawnStrat = 0;//Random.Range(0, 2);

    public GameObject keycard;
    public GameObject keyParent;

    public GameObject openSpawnPoints;
    public GameObject objectSpawnPoints;

    public GameObject storedCabinet;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnStrat == 1) //Location based
        {
            spawnKeyOpen();
            print("location based");
        }
        else //Object based
        {
            print("object based");
            spawnKeyObject();
        }
    }
    
    void spawnKeyOpen()
    {
        //Transform randSpawnRoom = getRandomChild(openSpawnPoints.transform);
        //Transform randSpawnPoint = getRandomChild(randSpawnRoom);
        Transform randSpawnPoint = getRandomChild(openSpawnPoints.transform);
        //transform.position = new Vector3(randSpawnPoint.position.x, randSpawnPoint.position.y + 2, randSpawnPoint.position.z);
        Quaternion randRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        GameObject key = Instantiate(keycard, position:randSpawnPoint.position, randRotation, parent:keyParent.transform);
    }

    void spawnKeyObject()
    {
        Transform randCabinet = getRandomChild(objectSpawnPoints.transform);
        //randCabinet.gameObject.SetActive(false);

        storedCabinet = randCabinet.gameObject;
    }

    Transform getRandomChild(Transform parent)
    {
        int childRange = parent.transform.childCount;
        int childIndex = Random.Range(0, childRange);
        Transform childChoice = parent.transform.GetChild(childIndex);
        return childChoice;
    }
}
