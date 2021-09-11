/**
 *
 * @file		Mission.cs
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

//To be able to see it in the unity editor
[System.Serializable]
public class Mission
{
    //Used to indicate if the mission is active
    public bool isActive;

    //Mission title
    public string title;
    //Mission description
    public string description;
    //XP to reward the player with once the mission is complete 
    public int xpReward;
    //Gold to reward the player with once the mission is complete 
    public int goldReward;
    //MissionGoal object
    public MissionGoal goal;

    /**
     * This function is called when the mission is successfully completed
     */
    public void Complete()
    {
        isActive = false;
    }
}
