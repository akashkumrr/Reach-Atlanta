using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollForceApply : MonoBehaviour
{
    public HandleRagdoll ragdollHandler;
    public Rigidbody hitPodyPart;
    public List<Rigidbody> listOfRb = new List<Rigidbody>();
    public RaycastHit hitInfo;
    public float forceMagnitude=20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
         
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100000f))
            {
                hitPodyPart = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                Debug.Log(hitInfo.collider.gameObject.name);
                if (hitPodyPart != null)
                {
                    listOfRb.Add(hitPodyPart);
                    AddForce();
                }
            }
        }*/

    
    }
    void AddForce()
    {
        ragdollHandler.EnableRagdoll(true, ragdollHandler.rigidbodies);
        Vector3 force = (hitInfo.point -Camera.main.transform.position).normalized *forceMagnitude;
        hitPodyPart.AddForce(force, ForceMode.Impulse);
        Debug.DrawRay(transform.position, force*100f, Color.blue);

        listOfRb.Clear();
    }

}
