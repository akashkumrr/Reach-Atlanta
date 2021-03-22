using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getupRagdollToAnimation : MonoBehaviour
{

    public HandleRagdoll theRdollHandlerScript;
    public bool toggleRagdoll=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            theRdollHandlerScript.EnableRagdoll(toggleRagdoll, theRdollHandlerScript.rigidbodies);
            toggleRagdoll = !toggleRagdoll;
        }
    }
}
