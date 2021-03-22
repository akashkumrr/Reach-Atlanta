using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Removed lerping as it was looking weird

public class movingPlayerWithJoystick : MonoBehaviour
{
    public Joystick moveJoystick;
    public float moveOffsetAngle;
    public Transform parentOfPlayer;
    public Rigidbody parentRigibody;    // FOR NOW, moving every player with parent rigidbody.
    public float runningSpeedStore;
    public float runningSpeed;
    public playerAnimationSelector thePAselector;
    public joyStickIsShootDirection theShootJoystickScript;
    public Vector3 dir;
    Quaternion moveRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()  // FIXED UPDATE IS VERY IMPORTANT whenever We are manipulating a rigidbody in script. It Solved Jittery movement with animation of player
    {
        if (Input.GetKey(KeyCode.A))
        {
            //moveRot = Quaternion.Euler(0, 180, 0);
        }


            if (Mathf.Abs(moveJoystick.Horizontal) > 0.1f || Mathf.Abs(moveJoystick.Vertical) > 0.1f)
        {
            thePAselector.isMoving = true;

            Quaternion moveRot = Quaternion.Euler(new Vector3(0, -Mathf.Atan2(moveJoystick.Vertical, moveJoystick.Horizontal) * Mathf.Rad2Deg + moveOffsetAngle, 0));
            
            
            //parentPlayerTransform.rotation = parentPlayerTransform.rotation * desiredRot;
            dir = moveRot * Vector3.forward;
            //if(thePAselector.rotate180OnceOnly==true)
                //parentOfPlayer.right = dir;


            parentRigibody.velocity = dir* runningSpeed;
            if(theShootJoystickScript.shootJoystickDragged==false)
            {
                //theShootJoystickScript.parentPlayerTransform.right = Vector3.Lerp(theShootJoystickScript.parentPlayerTransform.right, dir, theShootJoystickScript.roatateSpeed * Time.deltaTime);
                theShootJoystickScript.parentPlayerTransform.right = dir;
                //theShootJoystickScript.parentPlayerTransform.right = dir;
            }


        }
        else
        {
            thePAselector.isMoving = false;
            parentRigibody.velocity = Vector3.zero;
        }
        //thePAselector.aimingAngleWithMovingDirection = Vector3.SignedAngle(dir, theShootJoystickScript.parentPlayerTransform.right, Vector3.up);
        // 26 august the real aim is with the muzzle position not with the spine muzzle position denotes front direction PERIOD.

        //Debug.DrawRay(theShootJoystickScript.parentPlayerTransform.position, dir*10f, Color.red);
        //Debug.DrawRay(theShootJoystickScript.parentPlayerTransform.position, theShootJoystickScript.parentPlayerTransform.right*5f);

    }
}

