/**
 *
 * @file		GravityController.cs
 * @brief		This module represents the gravity control effect that will be 
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

[RequireComponent(typeof(Rigidbody))]
public class GravityController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;

    private Transform planetCenter;
    private float m_mass;
    private float m_radius;
    private float m_grv;
    private float m_coef;

    private Vector3 m_dir;
    private float m_dist;
    private Vector3 gravity;


    //Debuging
    [SerializeField] private float gravValue;
    [SerializeField] private float distValue;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PlanetController planetController = gameManager.planet.GetComponent<PlanetController>();

        planetCenter = gameManager.planet.transform;
        m_mass = planetController.mass;
        m_radius = planetController.radius;
        m_grv = planetController.grvInfl;
        m_coef = planetController.grvCoef;
    }

    void FixedUpdate()
    {
        Vector3 pos = planetCenter.position-transform.position;
        m_dir = pos.normalized;
        m_dist = pos.magnitude;

        if (m_dist < m_radius + m_grv)
        {
            gravity = m_dir * m_coef * m_mass / (m_dist * m_dist);
        }
        else
        {
            gravity = Vector3.zero;
        }

        rb.AddForce(gravity /* Time.fixedDeltaTime*/, ForceMode.Force);

        //Debuging
        distValue = m_dist;
        gravValue = gravity.magnitude;
    }
}
