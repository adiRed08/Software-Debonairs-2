using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventorySave inventorySave;
    public Item[] itemsToAdd;

    public void addToInventory(int id){
        inventorySave = inventoryManager.inventorySave;
        bool result = inventoryManager.addItem(itemsToAdd[id]);
        inventoryManager.saveInventory();
        if (result == true)
        {
            Debug.Log("Item Added!");
        } else 
        {
            Debug.Log("Item NOT Added.");
        }
    }
}
