using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwanEnemies : MonoBehaviour
{

    public float timer = 0;
    public int spwanCounter=0;
    [System.Serializable]
    public class SpwanData
    {
        public float spwaningTime;
        public Transform spwanPosition;
        public GameObject enemy;

    }

    public SpwanData[] spwanD; 

    void Start()
    {
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer<12)
        {
            // Timer was increment inside WHILE  which was inside update which is a PROBLEM
            //Debug.Log(timer + " space " + spwanD[spwanCounter].spwaningTime);
            if(spwanCounter<spwanD.Length&&timer>spwanD[spwanCounter].spwaningTime)
            {
                //Debug.Log(timer + " space " + spwanD[spwanCounter].spwaningTime);
                GameObject z= Instantiate(spwanD[spwanCounter].enemy, spwanD[spwanCounter].spwanPosition.position, Quaternion.identity);
                z.SetActive(true);
                spwanCounter++;
            }

          
        }
        
    }



}
