using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootAndMove : MonoBehaviour
{
    public mySwipeScript swipeScript;
    public Transform playerTransform;  // This Transform will be rotated on the basis josystick Input
    public float zValueForMouseIP;

    public Vector3 touchToLookAndFirePosition;
    // Start is called before the first frame update
    void Start()
    {/*
        if (Application.isEditor)
        {
            Debug.Log("inside Editor thing works. shoot and move");
            touchToLookAndFirePosition = Input.mousePosition;
        }
        */
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 clickPositionInWorld=Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(touchToLookAndFirePosition);
        RaycastHit hit;
        //        if(Physics.Raycast(ray,out hit,zValueForMouseIP,11))

        /*
        if (Physics.Raycast(ray, out hit, zValueForMouseIP))
        {
            Debug.Log(hit.transform.name);
        }
        */
        int shootingMaskQuad = LayerMask.GetMask("ShootingPlane");

        // THE LAYER MASK WORKS AS FOLLOWS : IT will check if there is a collision for that layer only
        if (Physics.Raycast(ray, out hit, zValueForMouseIP, shootingMaskQuad))
        { 
            clickPositionInWorld = hit.point;
        }

        //Debug.DrawLine(transform.position, clickPositionInWorld);
        clickPositionInWorld.y = 0;
        Vector3 dir = clickPositionInWorld - transform.position;

        //Debug.DrawLine(transform.position+new Vector3(0,5,0), clickPositionInWorld, Color.red);
        //Debug.DrawRay(transform.position, dir*50f);
        //Quaternion joyRotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2((worldPositionOfMouse.y - transform.position.y), (worldPositionOfMouse.x - transform.position.x)) * Mathf.Rad2Deg, 0));
        //Debug.Log(Mathf.Atan2((worldPositionOfMouse.y - transform.position.y), (worldPositionOfMouse.x - transform.position.x)) * Mathf.Rad2Deg);
        //playerTransform.forward = joyRotation * Vector3.forward;
       playerTransform.forward = dir;

        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("w pressed");
            swipeScript.sDir = mySwipeScript.swipeDirection.Up;
            swipeScript.swipped(swipeScript.sDir);
        }

        if(Input.GetKey(KeyCode.S))
        {
            swipeScript.sDir = mySwipeScript.swipeDirection.Down;
            swipeScript.swipped(swipeScript.sDir);

        }
        if (Input.GetKey(KeyCode.A))
        {
            swipeScript.sDir = mySwipeScript.swipeDirection.Left;
            swipeScript.swipped(swipeScript.sDir);

        }
        if (Input.GetKey(KeyCode.D))
        {
            swipeScript.sDir = mySwipeScript.swipeDirection.Right;
            swipeScript.swipped(swipeScript.sDir);


        }
    }
}
