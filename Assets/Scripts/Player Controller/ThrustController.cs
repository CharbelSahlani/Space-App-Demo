/**
 *
 * @file		ThrustController.cs
 * @brief		This module represents the thrust control effect that will be 
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

public class ThrustController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = Vector3.right * speed;
        rb.velocity = dir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
