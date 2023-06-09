using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarMechanics : MonoBehaviour
{
    public Slider health;
    public Opponent opponent;
    public TMP_Text valueText;
    public TMP_Text opponentName;
    // Start is called before the first frame update
    void Start()
    {
        health.value = 100;
        health.maxValue = 100;
        valueText.text = health.value.ToString() + "/" + health.maxValue.ToString();
        opponentName.text = opponent.name;
    }
    
    public void deduct_health()
    {
        health.value-=10;
    }
}
