using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    public GameObject coin;
    public bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckGround()
    {
        Vector3 origin = new Vector3(coin.transform.position.x, coin.transform.position.y - (coin.transform.localScale.y * .5f), coin.transform.position.z);
        Vector3 direction = coin.transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

}
