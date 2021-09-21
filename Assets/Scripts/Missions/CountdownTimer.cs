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
    [SerializeField]
    private TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    // We are displaying the countdown on the UI
    void FixedUpdate()
    {
        if (countdownIsActive)
        {
            currentTime -= 1 * Time.fixedDeltaTime;
            // We are using "0" in the ToString method to print only the int value
            countdownText.text = currentTime.ToString("0");
            if (currentTime <= 0)
            {
                currentTime = 0;
                countdownIsActive = false;
                //TODO Trigger the GameOver method
            }
        }
    }
}
