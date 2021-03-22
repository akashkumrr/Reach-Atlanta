using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPathPlayer : MonoBehaviour
{   // Array of waypoints to walk from one to the next one
     [SerializeField]
    private Transform[] waypoints;
    public float rotateTowardsWaypointSpeed;
    public Vector3 nextWaypointDirection;       //This is used to calculate the Angle with spine in playerAnimSelectorScript;
    public playerAnimationSelector thePASelector;
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // Use this for initialization
    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;   
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position,waypoints[waypointIndex].transform.position,moveSpeed * Time.deltaTime);
            nextWaypointDirection = waypoints[waypointIndex].position - transform.position;

            // This rotates the parent towards the waypoint
            if (thePASelector.aimingAngleWithMovingDirection > 120 && thePASelector.aimingAngleWithMovingDirection <= 180 || thePASelector.aimingAngleWithMovingDirection >= -180 && thePASelector.aimingAngleWithMovingDirection < -120)
            {   // rotate parent by 180 degree from the path so to look backwards
                //transform.right = Vector3.Lerp(transform.right, transform.position- waypoints[waypointIndex].position , Time.deltaTime * rotateTowardsWaypointSpeed);

                transform.right = -1 * (waypoints[waypointIndex].position - transform.position); // LERPING this CAUSEed JITTERNESS
            }
            else
            {
                transform.right = waypoints[waypointIndex].position - transform.position;
                //transform.right = Vector3.Lerp(transform.right, waypoints[waypointIndex].position - transform.position, Time.deltaTime * rotateTowardsWaypointSpeed);
            }
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}
