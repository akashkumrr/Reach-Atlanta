using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwaningZombieChaseRange : MonoBehaviour
{
    public spawnZombieMoveAi theZombieMoveScript;

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

        if(col.tag=="Player")
        {
            theZombieMoveScript.startChasingPlayer();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            theZombieMoveScript.stopChasingPlayerAndGoTowardsHouse();
            Debug.Log("player exited chase range"+col.name);
        }
    }
}
