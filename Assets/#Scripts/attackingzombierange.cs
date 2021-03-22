using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackingzombierange : MonoBehaviour
{
    public zombieScript theZS;
   
    // Start is called before the first frame update ko
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            theZS.isAttacking = true;
            theZS.canChasePlayer = false;
            theZS.collidedWithEnviornmentWhileChasing = false;
            theZS.zombieRigidbody.velocity = Vector3.zero;
            theZS.basicZombieAnimator.SetBool("isChasingPlayer", false);
            theZS.basicZombieAnimator.SetBool("attacking", true);

        }
    }


    private void OnTriggerExit(Collider collision)
    {
        // if player exits before animation is complete. CanChase is set to true This gives silding effect 
        /*
        if (collision.gameObject.tag=="Player")
        {
            theZS.isAttacking = false;

            theZS.basicZombieAnimator.SetBool("attacking", false);
            //theZS.canChasePlayer = true;
            theZS.canChasePlayer = true;
            theZS.collidedWithEnviornmentWhileChasing = false;
            theZS.basicZombieAnimator.SetBool("isChasingPlayer", true);

        }
        */
    }

    public void AfterAttackChasePlayer()
    {
        // is this necessary, yes Because Player can exit Attack trigger very soon, even before animation has completed
        // so if player exits trigger before attack anim, is completed. If on EXIT() chase is set to TRUE. The player will move
        // while attacking giving a slide like effect. So set can chase == True after Animation has completed i.e. HERE.
        //Debug.Log("ATTACK ANIM OVER CAN CHASE PLAYER");
        theZS.isAttacking = false;

        theZS.basicZombieAnimator.SetBool("attacking", false);
        //theZS.canChasePlayer = true;
        theZS.canChasePlayer = true;
        theZS.collidedWithEnviornmentWhileChasing = false;
        theZS.basicZombieAnimator.SetBool("isChasingPlayer", true);
    }
}
