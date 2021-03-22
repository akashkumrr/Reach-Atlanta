using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allWeaponsScript : MonoBehaviour
{

    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public int weaponID;
        public GameObject weaponObject;
        public float fireRate;
        public float Range;
        public float magzineSize;
        public float damage;
        public float accuracy;
        public float mobility;
        public float shootType; // 0 tap shoot and 1 is relase shoot and 2 can be unique 
        public int unlockedInGame = 0 ;

        [Space(10)]
        public Quaternion idleRotation;
        public Vector3 idlePosition;
        public Quaternion aimRotation;
        public Vector3 aimPosition;
        public Transform parentToTranform;
    }

   
    public  WeaponData[] allWeapons;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  GameObject getWeaponObjFromID(int id)
    {
        //Debug.Log("get weapon from id called");
        foreach (var w in allWeapons)
        {
            if(w.weaponID==id)
            {
                GameObject g = w.weaponObject;
                g.SetActive(false);
                return g;
            }
        }

        return null;
    }
}
