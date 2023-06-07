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
    public void EquipItem(Item item)
    {
    // Perform the item equipping logic here
    // You can update the equipped status of the item and apply any relevant changes to your game
    item.isEquipped = true;

    // Hide or disable the equip button after equipping the item
    }
}
