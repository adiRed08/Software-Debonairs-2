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
    public static BRIGHTNESSMANAGER instance;

    void Start()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set this as the instance and mark it to not be destroyed on scene change
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameObject.isStatic)
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }


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
