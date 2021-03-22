using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class plaerFollowingPath : MonoBehaviour
{
    public selectingPath.WaypointClass randomDefaultWaypoint;
    public selectingPath.WaypointClass currentWaypoint;
    public selectingPath.WaypointClass initialNextWaypoint;
    public selectingPath.WaypointClass initialStartingWaypoint;
    public Transform playerParentTransform;
    [Space(10)]
    public Vector3 wayPointHeadingDirection; // It's use is to keep running in the same direction if no swipe input is given AND maybe later u can use it as Reference
                                             // for calculating the angle of spine with move dir.. change it when you swipe or when you reach a waypoint. So as to get 

    public playerAnimationSelector thePASelector;
    public float rotateTowardsWaypointSpeed;
    /* 
     To implement running in the asme direction. see what was the last of waypoint you took. if last dir was right then keep on selecting right Of wayPoint and in that
     way you'll keeping going in the same direction if no input is provided. If there's no right then select any (randomDir) of the available direction from that waypoint.
         */
    public float moveSpeed = 2f;

    public enum currDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public currDirection currDir;

    [Space(10)]
    public mySwipeScript theSwipeScript;
    public bool take90degreeTurn;


    void Start()
    {
        playerParentTransform.position = initialStartingWaypoint.mainWpTransform.position;
        currentWaypoint = initialNextWaypoint;
        currDir = currDirection.Right;

    }

    void Update()
    {
        playerParentTransform.position = Vector3.MoveTowards(playerParentTransform.position, currentWaypoint.mainWpTransform.position, moveSpeed * Time.deltaTime);

        wayPointHeadingDirection = currentWaypoint.mainWpTransform.position - playerParentTransform.position;

        // NOW FOR SWIPE INPUT, JUST CHANGE THE LAST DIRECTION to the swipe direction. Also don't change last dir if waypoint doesn't exists in that swipe direction


        
        if (playerParentTransform.position == currentWaypoint.mainWpTransform.position)
        {
            take90degreeTurn = false;

            currDir = (currDirection) theSwipeScript.sDir;
            switch (currDir)
            {
                case currDirection.Up:
                                        if (currentWaypoint.theUpWaypoint)
                                        {
                                            randomDefaultWaypoint = currentWaypoint.theUpWaypoint.GetComponent<selectingPath>().thisWaypoint;
                                        }
                                        else
                                            choosingRandomDirection();
                                                            break;

                case currDirection.Down:
                    if (currentWaypoint.theDownWaypoint != null)
                    {
                        randomDefaultWaypoint = currentWaypoint.theDownWaypoint.GetComponent<selectingPath>().thisWaypoint;

                    }
                    else
                        choosingRandomDirection();
                                        break;

                case currDirection.Left:
                    if (currentWaypoint.theLeftWaypoint != null)
                    {
                        randomDefaultWaypoint = currentWaypoint.theLeftWaypoint.GetComponent<selectingPath>().thisWaypoint;
                    }
                    else
                        choosingRandomDirection();
                                        break;

                case currDirection.Right:
                    if (currentWaypoint.theRightWaypoint != null)
                    {
                        randomDefaultWaypoint = currentWaypoint.theRightWaypoint.GetComponent<selectingPath>().thisWaypoint;
                    }
                    else
                        choosingRandomDirection();
                                        break;

            }

            currentWaypoint = randomDefaultWaypoint;


        }

        if (thePASelector.aimingAngleWithMovingDirection > 120 && thePASelector.aimingAngleWithMovingDirection <= 180 || thePASelector.aimingAngleWithMovingDirection >= -180 && thePASelector.aimingAngleWithMovingDirection < -120)
        {   // rotate parent by 180 degree from the path so to look backwards
            //transform.right = Vector3.Lerp(transform.right, transform.position- currentWaypoint.mainWpTransform.position, Time.deltaTime * rotateTowardsWaypointSpeed);

            transform.right = -1 * (currentWaypoint.mainWpTransform.position - transform.position); // LERPING this CAUSEed JITTERNESS
        }
        else
        {
            transform.right = currentWaypoint.mainWpTransform.position - transform.position;
            //transform.right = Vector3.Lerp(transform.right, currentWaypoint.mainWpTransform.position - transform.position, Time.deltaTime * rotateTowardsWaypointSpeed);
        }

        //Selection.activeGameObject = currentWaypoint.mainWpTransform.gameObject;

    }

    public void choosingRandomDirection()
    {
        // if( no Player Input is given about direction) then set the next waypoint to take any one direction to waypoint i.e randomDefaultWaypoint waypoint;
        if (currentWaypoint.theUpWaypoint != null)
        {
            // UP direction is selected RANDOMLY. so change all the variables in RndomWayPoint according to UP direction. 
            currDir = currDirection.Up;
            randomDefaultWaypoint = currentWaypoint.theUpWaypoint.GetComponent<selectingPath>().thisWaypoint;

        }
        else if (currentWaypoint.theRightWaypoint != null)
        {
            currDir = currDirection.Right;
            randomDefaultWaypoint = currentWaypoint.theRightWaypoint.GetComponent<selectingPath>().thisWaypoint;
        }
        else if (currentWaypoint.theDownWaypoint != null)
        {
            //randomDefaultWaypoint = currentWaypoint.theDownWaypoint.mainDirTransform;
            currDir = currDirection.Down;
            randomDefaultWaypoint = currentWaypoint.theDownWaypoint.GetComponent<selectingPath>().thisWaypoint;

        }
        else if (currentWaypoint.theLeftWaypoint != null)
        {
            currDir = currDirection.Left;
            randomDefaultWaypoint = currentWaypoint.theLeftWaypoint.GetComponent<selectingPath>().thisWaypoint;
        }
    }
}
