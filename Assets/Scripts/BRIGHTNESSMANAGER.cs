using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BRIGHTNESSMANAGER : MonoBehaviour
{
    public Slider brightnessSlider;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;

    void Start()
    {
        brightness.TryGetSettings(out exposure);    
        if(!PlayerPrefs.HasKey("brightness"))
        {
            PlayerPrefs.SetFloat("brightness",1);
            Load();    
        }
        else
        {
            Load();
        }
        
    }

    public void AdjustBrightness(float value)
    {
        if(value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = .00f;
        }
        Save();
    }

    private void Load()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("brightness", brightnessSlider.value);
    }
}
