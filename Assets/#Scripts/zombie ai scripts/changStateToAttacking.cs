using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changStateToAttacking : MonoBehaviour
{
    public zombieAiBehaviour zAiScript;
   
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            zAiScript.spitZombieState = zombieAiBehaviour.stateSZ.attacking;
            zAiScript.isPlayerInsideAttackRange = true;
            Debug.Log("INSIDE ATTACK RANGE");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exited ATTACK");
            zAiScript.isPlayerInsideAttackRange = false;
        }
    }

}
