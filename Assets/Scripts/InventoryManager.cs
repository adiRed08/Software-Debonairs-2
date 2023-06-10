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
    public BattleDialogue battleDialogue;
    public HealthBarMechanics healthBarMechanics;
    //adds item to an empty slot in the inventory

    void Start()
    {
        inventorySave.LoadInventory(inventorySlots,possibleItems);
        addItem(possibleItems[0]);
        addItem(possibleItems[1]);
        addItem(possibleItems[2]);
        addItem(possibleItems[3]);
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
    if(clickedItem.item.stackable == false && battleDialogue.inBattle == false)
    {
        equipButton.gameObject.SetActive(true);
    }
    else if(clickedItem.item.stackable == true && battleDialogue.inBattle == true)
    {
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Consume";
        equipButton.gameObject.SetActive(true);
    }
    else
    {
        equipButton.gameObject.SetActive(false);
    }
    if (clickedItem.item.stackable){
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Consume";
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() =>
        {
            if(clickedItem.item.id == 2)
            {
                healthBarMechanics.heal(10);
            }
            else
            {
                healthBarMechanics.heal(5);
            }
            removeItem(clickedItem.item);
            itemDescriptionText.text = "(Consumed!)";
        });
        
    }
    else if(clickedItem.item.isEquipped == false && clickedItem.item.stackable == false){
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() =>
        {
            EquipItem(clickedItem.item);
        });
        }
    else if(clickedItem.item.isEquipped == true && clickedItem.item.stackable == false){
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() =>
        {
            unEquipItem(clickedItem.item);
        });
    }
    if(clickedItem.item.stackable == false && clickedItem.item.isEquipped)
    {
        itemNameText.text = clickedItem.item.name + "\n(Equipped)";
    }
    else 
    {
        itemNameText.text = clickedItem.item.name;
    }
    itemDescriptionText.text = clickedItem.item.desc;
    
    }
    void EquipItem(Item item)
    {
        Debug.Log("Item equipped: " + item);
        item.isEquipped = true;
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() =>
        {
            unEquipItem(item);
        });
    }
    void unEquipItem(Item item)
    {
        Debug.Log("Item unequipped: "+ item);
        item.isEquipped = false;
        // Change the text of the equip button to "Equip"
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        // Handle the equip button click event
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() =>
        {
            EquipItem(item);
        });
    }
    public void removeItem(Item item)
    {
        // Iterate over inventory slots to find the item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // Check if the item exists in the slot
            if (itemInSlot != null && itemInSlot.item == item)
            {
                // Decrease the count of the item
                itemInSlot.count--;

                // If count becomes zero, remove the item from the slot
                if (itemInSlot.count == 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    // Update the count display
                    itemInSlot.RefreshCount();
                }

                // Exit the loop after removing the item
                return;
            }
        }
    }
}



