using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitZombieAnimatorController : MonoBehaviour
{
    public spitZombieMoveTowards theSZMTScript;
    public Animator spitZombieAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(theSZMTScript.zState)
        {
            case spitZombieMoveTowards.State.Roaming:
                spitZombieAnimator.SetBool("isAttackingRightNow", false);
                break;
            
            case spitZombieMoveTowards.State.Attacking:
                spitZombieAnimator.SetBool("isAttackingRightNow", true);
                break;
        }
    }


}
