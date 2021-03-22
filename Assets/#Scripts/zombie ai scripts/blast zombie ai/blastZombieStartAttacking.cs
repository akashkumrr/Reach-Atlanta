using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastZombieStartAttacking : MonoBehaviour
{

    public blastZombieMoveTowards theBZMTScript;
    public GameObject blastEffectParticleSystem;
    public float delayHealthRedutionTimer = 1.5f;
    public bool hasBlasted = false;
    public bool blastOnce = true;
    public float blastDamage=40f;
  
    void Update()
    {
        /*
        if(hasBlasted==true)
        {
            delayHealthRedutionTimer -= Time.deltaTime;
        }

        if(delayHealthRedutionTimer<=0&&blastOnce==true)
        {
            blastOnce = false;
            dealDamage();
        }

        // ************************* using timer is causing problem

        */
    }

    private void dealDamage()
    {
        if (theBZMTScript.escapedBlast == false)
        {
            theBZMTScript.pHealthScript.currentHealth -= blastDamage;
            //Debug.Log("Dealt damage to the player");
        }
        else
        {
            //Debug.Log("Player has escaped the blast range");
        }

        theBZMTScript.destroyBlastZombieAfterBlast();

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag== "Player")
        {
            Instantiate(blastEffectParticleSystem, transform.position, Quaternion.identity);
            blastEffectParticleSystem.SetActive(true);
            hasBlasted = true;

            dealDamage();
        }
    }
}
