using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class zombieScript : MonoBehaviour
{


    public GameObject zombieParentGameobject;
    public int zombieID;
    public float zombieMoveSpeed;
    public float zombieHealth;
    public float zombieAttack;
    public float zombieRateOfAttack;
    public int zombieType; // to distinguish between abilities and attack using same function
    public Rigidbody zombieRigidbody;
    public float turingSpeedZombie;
    public Animator basicZombieAnimator;
    [Space(10)]
    public Transform zombieAvatarGameobjectTransform;
    public Transform target; // drag the player here    

    public bool canChasePlayer = false;
    public bool isAttacking = false;
    //public bool canRotateTowardsPlayer = true;
    public float lastAttackTime;
    public bool collidedWithEnviornmentWhileChasing = false;
    public Vector3 afterCollisionWithEnviornmentSecondaryPosition;
    public float afterCollisionWalkDistance = 4f;

    private playerHealth pHealthScript;

    void Start()
    {
        pHealthScript = Object.FindObjectOfType<playerHealth>();
        zombieRigidbody = this.gameObject.GetComponent<Rigidbody>();

    }

    /// <summary>
    ///  IMP collision is disabled with player layer and enemy so for triggers to work make them child of enemy and GIVE THEM LAYER OTHER THAN 'ENEMY'
    ///  ALSO use FixedUpdate as we are manipulating a rigidbody here. and it's a rule to use physics things inside FixedUpdate()
    /// </summary>
    void Update()
    {
        if (canChasePlayer == true)
        {
            //Debug.DrawRay(transform.position, (target.position - zombieAvatarGameobjectTransform.position) * 50);
            if (collidedWithEnviornmentWhileChasing == false)
            {
                zombieAvatarGameobjectTransform.forward = Vector3.Lerp(zombieAvatarGameobjectTransform.forward, new Vector3(target.position.x - zombieAvatarGameobjectTransform.position.x, 0, target.position.z - zombieAvatarGameobjectTransform.position.z), Time.deltaTime * turingSpeedZombie);
                zombieRigidbody.velocity = zombieAvatarGameobjectTransform.forward * zombieMoveSpeed;
            }
            else
            {
                if (zombieAvatarGameobjectTransform.position == afterCollisionWithEnviornmentSecondaryPosition)
                {
                    //Debug.Log("position Reached after collision");
                    collidedWithEnviornmentWhileChasing = false;
                }
                else
                {
                    zombieRigidbody.velocity = zombieAvatarGameobjectTransform.forward * zombieMoveSpeed;
                    zombieAvatarGameobjectTransform.forward = Vector3.Lerp(zombieAvatarGameobjectTransform.forward, (afterCollisionWithEnviornmentSecondaryPosition - zombieAvatarGameobjectTransform.position).normalized, Time.deltaTime * turingSpeedZombie);
                }
            }



        }

        //Debug.DrawRay(transform.position, afterCollisionWithEnviornmentSecondaryPosition-transform.position,Color.yellow);
        //Debug.DrawLine(transform.position, afterCollisionWithEnviornmentSecondaryPosition, Color.black);

        if (zombieHealth <= 0)
        {
            //Debug.Log("zombie died");
            this.gameObject.SetActive(false);
        }
        
    }



    void attackPlayer()
    {
        if (lastAttackTime >= zombieRateOfAttack)
        {
            pHealthScript.currentHealth -= zombieAttack;
            lastAttackTime = 0;
        }
    }


    private void OnTriggerEnter(Collider col)
    {
       // Debug.Log("Someone entered trigger");
        if(col.gameObject.tag=="bullet")
        {
           
            zombieHealth -= col.gameObject.GetComponent<bulletBehaviour>().bulletDamage;
            Destroy(col.gameObject); //
        }
       

       // if(col.gameObject.tag=="Player")        /// THIS WILL NOT WORK AS COLLISION IS DISABLED WITH PLAYER
       
    }

    public void OnCollisionEnter(Collision col)
    {
        if (canChasePlayer == true)
        {
            if (col.gameObject.tag != "enemy")
            {
                collidedWithEnviornmentWhileChasing = true;
                //Debug.Log("Zombie collided with enviornment while chasing");
                //zombieAvatarGameobjectTransform.forward = Vector3.Lerp(zombieAvatarGameobjectTransform.forward, Quaternion.Euler(0, 90, 0) * zombieAvatarGameobjectTransform.forward, Time.deltaTime * turingSpeedZombie);
                zombieAvatarGameobjectTransform.forward = Quaternion.Euler(0, 90, 0) * zombieAvatarGameobjectTransform.forward;
                afterCollisionWithEnviornmentSecondaryPosition = zombieAvatarGameobjectTransform.position + zombieAvatarGameobjectTransform.forward * afterCollisionWalkDistance;

            }
        }
    }
     
}
