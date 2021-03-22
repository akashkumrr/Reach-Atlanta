using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ragdollZombie : MonoBehaviour
{

    public float forceMagnitude;
    public HandleRagdoll ragdollHandler;
    public Rigidbody[] selectedRigidbodyToApplyForceWhenBullerHits;  // take 4 rigidbodies and apply force on any 1 selected at random. head + 2 shoulders + sipne
    public int selectBodyPartToapplyForce=2;
    private Vector3 force;

    [Space(10)]
    public float penetrationDepth = 0.3f;
    public zombieHealth theZombieHealthScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "bullet")
        {
            Debug.Log(col.name);
            ///col.tag = "Untagged"; Make bullet untagged in the zombie health script
            Debug.Log("Zombie hit by a bullet.");

            theZombieHealthScript.currentHealth -= col.gameObject.GetComponent<bulletBehaviour>().bulletDamage;

            // so that if another zombie touches already stuck arrow in another zombie. He should ignore it
            Rigidbody hitPodyPart = selectedRigidbodyToApplyForceWhenBullerHits[selectBodyPartToapplyForce];  // force would be applied on selected body part only
            //GameObject tempArrow = col.gameObject;
            col.attachedRigidbody.isKinematic = true;
            col.transform.position = hitPodyPart.gameObject.transform.position;

            Vector3 arrowDir = col.transform.up;
            col.transform.position = col.transform.position + penetrationDepth * arrowDir;
            col.transform.parent = hitPodyPart.transform;


            if (theZombieHealthScript.currentHealth <= 0)   // If die then only ragdoll and if tag is arrow then only apply Force to that ragdoll
            {


                force = -1 * transform.forward * forceMagnitude;


                //int randomSelect = Random.Range(1, 4);
                //selectBodyPartToapplyForce = randomSelect;

                                                                                                                  //GameObject tempArrow = col.gameObject;
                col.attachedRigidbody.isKinematic = true;
                col.transform.position = hitPodyPart.gameObject.transform.position;

                col.transform.position = col.transform.position + penetrationDepth * arrowDir;
                col.transform.parent = hitPodyPart.transform;

                Debug.Log(selectedRigidbodyToApplyForceWhenBullerHits[selectBodyPartToapplyForce].name);
                if (hitPodyPart != null)
                {
                    ragdollHandler.EnableRagdoll(true, ragdollHandler.rigidbodies);

                    //// trying by using col.transform.yp whihc is arrow front dir 
                    /////hitPodyPart.AddForce(col.GetComponent<Rigidbody>().velocity.normalized * forceMagnitude, ForceMode.Impulse);
                    hitPodyPart.AddForce(-1 * arrowDir * forceMagnitude, ForceMode.Impulse);

                }

                this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                //Destroy(col.gameObject);

            }
        }
    }
    
    
}
