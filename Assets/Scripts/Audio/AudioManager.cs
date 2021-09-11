/**
 *
 * @file		AudioManager.cs
 * @brief		
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
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    private float music = 0.0f;
    private float sfx = 0.0f;

    //create an array of type Sound (sound is the script Sound)
    public Sound[] sounds;

    /**
 * This function runs once before the Start function
 */
    void Awake()
    {
        //set instance
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

        //initial setup for each sound
        foreach (Sound s in sounds)
        {
            //Add an audio source to each sound in the audioManager gameOnject
            s.source = gameObject.AddComponent<AudioSource>();
            //set the sound clip
            s.source.clip = s.clip;
            //set the loop option
            s.source.loop = s.loop;
            //set the play onAwake option
            s.source.playOnAwake = s.playOnAwake;
            //set the output mixer group
            s.source.outputAudioMixerGroup = s.mixerGroup;

        }

        //Get the saved previously saved volume values
        music = PlayerPrefs.GetFloat("MusicVolume", 0f);
        sfx = PlayerPrefs.GetFloat("SFXVolume", 0f);
     
      
    }

    /**
     * This function runs only once on startup
     * and sets the corresponding volumes
     */
    void Start()
    {
        SetMusicVolume(music);
        SetSFXVolume(sfx);
        PlaySound("Main Theme");
    }

    /**
     * This function runs every frame and updates the volume accordingly
     */
    void Update()
    {
        foreach (Sound s in sounds)
        {

            s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));

        }
    }

    /**
     * This function sets the music volume
     * @param[in] sound	represents the sound file used.
     */
    public void PlaySound(string sound)
    {
        //Find sound by name
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        //set the sound volume and pitch and finally play the sound
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    /**
     * This function sets the music volume
     * @param[in] volume	represents the volume level.
     */
    public void SetMusicVolume(float volume)
    {
        MusicMixer.SetFloat("MusicVolume", volume);
        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("MusicVolume", volume);
      
        //Save changes
        PlayerPrefs.Save();
    }

    /**
     * This function sets the sfx volume
     * @param[in] volume	represents the volume level.
     */
    public void SetSFXVolume(float volume)
    {
        SFXMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
     
        //Save changes
        PlayerPrefs.Save();
    }

}
