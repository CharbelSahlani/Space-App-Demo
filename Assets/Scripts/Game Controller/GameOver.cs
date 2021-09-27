/**
 *
 * @file		GameOver.cs
 * @brief		This script is responsible for displaying the Game Over UI
 * @details     This script is attached to the Game Manager
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 25, 2021
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

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public static bool gameIsOver = false;
    public GameObject gameOverPanel;

    // Awake is called before the start method
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /**
     * This function plays the game over sequence
     */
    public void GameOverSequence()
    {
        gameIsOver = true;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        AudioManager.instance.MusicMixer.SetFloat("MusicVolume", -80f);
        AudioManager.instance.PlaySound("Mission Failed");
        Time.timeScale = 0f;
    }
}
