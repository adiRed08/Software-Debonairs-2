using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New object", menuName = "Item")]
public class Item : ScriptableObject 
{
   public int id;
   public TileBase tile;
   public Sprite image;
   public Type type;
   public bool stackable = true;
   public string desc;
   public bool isEquipped;
}

public enum Type 
{
   Consumable,
   Equippable
}
