using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItems : MonoBehaviour
{

    public bool nearLootableOject = false;
    public GameObject lootButton;
    public GameObject lootContainerObject;
    public lootable theLootableItemScript;

    public delegate void lootedItem(int id, int qty);
    public static event lootedItem OnLootingItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="lootable")
        {
            theLootableItemScript = col.gameObject.GetComponent<lootable>();
            lootContainerObject = col.gameObject;

            //Debug.Log("lootable object in range");
            if (theLootableItemScript.hasBeenLooted == false)
            {
                lootButton.SetActive(true);
            }
            nearLootableOject = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag=="lootable")
        {
            //Debug.Log("outside lootable range");

            lootButton.SetActive(false);

            lootContainerObject = null;
            nearLootableOject = false;
        }
    }

    public void LootButtonPressd_LootDatItem()
    {
       

        if(theLootableItemScript.hasBeenLooted ==false)
        {

            OnLootingItem(theLootableItemScript.lootItemId, theLootableItemScript.lootQuantity);
            theLootableItemScript.lootDisplayGameobject.SetActive(false);
            theLootableItemScript.hasBeenLooted = true;
            lootButton.SetActive(false);
        }


    }
}
