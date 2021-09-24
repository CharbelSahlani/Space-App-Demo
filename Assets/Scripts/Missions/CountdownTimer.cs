/**
 *
 * @file		CountdownTimer.cs
 * @brief		This script is responsible for the countdown when on a mission,
 *              it has the ability to trigger the gameover function
 * @details     This script is attached to the CountdownTimer
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 21, 2021
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
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public bool countdownIsActive = false;
    public float startingTime = 0f;
    float currentTime = 0f;
    int minutesRemaining = 0;
    int secondsRemaining = 0;
    // Current time but in integer
    int currentTimeInt = 0;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    // To play the counting sound
    private bool soundOn = true;
    // Critical time animator
    public Animator criticalTime;
    // Start is called before the first frame update
    void Start()
    {
        //TODO This shouldn't be here
        AudioManager.instance.MusicMixer.SetFloat("MusicVolume", 
                        PlayerPrefs.GetFloat("MusicVolume", 0f)); 
        currentTime = startingTime;
    }

    // Update is called once per frame
    // We are displaying the countdown on the UI
    void FixedUpdate()
    {
        if (countdownIsActive)
        {
            currentTime -= 1 * Time.fixedDeltaTime;
            DisplayTimer(currentTime);
        }
    }

    /**
     * This function plays counting sound every 1 second
     */
    IEnumerator PlayCountdownSound()
    {
        while (currentTime >= 0)
        {
            AudioManager.instance.PlaySound("Countdown");
            yield return new WaitForSeconds(1f);
        }
    }

    /**
     * This function displays the remaining time in this
     * format 00:00
     * param [in] currentTime   represents the current time
     */
    private void DisplayTimer(float currentTime)
    {
        currentTimeInt = (int)currentTime;
        minutesRemaining = currentTimeInt / 60;
        secondsRemaining = currentTimeInt % 60;
        // Play the counting sound starting from 10 seconds
        if (currentTimeInt < 11 && soundOn)
        {
            soundOn = false;
            countdownText.color = Color.red;
            //Play animation
            criticalTime.SetTrigger("criticalTime");
            StartCoroutine(PlayCountdownSound());
        }
        // Print the time on the UI in this format 00:00
        countdownText.text = string.Format("{0:00}:{1:00}", minutesRemaining, secondsRemaining);
        if (currentTime <= 0)
        {
            countdownIsActive = false;
            //TODO Trigger the GameOver method
            // Stop animation
            criticalTime.SetTrigger("timeEnded");
            AudioManager.instance.MusicMixer.SetFloat("MusicVolume", -80f);
            AudioManager.instance.PlaySound("Mission Failed");
        }
    }
}
