using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimAssistHelper : MonoBehaviour
{
    public List<aimAssistEnemy> aimAssistEnemies = new List<aimAssistEnemy>();
    public joyStickIsShootDirection theShootJoystickScript;
    public playerAnimationSelector thePAS;
   

    // Start is called before the first frame update
    void Start()
    {
        theShootJoystickScript = FindObjectOfType<joyStickIsShootDirection>();
        thePAS = FindObjectOfType<playerAnimationSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(thePAS.isAiming)
        {
            theShootJoystickScript.isAimAssisted = setGlobalAssist();
        }
    }

    

    bool setGlobalAssist()
    {
        foreach (var e in aimAssistEnemies)
        {
            if(e.causingToAssist==true)
            {
                return true;
            }
        }

        return false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="enemy")
        {
            Debug.Log("emey entered assist trigger");
            aimAssistEnemies.Add(col.GetComponent<aimAssistEnemy>());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "enemy")
        {
            Debug.Log("emey exited assist trigger");
            aimAssistEnemies.Remove(col.GetComponent<aimAssistEnemy>());
        }
    }
}
