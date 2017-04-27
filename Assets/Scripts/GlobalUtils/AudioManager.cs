using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

/// <summary>
/// Singleton to access AudioSources and play audio clips globally
/// </summary>

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource[] audioSources;

    // Grab all of the AudioSources attached to this game object and store references to them
    //
    void Awake()
    {
        audioSources = GetComponents<AudioSource>();
    }

    // Global function to play a given audio clip one time at a given volume
    //
    public void PlayOneShot(AudioClip clipToPlay, float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clipToPlay, volume);
                return;
            }
        }
    }
}
