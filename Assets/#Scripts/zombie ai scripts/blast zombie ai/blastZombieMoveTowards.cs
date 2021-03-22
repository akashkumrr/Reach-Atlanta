using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class blastZombieMoveTowards : MonoBehaviour
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
    public bool escapedBlast = false;

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
        switch (zState)
        {
            case State.MovingToHouse:
                break;
            case State.ChasingPlayer:
                myZombAgent.SetDestination(playerTransformToFollow.position);
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

    public void destroyBlastZombieAfterBlast()
    {
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        Debug.Log("Zombie should get destroyed"+gameObject.name+transform.position);
    }
}
