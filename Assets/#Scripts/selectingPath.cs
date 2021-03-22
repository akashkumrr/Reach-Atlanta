using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectingPath : MonoBehaviour
{
    

    [System.Serializable]
    public class WaypointClass
    {
        public Transform mainWpTransform;
        public GameObject theDownWaypoint;
        public GameObject theUpWaypoint;
        public GameObject theRightWaypoint;
        public GameObject theLeftWaypoint;

    }

  //  public WaypointClass startingWaypoint;
  //  public WaypointClass initialNextWaypoint;

  //  [Space(10)]
  //  public WaypointClass lastVisitedWaypoint;
 //   public WaypointClass currentWaypoint;       // current waypoint means that you are currently heading towards that waypoint

    /*      Dont feel the need of this right now
    [Space(10)]
    public WaypointClass nextWaypoint;          //*** very IMP look. current wayPoint's next waypoint. so One waypoint we have to choose from many available waypoints
                                            // as there are many directions to take from a road intersection

    */
   // [Space(10)]
   // public Transform playerParentTransform;
 //   public float moveSpeed;

    //[Space(10)]
    //public WaypointClass randomDefaultWaypoint;       // The one when there is no next waypoint to choose from OR when you automatically selects any random next waypoint for gamer.

    public WaypointClass thisWaypoint;

    void Start()
    {
       // getup();
        //playerParentTransform.position = startingWaypoint.mainWpTransform.position;
        //currentWaypoint = initialNextWaypoint;
    }

 
    void Update()
    {

    }


    /*
    public void move()
    {

        playerParentTransform.position = Vector3.MoveTowards(playerParentTransform.position, currentWaypoint.mainWpTransform.position, moveSpeed * Time.deltaTime);

        // if swipe left then



        if (playerParentTransform.position == currentWaypoint.mainWpTransform.position)
        {
            lastVisitedWaypoint = currentWaypoint;



            // if( no Player Input is given about direction) then set the next waypoint to take any one direction to waypoint i.e randomDefaultWaypoint waypoint;
            if (currentWaypoint.theUpWaypoint)
            {
                // UP direction is selected RANDOMLY. so change all the variables in RndomWayPoint according to UP direction. 

                randomDefaultWaypoint = thisWaypoint.theUpWaypoint.GetComponent<selectingPath>().thisWaypoint;
                // Get the up object and find wayPoint class associated with that up object

                
                randomDefaultWaypoint.mainWpTransform = currentWaypoint.theUpWaypoint.mainDirTransform;
                randomDefaultWaypoint.theUpWaypoint.dirUp = currentWaypoint.theUpWaypoint.dirUp;

                randomDefaultWaypoint.theUpWaypoint.dirDown???
                randomDefaultWaypoint.theUpWaypoint.dirRight??
                randomDefaultWaypoint.theUpWaypoint.dirLeft???

                randomDefaultWaypoint.theDownWaypoint.dirDown = currentWaypoint.theUpWaypoint.dirDown;
                randomDefaultWaypoint.theRightWaypoint.dirRight = currentWaypoint.theUpWaypoint.dirRight;
                randomDefaultWaypoint.theLeftWaypoint.dirLeft= currentWaypoint.theUpWaypoint.dirRight;
                

            }
            else if (currentWaypoint.theDownWaypoint)
            {
                //randomDefaultWaypoint = currentWaypoint.theDownWaypoint.mainDirTransform;
                randomDefaultWaypoint = thisWaypoint.theDownWaypoint.GetComponent<selectingPath>().thisWaypoint;

            }
            else if (currentWaypoint.theRightWaypoint)
            {
                randomDefaultWaypoint = thisWaypoint.theDownWaypoint.GetComponent<selectingPath>().thisWaypoint;
            }
            else if (currentWaypoint.theLeftWaypoint)
            {
                randomDefaultWaypoint = thisWaypoint.theLeftWaypoint.GetComponent<selectingPath>().thisWaypoint;
            }

            currentWaypoint = randomDefaultWaypoint;

            //if( theres no swipe) then currentWaypoint = randomDefaultWaypoint;
            // else set the current waypoint in the swipe direction also you have to check whether that if a Waypoint EVEN EXISTS IN THAT DIRECTION OF CURRENT WAYPOINT
            // currentWaypoint = 

            //currentWaypoint = nextWaypoint;
            // find the next waypoint by getting swipe direction from player
            // if there is no swipe input from the player find the default waypoint and choose that default waypoint as the nextWaypoint;
        }
    }

    */

    public void getup()
    {
//randomDefaultWaypoint = thisWaypoint.theUpWaypoint.GetComponent<selectingPath>().thisWaypoint;
    }

}
