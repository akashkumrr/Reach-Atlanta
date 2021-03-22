using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAiAttack : MonoBehaviour, InterfaceZombieAttack
{
    

    public Transform enemyBodyTransform;
    public Transform playerTransformToFollow;
    public FieldOfView theFovScript;
    public Transform parentEnemyTransform;
    [Space(10)]
    public float lastSpitAttackTime;
    public float spitFireRate;
    public Animator spitZombieAnimator;
    public GameObject spitProjectile;
    public float spitProjectileSpeed;
    public float rotSpeed;
    [Space(10)]
    public bool isLastAttackAnimationComplete;


    // Start is called before the first frame update
    void Start()
    {
        spitZombieAnimator.SetBool("isAttackAnimComplete", true);
        isLastAttackAnimationComplete = true;
    }

    // Update is called once per frame
    void Update()
    {
        lastSpitAttackTime += Time.deltaTime;
    }



    public void IzombieAttack()
    {

        enemyBodyTransform.forward = Vector3.Lerp(enemyBodyTransform.forward, Vector3.ProjectOnPlane((playerTransformToFollow.position - enemyBodyTransform.position).normalized, Vector3.up), rotSpeed * Time.deltaTime);
        theFovScript.fowardDirectionHelper.transform.right = Vector3.Lerp(enemyBodyTransform.forward, Vector3.ProjectOnPlane((playerTransformToFollow.position - enemyBodyTransform.position).normalized, Vector3.up), rotSpeed * Time.deltaTime);


        if (spitZombieAnimator.GetBool("isAttackAnimComplete") == true)
        {

            spitZombieAnimator.SetBool("isAttackAnimComplete", false);

            int attackRandomNumber = Random.Range(0, 3);
            Debug.Log(attackRandomNumber);
            switch (attackRandomNumber)
            {
                case 0:
                    isLastAttackAnimationComplete = false;
                    spitZombieAnimator.SetTrigger("doubleHandAttack");// play attack anim and wait until its complete. // reduce player health at the end of attack animation using animation event

                    break;
                case 1:
                    isLastAttackAnimationComplete = false;
                    spitZombieAnimator.SetTrigger("powerPunchAttack");
                    break;

                case 2:
                    if (lastSpitAttackTime >= 1 / spitFireRate)
                    {
                        spitZombieAnimator.SetTrigger("spit");
                        GameObject g = Instantiate(spitProjectile, parentEnemyTransform.position, Quaternion.identity);
                        g.GetComponent<Rigidbody>().velocity = spitProjectileSpeed * (playerTransformToFollow.position - parentEnemyTransform.position).normalized;
                        lastSpitAttackTime = 0;
                    }
                    break;
            }

            
        }
    }
}


