/**
 *
 * @file		PauseMenu.cs
 * @brief		This script is responsible for displaying the Pause Menu UI
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

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool togglePause = false;
    // Update is called once per frame
    void Update()
    {
        // Pause or unpause game when pressing the escape button
        if (Input.GetKeyDown(KeyCode.Escape) && 
            !MissionComplete.missionIsComplete && 
            !GameOver.gameIsOver)
        {
            togglePause = !togglePause;
            if (togglePause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    /**
     * This function pauses the game
     */
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuPanel.SetActive(true);
    }

    /**
     * This function pauses the game
     */
    public void ResumeGame()
    {
        togglePause = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
    }
}
