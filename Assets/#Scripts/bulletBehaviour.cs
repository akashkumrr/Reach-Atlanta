using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    // # NOTE : every bullet must have this script on it
    public float bulletDamage; // DO NOT SET THIS IN INSPECTOR. This is just to refence the wapon damage out 
                               // from which the bullet came from
    
    void Start()
    {       
    }

    void Update()
    {        
    }

    // # NOTE : it'd would be better if we check zombie if any bullet hit them, not if bullet hit any zombie.
    // because in the latter case. it'd expensive to reduce the health of the zombie who has been hit.

    /*

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Bullet collided with something");
        if(col.gameObject.tag=="enemy")
        {
            Debug.Log("Bullet collided into enemy. Take enemy health down");

        }
    }
    */

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            gameObject.GetComponent<TrailRenderer>().enabled=false;
        }
    }
}
