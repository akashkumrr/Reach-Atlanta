using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class normalZombieMoveTowards : MonoBehaviour
{


    public Transform playerTransformToFollow;
    public Transform houseTransform;
    public NavMeshAgent myZombAgent;

    public enum State
    {
        MovingToHouse,
        ChasingPlayer,
        Attacking
    }

    public State zState;
    public bool insideAttackRange = false;

    public float zombieTurningSpeed;
    public playerHealth pHealthScript;

    void Start()
    {
        zState = State.MovingToHouse;

        pHealthScript = Object.FindObjectOfType<playerHealth>();        
        myZombAgent.SetDestination(houseTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch(zState)
        {
            case State.MovingToHouse:
                                        break;
            case State.ChasingPlayer:                myZombAgent.SetDestination(playerTransformToFollow.position);
                                        break;
            case State.Attacking:
                                        break;
        }
    }

    public void setDestinationAsHouse()
    {
        myZombAgent.isStopped = false;
        myZombAgent.SetDestination(houseTransform.position);
    }
}
