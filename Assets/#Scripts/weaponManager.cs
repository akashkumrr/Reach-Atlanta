using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    //public mouseClick joyReference;     //    *** now joystick is in testRotationAndVectors scrits;
    public testRotationAndVectors joyReference;
    public touchingAreasTesting touchAreaScript;  // to get the legal touch area to shoot

    [Space(20)]
    public Transform parentPlayerTransform; // This is used for determining the direction in which the bullets will go after shooting
    public bool leftFingerDown=false;
    public bool leftFingerUp=false;
    public int activeWeapon;
    public bool isDragged;

    // DELETE THE RECOIL AND RELOAD ANIMATION AS IT WOULD BE OVERKILL
    // hi there

    [System.Serializable]
    public class weaponData
    {
        public int wID;            // Every weapon has an unique wID, right now we aren't using it. INDEX is used instead
        public int shootType;           // This shows whether the Gun is release to shoot or HOLD to shoot
        public GameObject weaponObject; // This references the Gun gameobject
        public Transform muzzlePosition;// This is the Transform from where bullets will come out
        public GameObject bulletObject; // The bullet object that the gun will fire
        public float fireRate;          // Bullets per second
       
        public float damage;            // the bullet on collision with something will cause this much amount of damage
        public int clipSize;
        public int currentAmmoInClip;
        public int ammoCategory=0;
        public int totalAmmoAvailable;
        public float reloadTimeOfWeapon;
        public float countReloadTime=0;
        public bool isReloading=false;
        public float adsTime = 0.5f;        // this is for the weapon not start shooting immediately as animation is in transition and some bullets go inside ground
        public float countAdsTime =0;

        // Additional Parameters to be added
        // RECOIL
        // BulletFORCE
        // Damage ? does damage should be associated with bullet or gun?
        // RANGE
    }

    [System.Serializable]
    public class AmmoData
    {
        public int ammoType;        // 0 = small ammo : smgs,pistols  and if type is 1 = big ammo : ARs, shotguns
        public int ammoAvailable;
    }

    public AmmoData[] ammoGlobal;

    public weaponData[] weapons; // weapon, bullet, muzzle position
    public float bulletForce;
    public float lastBulletFiredTime=0;

    void Start()
    {

        ammoGlobal[0].ammoType = 0;
        ammoGlobal[0].ammoAvailable = 400;

        ammoGlobal[1].ammoType = 1;
        ammoGlobal[1].ammoAvailable = 400;

        weapons[0].bulletObject.GetComponent<bulletBehaviour>().bulletDamage = weapons[0].damage;
        weapons[1].bulletObject.GetComponent<bulletBehaviour>().bulletDamage = weapons[1].damage;
        //weapons[2].bulletObject.GetComponent<bulletBehaviour>().bulletDamage = weapons[2].damage;

        weapons[activeWeapon].weaponObject.GetComponent<LineRenderer>().enabled = false;



    }

    void Update()
    {


        if (weapons[activeWeapon].isReloading == true)
        {
            weapons[activeWeapon].countReloadTime += Time.deltaTime;
        }

        if (weapons[activeWeapon].countReloadTime >= weapons[activeWeapon].reloadTimeOfWeapon)
        { 
            weapons[activeWeapon].totalAmmoAvailable = ammoGlobal[weapons[activeWeapon].ammoCategory].ammoAvailable;
            weapons[activeWeapon].countReloadTime = 0;
            weapons[activeWeapon].currentAmmoInClip = ammoGlobal[weapons[activeWeapon].ammoCategory].ammoAvailable >= weapons[activeWeapon].clipSize ? weapons[activeWeapon].clipSize : ammoGlobal[weapons[activeWeapon].ammoCategory].ammoAvailable; 
            ammoGlobal[weapons[activeWeapon].ammoCategory].ammoAvailable -= weapons[activeWeapon].currentAmmoInClip;
            weapons[activeWeapon].isReloading = false;

        }
        /*
        /// if the mouse is released and joystick is dragged to reasonable distance in a direction 
        /// this Will NOT work as when we release mouse button the joystick is reseted, so joystick distance becomes zero 
        /// immediately when we release the MouseButton. So, these two conditions can't be TRUE simultaneously
        if ( Input.GetMouseButtonUp(0) && joyReference.distanceFromCenterJoyStick > 0.1)
            Debug.Log(joyReference.distanceFromCenterJoyStick + " plus ");

        */



        if(leftFingerUp /*&& touchAreaScript.insideTouchArea==true*//*&&isDragged==true*/&& weapons[activeWeapon].shootType==1)
        {
            weapons[activeWeapon].weaponObject.GetComponent<LineRenderer>().enabled = false;

            weapons[activeWeapon].weaponObject.SetActive(true);
            shoot(activeWeapon);
        }
        /* end release to shoot weapons */

        // NOTE TO SELF : LeftFingerUp is not the opposite of LeftFinderDown. LeftFinger is said to be up when you dragged it to some distance then release it

        if(leftFingerDown)  // means you're Aiming 
        {
            weapons[activeWeapon].weaponObject.GetComponent<LineRenderer>().enabled = true;

        }else
        {
            weapons[activeWeapon].weaponObject.GetComponent<LineRenderer>().enabled = false;

        }

        /* for HOLD to shoot weapons like ARs SMGs */
        if (leftFingerDown/* && touchAreaScript.insideTouchArea == true *//*&& joyReference.distanceFromCenterJoyStick > 0.1*/ && weapons[activeWeapon].shootType == 0)
        {
            if (weapons[activeWeapon].countAdsTime >= weapons[activeWeapon].adsTime)
            {
                shoot(activeWeapon);
            }
            else
            {
                weapons[activeWeapon].countAdsTime += Time.deltaTime;
            }
        }


        if(leftFingerUp && weapons[activeWeapon].shootType==0)
        {

            weapons[activeWeapon].countAdsTime = 0;
        }
        /* end HOLD to shoot weapons*/

        /* This is keep track when the last bullet was fired so to control Fire rate of weapon */
        lastBulletFiredTime += Time.deltaTime; 
    }

    public void shoot(int wID)
    {
        switch (wID)
        {
            case 0:
                if (lastBulletFiredTime >= 1 / weapons[wID].fireRate)
                {
                    // If the weapon has ammo in magazine then fire. 
                    if (weapons[activeWeapon].currentAmmoInClip != 0)
                    {
                        // Instantiate the bullet of the weapon type, at the appropriate muzzle position of that weapon
                        GameObject bullet = Instantiate(weapons[wID].bulletObject, weapons[wID].muzzlePosition.transform.position, Quaternion.identity);
                        bullet.transform.up = weapons[wID].muzzlePosition.right;
                        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                        bulletRb.AddForce(parentPlayerTransform.right * bulletForce);
                       
                        //Debug.Log("weapon 1 fired. SNIPER RIFLE");
                        lastBulletFiredTime = 0;
                        weapons[activeWeapon].currentAmmoInClip -= 1;
                    }
                    else if (weapons[activeWeapon].totalAmmoAvailable != 0)
                    {
                        if(weapons[activeWeapon].isReloading==false)
                        {
                            weapons[activeWeapon].isReloading = true;                            
                        }                    

                        /*
                        if (weapons[activeWeapon].reloadEvent.isDoingReload == false)
                        {   // Reload the gun. Play animaion of reloading. Actual reload takes place in animation event there
                            // so now that we have decided to remove Reload animation. Make a timer counter for reload and set some booleans
                            Debug.Log("Animation requested to be played");
                           
                        }
                        */
                    }
                    else
                    {
                        //Debug.Log("the weapon is empty");
                    }
                }
                
                break;

            case 1:
                if (lastBulletFiredTime >= 1 / weapons[wID].fireRate)
                {
                    if (weapons[activeWeapon].currentAmmoInClip != 0)
                    {
                        GameObject bullet2 = Instantiate(weapons[wID].bulletObject, weapons[wID].muzzlePosition.transform.position, Quaternion.identity);
                        bullet2.transform.up = -1*weapons[wID].muzzlePosition.right; // this is for shooting arrow with it's tip in forward direction
                        //bullet2.transform.parent = weapons[wID].muzzlePosition.transform;
                        //bullet2.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                        Rigidbody bulletRb2 = bullet2.GetComponent<Rigidbody>();
                        bulletRb2.AddForce(parentPlayerTransform.right * bulletForce);
                        //bullet2.transform.parent = null;

                        //Debug.Log("weapon 2 fired. BOW");
                        lastBulletFiredTime = 0;
                        weapons[activeWeapon].currentAmmoInClip -= 1;
                    }
                    else if (weapons[activeWeapon].totalAmmoAvailable != 0)
                    {
                        if (weapons[activeWeapon].isReloading == false)
                        {
                            weapons[activeWeapon].isReloading = true;
                        }
                    }
                    else
                    {
                        //Debug.Log("the weapon is empty");
                    }
                }
                break;
            case 2:

                if (lastBulletFiredTime >= 1 / weapons[wID].fireRate)
                {
                    if (weapons[activeWeapon].currentAmmoInClip != 0)
                    {

                        //Debug.Log(weapons[wID].muzzlePosition.right + " see below the converted x and y component");
                        // X component of 30 degree clockwise of front vector
                        //Debug.Log(weapons[wID].muzzlePosition.right.x * Mathf.Cos(30 * Mathf.Deg2Rad) + weapons[wID].muzzlePosition.right.y * Mathf.Sin(30 * Mathf.Deg2Rad));
                        // Y component of 30 degree clockwise of front vector
                        //Debug.Log(-weapons[wID].muzzlePosition.right.x * Mathf.Sin(30 * Mathf.Deg2Rad) + weapons[wID].muzzlePosition.right.y * Mathf.Cos(30 * Mathf.Deg2Rad));
                        // wait a min. You can set these thing in start function. No need to do it every time here
                        //Debug.Log(Quaternion.AngleAxis(30, weapons[wID].muzzlePosition.right) * weapons[wID].muzzlePosition.right);
                        GameObject bullet3 = Instantiate(weapons[wID].bulletObject, weapons[wID].muzzlePosition.transform.position, Quaternion.identity);
                        Rigidbody bulletRb3 = bullet3.GetComponent<Rigidbody>();
                        bulletRb3.AddForce(parentPlayerTransform.right * bulletForce);

                        GameObject bullet31 = Instantiate(weapons[wID].bulletObject, weapons[wID].muzzlePosition.transform.position, Quaternion.identity);
                        Rigidbody bulletRb31 = bullet31.GetComponent<Rigidbody>();
                        bulletRb31.AddForce(Quaternion.AngleAxis(5, Vector3.up) * parentPlayerTransform.right * bulletForce);

                        GameObject bullet32 = Instantiate(weapons[wID].bulletObject, weapons[wID].muzzlePosition.transform.position, Quaternion.identity);
                        Rigidbody bulletRb32 = bullet32.GetComponent<Rigidbody>();
                        bulletRb32.AddForce(Quaternion.AngleAxis(355, Vector3.up) * parentPlayerTransform.right * bulletForce);



                        //Debug.Log("weapon 3 fired. SHOTGUN");
                        lastBulletFiredTime = 0; weapons[activeWeapon].currentAmmoInClip -= 1;
                    }
                    else if (weapons[activeWeapon].totalAmmoAvailable != 0)
                    {
                        if (weapons[activeWeapon].isReloading == false)
                        {
                            weapons[activeWeapon].isReloading = true;
                        }
                    }
                    else
                    {
                        //Debug.Log("the weapon is empty");
                    }
                }
                break;
                

        }
    }

    public void cycleThroughWeapons()
    {
        // * NOTE : You need to take caare while switching FROM a weapon which is reloading. 

        /*
        if(weapons[activeWeapon].reloadEvent.isDoingReload==true)
        {
            weapons[activeWeapon].reloadEvent.isDoingReload = false;
            weapons[activeWeapon].reloadEvent.hasCompletedReloading = true; // it'll give an impression that it has completed
            // reloading but in reality the weapon hasn't( beacause in animation event actual reload takes place) so it would have to reload again
        }
        */

        weapons[activeWeapon].countReloadTime = 0;

        weapons[activeWeapon].weaponObject.SetActive(false); // set the current weapons to inactive
        activeWeapon=activeWeapon+1;
        
       if(activeWeapon>weapons.Length-1)
        {
            activeWeapon = 0;
        }
        
        weapons[activeWeapon].weaponObject.SetActive(true);
        weapons[activeWeapon].weaponObject.GetComponent<LineRenderer>().enabled = false;

        //Debug.Log("weapon switched");
    }

    public GameObject getWeaponObjFromID(int id)
    {
        Debug.Log("get weapon from id called WMAN");
        foreach (var w in weapons)
        {
            if (w.wID == id)
            {
                GameObject g = w.weaponObject;
                g.SetActive(false);
                return g;
            }
        }

        Debug.Log("No weapon exists with that ID");
        return null;
    }

    public void setIndexOfWeaponActiveFromWeaponId(int id)
    {
        int i = 0;
        foreach (var w in weapons)
        {
            
            if (w.wID == id)
            {
                activeWeapon = i;
                return ; 
            }
            i = i + 1;
        }

    }
}
