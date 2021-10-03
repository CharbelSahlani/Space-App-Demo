/**
 *
 * @file		Sound.cs
 * @brief		This script provides the detailed options of each sound
 * @details
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 11, 2021
 * @note        
 * @see        
 * @version 	1
 * @warning     
 * @copyright
 * 2021 Team Dark Matter
 * 
 */
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    //name of the audio clip
    public string name;
    //the audio clip
    public AudioClip clip;
    /*
     * Various settings for the sound
     */
    [Range(0f, 1f)]
    public float volume = .75f;
    [Range(0f, 1f)]
    public float volumeVariance = .1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = .1f;

    public bool loop = false;
    public bool playOnAwake = false;	

    //Reference to the mixer group
    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;

}
