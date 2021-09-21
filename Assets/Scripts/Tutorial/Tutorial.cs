/**
 *
 * @file		Tutorial.cs
 * @brief		This script is responsible for the game tutorial
 * @details     This script is attached to the Tutorial
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

public class Tutorial : MonoBehaviour
{
    //current order of the tutorial
    public int order = 0;
     void Awake()
    {
        TutorialManager.Instance.Tutorials.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * This function checks if the current tutorial is playing
     */
    public virtual void CheckIfHappening()
    {

    }
}
