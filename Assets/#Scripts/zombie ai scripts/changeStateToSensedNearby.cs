using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeStateToSensedNearby : MonoBehaviour
{
    public zombieAiBehaviour zAiScript;

    // This script goes on the child of zombie character. This will interact only with the BODY collider of the player character.
    // So, be careful to set the collider matrix layers in the project settings

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            zAiScript.spitZombieState = zombieAiBehaviour.stateSZ.sensedPlayerNearby;
            zAiScript.isPlayerSensedNearby = true;
            Debug.Log("player character NEARBY");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exited Sensed range");
            zAiScript.isPlayerSensedNearby = false;
        }
    }

}
