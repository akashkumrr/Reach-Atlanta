using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipWeapon : MonoBehaviour
{

    public  bool euippedButtonPressed = false;
    public int weaponIdOfWeaponToBeEquipped;    
    public int bagSlotPocket = -1; // to keep track if they are already in other slot then don't equip in both slots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEquipIDWhenEquipButtonPressed(int eID)
    {
        euippedButtonPressed = true;
        weaponIdOfWeaponToBeEquipped = eID;
    }
}
