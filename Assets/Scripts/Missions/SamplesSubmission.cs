/**
 *
 * @file		SamplesSubmission.cs
 * @brief		This script is responsible for submitting samples to the lab
 * @details     This script is attached to the Lab
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 27, 2021
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
public class SamplesSubmission : MonoBehaviour
{
    private TextMeshProUGUI keyPressText;
    // Start is called before the first frame update
    void Start()
    {
        keyPressText = GameObject.FindGameObjectWithTag("Press Key Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 /**
 * This function enables the player to submit the collected samples
 * to the lab when the "E" key is pressed. 
 * @param[in] other	represents the collider that caused the trigger.
 */
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null && !player.mission.goal.IsReached())
            {
                keyPressText.text = "You do not have enough samples!";
            }

            if (player != null && player.mission.goal.IsReached())
            {
                //Display the press key message
                keyPressText.text = "Press \"E\" to submit your collected samples!";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Erase the press key message
                    keyPressText.text = "";
                    player.mission.isActive = false;
                    MissionComplete.instance.MissionCompleteSequence();
                }
            }
        }
    }

    /**
     * This function erases the press key message when the player 
     * moves far away from the lab
     */
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            keyPressText.text = "";
        }
    }
}
