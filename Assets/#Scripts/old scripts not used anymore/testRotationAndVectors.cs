using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotationAndVectors : MonoBehaviour
{

    public Transform playerTransform;
    //public Animator playerAnimators;
    //public Transform spineOfPlayer;
    public float angleA;
    public float angleB;
    public float angleC;
    public float rotationSpeed;
    public float x;
    public float y;
    public float z;
    public Transform parentTransform;
    public Joystick joystick;
    public float angleOfJoystick;
    public Vector3 myVector;
    public float distanceFromCenterJoyStick;

    // Start is called before the first frame update
    void Start()
    {
        /* // No need to get the bone Transofrm like this. Normal assignment also works
        spineOfPlayer = playerAnimators.GetBoneTransform(HumanBodyBones.Spine);
        */

    }

    // We will make a vector point in appropriate direction on the basis of joystick input and then ROTATE the spine of the character in that direction


    void LateUpdate()
    {

        Quaternion rot = Quaternion.Euler(angleA, angleB, angleC);
        Quaternion desiredRot = Quaternion.Euler(new Vector3(angleA, angleB, angleC));
        Quaternion q = Quaternion.Euler(x, y, z);

        ////parentTransform.rotation = rot;
        //playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, desiredRot, rotationSpeed * Time.deltaTime); // DOESN'T WORK for BONES

        //one way to rotate bone :1. values are changed in inspector 2. this causes a little head tilt on 90 degree angle, 180 degree was fine
        //playerTransform.localRotation = playerTransform.localRotation * q;

        //2nd way: but this doesn't changes the values in the inspector
        //playerTransform.rotation = q;

        //3rd way: similar as 1st way. but here 180 degree also tilted upwards
        //playerTransform.localRotation = q;

        //4th way: this is similar to 1st
        //playerTransform.rotation= playerTransform.rotation* q;

        //5th way : using this makes the upper body unaffected by parents rotation
        ////playerTransform.rotation = Quaternion.Euler(x,y,z);
        // hi there
        distanceFromCenterJoyStick = joystick.Horizontal * joystick.Horizontal + joystick.Vertical * joystick.Vertical;

        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            // 90 degrees is addded an an offset so that character looks in the right direction of joystick
            Quaternion joyRotation = Quaternion.Euler(new Vector3(0, -1 * Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg + 90f, 0));
            angleOfJoystick = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

            myVector = joyRotation* Vector3.forward ;
            /* This is so simple. Just see The world forward in Unity. You can almost visualize this happening.
             * A quaternion doesn't have a direction by itself. It is a rotation. 
             * It can be used to rotate any vector by the rotation it represents. Just multiply a Vector3 by the quaternion.
            */
            playerTransform.forward = myVector; // We can lerp it afterwards to give it a rotation time
        }

       // Debug.DrawRay(transform.position, myVector.normalized * 10f, Color.blue);
        
        
    }
}
