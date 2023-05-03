using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static SaveFile;

public class InventorySave: MonoBehaviour 
{
  public GAMEMYDATA data;
  public Save save;
  public InventoryManager inventoryManager;
  public GameObject inventoryItemPrefab;
  private void Awake()
  {
    GameObject myGameObject = GameObject.Find("GAMEMYDATA");
    data = (GAMEMYDATA)myGameObject.GetComponent(typeof(GAMEMYDATA));
    this.save = this.data.mySave;
    Debug.Log("Bruh");
  }

  public void SaveInventory(InventorySlot[] slots)
  {
    for (int i = 0; i < this.save.items.Length; i++)
    {
      try
      {
        int currID = slots[i].GetComponentInChildren<InventoryItem>().item.id;
        int currCount = slots[i].GetComponentInChildren<InventoryItem>().count;
        this.save.items[i] = currID.ToString() + '_' + currCount.ToString();
      }
      catch
      {

      }
    }
    string json = JsonUtility.ToJson(this.save, true);
    File.WriteAllText(Application.dataPath + "/Saves/" + save.fileName + ".json",json);
    // Debug.Log(save.fileName);
  }

  

  public void LoadInventory(InventorySlot[] slots, Item[] items)
  {
    for (int i = 0; i < this.save.items.Length; i++)
    {
      if (this.save.items[i] != "-1_0")
      {
        // string json = File.ReadAllText(Application.dataPath + "/Saves/" + save.fileName + ".json");
        // Save data = JsonUtility.FromJson<Save>(json);
        string[] itemData = this.save.items[i].Split('_');
        Save data = SaveFile.loadSave(save.fileName);
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slots[i].transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.count = int.Parse(itemData[itemData.Length-1]);
        inventoryItem.InitialiseItem(items[int.Parse(itemData[0])]);
      }
    }
  }
}
