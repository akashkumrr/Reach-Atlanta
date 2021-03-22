using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class patrolWaypoints : MonoBehaviour
{

    public Transform[] patrolPoints;
    public Transform nextPatrolPoint;
    public NavMeshAgent zombieNavmeshAgent;
    [Space(10)]
    public float targetSampleNavmeshRadiusAllowed;
    public bool hasReachedPatrolPoint;
    public Vector3 newPatrolPosition;
    // Start is called before the first frame update
    void Start()
    {
        hasReachedPatrolPoint = false;
        if (nextPatrolPoint==null)
        {
            nextPatrolPoint = patrolPoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

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
    }
}
