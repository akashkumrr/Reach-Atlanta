using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseClick : MonoBehaviour
{
    public bool toggle=false;
    public Transform playerTransform;  // This Transform will be rotated on the basis josystick Input
    public Joystick joystick;
    public float rotationSpeed;
    public float distanceFromCenterJoyStick;
    public float angleOfJoystick;

    public Animator villagerAnimator;


    void Start()
    {
        playerTransform = villagerAnimator.GetBoneTransform(HumanBodyBones.Spine);
    }
   
    // changing Update() to LateUpdate() 4/8/2020 ************************* IMP Bones in LateUpdate() only!!!! Else it doesn't work

    // There will be two type of Aiming systems : 1. Virtual D-pad 2. Point to aim in that direction
    void LateUpdate()
    {

        //float horizontalMove = joystick.Horizontal * 21;

        Vector3 worldPositionOfMouse = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        /*  POINT & SHOOT : Atan2 is better than Atan because is doesn't filps around the +90 and -90 cases
            The above code is to sets the Z angle of Player in the inspector equal to angle B/W 
            mouse and player
        */
        // transform.rotation = Quaternion.Euler( new Vector3 (0,0,Mathf.Atan2((worldPositionOfMouse.y-transform.position.y),(worldPositionOfMouse.x-transform.position.x)) * Mathf.Rad2Deg));

        distanceFromCenterJoyStick = joystick.Horizontal * joystick.Horizontal + joystick.Vertical * joystick.Vertical;
        /*  VIRTUAL JOYSTICK SHOOT (select one of the aiming methods) */
        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            Quaternion desiredRot= Quaternion.Euler(new Vector3(0, -1 * Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg, 0));
            angleOfJoystick = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

            // playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, desiredRot, rotationSpeed * Time.deltaTime);
            // the above was the old way of rotation a vector by joystick.

            playerTransform.rotation = playerTransform.rotation * desiredRot;

            // Now. the spine bone is rotated by this code
        }


    }
}
