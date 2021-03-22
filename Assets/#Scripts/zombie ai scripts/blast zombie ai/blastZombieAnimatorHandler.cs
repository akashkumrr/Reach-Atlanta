using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastZombieAnimatorHandler : MonoBehaviour
{


    public Animator blastZombieAnimator;
    public blastZombieMoveTowards theBZMTScript;
    // Start is called before the first frame update
    void Start()
    {
        int runType = 1; // 1 denotes walking cliche animation
        blastZombieAnimator.SetInteger("runAnimType", runType);

        if (runType == 1)
            theBZMTScript.myZombAgent.speed = 0.3f;
     
    }

    // Update is called once per frame
    void Update()
    {
        switch (theBZMTScript.zState)
        {
            case blastZombieMoveTowards.State.MovingToHouse:
                blastZombieAnimator.SetBool("isAttackingRightNow", false);
                break;
            case blastZombieMoveTowards.State.ChasingPlayer:
                blastZombieAnimator.SetInteger("runAnimType", 2);
                theBZMTScript.myZombAgent.speed = 5.0f;

                blastZombieAnimator.SetBool("isAttackingRightNow", false);
                break;
            case blastZombieMoveTowards.State.Attacking:
                blastZombieAnimator.SetBool("isAttackingRightNow", true);
                break;
        }
    }

    public void HitReactionEvent()      //  Invoke it using delegate or get an reference to this script in the gameobject which has zombie collider, if bullet enters trig call this function
    {
        blastZombieAnimator.SetTrigger("HitRxn");
    }

    public void randomAttackAnimationSelectionEvent()       // This is an animation event and gets called when an attack animation is completed
    {      
        int randAttackAnim = Random.Range(1, 4);   // generates an int between [1 and 3] 
        blastZombieAnimator.SetInteger("attackTypeAnimID", randAttackAnim);
    }
}
    
