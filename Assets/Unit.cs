using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Image image;
    public string unitName;

    public int damage;
    public float critChance;
    public float evasion;
    public float accuracy;
    public int defense;

    public int maxHP;
    public int currentHP;
}
