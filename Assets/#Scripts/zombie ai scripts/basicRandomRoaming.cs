using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class basicRandomRoaming : MonoBehaviour
{
    public NavMeshAgent zombieNavmeshAgent;
    public bool hasReachedRoamingTarget;
    public Vector3 newRoamingPosition;
    public Vector3 direction;
    public float targetSampleNavmeshRadiusAllowed=5.0f;  //  this will tell that if the position on navmesh isn't available then search for other positions on navmesh within that radius
    

    void Start()
    {
        hasReachedRoamingTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasReachedRoamingTarget == true)
        {

            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0: direction = Quaternion.AngleAxis(22.17f, Vector3.up) * Vector3.forward;
                    break;
                case 1: direction = Quaternion.AngleAxis(22.17f, Vector3.up) * Vector3.back;
                    break;
                case 2: direction = Quaternion.AngleAxis(22.17f, Vector3.up) * Vector3.right;
                    break;
                case 3: direction = Quaternion.AngleAxis(22.17f, Vector3.up) * Vector3.left;
                    break;             
            }

            direction = direction * 5;
            newRoamingPosition = newRoamingPosition+ direction;
            NavMeshHit hit;
            NavMesh.SamplePosition(newRoamingPosition, out hit, targetSampleNavmeshRadiusAllowed, 1);
            newRoamingPosition = hit.position;
            hasReachedRoamingTarget = false;
        }
        else
        {
            zombieNavmeshAgent.SetDestination(newRoamingPosition);
            if (!zombieNavmeshAgent.pathPending)
            {
                if (zombieNavmeshAgent.remainingDistance <= zombieNavmeshAgent.stoppingDistance)
                {
                    if (!zombieNavmeshAgent.hasPath || zombieNavmeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        hasReachedRoamingTarget = true;
                    }
                }
            }
        }
        //Debug.Log(zombieNavmeshAgent.remainingDistance);

        
    }
}
