using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableWeaponEquipScreen : MonoBehaviour
{
    public GameObject equipWeaponScreen;

    // Start is called before the first frame update
    void Start()
    {
        equipWeaponScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="Player")
        {
            equipWeaponScreen.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            equipWeaponScreen.SetActive(false);
        }
    }
}
