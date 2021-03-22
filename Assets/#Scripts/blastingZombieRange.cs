using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastingZombieRange : MonoBehaviour
{
    public zombieScript theZS;
    public GameObject theBlastPS;
    public playerHealth thePHScript;
    // Start is called before the first frame update ko
    void Start()
    {
        thePHScript = Object.FindObjectOfType<playerHealth>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // why use animation event for blast?? oh for deducting player heath after a certain time!!
            Instantiate(theBlastPS, transform.position, Quaternion.identity);
            theBlastPS.transform.parent = null;
            theBlastPS.SetActive(true);
            theZS.zombieAvatarGameobjectTransform.gameObject.SetActive(false);

            theZS.canChasePlayer = false;
            
            theZS.zombieRigidbody.velocity = Vector3.zero;
            theZS.basicZombieAnimator.SetBool("isChasingPlayer", false);
            theZS.basicZombieAnimator.SetBool("attacking", true);

          
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            theZS.basicZombieAnimator.SetBool("attacking", false);
            //theZS.canChasePlayer = true;


        }
    }

    public void AfterAttackChasePlayer()
    {
        //Debug.Log("ATTACK ANIM OVER CAN CHASE PLAYER");
        theZS.canChasePlayer = true;
        theZS.collidedWithEnviornmentWhileChasing = false;
        theZS.basicZombieAnimator.SetBool("isChasingPlayer", true);
    }

    // HOW BLAST WILL TAKE PLACE :
    // Attack animation (here blast animation) would be played. In starting of that anim. fire an anim event to instantiate Blast effect.
    // in mid animation (after a couple of sec after blast has started). reduce the health of player.

        public void startBlastAsAttackingPlayer()
    {
        Instantiate(theBlastPS, transform.position, Quaternion.identity);
        theBlastPS.transform.parent = null;
        theBlastPS.SetActive(true);
        theZS.zombieAvatarGameobjectTransform.gameObject.SetActive(false);

    }

    public void midBlastEffect()
    {
        thePHScript.currentHealth -= theZS.zombieAttack;
    }
}
