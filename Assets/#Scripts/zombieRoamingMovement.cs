using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieRoamingMovement : MonoBehaviour
{
    public GameObject parentOfZombie;
    public float roamSpeed;
    public float maxRoamingDistance=5f;
    public float roamingDistance= 5f;
    public float roamingDistanceOnCollisionWithEnviornment= 1f;
    public float rotationSpeed=10f;

    public bool collidedWithEnviornment = false;
    public Vector3 targetRoamPosition;
    public bool isRoaming = true;
    public Animator CrawlZombieAnimator;

    public zombieScript theZS;
    public zombieSensingNearbyPlayer theSenseScript;
    public zombieChaseRange theChaseScript;
    public attackingzombierange theAttackScript;

    // not always walk randomly but radomly. i.e. 
    // generate a random no to switch between walking and idle standing state
    // but when it is walking wait for it to reach it destination
    void Start()
    {
        targetRoamPosition = parentOfZombie.transform.position + parentOfZombie.transform.forward * roamingDistance;
        CrawlZombieAnimator.SetBool("isRoamingRandomly", true);
    }

    void Update()
    {
        if(theZS.canChasePlayer==true||theZS.isAttacking==true)
        {
            isRoaming = false;
            CrawlZombieAnimator.SetBool("isRoamingRandomly", false);

        }
        else
        {
            isRoaming = true;
            CrawlZombieAnimator.SetBool("isRoamingRandomly", true);

        }
        if (isRoaming==true)
        {

            if (parentOfZombie.transform.position == targetRoamPosition)
            {
                roamingDistance = Random.Range(1f, maxRoamingDistance);
                targetRoamPosition = parentOfZombie.transform.position + Quaternion.Euler(0, 90, 0) * parentOfZombie.transform.forward * roamingDistance;

            }
            else
            {
                parentOfZombie.transform.position = Vector3.MoveTowards(parentOfZombie.transform.position, targetRoamPosition, roamSpeed * Time.deltaTime);
                //parentOfZombie.transform.rotation = Quaternion.RotateTowards(parentOfZombie.transform.rotation, desiredRot, rotationSpeed * Time.deltaTime); // DOESN'T WORK for BONES
                parentOfZombie.transform.forward = Vector3.Lerp(parentOfZombie.transform.forward, (targetRoamPosition - parentOfZombie.transform.position).normalized, Time.deltaTime * rotationSpeed);
                //parentOfZombie.transform.forward = (targetRoamPosition- parentOfZombie.transform.position).normalized;

            }
            //Debug.DrawRay(transform.position, targetRoamPosition - transform.position, Color.red);
        }
        else
        {
            CrawlZombieAnimator.SetBool("isRoamingRandomly", false);

        }

        //if(isRoaming==true)
     //   {
       //     theZS.canRotateTowardsPlayer = false;
      //  }
    }

    void OnCollisionEnter(Collision col)
    {
        if (isRoaming)
        {
            if (col.gameObject.tag != "enemy")
            {
                //Debug.Log("Collided with enviornment. Rotate and then move");

                parentOfZombie.transform.forward = Vector3.Lerp(parentOfZombie.transform.forward, Quaternion.Euler(0, 90, 0) * parentOfZombie.transform.forward.normalized, Time.deltaTime * rotationSpeed);

                targetRoamPosition = parentOfZombie.transform.position + Quaternion.Euler(0, 90, 0) * parentOfZombie.transform.forward * roamingDistanceOnCollisionWithEnviornment;


                //targetRoamPosition = parentOfZombie.transform.position + parentOfZombie.transform.forward* roamingDistanceOnCollisionWithEnviornment;

            }
        }

    }

    void OnCollisionExit(Collision col)
    {
        
           collidedWithEnviornment = false;    


    }
}
