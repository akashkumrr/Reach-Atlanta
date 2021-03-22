using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Interfaces are so cool ! Here you implement an interface and when the zombie dies in zombieHealth.cs we can look for that interface only, eliminating the need 
// to reference Different type of zombie scripts in the zombie health script
public class spitZombieMoveTowards : MonoBehaviour , InterfaceZombieDeath 
{
    public Transform playerTransformToFollow;
    public Transform houseTransform;
    public NavMeshAgent myZombAgent;
    public Animator spitZombieAnimator;
    public GameObject spitProjectile;
    public Rigidbody spitRb;
    public float spitProjectileSpeed;
    public enum State
    {
        Roaming,
        Attacking,
        SearchingForPlayer      // This state is for when the player is spotted in FOV leaves enemy line of sight and then the enemy goes and check last position
    }

    public State zState;
    public bool escapedBlast = false;

    public float zombieTurningSpeed;
    public playerHealth pHealthScript;
    public float spitFireRate;
    public float lastSpitAttackTime=0;
    public float rotSpeed = 10;
    [Space(10)]
    public Transform parentEnemyTransform;
    public Transform enemyBodyTransform;
    public FieldOfView theFovScript;
    
    void Start()
    {
        zState = State.Roaming;

        pHealthScript = Object.FindObjectOfType<playerHealth>();
        playerTransformToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        houseTransform = GameObject.FindGameObjectWithTag("house").transform;
        if (myZombAgent.enabled == true)
        {
            myZombAgent.SetDestination(houseTransform.position);
        }
            spitRb = spitProjectile.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lastSpitAttackTime += Time.deltaTime;
        switch (zState)
        {
            case State.Roaming:
                break;
            case State.Attacking:

                //parentEnemyTransform.forward = Vector3.Lerp(parentEnemyTransform.forward, (playerTransformToFollow.position - parentEnemyTransform.position).normalized, rotSpeed * Time.deltaTime);
                // dont rotate using transform forward as the forward of the parent is not really the forward of the player.

                enemyBodyTransform.forward = Vector3.Lerp(enemyBodyTransform.forward, Vector3.ProjectOnPlane((playerTransformToFollow.position - enemyBodyTransform.position).normalized, Vector3.up), rotSpeed * Time.deltaTime);
                theFovScript.fowardDirectionHelper.transform.right = Vector3.Lerp(enemyBodyTransform.forward, Vector3.ProjectOnPlane((playerTransformToFollow.position - enemyBodyTransform.position).normalized,Vector3.up), rotSpeed * Time.deltaTime);


                //float rotateEnemyAngleOnYAxis = Vector3.Angle(enemyBodyTransform.forward, (playerTransformToFollow.position - enemyBodyTransform.position).normalized);
                //Debug.Log(rotateEnemyAngleOnYAxis);
                //Quaternion q = Quaternion.Euler(0, rotateEnemyAngleOnYAxis, 0);
                Quaternion temp = parentEnemyTransform.rotation;
                //parentEnemyTransform.rotation = Quaternion.RotateTowards(parentEnemyTransform.rotation, q, 500.0f * Time.deltaTime);
                //parentEnemyTransform.rotation = q;

                Debug.DrawRay(enemyBodyTransform.position, (playerTransformToFollow.position - enemyBodyTransform.position).normalized * 100f,Color.blue);

                if (lastSpitAttackTime>=1/spitFireRate)
                {
                    spitZombieAnimator.SetTrigger("spit");
                    GameObject g = Instantiate(spitProjectile, parentEnemyTransform.position, Quaternion.identity);
                    g.GetComponent<Rigidbody>().velocity = spitProjectileSpeed * (playerTransformToFollow.position - parentEnemyTransform.position).normalized;
                    lastSpitAttackTime = 0;
                }
                    
                break;
        }

        if (theFovScript.playerInFovSpotted == true)
        {
            StartAttackingPlayer();
        }
        else
        {
            StartRoaming();
        }

        //Debug.DrawRay(transform.position, spitProjectileSpeed * (playerTransformToFollow.position - transform.position).normalized, Color.blue);
    }

    public void IzombieDeath()
    {
        Debug.Log("Zombie Inteface called");
        this.GetComponent<spitZombieMoveTowards>().enabled = false;
    }


    public void StartRoaming()
    {
        zState = State.Roaming;
    }

    public void StartAttackingPlayer()
    {
        zState = State.Attacking;
    }

}