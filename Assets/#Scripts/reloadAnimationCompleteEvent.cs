using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadAnimationCompleteEvent : MonoBehaviour
{
    public int weaponId; // To set the number of bullets right in the clip depending upon thw weapon
    public bool hasCompletedReloading;
    public bool isDoingReload;
    public weaponManager theWeaponMan;

    // Start is called before the first frame update
    void Start()
    {
        hasCompletedReloading = false;
        isDoingReload = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void reloadingComplete()
    {
        //Debug.Log("RELOAD COMPLETE");

        hasCompletedReloading = true;
        isDoingReload = false;

        theWeaponMan.weapons[weaponId].currentAmmoInClip = theWeaponMan.weapons[weaponId].totalAmmoAvailable >= theWeaponMan.weapons[weaponId].clipSize ? theWeaponMan.weapons[weaponId].clipSize : theWeaponMan.weapons[weaponId].totalAmmoAvailable;
        theWeaponMan.weapons[weaponId].totalAmmoAvailable -= theWeaponMan.weapons[weaponId].currentAmmoInClip;
        ///Debug.Log("Reload the weapon 1 SNIPER RIFLE");
        


    }

    void reloadingStarted()
    {
        //Debug.Log("RELOAD STARTED");
        hasCompletedReloading = false;
        isDoingReload = true;

    }
}
