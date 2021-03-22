using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiTouchMoveAndShoot : MonoBehaviour
{
    // Move by tapping on the ground in the scenery or with the joystick in the lower left corner.
    // Look around by dragging the screen.

    int leftFingerId = -1;
    int rightFingerId = -1;
    Vector2 leftFingerStartPoint;
    Vector2 leftFingerCurrentPoint;
    Vector2 rightFingerStartPoint;
    Vector2 rightFingerCurrentPoint;
    Vector2 rightFingerLastPoint;

    [Space(10)]
    public touchingAreasTesting firetouchArea;
    public shootAndMove theMoveShotScript;
    public weaponManager theWeaponMan;
    public touchingAreasTesting theTouchAreaScript;
    void Start()
    {
    }
    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))  // YOU CHANGED IT TO GET MOUSEDOWN (simulating HOLDing LMB AND IT GOT MESSED UP!
                OnTouchBegan(0, Input.mousePosition);
            else if (Input.GetMouseButtonUp(0))
                OnTouchEnded(0);
            else if (leftFingerId != -1 || rightFingerId != -1)
                OnTouchMoved(0, Input.mousePosition);
        }
        else
        {
            int count = Input.touchCount;

            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                    OnTouchBegan(touch.fingerId, touch.position);
                else if (touch.phase == TouchPhase.Moved)
                {
                   // Debug.Log("some finger movement detected");
                    OnTouchMoved(touch.fingerId, touch.position);
                }
                else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                    OnTouchEnded(touch.fingerId);
            }
        }

        /*
        if (leftFingerId != -1) // left dragging
            MoveFromJoystick();
        else if (isMovingToTarget) //
            MoveToTarget();

        if (rightFingerId != -1 && isRotating)
            Rotate();
        */
    }

    void OnTouchBegan(int fingerId, Vector2 pos)
    {  // IN OUR CASE LEFT FINGER CONTROLS FIRING
        theTouchAreaScript.whetherScreenTouched = true;
        //theMoveShotScript.touchToLookAndFirePosition = pos; // this will look regardless of whther touch inside look area or not

        if (leftFingerId == -1)// && firetouchArea.insideTouchArea==true)
        {
            
                //Debug.Log("left started with F ID " + fingerId);
                leftFingerId = fingerId;
                leftFingerStartPoint = leftFingerCurrentPoint = pos;
                // then look in that direction and   start firing
                // pass this finger position to Ray ray = Camera.main.ScreenPointToRay(*** H E R E ***);
                // so that our player looks in that dierction.
                // but shooting is done in weapon manager sccript. So set a variable true here and pass it Weapon script to start shooting.

                theTouchAreaScript.touchXcoordinate = leftFingerCurrentPoint.x;
                theTouchAreaScript.touchYcoordinate = leftFingerCurrentPoint.y;
            if (firetouchArea.insideTouchArea == true)
            {
                theMoveShotScript.touchToLookAndFirePosition = leftFingerCurrentPoint;

                theWeaponMan.leftFingerDown = true;
                theWeaponMan.leftFingerUp = false;  // because when you're pressing button you are not releasing IT. Got it? It is simple logic
            
            }
        }
        else if (rightFingerId == -1) // right gets started even when we first click inside the look area. W T F !??
        {
            //Debug.Log("right started with F ID " + fingerId);

            rightFingerStartPoint = rightFingerCurrentPoint = rightFingerLastPoint = pos;
            rightFingerId = fingerId;

            // why are you calculating inside area of right finger?? Firing weapon will depend on whether left finger is in inside area
            // calculating inside touch when clearly we are out side will stop the weapon from shooting when we are using two fingers
            //theTouchAreaScript.touchXcoordinate = rightFingerCurrentPoint.x;
            //theTouchAreaScript.touchYcoordinate = rightFingerCurrentPoint.y;

            // set a B O O L variable i.e. is using joytick here and 
        }
    }



    void OnTouchEnded(int fingerId)
    {
        theTouchAreaScript.whetherScreenTouched = false;

        if (fingerId == leftFingerId)
        {
            leftFingerId = -1;
            theTouchAreaScript.insideTouchArea = false;     // SET inside touch area to false as it remains true, until you touch outside
            theWeaponMan.leftFingerDown = false;
            theWeaponMan.leftFingerUp = true;
        }
        else if (fingerId == rightFingerId)
        {
            rightFingerId = -1;
        }
    }



    void OnTouchMoved(int fingerId, Vector2 pos)
    {
       // theMoveShotScript.touchToLookAndFirePosition = pos; // this will look regardless of whther touch inside look area or not

        theTouchAreaScript.whetherScreenTouched = true;
        if (fingerId == leftFingerId && firetouchArea.insideTouchArea == true)
        {
            //Debug.Log("left moved");
            theWeaponMan.leftFingerDown = true;
            leftFingerCurrentPoint = pos;

            theMoveShotScript.touchToLookAndFirePosition = leftFingerCurrentPoint;
            theTouchAreaScript.touchXcoordinate = leftFingerCurrentPoint.x;
            theTouchAreaScript.touchYcoordinate = leftFingerCurrentPoint.y;

        }
        else if (fingerId == rightFingerId)
        {
            rightFingerCurrentPoint = pos;
            
           // theTouchAreaScript.touchXcoordinate = rightFingerCurrentPoint.x;
           // theTouchAreaScript.touchYcoordinate = rightFingerCurrentPoint.y;
        }
    }
}
