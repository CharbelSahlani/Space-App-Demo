/**
 *
 * @file		MissionComplete.cs
 * @brief		This script is responsible for displaying the Mission complete UI
 * @details     This script is attached to the Game Manager
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 25, 2021
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
public class MissionComplete : MonoBehaviour
{
    public static MissionComplete instance;
    public static bool missionIsComplete = false;
    public GameObject missionCompletePanel;
    public TextMeshProUGUI earnedXP;
    public TextMeshProUGUI totalXP;
    public Player player;
    public float scoreCountDuration = 2f;
    private int totalXPInt = 0;
    // Awake is called before the start method
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        missionIsComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * This function plays the mission complete sequence
     */
    public void MissionCompleteSequence()
    {
        missionIsComplete = true;
        missionCompletePanel.SetActive(true);
        earnedXP.text = "EARNED XP: " + player.mission.xpReward.ToString();
        Cursor.lockState = CursorLockMode.None;
        AudioManager.instance.PlaySound("Mission Complete");
        totalXPInt = player.mission.xpReward + player.XP;
        CountdownTimer.countdownIsActive = false;
        StartCoroutine(TotalXPCount());
    }

    /**
     * This function plays the score counting effect 
     */
    IEnumerator TotalXPCount()
    {
        //score animation
        int start = 0;
        int value = 0;
        for (float timer = 0; timer < scoreCountDuration; timer += Time.deltaTime)
        {
            float progress = timer / scoreCountDuration;
            value = (int)Mathf.Lerp(start, totalXPInt, progress);
            totalXP.text = "TOTAL XP: " + value;
            if (value != 0)
                AudioManager.instance.PlaySound("XP");
            if (value == totalXPInt)
                break;
            yield return null;
        }
        start = 0;
        value = 0;
        totalXP.text = "TOTAL XP: " + totalXPInt;
        PlayerPrefs.SetInt("Total XP", totalXPInt);
        PlayerPrefs.Save();
        player.XP = totalXPInt;
        player.mission.Complete();
    }
}
