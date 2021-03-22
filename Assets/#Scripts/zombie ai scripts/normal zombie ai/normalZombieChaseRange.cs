using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalZombieChaseRange : MonoBehaviour
{
    public normalZombieMoveTowards theZMTNormalScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {
            theZMTNormalScript.zState = normalZombieMoveTowards.State.ChasingPlayer;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            theZMTNormalScript.zState = normalZombieMoveTowards.State.MovingToHouse;
            theZMTNormalScript.setDestinationAsHouse();
        }
    }
}
