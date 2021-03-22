using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSensingNearbyPlayer : MonoBehaviour
{
    public zombieScript theZombScript;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
       // Debug.Log("Someone entered zombie sensing trigger");  // GIVE TAG to ALL children OF PLAYER. because then on enter will not show PLAYER tag. if child enters first
        //Debug.Log(col.gameObject.name.ToString());
       

        if (col.gameObject.tag == "Player")
        {
            //theZombScript.myZombieAgent.isStopped = false;
            theZombScript.collidedWithEnviornmentWhileChasing = false;
            theZombScript.canChasePlayer = true;
            //theZombScript.canRotateTowardsPlayer = true;

            theZombScript.basicZombieAnimator.SetBool("isChasingPlayer", true);
         

            //theZombScript.myAgent.SetDestination(theZombScript.target.position);

        }
    }

}
