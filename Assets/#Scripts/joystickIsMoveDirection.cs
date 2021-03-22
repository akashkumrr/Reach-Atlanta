using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickIsMoveDirection : MonoBehaviour
{

    public Joystick joyIP;
    public mySwipeScript thSwipeScript;
    public float minDragX, minDragY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(joyIP.Horizontal) > minDragX && Mathf.Abs(joyIP.Vertical) > minDragY)
        {
            ////Debug.DrawRay(transform.position, new Vector3(joyIP.Horizontal, joyIP.Vertical, 0)*10f);
            if (Mathf.Abs(joyIP.Horizontal) > Mathf.Abs(joyIP.Vertical))
            {
                //Debug.Log("swipe dir is horizontally");
                if (joyIP.Horizontal > 0)
                {
                    thSwipeScript.sDir = mySwipeScript.swipeDirection.Right;
                    thSwipeScript.swipped(thSwipeScript.sDir);
                   // Debug.Log("righty swipyyy");
                }
                else
                {

                    thSwipeScript.sDir = mySwipeScript.swipeDirection.Left;
                    thSwipeScript.swipped(thSwipeScript.sDir);
                   // Debug.Log("leftyy swipyyy");
                }
            }
            else
            {

                //Debug.Log("swipe dir is vetical");
                if (joyIP.Vertical > 0)
                {
                    thSwipeScript.sDir = mySwipeScript.swipeDirection.Up;
                    thSwipeScript.swipped(thSwipeScript.sDir);
                   // Debug.Log("Up swipyyy");
                }
                else
                {

                    thSwipeScript.sDir = mySwipeScript.swipeDirection.Down;
                    thSwipeScript.swipped(thSwipeScript.sDir);
                   // Debug.Log("DOWNN swipyyy");
                }
            }
        }
    }
}
