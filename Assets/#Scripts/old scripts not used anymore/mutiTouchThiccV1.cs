using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutiTouchThiccV1 : MonoBehaviour
{
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

       // Debug.Log(Input.touchCount);
    }

    void OnTouchBegan(int fingerId, Vector2 pos)
    {  // IN OUR CASE LEFT FINGER CONTROLS FIRING
        theTouchAreaScript.whetherScreenTouched = true;
        //theMoveShotScript.touchToLookAndFirePosition = pos; // this will look regardless of whther touch inside look area or not
        theTouchAreaScript.touchXcoordinate = pos.x;
        theTouchAreaScript.touchYcoordinate = pos.y;
        //Debug.Log( theTouchAreaScript.checkWhetherInsideOrOutsideTouchArea());
       // Debug.Log(pos);
      //  Debug.Log( firetouchArea.insideTouchArea);
        if (firetouchArea.insideTouchArea == true)
        {
            leftFingerId = fingerId;
            //Debug.Log(" YOU touched the shooting area so Look and shoot");
            theWeaponMan.leftFingerDown = true;
            theMoveShotScript.touchToLookAndFirePosition = pos;            
        }else
        {
            //Debug.Log("Began didnt touch area");
        }
        //Debug.Log(firetouchArea.insideTouchArea);

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
       

    }



    void OnTouchMoved(int fingerId, Vector2 pos)
    {
        // theMoveShotScript.touchToLookAndFirePosition = pos; // this will look regardless of whther touch inside look area or not

        theTouchAreaScript.whetherScreenTouched = true;
        //theMoveShotScript.touchToLookAndFirePosition = pos; // this will look regardless of whther touch inside look area or not
        
        // if it is the same finger that was inside touch area, then move the player. 
        // we will know that it is the same finger by checking it's touchId 
        if (leftFingerId == fingerId)
        {
            theTouchAreaScript.touchXcoordinate = pos.x;
            theTouchAreaScript.touchYcoordinate = pos.y;

            if (firetouchArea.insideTouchArea == true)
            {
                //Debug.Log("MOVING inside touch area");
                leftFingerId = fingerId;
                // YOU touched the shooting area so Look and shoot
                theWeaponMan.leftFingerDown = true;
                theMoveShotScript.touchToLookAndFirePosition = pos;
                theTouchAreaScript.touchXcoordinate = pos.x;
                theTouchAreaScript.touchYcoordinate = pos.y;
            }
        }
       
           // Debug.Log("Moving outside area");
        


    }


}
