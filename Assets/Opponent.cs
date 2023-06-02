using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu]
public class Opponent : ScriptableObject 
{
    public Sprite image;
    public string name;
    public string[] statements;
    public string endRemark;
}
