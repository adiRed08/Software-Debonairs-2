using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpponentInfo : MonoBehaviour
{
    public Opponent opponent;
    public Image image;
    public Sprite female;
    private GAMEMYDATA saveHolder;
    //:)

    void Start()
    {
        GameObject _myGameObject = GameObject.Find("GAMEMYDATA");
        saveHolder = (GAMEMYDATA)_myGameObject.GetComponent(typeof(GAMEMYDATA));

        if (!saveHolder.mySave.male)
        {
            this.image.sprite = female;
            opponent.name = "Karen";
        }
        else
        {
            this.image.sprite = opponent.image;
        }

        //if (_genderMale)
        //{
        //    this.image.sprite = opponent.image;
        //}
        //else
        //{
        //    this.image.sprite = female;
        //    opponent.name = "Karen";
        //}
        Debug.Log(opponent.name);
    }
}
