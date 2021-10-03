/**
 *
 * @file		Checkpoint.cs
 * @brief		This script is responsible for saving the player position at the checkpoint
 * @details     This script is attached to the checkpoints
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

public class Checkpoint : MonoBehaviour
{
    //reference to the checkoint controller
    private CheckpointController CC;

     void Start()
    {
        CC = GameObject.FindGameObjectWithTag("Checkpoint Controller").GetComponent<CheckpointController>();
    }

    /**
     * This function saves the last checkpoint position
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CC.lastCheckpointPos = transform.position;
        }
    }
}
