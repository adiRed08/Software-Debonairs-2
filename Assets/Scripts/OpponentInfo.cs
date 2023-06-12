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
    [SerializeField] private GameObject myGameObject;
    [SerializeField] private GAMEMYDATA saveHolder;
    //:)

    void Start()
    {
        try
        {
            myGameObject = GameObject.Find("GAMEMYDATA");
            saveHolder = (GAMEMYDATA)myGameObject.GetComponent(typeof(GAMEMYDATA));
        }
        catch
        {

        }


        if (saveHolder.mySave.male)
        {
            this.image.sprite = opponent.image;
            
        }
        else
        {
            opponent.name = "Karen";
            this.image.sprite = female;
        }   
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
}
