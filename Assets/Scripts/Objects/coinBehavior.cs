using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    private Rigidbody rb;
    SphereCollider coinRad;

    [SerializeField] AudioClip landSound;
    [SerializeField] AudioClip disappearSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinRad = GetComponent<SphereCollider>();
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<NavMeshSurface>() != null)
        {

            //int amount = collision.contactCount;
            coinRad.radius = 13f;
            soundFXManager.instance.PlayFXClip(landSound, transform);
        }
    }

    private void OnDestroy()
    {
        soundFXManager.instance.PlayFXClip(disappearSound, transform, volume:0.25f);
    }
}
