using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitZombieSpitRange : MonoBehaviour
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

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            theSZMTScript.zState = spitZombieMoveTowards.State.Roaming;
            //theSZMTScript.setDestinationAsHouse();
            theSZMTScript.myZombAgent.isStopped = false;
        }
    }

}
