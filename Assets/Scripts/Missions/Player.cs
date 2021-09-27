/**
 *
 * @file		Player.cs
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player health
    public int health = 5;
    //Player experience points
    public int XP = 0;
    //Player gold amount
    public int gold = 0;
    //Mission object to access the mission class
    public Mission mission;

    //TODO check if the goal is completed here

    private void Start()
    {
        Time.timeScale = 1f;
        AudioManager.instance.MusicMixer.SetFloat("MusicVolume",
                PlayerPrefs.GetFloat("MusicVolume", 0f));
        XP = PlayerPrefs.GetInt("Total XP", 0);
    }
}
