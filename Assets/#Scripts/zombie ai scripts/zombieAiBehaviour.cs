using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class zombieAiBehaviour : MonoBehaviour
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
    public FieldOfView fovScript;
    public bool isPlayerSensedNearby = false;
    public LayerMask obstacles;
    public float lineOfSightDistance;
    public float lineOfSightAngle;
    public bool isPlayerInLos;
    [Space(10)]
    public InterfaceZombieAttack attackScript;          // For getting the interface Implemented script you have to get that component realtime.
    public bool isPlayerInsideAttackRange = false;
    [Space(20)]
    public Transform[] patrolPoints;
    public Transform nextPatrolPoint;
    public NavMeshAgent zombieNavmeshAgent;    
    public float targetSampleNavmeshRadiusAllowed;
    public bool hasReachedPatrolPoint;
    public Vector3 newPatrolPosition;
    [Space(10)]
    public Transform playerTransform;
    public Transform enemyTransform;
    

    // Start is called before the first frame update
    void Start()
    {

        attackScript = GetComponent<InterfaceZombieAttack>();

        spitZombieState = stateSZ.sensedPlayerNearby;
        

        hasReachedPatrolPoint = false;
        if (nextPatrolPoint == null)
        {
            nextPatrolPoint = patrolPoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spitZombieState)
        {

            case stateSZ.sensedPlayerNearby:
                spitZombieState = stateSZ.chasePlayerUntilInLos;
                break;

            case stateSZ.attacking:

                if(isPlayerInsideAttackRange==true)
                {
                    attackScript.IzombieAttack();
                    zombieNavmeshAgent.isStopped = true;
                }
                else
                {
                    spitZombieState = stateSZ.chasePlayerUntilInLos;
                    zombieNavmeshAgent.isStopped = false;
                }
                break;

            case stateSZ.chasePlayerUntilInLos:
                if (isPlayerWithinLos() == true || isPlayerSensedNearby == true)        // los should work only if the angle is in the front of zommbie or If very close
                {
                    zombieNavmeshAgent.SetDestination(playerTransform.position);
                }
                else
                {
                    // go to Roaming player state, after going to the last spotted position
                    if (zombieNavmeshAgent.remainingDistance <= zombieNavmeshAgent.stoppingDistance)
                    {
                        if (!zombieNavmeshAgent.hasPath || zombieNavmeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            spitZombieState = stateSZ.roaming;
                        }
                    }
                }
                break;

            case stateSZ.huntingPlayer:     // this state is kinda a bit whacky : 1. Go to last spotted player Position 2. Patrol in 4 directions for sometime 3. Back to roaming state
                break;

            case stateSZ.roaming:
                if (hasReachedPatrolPoint == true)
                {
                    int rand = Random.Range(0, patrolPoints.Length - 1);
                    nextPatrolPoint = patrolPoints[rand];

                    NavMeshHit hit;
                    NavMesh.SamplePosition(nextPatrolPoint.position, out hit, targetSampleNavmeshRadiusAllowed, 1);
                    nextPatrolPoint.position = hit.position;
                    hasReachedPatrolPoint = false;
                }
                else
                {
                    zombieNavmeshAgent.SetDestination(nextPatrolPoint.position);
                    if (!zombieNavmeshAgent.pathPending)
                    {
                        if (zombieNavmeshAgent.remainingDistance <= zombieNavmeshAgent.stoppingDistance)
                        {
                            if (!zombieNavmeshAgent.hasPath || zombieNavmeshAgent.velocity.sqrMagnitude == 0f)
                            {
                                hasReachedPatrolPoint = true;
                            }
                        }
                    }
                }

                if (isPlayerWithinLos() == true)
                {
                    spitZombieState = stateSZ.chasePlayerUntilInLos;
                }
                break;
        }
    }

    public bool isPlayerWithinLos()
    {

        RaycastHit hit;

        //Debug.Log(Vector3.Angle(enemyTransform.forward, playerTransform.position - enemyTransform.position));

        //lineOfSightDistance = Vector3.Magnitude( playerTransform.position-enemyTransform.position);
        if (lineOfSightDistance >= Vector3.Magnitude(playerTransform.position - enemyTransform.position) && Vector3.Angle(enemyTransform.forward, playerTransform.position- enemyTransform.position) <= lineOfSightAngle)
        {
            bool didHitAnyObstacleInBetween = Physics.Raycast(enemyTransform.position, playerTransform.position - enemyTransform.position, out hit, lineOfSightDistance, obstacles);
            if (didHitAnyObstacleInBetween == true)
            {
                Debug.DrawRay(enemyTransform.position, -1 * (enemyTransform.position - playerTransform.position).normalized * lineOfSightDistance, Color.red);
                //Debug.Log(hit.collider.name);
            }
            else
                Debug.DrawRay(enemyTransform.position, -1 * (enemyTransform.position - playerTransform.position).normalized * lineOfSightDistance, Color.green);
            isPlayerInLos = !didHitAnyObstacleInBetween;
            return isPlayerInLos;
        }
        else
        {
            isPlayerInLos = false;
            //Debug.Log(isPlayerInLos);
            return false;
        }
    }
}
