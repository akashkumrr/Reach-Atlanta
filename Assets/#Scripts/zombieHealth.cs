using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHealth : MonoBehaviour
{

    public float maxZombieHealth=100;
    public float currentHealth = 100;

    public playerHealth thePlayerHealthScript;

    public delegate void zombieDied();
    public static event zombieDied onZombieDeath;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerHealthScript = FindObjectOfType<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            //Debug.Log("zombie died");
            //this.gameObject.SetActive(false);
        }
    }


    public void decreasePlayerCharacterHealth(int zombieDamagePoints)
    {
        thePlayerHealthScript.decreaseHealth(zombieDamagePoints);
    }

    void OnTriggerEnter(Collider col)
    {
          

        if (col.tag=="bullet")
            {

            Debug.Log("Bullet entered Decrease zombie health");

            col.gameObject.tag = "Untagged";

            currentHealth -= col.GetComponent<bulletBehaviour>().bulletDamage;

            if (currentHealth <= 0)
            {
                if (onZombieDeath != null)
                {
                    onZombieDeath();        // I think nobody is subscribed to this event. NO level manager is subsribed to this event to decrease zombie count
                    Debug.Log("zombie health is zero disable AIM ASSIST on that zombie");
                    this.GetComponentInParent<aimAssistEnemy>().enabled = false;

                    // get the component which implements the Interface of zombie death
                    this.GetComponentInParent<InterfaceZombieDeath>().IzombieDeath();
                }
            }
            }
        
    }


}
