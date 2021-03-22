using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRagdoll : MonoBehaviour
{

    public Collider mainCollider;
    public Rigidbody mainRigidbody;
    public Animator anim;

    public Rigidbody[] childrenRbs;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public bool ragdollActive = false;
    // Start is called before the first frame update
    void Start()
    {
        childrenRbs = GetComponentsInChildren<Rigidbody>(); 

        foreach(Rigidbody rb in childrenRbs)
        {
            rigidbodies.Add(rb);
        }

        //EnableRagdoll(ragdollActive, rigidbodies);
        EnableRagdoll(false, rigidbodies);          // very imp to set it here  VERY IMPORTANT OTHERWISE CHARACTER GETS SLAMMED
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EnableRagdoll(ragdollActive, rigidbodies);
            ragdollActive = !ragdollActive;
        }
        */
    }

    public void EnableRagdoll(bool enable, List<Rigidbody> rigidbodies)
    {

 for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = !enable;
        }


        
         if (mainCollider != null)
        {
            mainCollider.enabled = !enable;
        }
        
        if (mainRigidbody != null)
        {
            mainRigidbody.isKinematic = enable;
        }
        if (anim != null)
        {
            anim.enabled = !enable;
        }
    }
}
