using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawnZombieMoveAi : MonoBehaviour
{

    public NavMeshAgent zombieNavmeshAgent;
    public Transform houseAsATargetTransform;
    public Transform playerTransform;

    public Animator spawnedZombieAnimator;
    public playerHealth pHealthScript;

    public bool isChasingPlayer=false;
    

    // *** This script will be on gameobject which has Animator i.e. on actual model. Genererally we have scripts on parents so that all children move together
    // but here we need ATTACK animations to set some values in this script. So we need to place this script on the model
    void Start()
    {        
        //zombieNavmeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        // zombieNavmeshAgent is on the parent object
        zombieNavmeshAgent.SetDestination(houseAsATargetTransform.position);
        pHealthScript = Object.FindObjectOfType<playerHealth>();

    }


    void Update()
    {
        if(isChasingPlayer==true)
        {
            zombieNavmeshAgent.SetDestination(playerTransform.position);
        }

         //transform.forward = Vector3.Lerp(transform.forward, new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z), Time.deltaTime * zombieTurningSpeed);


    }



    public void startChasingPlayer()
    {
        spawnedZombieAnimator.SetTrigger("run"); // then choose attack animation in 

        zombieNavmeshAgent.isStopped = false;
        isChasingPlayer = true;
    }

    public void stopChasingPlayerAndGoTowardsHouse()
    {
        spawnedZombieAnimator.SetTrigger("run"); // then choose attack animation in 
        Debug.Log("set target to house");
        isChasingPlayer = false;
        zombieNavmeshAgent.isStopped = false;
        zombieNavmeshAgent.SetDestination(houseAsATargetTransform.position);
    }

    public void zombieGotHitScreamAndChaseLikeViral()  // on getting hit from player at a certain far away distance
    {
        zombieNavmeshAgent.isStopped = true;    // Stop and scream and then after scream start moving again and chase the player i.e. call startChasingPlayer() Anim Event on Scream Animation
        //zombieNavmeshAgent.isStopped = false;
        
        spawnedZombieAnimator.SetTrigger("viral scream and sprint fast towards player");
    }


    public void zombieIsAttackingPlayerStopMoving()
    {
        isChasingPlayer = false;
        zombieNavmeshAgent.isStopped = true; 
        spawnedZombieAnimator.SetTrigger("attack"); // then choose attack animation in 
    }

    public void chooseFromDifferentAttackAnimations()  // this is an Animation Event 
    {
        int attackType = Random.Range(1, 4); //  If using floats, the min and max value are inclusive, if using integers, the max value is exclusive. that's why 4

        spawnedZombieAnimator.SetInteger("atktype", attackType);
    }
}
