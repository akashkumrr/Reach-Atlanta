using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mySwipeScript : MonoBehaviour
{
    public Vector3 worldPositionOfMouseStart;
    public Vector3 worldPositionOfMouseEnd;

    public float deltaX;
    public float deltaY;

    public plaerFollowingPath pathScript;

    Rect touchable_area;   
    public bool insideTouchArea;

    public enum swipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public swipeDirection sDir;


    void Start()
    {

        #region Touch Handler (Touch will not work in these rectangles)
        touchable_area = new Rect(0, 0, Screen.width, Screen.height * 0.75f);
        #endregion
    }

    void OnGUI()
    {
        GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        GUI.Box(touchable_area, "");
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) && touchable_area.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))))
        {
            //Debug.Log("INSIDE TOUCH AREA");
            insideTouchArea = true;

        }
        if ((Input.GetMouseButtonDown(0) && !touchable_area.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))))
        {
            //Debug.Log("NO! outside touch area");
            insideTouchArea = false;

        }

        if (insideTouchArea == true)
        {
            if (Input.GetMouseButtonDown(0))
                worldPositionOfMouseStart = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

            if (Input.GetMouseButtonUp(0))
            {
                worldPositionOfMouseEnd = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

                deltaX = worldPositionOfMouseEnd.x - worldPositionOfMouseStart.x;
                deltaY = worldPositionOfMouseEnd.y - worldPositionOfMouseStart.y;

                if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
                {
                    // vertical swipe. now figure out up or down swipe

                    if (deltaY > 0)
                    {
                       
                        sDir = swipeDirection.Up;
                        /*
                            if going DOWN and swipped UP then change waypoint to UP (if up exists)
                            else if there was an input other UP, then SET then no further direction input (swipe) can be taken until it reaches it's curr wayPoint.

                            set currDir to UP.
                         */
                        if (pathScript.currDir==plaerFollowingPath.currDirection.Down)//  && !pathScript.take90degreeTurn)
                        {
                            if (pathScript.currentWaypoint.theUpWaypoint)
                            {
                                pathScript.currentWaypoint = pathScript.currentWaypoint.theUpWaypoint.GetComponent<selectingPath>().thisWaypoint;
                                pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                                pathScript.currDir = plaerFollowingPath.currDirection.Up;
                            }
                            
                        }
                        
                    }
                    else
                    {
                       
                        sDir = swipeDirection.Down;

                        if (pathScript.currDir == plaerFollowingPath.currDirection.Up)// && !pathScript.take90degreeTurn)
                        {
                            if (pathScript.currentWaypoint.theDownWaypoint)
                            {
                                pathScript.currentWaypoint = pathScript.currentWaypoint.theDownWaypoint.GetComponent<selectingPath>().thisWaypoint;
                                pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Down;
                            }
                        }

                    }
                }
                else
                {
                    if (deltaX > 0)
                    {
                        sDir = swipeDirection.Right;

                        if (pathScript.currDir == plaerFollowingPath.currDirection.Left)// && !pathScript.take90degreeTurn)
                        {
                            if (pathScript.currentWaypoint.theRightWaypoint)
                            {
                                pathScript.currentWaypoint = pathScript.currentWaypoint.theRightWaypoint.GetComponent<selectingPath>().thisWaypoint;
                                pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Right;
                            }
                        }

                    }
                    else
                    {
                        sDir = swipeDirection.Left;

                        if (pathScript.currDir == plaerFollowingPath.currDirection.Right)// && !pathScript.take90degreeTurn)
                        {
                            if (pathScript.currentWaypoint.theLeftWaypoint)
                            {
                                pathScript.currentWaypoint = pathScript.currentWaypoint.theLeftWaypoint.GetComponent<selectingPath>().thisWaypoint;
                                pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Left;
                            }
                        }

                    }
                }

            }
        }
    }


    // to be used by other script to emulate turning 180 on same path direction

    public void swipped(swipeDirection sDir)
    {
        switch(sDir)
        {
            case swipeDirection.Up:
                if (pathScript.currDir == plaerFollowingPath.currDirection.Down)//  && !pathScript.take90degreeTurn)
                {
                    if (pathScript.currentWaypoint.theUpWaypoint)
                    {
                        pathScript.currentWaypoint = pathScript.currentWaypoint.theUpWaypoint.GetComponent<selectingPath>().thisWaypoint;
                        pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Up;
                    }

                }
                break;

            case swipeDirection.Down:
                if (pathScript.currDir == plaerFollowingPath.currDirection.Up)// && !pathScript.take90degreeTurn)
                {
                    if (pathScript.currentWaypoint.theDownWaypoint)
                    {
                        pathScript.currentWaypoint = pathScript.currentWaypoint.theDownWaypoint.GetComponent<selectingPath>().thisWaypoint;
                        pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Down;
                    }
                }
                break;

            case swipeDirection.Right:
                if (pathScript.currDir == plaerFollowingPath.currDirection.Left)// && !pathScript.take90degreeTurn)
                {
                    if (pathScript.currentWaypoint.theRightWaypoint)
                    {
                        pathScript.currentWaypoint = pathScript.currentWaypoint.theRightWaypoint.GetComponent<selectingPath>().thisWaypoint;
                        pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Right;
                    }
                }
                break;
            case swipeDirection.Left:
                if (pathScript.currDir == plaerFollowingPath.currDirection.Right)// && !pathScript.take90degreeTurn)
                {
                    if (pathScript.currentWaypoint.theLeftWaypoint)
                    {
                        pathScript.currentWaypoint = pathScript.currentWaypoint.theLeftWaypoint.GetComponent<selectingPath>().thisWaypoint;
                        pathScript.playerParentTransform.right = -1 * (pathScript.currentWaypoint.mainWpTransform.position - pathScript.playerParentTransform.position);
                        pathScript.currDir = plaerFollowingPath.currDirection.Left;
                    }
                }
                break;

        }
    }
}
