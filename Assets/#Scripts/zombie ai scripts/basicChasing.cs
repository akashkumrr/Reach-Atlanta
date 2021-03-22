using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class basicChasing : MonoBehaviour
{

    public Transform playerTransform;
    public Transform enemyTransform;
    public LayerMask obstacles;
    public float lineOfSightDistance;
    public bool isPlayerInLos;

    public NavMeshAgent zombNavemeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         if in line of sight then follow player
         else move to the last known position of the player and then start roaming randomly.
         */
        if(isPlayerWithinLos()==true)
        {
            zombNavemeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            // go to Roaming player state
        }
        //Debug.Log(isPlayerInLos);

    }

    public bool isPlayerWithinLos()
    {

        RaycastHit hit;

        //lineOfSightDistance = Vector3.Magnitude( playerTransform.position-enemyTransform.position);
        if (lineOfSightDistance >= Vector3.Magnitude(playerTransform.position - enemyTransform.position))
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
            Debug.Log(isPlayerInLos);
            return false;
        }
    }
}
