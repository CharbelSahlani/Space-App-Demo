/**
 *
 * @file		PauseMenu.cs
 * @brief		This script is responsible for displaying the Pause Menu UI
 * @details     This script is attached to the Game Manager
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 26, 2021
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
    //Reference to the PanelFade animator
    Animator panelFade;
    private bool togglePause = false;
    GameObject orion;
    // Start is called before the first frame update
    private void Start()
    {
        // Assign panelFade to the corresponding animator
        panelFade = pauseMenuPanel.GetComponent<Animator>();
        orion = GameObject.FindGameObjectWithTag("Orion");
    }
    // Update is called once per frame
    void Update()
    {
        // Pause or unpause game when pressing the escape button
        if (Input.GetKeyDown(KeyCode.Escape) && 
            !MissionComplete.missionIsComplete && 
            !GameOver.gameIsOver &&
            !MissionGiver.missionWindowIsOpen)
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
        if (orion != null)
        {
            orion.GetComponent<PhysicsController>().enabled = false;
        }
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuPanel.SetActive(true);
    }

    /**
     * This function calls the ResumeGameRoutine
     */
    public void ResumeGame()
    {
        StartCoroutine(ResumeGameRoutine());
    }

    /**
     * This function resumes the game
     */
    public IEnumerator ResumeGameRoutine()
    {
        togglePause = false;
        panelFade.SetTrigger("PanelFadeOut");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        if (orion != null)
        {
            orion.GetComponent<PhysicsController>().enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
    }
}
