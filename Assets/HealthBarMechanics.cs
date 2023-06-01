using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarMechanics : MonoBehaviour
{
    public Slider health;
    // Start is called before the first frame update
    void Start()
    {
        health.value = 100;
    }
    
    public void deduct_health()
    {
        health.value-=10;
    }
}
