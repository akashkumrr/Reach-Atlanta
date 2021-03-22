using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastZombieBlastRange : MonoBehaviour
{

    public blastZombieMoveTowards theBZMTScript;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            theBZMTScript.escapedBlast = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            theBZMTScript.escapedBlast = true;
        }
    }

}
