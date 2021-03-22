using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class zombieMoveTowardsPlayer : MonoBehaviour
{
    public Transform playerTransformToFollow;
    public Transform houseTransform;
    public NavMeshAgent myZombAgent;

    public Animator spawnedZombieAnimator;
    public bool isAttacking = false;
    public bool lastAttackComplete = true;
    public int[] attackTypes;

    public bool doOnce=true;
    public bool playerSpottedInRangeStartChasing;
    public float[] zombieMovingSpeeds;
    public float zombieTurningSpeed;
    public playerHealth pHealthScript;

    void Start()
    {
        pHealthScript = Object.FindObjectOfType<playerHealth>();
        myZombAgent = this.gameObject.GetComponent<NavMeshAgent>();
        myZombAgent.SetDestination(houseTransform.position);


        int randRunAnim = Random.Range(1, 3);
        spawnedZombieAnimator.SetInteger("runAnimType", randRunAnim);

        if (randRunAnim == 1 || randRunAnim == 2)
            myZombAgent.speed = 0.3f;
        else
            myZombAgent.speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position,playerTransformToFollow.position,zombieMovingSpeed*Time.deltaTime);
        //transform.forward = Vector3.Lerp(transform.forward, new Vector3(playerTransformToFollow.position.x - transform.position.x, 0, playerTransformToFollow.position.z - transform.position.z), Time.deltaTime * zombieTurningSpeed);
        if (isAttacking == false) // IF NOT ATTACKING THEN ONLY CAN MOVE
        {

            spawnedZombieAnimator.SetBool("isAttackingRightNow", false);
            myZombAgent.isStopped = false;

            if (playerSpottedInRangeStartChasing == true)
            {
                myZombAgent.SetDestination(playerTransformToFollow.position);  // player changes position se we neeed to SET destination every time in Update()
                doOnce = true;
            }
            else
            {
                if (doOnce == true)
                {
                    myZombAgent.SetDestination(houseTransform.position);    // but house position is fixed so need not to worry about it's postion every frame
                    doOnce = false;
                }
            }
        }

        if(isAttacking==true)
        {
            myZombAgent.isStopped = true;

            // this is to enable zombie to rotate while attacking. 
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(playerTransformToFollow.position.x - transform.position.x, 0, playerTransformToFollow.position.z - transform.position.z), Time.deltaTime * zombieTurningSpeed);
        }
        if (isAttacking==true && lastAttackComplete==true)
        {
            lastAttackComplete = false;
            int attackId = Random.Range(1, attackTypes.Length+1);
            Debug.Log(attackId);
            spawnedZombieAnimator.SetInteger("attackTypeAnimID", attackId);
            spawnedZombieAnimator.SetBool("isAttackingRightNow", true);


            int randRunAnim = Random.Range(1, 3);
            spawnedZombieAnimator.SetInteger("runAnimType", randRunAnim);

            

            // set int in animator to play that type of animation
        }
        //parentOfZombie.transform.position = Vector3.MoveTowards(parentOfZombie.transform.position, targetRoamPosition, roamSpeed * Time.deltaTime);
    }

    
}
