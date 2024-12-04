using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour
{
    // Instance
    public static keyManager instance;

    // Spawn Technique
    float spawnStrat = 0;//Random.Range(0, 2);

    // Spawning key
    [SerializeField] GameObject keycard;
    [SerializeField] GameObject keyParent;

    // Spawnpoints
    [SerializeField] GameObject openSpawnPoints;
    [SerializeField] GameObject objectSpawnPoints;

    // Selected Cabinet
    public GameObject storedCabinet;

    // Player has key
    public bool playerHasKey = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

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

        GameObject key = Instantiate(keycard, position: randSpawnPoint.position, randRotation, parent: keyParent.transform);
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
