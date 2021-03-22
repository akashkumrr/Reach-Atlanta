using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitZombieStartAttacking : MonoBehaviour
{
    public spitZombieMoveTowards theSZMTScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            theSZMTScript.zState = spitZombieMoveTowards.State.Attacking;
            if (theSZMTScript.myZombAgent.enabled == true)
            {
                theSZMTScript.myZombAgent.isStopped = true;
            }
        }
    }
}
