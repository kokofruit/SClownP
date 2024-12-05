using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    private Rigidbody rb;
    SphereCollider coinRad;

    [SerializeField] AudioClip landSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinRad = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "surface")
        {

            //int amount = collision.contactCount;
            coinRad.radius = 8f;
            soundFXManager.instance.PlayFXClip(landSound, transform);
            //Destroy(gameObject, 4f);
        }
    }
}
