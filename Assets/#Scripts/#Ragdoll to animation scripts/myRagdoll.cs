// IN PLACE GETTING UP, ROTATION FIXED
// ALL FIXED
// This script must be attached to animated enemy character

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class myRagdoll : MonoBehaviour
{

    public Transform animatedRig;
    public Transform[] animatedRigTransformsChildren;
    public Transform ragdollRig;
    public Transform[] ragdollChildrenTransforms;
    [Space(10)]
    public Rigidbody rb;
    public Animator anim;
    public Transform faceDirectionHelper;
    [Space(20)]
    public Transform animatedPelvis;
    public Transform ragdollPelvis;
    public Transform ragdolledCharacter;
    //public Transform ragdollPelvis;
    [Space(10)]
    public bool ragdollActive;
    public bool ragdollToAnimatedInProcess = false;
    public float blendCounter = 0f;
    public float blendingSpeed = 5f;
    public GameObject ragdollObject;
    public GameObject meshAnimatedPart1;
    public GameObject meshAnimatedPart2;

    public List<Vector3> tempPositions = new List<Vector3>();
    public List<Quaternion> tempRots = new List<Quaternion>();

    public List<Collider> cols = new List<Collider>();
    public List<Rigidbody> rbs = new List<Rigidbody>();
    [Space(10)]
    public GameObject mainParentCharacter;
    public GameObject animatedRpgCharacter;     // The character on which Animator is there
    public Vector3 tempPosition;
    public NavMeshAgent zombieNMAgent;
    public Collider hitTrigger;
    [Space(10)]
    public Rigidbody hitBodyPartRigidbody;
    public Transform animatedEquivalentOfHitBodyPart;       // cuz the arrow stuck in the rigidbody will be disabled 
    public float forceAmount;
    public float penetrationDepth;
    [Space(10)]
    public Transform characterBodyTransform;
    public Transform fovDirectionHelper;
    public GameObject fovMeshObject;
    public GameObject referenceToFakeArrow;
    [Space(10)]
    public float getUpTime;
    public zombieHealth theZombieHealthScript;

    // Start is called before the first frame update
    void Start()
    {
        theZombieHealthScript = GetComponent<zombieHealth>();
        animatedRigTransformsChildren = animatedRig.GetComponentsInChildren<Transform>();
        ragdollChildrenTransforms = ragdollRig.GetComponentsInChildren<Transform>();
        ragdollObject.SetActive(false);
    }



    void enableRagdoll()
    {
        this.GetComponentInParent<aimAssistEnemy>().enabled = false;
        hitTrigger.enabled = false;     // to not allow bullet to hit after the zombie has fallen to the ground.

        fovMeshObject.SetActive(false);
        ragdollActive = true;

        if (zombieNMAgent.enabled == true)
        {
            zombieNMAgent.isStopped = true;

        }
        Debug.Log("enable Ragdoll function called");

        // disable the mesh which is being animated;
        // copy the animated tranforms to ragdoll transforms to give initial postion to ragdoll

        ragdolledCharacter.forward = transform.forward;
        anim.enabled = true;
        anim.applyRootMotion = false;

        meshAnimatedPart1.GetComponent<SkinnedMeshRenderer>().enabled = false;
        meshAnimatedPart2.GetComponent<SkinnedMeshRenderer>().enabled = false;

        for (int i = 0; i < ragdollChildrenTransforms.Length; i++)      // to set the initial position of ragdoll to the animation which was playing
        {
            ragdollChildrenTransforms[i].position = animatedRigTransformsChildren[i].position;
            ragdollChildrenTransforms[i].rotation = animatedRigTransformsChildren[i].rotation;
        }

        cols.Clear();
        foreach (Collider col in ragdollRig.GetComponentsInChildren<Collider>())
        {
            cols.Add(col);
            col.enabled = true;
            //Debug.Log(col.name + " true");
        }

        rbs.Clear();
        foreach (Rigidbody rb in ragdollRig.GetComponentsInChildren<Rigidbody>())
        {
            rbs.Add(rb);
            rb.isKinematic = false;
        }



        rb.isKinematic = false;
        ragdollObject.SetActive(true);

        StartCoroutine("getUpWithDelay", getUpTime);
    }


    IEnumerator getUpWithDelay(float t)
    {
        Debug.Log(t);
        yield return new WaitForSeconds(t);

        if (theZombieHealthScript.currentHealth > 0)
        {
            disableRagdoll();

        }
    }

    void disableRagdoll()
    {
        this.GetComponentInParent<aimAssistEnemy>().enabled = true;

        hitTrigger.enabled = true;

        Destroy(referenceToFakeArrow);
        //tempPosition = new Vector3( ragdollPelvis.position.x, mainParentCharacter.transform.position.y, ragdollPelvis.position.z) ;   // changed on 16 sept, cuz Fov position on Y was getting inside ground
        tempPosition = ragdollPelvis.position;

        Vector3 temp2 = new Vector3(ragdollPelvis.position.x, mainParentCharacter.transform.position.y, ragdollPelvis.position.z);

        mainParentCharacter.transform.position = temp2;
        //animatedRpgCharacter.transform.position = temp2;
        ragdollPelvis.position = tempPosition;
        ragdollActive = false;
        anim.enabled = true;
        Vector3 afterGettingUpForwardVector;

        bool faceUp = Vector3.Dot(faceDirectionHelper.up, Vector3.up) > 0f;
        if (faceUp)
        {
            afterGettingUpForwardVector= transform.forward = -1 * Vector3.ProjectOnPlane(-ragdollPelvis.right, Vector3.up).normalized; // Makes animated forward in the direction of ragdoll
            anim.SetTrigger("StandUpFaceUp");
        }
        else
        {
            afterGettingUpForwardVector = transform.forward = Vector3.ProjectOnPlane(-ragdollPelvis.right, Vector3.up).normalized;    // this rotates the animated character in the direction of ragdoll fall
            anim.SetTrigger("StandUpFaceDown");
        }
       
        rb.isKinematic = true;

        tempPositions.Clear();
        tempRots.Clear();
        // This solves the jittering effect which we were getiing while lerping. 
        for (int i = 0; i < ragdollChildrenTransforms.Length; i++)
        {
            tempPositions.Add(ragdollChildrenTransforms[i].position);
            tempRots.Add(ragdollChildrenTransforms[i].rotation);
        }


        foreach (Collider col in ragdollRig.GetComponentsInChildren<Collider>())
        {
            col.enabled = true;
           // Debug.Log(col.name + " disabled");
        }

        foreach (Rigidbody rb in ragdollRig.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }


        ragdollToAnimatedInProcess = true;


        
        // The Character appear to have rotated after getting up is because of navemesh is forcing the character to go in a particular direction

        //Vector3 afterGettingUpForwardVector = transform.forward;
        Debug.Log("Parent forward before : " + mainParentCharacter.transform.forward);
        mainParentCharacter.transform.forward = afterGettingUpForwardVector;
        Debug.Log("Parent forward AFTER : " + mainParentCharacter.transform.forward);
        transform.forward = afterGettingUpForwardVector;


        fovDirectionHelper.right = afterGettingUpForwardVector;
        fovMeshObject.SetActive(true);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ragdollActive == true)
            {
                disableRagdoll();
            }
            else
            {
                enableRagdoll();
            }
        }

        Debug.DrawRay(transform.position, transform.forward*100, Color.red);
        //Debug.DrawRay(transform.position, mainParentCharacter.transform.forward*100, Color.cyan);


    }

    void LateUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(ragdollPelvis.up, Vector3.up) * 20f);

        animatedPelvis.position = new Vector3(ragdollPelvis.position.x, animatedPelvis.position.y, ragdollPelvis.position.z);
        // Rootmotion works when above is commented

        if (ragdollToAnimatedInProcess == true)
        {
            if (blendCounter <= 1)
            {
                for (int i = 0; i < ragdollChildrenTransforms.Length; i++)
                {

                    ragdollChildrenTransforms[i].position = Vector3.Lerp(tempPositions[i], animatedRigTransformsChildren[i].position, blendCounter);
                    ragdollChildrenTransforms[i].rotation = Quaternion.Lerp(tempRots[i], animatedRigTransformsChildren[i].rotation, blendCounter);
                }

                blendCounter += Time.deltaTime * blendingSpeed;

            }
            else
            {
                Debug.Log("ragdoll is made to look forward in the same direction as the parent");

                if (zombieNMAgent.enabled == true)
                {
                    zombieNMAgent.isStopped = false;
                }
                //Debug.Log("blending Complted");
                ragdollToAnimatedInProcess = false;
                meshAnimatedPart1.GetComponent<SkinnedMeshRenderer>().enabled = true;
                meshAnimatedPart2.GetComponent<SkinnedMeshRenderer>().enabled = true;
                
                ragdollObject.SetActive(false);
                //anim.applyRootMotion = true;

                blendCounter = 0;
            }

        }

       
        animatedPelvis.position = new Vector3(ragdollPelvis.position.x, animatedPelvis.position.y, ragdollPelvis.position.z);

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="bullet")
        {
            blendCounter = 0;
            ragdollToAnimatedInProcess = false;
            //the above two lines are for whne the ragdoll is getting up by blending and then arrow comes in the ass. So, stop blending in the LateUpdate and make it 
            // ragdoll

            enableRagdoll();

            col.attachedRigidbody.isKinematic = true;
            col.transform.position = hitBodyPartRigidbody.gameObject.transform.position;           

            Vector3 arrowDir = col.transform.up;
            col.transform.position = col.transform.position + penetrationDepth * arrowDir;
            col.transform.parent = hitBodyPartRigidbody.transform;

            referenceToFakeArrow = col.gameObject;
            hitBodyPartRigidbody.AddForce(-1*col.transform.up*forceAmount);
            ragdollActive = true;
            //Destroy(col.gameObject);        // destroy the main arow bullet as it is subsituted with the fake model arrow
        }
    }
}



/// <summary>
/// 
//  Approach the problems in this manner so that you get better Idea of what's going and what are th edifferent ways you can solve that problem
//
// Given: The animations are still going on but the mesh isn't visible.
// Problem :
// Solution 1 :
// Solution 2 :