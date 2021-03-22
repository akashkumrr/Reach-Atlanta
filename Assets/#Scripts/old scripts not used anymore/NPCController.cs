 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
using UnityEngine.AI;
 public class NPCController : MonoBehaviour {
     public float timeToChangeDirection;
    public float distanceToTravel;
    public float t;
    public NavMeshAgent myZombAgent;
    public Rigidbody rb;
     public void Start () {

        rb=this.gameObject.GetComponent<Rigidbody>();
        myZombAgent = this.gameObject.GetComponent<NavMeshAgent>();
         ChangeDirection();
     }
     
     // Update is called once per frame
     public void Update () {
         t -= Time.deltaTime;
 
         if (t <= 0) {
             ChangeDirection();
         }
 
         
     }
 
 
 
     private void ChangeDirection() {
         float angle = Random.Range(0f, 360f);
         Quaternion quat = Quaternion.AngleAxis(angle, Vector3.up);
        //Debug.Log("dir changed");
         Vector3 newUp = quat * Vector3.forward*distanceToTravel ;
        //Debug.DrawRay(transform.position, newUp);
        myZombAgent.SetDestination(newUp);

         t=timeToChangeDirection;
     }
 }