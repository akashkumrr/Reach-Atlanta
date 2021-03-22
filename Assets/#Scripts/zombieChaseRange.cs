using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieChaseRange : MonoBehaviour
{
    public zombieScript theZS;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            //Debug.Log("player exited the chase trigger");
            theZS.canChasePlayer = false;
            theZS.collidedWithEnviornmentWhileChasing = false;
           // theZS.canRotateTowardsPlayer = false;
            //theZS.myZombieAgent.isStopped = true;
            theZS.basicZombieAnimator.SetBool("isChasingPlayer", false);
            theZS.zombieRigidbody.velocity = Vector3.zero;

        }
    }
}
