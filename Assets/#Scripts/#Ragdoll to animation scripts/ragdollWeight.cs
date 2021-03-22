using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollWeight : MonoBehaviour
{

    public Rigidbody rb;
    public Animator anim;
    public Transform[] skinnedRigTransforms;
    public Transform skinnedRig;
    public Transform[] ragdollTransforms;
    public Transform ragdoll;
    public Transform[] animatedRigTransforms;
    public Transform animatedRig;
    [Space(10)]
    public bool ragdollOn;
    public float blendFactor;
    public float blendSpeed;
    [Space(10)]
    public Transform faceDirectionHelper;
    public Transform animatedPelvis;
    public Transform ragdollPelvis;

    public bool setAnimatedRigPositionToRagdollPosition;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        skinnedRigTransforms =skinnedRig.GetComponentsInChildren<Transform>();
        ragdollTransforms =ragdoll.GetComponentsInChildren<Transform>();
        animatedRigTransforms =animatedRig.GetComponentsInChildren<Transform>();
        ragdoll.gameObject.SetActive(false);
    }


    void EnableRagdoll()
    {
        Debug.Log("Enable ragdoll");
        if (blendFactor > 0.5f)
        {
            return;
        }
        for (int i = 0; i < ragdollTransforms.Length; i++)      // to set the initial position of ragdoll to the animation which was playing
        {
            ragdollTransforms[i].localPosition =animatedRigTransforms[i].localPosition;
            ragdollTransforms[i].localRotation =animatedRigTransforms[i].localRotation;
        }
        rb.isKinematic = true;
        ragdoll.gameObject.SetActive(true);
        ragdollOn = true;
        blendFactor = 1f;               // In LateUpdate() , depending upon blend factor the skinned rig  = animated rig or ragdoll rig.

    }

    void DisableRagdoll()
    {
        bool faceUp = Vector3.Dot(faceDirectionHelper.forward, Vector3.up) > 0f;
        if (faceUp)
        {
            anim.SetTrigger("StandUpFaceUp");
        }
        else
        {
            anim.SetTrigger("StandUpFaceDown");
        }
        StartCoroutine("BlendFromRagdoll");
    }

    IEnumerator BlendFromRagdoll()
    {
        while (blendFactor > 0f)
        {
            blendFactor -= Time.deltaTime * blendSpeed;
            yield return null;
        }
        blendFactor = 0f;
        rb.isKinematic = false;
        blendFactor = 0f;
        ragdollOn = false;
        ragdoll.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(ragdollOn==true)
            {
                DisableRagdoll();
                //setAnimatedRigPositionToRagdollPosition = false;

            }
            else
            {
                setAnimatedRigPositionToRagdollPosition = true;
                EnableRagdoll();
            }
        }
    }

    void LateUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(ragdollPelvis.up, Vector3.up) * 20f);

        if (setAnimatedRigPositionToRagdollPosition == true)
        {
            animatedPelvis.position = new Vector3 (ragdollPelvis.position.x, animatedPelvis.position.y,ragdollPelvis.position.z);
            //ragdoll.up =  -1*Vector3.ProjectOnPlane(ragdollPelvis.up, Vector3.up);
            // now you have to fix the rotation of animated rig as, it always faces a particular direction
            //animatedPelvis.rotation = Quaternion.Euler(animatedPelvis.rotation.x, ragdollPelvis.rotation.y, animatedPelvis.rotation.z);
            //Debug.Log("animated rig should be changed to ragdoll position");
            //setAnimatedRigPositionToRagdollPosition = false;
        }
        for (int i = 0; i < skinnedRigTransforms.Length; i++)
        {
          
                skinnedRigTransforms[i].localPosition = Vector3.Lerp(animatedRigTransforms[i].localPosition, ragdollTransforms[i].localPosition, blendFactor);
           
            //skinnedRigTransforms[i].localPosition =Vector3.Lerp(animatedRigTransforms[i].localPosition,ragdollTransforms[i].localPosition,blendFactor);
            skinnedRigTransforms[i].localRotation =Quaternion.Lerp(animatedRigTransforms[i].localRotation,ragdollTransforms[i].localRotation,blendFactor);
        }
        

    }
}
