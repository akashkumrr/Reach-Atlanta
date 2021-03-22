using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalZombieAnimatorHandler : MonoBehaviour
{

    public Animator normalZombieAnimator;
    public normalZombieMoveTowards theZMTNormalScript;
    // Start is called before the first frame update
    void Start()
    {
        int randRunAnim = Random.Range(1, 4);   // generates an int between [1 and 3] 
        normalZombieAnimator.SetInteger("runAnimType", randRunAnim);

        if (randRunAnim == 1 || randRunAnim == 2)
            theZMTNormalScript.myZombAgent.speed = 0.3f;
        else
            theZMTNormalScript.myZombAgent.speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch(theZMTNormalScript.zState)
        {
            case normalZombieMoveTowards.State.MovingToHouse:       normalZombieAnimator.SetBool("isAttackingRightNow", false);
                break;
            case normalZombieMoveTowards.State.ChasingPlayer:       normalZombieAnimator.SetBool("isAttackingRightNow", false);
                break;
            case normalZombieMoveTowards.State.Attacking:           normalZombieAnimator.SetBool("isAttackingRightNow", true);
                break;
        }
    }

    public void HitReactionEvent()      //  Invoke it using delegate or get an reference to this script in the gameobject which has zombie collider, if bullet enters trig call this function
    {
        normalZombieAnimator.SetTrigger("HitRxn");
    }

    public void randomAttackAnimationSelectionEvent()       // This is an animation event and gets called when an attack animation is completed
    {
        int randAttackAnim = Random.Range(1, 4);   // generates an int between [1 and 3] 
        normalZombieAnimator.SetInteger("attackTypeAnimID", randAttackAnim);
    }

    public void reducePlayerHealth()        // This is also an animation event and gets called in mid of an attack animation to reduce player's health if the player is close enough to take damage
    {
        if(theZMTNormalScript.insideAttackRange==true)
        {
            theZMTNormalScript.pHealthScript.currentHealth -= 10f;
        }
    }
}
