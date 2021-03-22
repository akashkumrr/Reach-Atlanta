using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float totalHealth=100;
    public float currentHealth;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth<=0)
        {
            //Debug.Log("player Died. GAME OVER");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.gameObject.name);
        if(col.gameObject.tag=="zombieSpit")
        {
            currentHealth -= col.gameObject.GetComponent<spitBehaviour>().damageCausedBySpit;
            //Destroy(col.gameObject);
        }

    }

    public void decreaseHealth(int damageDone)
    {
        currentHealth -= damageDone;
    }
    
}
