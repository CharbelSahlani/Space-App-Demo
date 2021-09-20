/**
 *
 * @file		MissionGoal.cs
 * @brief		This script provides the mission goal type and returns 
 *              the progress of the mission
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
public class MissionGoal
{
    public GoalType goalType;
    //The required amout set by the mission objective
    public int requiredAmount;
    //The current amount
    public static int currentAmount;

    /**
     * This function returns true if goal targed is reached and false otherwise
     * @return the result of the comparision.
     */
    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    /**
     * This function increases the currentAmount each time 
     * the player reaches a checkpoint when the goal type is 
     * target reaching
     */
    public void CheckpointReached()
    {
        if (goalType == GoalType.TargetReaching)
        {
            currentAmount++;
        }
    }

    /**
     * This function is called whenever the player
     * lands successfully on a planet
     */
    public void LandedOnPlanet()
    {
        if (goalType == GoalType.Landing)
        {
            //TODO landing completed
        }
    }

    /**
     * This function increases the currentAmount each time 
     * the player collects a sample when the goal type is 
     * samples gathering
     */
    public void SampleCollected()
    {
        if (goalType == GoalType.SamplesGathering)
        {
            currentAmount++;
        }
    }
}

//It holds the different types of missions
public enum GoalType
{
    TargetReaching,
    SamplesGathering,
    Landing,
}