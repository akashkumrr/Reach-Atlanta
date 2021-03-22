using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inBagWeapons : MonoBehaviour
{
    public int activeWeaponId;
    public equipWeapon theEquipWeaponScript;
    public weaponManager theWMan;

    [System.Serializable]
    public class WeaponBag
    {
        public GameObject weaponObj;
        public int BagWeaponId;
        public int lastSlotId;
    };

    public WeaponBag[] wBag;

    public int initialWeaponIDInFirstSlot=11;      // Default weapon in slot 1;
    public int initialWeaponIDInSecondSlot=22;     // Default weapon in slot 2;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var  w in wBag)
        {
            w.lastSlotId = -1;
            w.BagWeaponId = -69;
        }

        wBag[0].BagWeaponId = initialWeaponIDInFirstSlot;
        wBag[0].weaponObj = theWMan.getWeaponObjFromID(initialWeaponIDInFirstSlot);      
        wBag[0].weaponObj.SetActive(true);
        activeWeaponId = initialWeaponIDInFirstSlot;

        wBag[1].BagWeaponId = initialWeaponIDInSecondSlot;
        wBag[1].weaponObj = theWMan.getWeaponObjFromID(initialWeaponIDInSecondSlot);
       // ONLY set active the weapon in first slot as active initially
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void equipTheSelectedWeapon(int slotId)  // also implement that slot 1 can't be empty;
    {
        if(theEquipWeaponScript.euippedButtonPressed ==true)
        {
            int Wid = theEquipWeaponScript.weaponIdOfWeaponToBeEquipped;
            theEquipWeaponScript.euippedButtonPressed  = false;
            Debug.Log("Weapon equipped with id : " + Wid);

                     


            int otherSlot = slotId == 1 ? 0 : 1;

            if (wBag[slotId].weaponObj != null)     // clear previous weapon in that slot
            {
                wBag[slotId].weaponObj.SetActive(false);
            }

            if (wBag[otherSlot].weaponObj != null)     // set weapon active to false weapon in the other slot
            {
                wBag[otherSlot].weaponObj.SetActive(false);
            }

            Debug.Log(otherSlot +""+ slotId);

            if(Wid== wBag[otherSlot].BagWeaponId)
            {
                wBag[otherSlot].BagWeaponId = -69;
                wBag[otherSlot].weaponObj = null;
            }

            wBag[slotId].BagWeaponId = Wid;
            wBag[slotId].weaponObj = theWMan.getWeaponObjFromID(Wid);

           /* 
            if(slotId==0)
            {
                wBag[slotId].weaponObj.SetActive(true);     // only weapons In slot 1 will be active initially after eqiuping
                activeWeaponId = Wid;
            }
            */
            // or you could just set active active whatever last weapon was assigned in the slots
            // like so



            
            wBag[slotId].weaponObj.SetActive(true);

            activeWeaponId = Wid;
            theWMan.setIndexOfWeaponActiveFromWeaponId(activeWeaponId); // This sets the Weapon PROPERTIES of ACTIVE weapon by seeing it's weaponID

        }
    }

    public void cycleThroughWeaponsInBag()
    {
        theWMan.weapons[theWMan.activeWeapon].countReloadTime = 0;  // resets the reload if weapon switched

        if (wBag[0].weaponObj!=null && wBag[0].weaponObj.activeInHierarchy == true)
        {
          

            wBag[0].weaponObj.SetActive(false);
            if (wBag[1].weaponObj != null)
            {
                wBag[1].weaponObj.SetActive(true);
                activeWeaponId = wBag[1].BagWeaponId;
            }
            else
            {
                Debug.Log("No weapon is assigned to other slot");
            }
        }
        else if (wBag[1].weaponObj != null && wBag[1].weaponObj.activeInHierarchy == true)
        {
            wBag[1].weaponObj.SetActive(false);
            if (wBag[0].weaponObj != null)
            {
                wBag[0].weaponObj.SetActive(true);
                activeWeaponId = wBag[0].BagWeaponId;
            }
            else
            {
                Debug.Log("No weapon is assigned to other slot");
            }
        }

        theWMan.setIndexOfWeaponActiveFromWeaponId(activeWeaponId);
    }
}
