/**
 *
 * @file		GameManager.cs
 * @brief		This module represents the game manager
 * @details
 * @author		Joseph Hage (joe.m.hage@gmail.com)
 * @date		Sep 08, 2021
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

public class GameManager : MonoBehaviour
{
    public int stage;
    [HideInInspector] public GameObject planet;

    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
