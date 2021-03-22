using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieRootmotion : MonoBehaviour
{

    public NavMeshAgent agent;
    public float rotateSpeed;
    public Animator anim;

    public myRagdoll ragdollScript;
    //agent is the NavMeshAgent component
    //rotateSpeed is your float variable
    //anim is the Animator component
    void Update()
    {
        //direction towards current "corner" on the current path towards destination
        Vector3 targetDirection = agent.steeringTarget - transform.position;
        targetDirection.y = 0; //Set y to zero so that the agent doesn't rotate upwards/downwards

        //rotate smoothly towards desired direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), rotateSpeed * Time.deltaTime);
    }

    void OnAnimatorMove()
    {
        if (ragdollScript.ragdollActive == false)
        {
            Vector3 animVelocity = anim.deltaPosition / Time.deltaTime;
            agent.velocity = animVelocity;
        }
    }
}
