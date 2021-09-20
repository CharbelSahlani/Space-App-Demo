/**
 *
 * @file		Collectible.cs
 * @brief		This script is responsible for collecting samples
 * @details     This script is attached to the collectibles
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 20, 2021
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
using TMPro;
public class Collectible : MonoBehaviour
{
    private TextMeshProUGUI keyPressText;

    /**
     * This function gets the text game object automatically on start
     * to be able to modify it later.
     */
    void Start()
    {
        keyPressText = GameObject.FindGameObjectWithTag("Press Key Text").GetComponent<TextMeshProUGUI>();
    }
    /**
     * This function enables the player to collect samples
     * when the "E" key is pressed. 
     * This function accesses the mission goal class to increase
     * the ammount of collected samples.
     * @param[in] other	represents the collider that caused the trigger.
     */
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Display the press key message
            keyPressText.text = "Press \"E\" to collect";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    //Erase the press key message
                    keyPressText.text = "";
                    player.mission.goal.SampleCollected();
                    AudioManager.instance.PlaySound("Collect");
                    //Destroy the game object to give the illusion that the sample was collected
                    Destroy(this.gameObject);
                }
            }
        }
    }

    /**
     * This function erases the press key message when the player 
     * moves far away from the sample
     */
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            keyPressText.text = "";
        }
    }
}
