/**
 *
 * @file		LevelLoader.cs
 * @brief		This script is responsible for loading the scenes 
 *              in the desired manner
 * @details     This script is attached to the LevelLoader component
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
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    //Reference to the crossFade animator
    public Animator transition;
    //Time to wait while the animation is playing
    public float transitionTime = 1f;
    [SerializeField] VideoPlayer video_player;
    private int savedLevelIndex = 0;
    private void Start()
    {
        savedLevelIndex = PlayerPrefs.GetInt("SavedLevel", 0);
        if (savedLevelIndex == 0)
        {
            savedLevelIndex = 1;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (video_player)
        {
            if (!video_player.isPlaying)
            {
                LoadNextLevel();
                AudioManager.instance.MusicMixer.SetFloat("MusicVolume", -80f);
            }
        }
    }
    /**
      * This function loads the next level 
      */
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /**
      * This function loads the next level 
      */
    public void LoadSameLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    /**
      * This function loads the main menu scene
      */
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    /**
      * This function loads the settings menu scene
      */
    public void LoadSettingsMenu()
    {
        StartCoroutine(LoadLevel(5));
    }

    /**
      * This function loads the free navigation scene
      */
    public void LoadFreeNavigationScene()
    {
        StartCoroutine(LoadLevel(4));
    }
    /**
      * This function quits the game
      */
    public void QuitGame()
    {
        Application.Quit();
    }

    /**
      * This function loads a scene at a specified index
      * @param[in] leveIndex	represents the scene index.
      */
    IEnumerator LoadLevel(int leveIndex)
    {
        Time.timeScale = 1f;
        AudioManager.instance.MusicMixer.SetFloat("MusicVolume",
                PlayerPrefs.GetFloat("MusicVolume", 0f));
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene(leveIndex);
    }

    /**
     * This function plays the sound of a button press
     */
    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySound("Button Press");
    }

    public void LoadSavedLevel()
    {
        StartCoroutine(LoadLevel(savedLevelIndex));
    }
}
