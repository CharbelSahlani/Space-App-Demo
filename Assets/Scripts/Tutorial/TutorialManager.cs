/**
 *
 * @file		TutorialManager.cs
 * @brief		This script is responsible for all of the tutorials
 * @details     This script is attached to the Tutorial Manager 
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

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> Tutorials = new List<Tutorial>();
    private static TutorialManager instance;
    private Tutorial currentTutorial;
    public static bool finishedTutorial = true;
    /**
     * This function creates an instance of the tutorial manager
     */
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();
            if (instance == null)
                Debug.Log("TutorialManager gameObject not found!");
            return instance;
        }
    }

   
    // Start is called before the first frame update
   //  We are setting the current tutorial to the first tutorial
    void Start()
    {
        finishedTutorial = false;
        SetNextTutorial(0);
    }

    // Update is called once per frame
    // We are calling the CheckIfHappening function to play the tutorial 
    void Update()
    {
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }

    /**
     * This function assigns the next tutorial when the current one completes
     */
    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.order + 1);
    }

    /**
     * This function assigns the next tutorial
     * param[in] currentOrder   represents the current tutorial index
     */
    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }
    }


    /**
     * This function is called when all tutorials are completed
     */
    public void CompletedAllTutorials()
    {
        finishedTutorial = true;
        Debug.Log("Tutorials completed");
    }

    /**
     * This function gets the Tutorial objects by index and lists them in an array
     * param[in] order   represents the current tutorial index
     */
    public Tutorial GetTutorialByOrder(int order)
    {
      for(int i =0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].order == order)
                return Tutorials[i];

        }
        return null;
    }
}
