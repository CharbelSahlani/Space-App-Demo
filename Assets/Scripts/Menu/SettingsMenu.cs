/**
 *
 * @file		SettingsMenu.cs
 * @brief		This script is responsible for controlling the settings menu
 * @details
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 14, 2021
 * @note        
 * @see        
 * @version 	1
 * @warning     
 * @copyright
 * 2021 Team Dark Matter
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{

    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    private float musicVolume = 0.0f;
    private float sfxVolume = 0.0f;
    public Slider sliderMusic;
    public Slider sliderSFX;

    /**
     * This function runs only once on startup
     * and sets the corresponding volumes
     */
    void Start()
    {
        
         musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
         sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
         SetMusicVolume(musicVolume);
         SetSFXVolume(sfxVolume);
        if (sliderMusic == null && sliderSFX == null)
        {
            return;
        }
        else
        {
            sliderMusic.value = musicVolume;
            sliderSFX.value = sfxVolume;
        }
    }

    /**
      * This function sets the music volume
      * @param[in] volume	represents the volume setting.
      */
    public void SetMusicVolume(float volume)
    {
        //Set the Music Mixer's volume
        MusicMixer.SetFloat("MusicVolume", volume);
        //Update PlayerPrefs "MusicVolume"
        PlayerPrefs.SetFloat("MusicVolume", volume);
        //Save changes
        PlayerPrefs.Save();
    }

    /**
      * This function sets the sfx volume
      * @param[in] volume	represents the volume setting.
      */
    public void SetSFXVolume(float volume)
    {
        //Set the SFX Mixer's volume
        SFXMixer.SetFloat("SFXVolume", volume);
        ////Update PlayerPrefs "SFXVolume"
        PlayerPrefs.SetFloat("SFXVolume", volume);
        //Save changes
        PlayerPrefs.Save();
    }

}
