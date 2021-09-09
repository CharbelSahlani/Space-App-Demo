/**
 *
 * @file		DragController.cs
 * @brief		This module represents the planet physics simulation that will be 
 *              executed on the spacecraft
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

public class PlanetController : MonoBehaviour
{
    public float mass;
    public float radius;
    public float atmInfl;
    public float grvInfl;
    public float surfDrag;
    public float surfGravity;
    [HideInInspector] public float grvCoef;
    [HideInInspector] public float drgCoef;


    void Awake()
    {
        Vector3 temp = transform.localScale;
        temp *= (2 * radius);
        transform.localScale = temp;

        grvCoef = surfGravity * radius * radius / mass;
        drgCoef = surfDrag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
