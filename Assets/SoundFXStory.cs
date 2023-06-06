using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXStory : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundEffects;
    private AudioSource audioSource;
    private float volume = 1f; // Default volume

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
            audioSource.volume = volume;
        }
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        AudioClip soundEffect = FindSoundEffectByName(soundEffectName);
        if (soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    private AudioClip FindSoundEffectByName(string soundEffectName)
    {
        foreach (AudioClip soundEffect in soundEffects)
        {
            if (soundEffect.name == soundEffectName)
            {
                return soundEffect;
            }
        }
        Debug.LogWarning("Sound effect not found: " + soundEffectName);
        return null;
    }
}