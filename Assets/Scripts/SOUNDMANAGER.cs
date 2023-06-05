using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOUNDMANAGER : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public static SOUNDMANAGER instance;
    public AudioSource source;
    public AudioClip clip;


    void Start()
    {
        Debug.Log("Start");
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        
        //soundPlayed = true;
        //Destroy(gameObject);
    }

    void Awake()
    {
        Debug.Log("Awake");
        // Check if an instance already 

        if (instance == null)
        {
            // If not, set this as the instance and mark it to not be destroyed on scene change
            instance = this;
            DontDestroyOnLoad(gameObject);
            source.PlayOneShot(clip);
        }

        else if (gameObject.isStatic)
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }

        //else if (soundPlayed == true)
        //{
        //    Destroy(gameObject);
        //}
        //else if (gameObject.isStatic)
        //{
        //    //If an instance already exists, destroy this one
        //    soundPlayed = true;
        //    Destroy(gameObject);
        //}

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
