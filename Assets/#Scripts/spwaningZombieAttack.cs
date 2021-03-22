using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwaningZombieAttack : MonoBehaviour
{

    public spawnZombieMoveAi theZombieMoveScript;
    public bool isPlayerCanBeAttacked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkIfPlayerInAttackingTriggerThenReduceHealth()  // this is an Animation Event 
    {
        if (isPlayerCanBeAttacked == true)   // as long as player doesn't leave the attack trigger The player is close enough to take damge from the zombie attack
        {
            theZombieMoveScript.pHealthScript.currentHealth -= 10f; // this number to be subtracted should depend on attack type
        }
    }

    void setZombieAttackAsComplete()        // this will be set when zombie has completed attacking the player
    {
       // Debug.Log("last Attck done");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "House")
        {
            isPlayerCanBeAttacked  = true;
            theZombieMoveScript.zombieIsAttackingPlayerStopMoving();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "House")
        {
            isPlayerCanBeAttacked = false;
            theZombieMoveScript.startChasingPlayer();

        }
    }
}
