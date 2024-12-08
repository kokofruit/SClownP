using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundAmbiencePlayer : MonoBehaviour
{
    public static soundAmbiencePlayer instance;

    private void Awake()
    {
        instance = this;
    }

}
