using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessValues : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    
    void Start()
    {
        _slider.onValueChanged.AddListener((b) => {
            _sliderText.text = b.ToString("0%");
        });
    }
    
}
