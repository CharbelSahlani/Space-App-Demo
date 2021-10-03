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

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI xpValueText;
    //public TextMeshProUGUI goldText;
    public TextMeshProUGUI collectiblesText;
    GameObject orion;
    //Reference to the PanelFade animator
    Animator panelFade;

    public static MissionGiver instance;
    public static bool missionWindowIsOpen = false;
    private void Awake()
    {
        instance = this;
        orion = GameObject.FindGameObjectWithTag("Orion");
        if (orion != null)
        {
            orion.GetComponent<PhysicsController>().enabled = false;
        }
    }
    // Start is called on the first frame
    private void Start()
    {
        // Assign panelFade to the corresponding animator
        panelFade = missionWindow.GetComponent<Animator>();
        Time.timeScale = 0f;
        OpenMissionWindow();
        if (xpValueText != null)
        {
            xpValueText.text = player.XP.ToString();
        }
    }
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
        missionWindowIsOpen = true;
        titleText.text = mission.title;
        descriptionText.text = "DESCRIPTION: " + mission.description;
        experienceText.text = "XP REWARD: " + mission.xpReward.ToString();
        //goldText.text = mission.goldReward.ToString();
    }

    /**
     * This function is called when the player accepts the mission
     */
    public void AcceptMission()
    {
        StartCoroutine(AcceptMissionRoutine());
    }

    /**
     * This function is called by the accept mission function
     */
    public IEnumerator AcceptMissionRoutine()
    {
        mission.isActive = true;
        player.mission = mission;
        Cursor.lockState = CursorLockMode.Locked;
        panelFade.SetTrigger("PanelFadeOut");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        if (orion != null)
        {
            orion.GetComponent<PhysicsController>().enabled = true;
            //orion.GetComponent<UIController>().ResetUI();
        }
        missionWindow.SetActive(false);
        missionWindowIsOpen = false;

    }

}
