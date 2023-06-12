using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarMechanics : MonoBehaviour
{
    public Item[] possibleItems;
    public Slider health;
    public Opponent opponent;
    public TMP_Text valueText;
    public TMP_Text opponentName;
    public bool isOpponent;
    public GAMEMYDATA saveHolder;
    // Start is called before the first frame update
    void Start()
    {
        health.value = 100;
        health.maxValue = 100;

        try
        {
            GameObject _myGameObject = GameObject.Find("GAMEMYDATA");
            saveHolder = (GAMEMYDATA)_myGameObject.GetComponent(typeof(GAMEMYDATA));
        }
        catch
        {

        }

        if (isOpponent)
        {
            opponentName.text = opponent.name;
            valueText.text = health.value.ToString() + "/" + health.maxValue.ToString();
        }

        else if (!isOpponent)
        {
            opponentName.text = saveHolder.mySave.name;
        }


        if (possibleItems[0].isEquipped)
        {
            health.maxValue = health.maxValue + 10;
            health.value = health.value + 10;
            valueText.text = health.value.ToString() + "/" + health.maxValue.ToString();
        }

        else
        {
            valueText.text = health.value.ToString() + "/" + health.maxValue.ToString();
        }







        //if (isOpponent)
        //{
        //    health.value = 100;
        //    health.maxValue = 100;
        //    opponentName.text = opponent.name;
        //    valueText.text = health.value.ToString() + "/" + health.maxValue.ToString();
        //}



        //&& possibleItems[0].isEquipped

    }

    public void heal(int amount)
    {
        health.value = health.value + amount;
        valueText.text = health.value.ToString() + "/" + health.maxValue.ToString();
    }
}
