using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItems
    {
        public InventoryItems(int id, int count)
        {
            itemId = id;
            itemCount = count;
        }

        public int itemId;
        public int itemCount;
    }


   
    //public InventoryItems[] inventory;
    public List<InventoryItems> inventory = new List<InventoryItems>();

    // Start is called before the first frame update
    void Start()
    {
        collectItems.OnLootingItem += UpdateInventory; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SearchForInventoryItemIndexById(int itemID)
    {
        int index = 0;
        foreach (var i in inventory)
        {
            if(i.itemId==itemID)
            {
                return index;
            }
            index += 1;
        }

        return -1;
    }

    public void UpdateInventory(int itemID, int qty)
    {

        if (SearchForInventoryItemIndexById(itemID) != -1)
        {
            inventory[SearchForInventoryItemIndexById(itemID)].itemCount += qty;    // if item not found add it to last of inventory;
        }
        else
        {
            inventory.Add(new InventoryItems(itemID, qty));
        }
            /*
        if (SearchForInventoryItemIndexById(itemID)!=-1)
        {
            inventory[SearchForInventoryItemIndexById(itemID)].itemCount += qty;    // if item not 
        }
        else
        {
            Debug.Log("New Item. Item of that ID doesnt exists in Inventory."); // think of how you want to handle that
            // one way is : At one place we have all the possible items with their IDs and then take item properties from their to another script which manages count n shit
            // other way is : whatever that item is, just add that item in the inventory whever you find an empty space;
        }
        */
    }
}
