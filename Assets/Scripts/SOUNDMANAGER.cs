using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOUNDMANAGER : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public static SOUNDMANAGER instance;


    void Start()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set this as the instance and mark it to not be destroyed on scene change
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // else
        // {
        //     // If an instance already exists, destroy this one
        //     Destroy(gameObject);
        // }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
