using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpponentInfo : MonoBehaviour
{
    public Opponent opponent;
    public Image image;
    //:)

    void Start()
    {
        this.image.sprite = opponent.image;
    }
}
