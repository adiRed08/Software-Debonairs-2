using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeValues : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    void Start()
    {
        //Reflect the slider value on UI text
        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("0%");
        });

        _sliderText.text = _slider.value.ToString("0%");
    }
}