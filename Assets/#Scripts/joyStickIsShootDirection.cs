using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Removed lerping as it was looking weird


public class joyStickIsShootDirection : MonoBehaviour
{
    public Joystick shootingJoystick;
    public Transform parentPlayerTransform;
    public weaponManager theWeaponMan;
    public bool shootJoystickDragged;
    public float roatateSpeed;
    public float offset_angleDependingUponCameraAngle;  
    public playerAnimationSelector thePASelector;
    public float remainingAngle;
    public bool joystickLiftedWhenWantsToShoot=false; // this will be true if dragged to 25% of joystick else false because then we want to cancel the shot
    public Vector3 joystickRotationDirectionVector;
    public bool isAimAssisted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainingAngle = Vector3.SignedAngle(joystickRotationDirectionVector, parentPlayerTransform.right, Vector3.up);

        if (Mathf.Abs(shootingJoystick.Horizontal) > 0.1f || Mathf.Abs(shootingJoystick.Vertical) > 0.1f)
        {
            thePASelector.isAiming = true;

            // add extra 90 degrees to make joystick angle start from up, else it starts from left;(i.e. -90 )
            
                Quaternion joystickRotation = Quaternion.Euler(new Vector3(0, -Mathf.Atan2(shootingJoystick.Vertical, shootingJoystick.Horizontal) * Mathf.Rad2Deg + offset_angleDependingUponCameraAngle + 90, 0));
                joystickRotationDirectionVector = joystickRotation * Vector3.forward;
                Debug.DrawRay(transform.position, joystickRotationDirectionVector * 20f, Color.green);

            
            if (Mathf.Abs(shootingJoystick.Horizontal) > 0.2f || Mathf.Abs(shootingJoystick.Vertical) > 0.2f)
            {
                joystickLiftedWhenWantsToShoot = true;
            }
            else
            {
                joystickLiftedWhenWantsToShoot = false;
            }


            if (thePASelector.isMoving == false)
            {
               
                if (remainingAngle > 0)
                {
                    thePASelector.playerAnimator.SetFloat("turningWhileStanding", 1.1f);    //turning right
                }
                else if(remainingAngle < 0)
                {
                    thePASelector.playerAnimator.SetFloat("turningWhileStanding", -1.1f);   //turning left 
                }
                else
                {
                    thePASelector.playerAnimator.SetFloat("turningWhileStanding", 0f);      // back to standing animation
                }

            }

            shootJoystickDragged = true;
            theWeaponMan.leftFingerDown = true;
            if (isAimAssisted == false)
            {
                //parentPlayerTransform.right = Vector3.Lerp(parentPlayerTransform.right, joystickRotationDirectionVector, roatateSpeed * Time.deltaTime);
                parentPlayerTransform.right = joystickRotationDirectionVector;
            }
        }
        else
        {
            isAimAssisted = false;
            theWeaponMan.leftFingerDown = false;
            theWeaponMan.leftFingerUp = false;
            shootJoystickDragged = false;
            thePASelector.isAiming = false;
            shootingJoystick.joystickLifted = true;
            
            thePASelector.playerAnimator.SetFloat("turningWhileStanding", 0f);            // back to standing animation
        }


        if (joystickLiftedWhenWantsToShoot ==true && shootingJoystick.joystickLifted==true)
        {
            theWeaponMan.leftFingerUp = true;
            joystickLiftedWhenWantsToShoot = false;
        }


        // this ray is for testing the bow direction commet it out as soon as bow starts working
        //Debug.DrawRay(parentPlayerTransform.position, Quaternion.Euler(0,90,0) *dir  * 10f, Color.black);

    }
}
