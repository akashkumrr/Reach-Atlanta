using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class spitZombieEnemyAi : MonoBehaviour
{ 
    public enum stateSZ
    {
        sensedPlayerNearby,
        attacking,
        chasePlayerUntilInLos,
        huntingPlayer,
        roaming
    }

    public stateSZ spitZombieState;
    [Space(10)]
    public Transform enemyTransform;
    public Transform playerTransform;
    public NavMeshAgent enemyZombAgent;
    [Space(10)]
    public float roamRadius = 5.0f;
    public float waitTime = 3.0f;
    public Vector3 newRoamingPosition;
    [Space(10)]
    public FieldOfView fovScript;
    public bool isPlayerSensedNearby = false;
    public InterfaceZombieAttack attackScript;
    public bool isPlayerInLos;
    [Space(10)]
    public Vector3 lastSpottedPlayerPosition;
    public float huntingTimer = 10.0f;
    public float huntRadius = 10.0f;


    void Start()
    {
        attackScript = GetComponent<InterfaceZombieAttack>();

        spitZombieState = stateSZ.sensedPlayerNearby;
        newRoamingPosition = Vector3.zero;
    }

    void Update()
    {
        switch(spitZombieState)
        {

            case stateSZ.sensedPlayerNearby:

                enemyZombAgent.SetDestination(playerTransform.position);
                
                if(fovScript.playerInFovSpotted==true)
                {
                    spitZombieState = stateSZ.attacking;
                    enemyZombAgent.isStopped = true;
                }

                break;
            case stateSZ.attacking:
                if (fovScript.playerInFovSpotted == true)
                {
                    attackScript.IzombieAttack();
                }
                else
                {
                    enemyZombAgent.isStopped = false;       // this is dangerous as we don't know where the NavMeshAgent destination is set to

                    if (checkPlayerInLineOfSight()==true){
                        spitZombieState = stateSZ.chasePlayerUntilInLos;
                    }else
                    {
                        spitZombieState = stateSZ.huntingPlayer;
                    }
                }
                break;

            case stateSZ.chasePlayerUntilInLos:

                if (checkPlayerInLineOfSight() == true)
                {
                    enemyZombAgent.SetDestination(playerTransform.position);

                    if (fovScript.playerInFovSpotted == true)
                    {
                        spitZombieState = stateSZ.attacking;
                        enemyZombAgent.isStopped = true;
                    }
                }
                else
                {
                    lastSpottedPlayerPosition = playerTransform.position;
                    spitZombieState = stateSZ.huntingPlayer;
                    enemyZombAgent.SetDestination(lastSpottedPlayerPosition);
                }

                break;

            case stateSZ.huntingPlayer:     // this state is kinda a bit whacky : 1. Go to last spotted player Position 2. Patrol in 4 directions for sometime 3. Back to roaming state


                if (enemyTransform.position == lastSpottedPlayerPosition)
                {
                    if (huntingTimer >= 0)
                    {
                        huntingTimer -= Time.deltaTime;
                        Vector3 randomDirection = Random.insideUnitSphere * huntRadius;
                        randomDirection = new Vector3(randomDirection.x, 0f, randomDirection.z);
                        randomDirection += transform.position;
                        NavMeshHit hit;
                        NavMesh.SamplePosition(randomDirection, out hit, huntRadius, 1);
                        lastSpottedPlayerPosition = hit.position;
                    }
                    else
                    {
                        huntingTimer = 10.0f;
                        spitZombieState = stateSZ.roaming;
                    }
                }
                else
                {
                    enemyZombAgent.SetDestination(lastSpottedPlayerPosition);
                }

                    if (checkPlayerInLineOfSight() == true)
                    {
                        huntingTimer = 10.0f;
                        spitZombieState = stateSZ.chasePlayerUntilInLos;
                    }                
               

                break;

            case stateSZ.roaming:

                if(waitTime>=3)
                {
                    if(newRoamingPosition== Vector3.zero)
                    {
                        newRoamingPosition = getRandomRoamingPosition();
                    }
                    else if (enemyTransform.position!=newRoamingPosition)
                    {
                        enemyZombAgent.velocity = newRoamingPosition - enemyTransform.position;
                        enemyTransform.forward = enemyZombAgent.velocity.normalized;
                    }
                    else if(enemyTransform.position == newRoamingPosition)
                    {
                        newRoamingPosition = Vector3.zero;
                        waitTime = 0;
                    }
                }else
                {
                    waitTime += Time.deltaTime;
                }

                break;

        }
    }


    bool checkPlayerInLineOfSight()
    {
        RaycastHit hit;

        float lineOfSightDistance = 100.0f;
        bool didHitAnyObstacleInBetween = Physics.Raycast(playerTransform.position, enemyTransform.position-playerTransform.position, out hit, lineOfSightDistance, fovScript.obstacleMask);
        if(didHitAnyObstacleInBetween==true)
        Debug.Log(hit.collider.name);
        Debug.DrawRay(enemyTransform.position, -1*(enemyTransform.position - playerTransform.position).normalized * lineOfSightDistance,Color.green);
        isPlayerInLos = !didHitAnyObstacleInBetween;
        //Debug.Log(isPlayerInLos);
        return isPlayerInLos;


    }

    Vector3 getRandomRoamingPosition()
    {
        Vector3 randomDir=  Random.insideUnitSphere * roamRadius;
        randomDir += enemyTransform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomDir, out hit, roamRadius, 1);
        return hit.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="player")
        {
            isPlayerSensedNearby = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.tag == "player")
        {
            isPlayerSensedNearby = false;
        }
    }


}

