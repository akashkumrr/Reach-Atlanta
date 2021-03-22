using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitZombieAttack : MonoBehaviour,InterfaceZombieAttack
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



    // Start is called before the first frame update
    void Start()
    {
        
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

        //Debug.Log("Attacking via the interface.");
        //float rotateEnemyAngleOnYAxis = Vector3.Angle(enemyBodyTransform.forward, (playerTransformToFollow.position - enemyBodyTransform.position).normalized);
        //Debug.Log(rotateEnemyAngleOnYAxis);
        //Quaternion q = Quaternion.Euler(0, rotateEnemyAngleOnYAxis, 0);
        Quaternion temp = parentEnemyTransform.rotation;
        //parentEnemyTransform.rotation = Quaternion.RotateTowards(parentEnemyTransform.rotation, q, 500.0f * Time.deltaTime);
        //parentEnemyTransform.rotation = q;

        Debug.DrawRay(enemyBodyTransform.position, (playerTransformToFollow.position - enemyBodyTransform.position).normalized * 100f, Color.blue);

        if (lastSpitAttackTime >= 1 / spitFireRate)
        {
            spitZombieAnimator.SetTrigger("spit");
            GameObject g = Instantiate(spitProjectile, parentEnemyTransform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().velocity = spitProjectileSpeed * (playerTransformToFollow.position - parentEnemyTransform.position).normalized;
            lastSpitAttackTime = 0;
        }
    }
}
