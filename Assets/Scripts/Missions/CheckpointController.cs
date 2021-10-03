/**
 *
 * @file		CheckpointController.cs
 * @brief		This script is responsible for saving the player position at the checkpoint
 * @details     This script is attached to the checkpoint controller
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 17, 2021
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

public class CheckpointController : MonoBehaviour
{
    private static CheckpointController instance;
    public Vector3 lastCheckpointPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
