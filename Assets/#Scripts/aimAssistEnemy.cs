using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimAssistEnemy : MonoBehaviour
{
    public joyStickIsShootDirection theShootScript;
    public playerAnimationSelector thePAS;
    public Transform parentPlayerTransform;
    public Transform enemyTransform;
    public float angleBwAimAndEnemy;
    public float minDistForAimAssistOn=5f;
    public bool causingToAssist = false;
    // Start is called before the first frame update

    void Start()
    {
        enemyTransform = this.gameObject.transform;
        theShootScript = FindObjectOfType<joyStickIsShootDirection>();
        thePAS = FindObjectOfType<playerAnimationSelector>();
        parentPlayerTransform = FindObjectOfType<playerHealth>().transform;
    }

    void LateUpdate()
    {
        if (causingToAssist)
        {// for setting the weapon in the aiming direction

            thePAS.theWManScript.weapons[thePAS.theWManScript.activeWeapon].weaponObject.transform.right = new Vector3((enemyTransform.position - parentPlayerTransform.position).normalized.x, theShootScript.joystickRotationDirectionVector.y, (enemyTransform.position - parentPlayerTransform.position).normalized.z);
            thePAS.aimingAngleWithMovingDirection = Vector3.SignedAngle(thePAS.theJoystickMoveScript.dir, thePAS.theWManScript.weapons[thePAS.theWManScript.activeWeapon].weaponObject.transform.right, Vector3.up);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(thePAS.isAiming)
        {
            angleBwAimAndEnemy = Vector3.SignedAngle(theShootScript.joystickRotationDirectionVector, enemyTransform.position - parentPlayerTransform.position, Vector3.up);

            if (Vector3.Distance(enemyTransform.position,parentPlayerTransform.position)< minDistForAimAssistOn && Mathf.Abs( angleBwAimAndEnemy)<30)
            {
                causingToAssist = true;// AIM AT THE ENEMY WHICH IS CAUSING IT TO BE TRUE// YOU ARE SETTING HERE THE MAIN Transform of the player, but the weapon transform should also point in that direction
                // In the Player Animation selector You set the GUN PARALLEL false when assisting

                parentPlayerTransform.right = new Vector3(( enemyTransform.position- parentPlayerTransform.position).normalized.x,theShootScript.joystickRotationDirectionVector.y, (enemyTransform.position - parentPlayerTransform.position).normalized.z);
                Debug.DrawRay(parentPlayerTransform.position, parentPlayerTransform.right*8f,Color.yellow);
               // Debug.Log("SHOULD AIM at enemy" + angleBwAimAndEnemy);
            }
            else
            {
                causingToAssist = false;              

                //theShootScript.isAimAssisted = false; // Any other zombie not in range will force it to be false even when a zombie is in the range
            }
            theShootScript.isAimAssisted = theShootScript.isAimAssisted||causingToAssist;

        }
        else
        {
            causingToAssist = false;
        }
    }
}
