using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastZombieChaseRange : MonoBehaviour
{
    public blastZombieMoveTowards theBZMTNormalScript;

   
    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {
            theBZMTNormalScript.zState = blastZombieMoveTowards.State.ChasingPlayer;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            theBZMTNormalScript.zState = blastZombieMoveTowards.State.MovingToHouse;
            theBZMTNormalScript.setDestinationAsHouse();
        }
    }
}
