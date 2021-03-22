using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationSelector : MonoBehaviour
{

    /* This will work as : Parent player Front direction (in our case X axis) will always look at the next waypoint
     * this will make the player chesticles LOL (also the whole player) always face the correct direction. Then calculate
     * the angle made while aiming with the Parent Front direction. To select the right animation whether it is strafe or
     * run. all depending on the above ANGLE. 
     * 0<angle<30 : Front running
     * 30<angle<180 : strafe left sideways of PARENT FRONT
     * 180<angle<330 : strafe right sideways of PARENT FRONT
     * 330<angle<360 : Front Running;
     * 
     * We will rotate the Transform of spine accoring to the rotation of the joystick.
     * 
     * 
     */

    public Animator playerAnimator;
    public RuntimeAnimatorController[] weaponsControllers;
    public int animatorIndexDependingOnWeaponEquipped=0;
    public joyStickIsShootDirection theJoystickShootScript;
    public movingPlayerWithJoystick theJoystickMoveScript;
    public weaponManager theWManScript;
    [Space(20)]
    public float aimingAngleWithMovingDirection;
    public Transform parentTransform;
    public Transform playerTranform;
 
    public bool addOnceToPositionWhenChangingFromOrToIdleAnimation=false;  // initialization very important to false; because then is moving is false and then position gets subtracted in else part
    public bool onlyOnceAimPositionAddition = false;  // initialization very important to false; because then is moving is false and then position gets subtracted in else part


    public Vector3 playerPositionWhenIdle;
    public Vector3 playerPositionWhenMoving;
    public float deltaYIdle;
    public float deltaYMoving;

    public float rotateBackSpeed=20f;
    public bool rotate180OnceOnly = true;
    public bool isMoving = false;
    public bool isAiming = false;

    void Start()
    {        
    }

    void LateUpdate()  // 21 august changed to lateupdate to make gun parallel to ground
    {
        // this gives angle between -180 and 180 deending upon the aiming angle with the movement direction
        //aimingAngleWithMovingDirection = Vector3.SignedAngle(playerTranform.forward,followP.wayPointHeadingDirection, Vector3.up);

        // the left of the player is considered positive and right side of player is considered negative

         if (isAiming==true)
        {
            playerAnimator.SetLayerWeight(4, 1);
            //theWManScript.weapons[theWManScript.activeWeapon].weaponObject.transform.right = theJoystickShootScript.dir;     // Make gun parallel to gorund  

            if(theJoystickShootScript.isAimAssisted==false)
           { 
            theWManScript.weapons[theWManScript.activeWeapon].weaponObject.transform.right = Vector3.Lerp(theWManScript.weapons[theWManScript.activeWeapon].weaponObject.transform.right, theJoystickShootScript.joystickRotationDirectionVector, theJoystickShootScript.roatateSpeed * Time.deltaTime);
            aimingAngleWithMovingDirection = Vector3.SignedAngle(theJoystickMoveScript.dir, theWManScript.weapons[theWManScript.activeWeapon].weaponObject.transform.right, Vector3.up);
            }
            if(theWManScript.activeWeapon==1)   // if bow is active weapon, Why were we doing this??
            {
                //aimingAngleWithMovingDirection += 90;
            }
            
        }
        else
        {
            aimingAngleWithMovingDirection = Vector3.SignedAngle(theJoystickMoveScript.dir, theJoystickShootScript.parentPlayerTransform.right, Vector3.up);
            playerAnimator.SetLayerWeight(4, 0);
        }

       
        if (isMoving == true)
        {
            //playerAnimator.SetLayerWeight(6, 0);

            playerPositionWhenMoving = playerTranform.position;            
            addOnceToPositionWhenChangingFromOrToIdleAnimation = false;            
            playerAnimator.SetBool("isWalking", true);

            if (aimingAngleWithMovingDirection >= 0 && aimingAngleWithMovingDirection <= 30)
            {
                playerAnimator.SetFloat("strafeByFloat", -1);
                //Debug.Log("Normal front running. Although seeing left");
            }
            else if (aimingAngleWithMovingDirection > 30 && aimingAngleWithMovingDirection <= 120)
            {
                playerAnimator.SetFloat("strafeByFloat", 1);
                //Debug.Log("Player lokking to its left. STRAFE LEFT");
            }
            else if (aimingAngleWithMovingDirection > 120 && aimingAngleWithMovingDirection <= 180)
            {
                playerAnimator.SetFloat("strafeByFloat", 4);

                //Debug.Log("Player almost looking Behind via its left. Jump Backwards animation like Xenowrek");
            }
            else if (aimingAngleWithMovingDirection >= -30 && aimingAngleWithMovingDirection < 0)
            {
                playerAnimator.SetFloat("strafeByFloat", -1);
                // Debug.Log("Normal front running. Although seeing right");
            }
            else if (aimingAngleWithMovingDirection >= -120 && aimingAngleWithMovingDirection < -30)
            {
                playerAnimator.SetFloat("strafeByFloat", 2);
                // Debug.Log("player looking to its right. STRAFE RIGHT");
            }
            else if (aimingAngleWithMovingDirection >= -180 && aimingAngleWithMovingDirection < -120)
            {
                playerAnimator.SetFloat("strafeByFloat", 4);

                // Debug.Log("Player almost looking Behind via its RIGHT. Jump Backwards animation like Xenowrek");
            }
        }
        else
        {
            playerPositionWhenIdle = playerTranform.position;
            playerAnimator.SetBool("isWalking", false);
        }        
    }

    public void changeAnimator()
    {
        animatorIndexDependingOnWeaponEquipped += 1;
        if(animatorIndexDependingOnWeaponEquipped>=weaponsControllers.Length)
        {
            animatorIndexDependingOnWeaponEquipped = 0;
        }
        
        playerAnimator.runtimeAnimatorController = weaponsControllers[animatorIndexDependingOnWeaponEquipped];
    }
}
