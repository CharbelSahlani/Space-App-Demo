/**
 *
 * @file		MissionGiver.cs
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
using UnityEngine.UI;

public class MissionGiver : MonoBehaviour
{
    //Create class objects
    public Mission mission;
    public Player player;
    //Mission Window 
    public GameObject missionWindow;

    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    /**
     * This function opens the missions window and shows the details of the mission.
     */
    public void OpenMissionWindow()
    {
        missionWindow.SetActive(true);
        titleText.text = mission.title;
        descriptionText.text = mission.description;
        experienceText.text = mission.xpReward.ToString();
        goldText.text = mission.goldReward.ToString();
    }

    /**
     * This function is called when the player accepts the mission
     */
    public void AcceptMission()
    {
        missionWindow.SetActive(false);
        mission.isActive = true;
        player.mission = mission;
    }

}