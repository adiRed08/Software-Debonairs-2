using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 50;
    public InventorySave inventorySave;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] possibleItems;

    //adds item to an empty slot in the inventory

    void Start()
    {
        inventorySave.LoadInventory(inventorySlots,possibleItems);
    }
    
    public bool addItem(Item item)
    {
        // check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        // find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null){
                spawnNewItem(item, slot);
                // Debug.Log(i);
                return true;
            }
        }

        return false;
    }

    void spawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    } 

    public void saveInventory(){
        inventorySave.SaveInventory(inventorySlots);
    }
}
