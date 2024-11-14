using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keySpawn : MonoBehaviour
{
    public Dictionary<string, int[,]> coordDict = new Dictionary<string, int[,]>();

    // Start is called before the first frame update
    void Start()
    {
        //coordDict.Add("first", new int[1,2] {{1,2}});

        //foreach (string key in coordDict.Keys)
        //{
        //    print(coordDict[key].ToString());
        //}
    }
}
