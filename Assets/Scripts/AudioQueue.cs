using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
   
    public void playAudioClip()
    {
        source.PlayOneShot(clip);
    }
}
