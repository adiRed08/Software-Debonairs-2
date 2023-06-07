using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 50;
    public InventorySave inventorySave;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] possibleItems;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Button equipButton;

    //adds item to an empty slot in the inventory

    void Start()
    {
        inventorySave.LoadInventory(inventorySlots,possibleItems);
        addItem(possibleItems[0]);
        addItem(possibleItems[1]);
        addItem(possibleItems[2]);
        addItem(possibleItems[3]);
        addItem(possibleItems[4]);
        addItem(possibleItems[5]);
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

        EventTrigger eventTrigger = newItemGo.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => OnItemClicked(inventoryItem));
        eventTrigger.triggers.Add(entry);
    } 

    public void saveInventory(){
        inventorySave.SaveInventory(inventorySlots);
    }

    void OnItemClicked(InventoryItem clickedItem)
    {
    if(clickedItem.item.stackable == false)
    {
        equipButton.gameObject.SetActive(true);
    }
    else{
        equipButton.gameObject.SetActive(false);
    }

    equipButton.onClick.RemoveAllListeners();
    equipButton.onClick.AddListener(() =>
    {
        EquipItem(clickedItem.item);
    });
    itemNameText.text = clickedItem.item.name;
    itemDescriptionText.text = clickedItem.item.desc;
    
    }
    void EquipItem(Item item)
    {
    if(item.isEquipped == false)
    {
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
        item.isEquipped = true;
    }
    else
    {
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        item.isEquipped = false;
    }
    

    }
}

