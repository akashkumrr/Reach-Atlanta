using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalZombieAttackRange : MonoBehaviour
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
            theZMTNormalScript.myZombAgent.isStopped = true;
            theZMTNormalScript.zState = normalZombieMoveTowards.State.Attacking;
            theZMTNormalScript.insideAttackRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            theZMTNormalScript.myZombAgent.isStopped = false;
            theZMTNormalScript.zState = normalZombieMoveTowards.State.ChasingPlayer;
            theZMTNormalScript.insideAttackRange = false;
        }
    }

}
