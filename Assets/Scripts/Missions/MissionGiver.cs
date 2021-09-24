/**
 *
 * @file		MissionGiver.cs
 * @brief		This script is responsible for organizing and giving missions to the player
 * @details     This script is attached to the mission giver component
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
using TMPro;
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
    public TextMeshProUGUI collectiblesText;

     void Update()
    {
        if (mission.isActive && mission.goal.goalType == GoalType.SamplesGathering)
        {
            collectiblesText.text = MissionGoal.currentAmount.ToString() + "/" + mission.goal.requiredAmount.ToString();
        }
    }
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
