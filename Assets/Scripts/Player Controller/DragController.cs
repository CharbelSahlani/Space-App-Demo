/**
 *
 * @file		DragController.cs
 * @brief		This module represents the drag control effect that will be 
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
public class DragController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;

    private Transform planetCenter;
    private float m_radius;
    private float m_atm;
    private float m_coef;

    private Vector3 m_dir;
    private float m_height;
    private Vector3 drag;


    //Debuging
    [SerializeField] private float dragValue;
    [SerializeField] private float distValue;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PlanetController planetController = gameManager.planet.GetComponent<PlanetController>();

        planetCenter = gameManager.planet.transform;
        m_radius = planetController.radius;
        m_atm = planetController.atmInfl;
        m_coef = planetController.drgCoef;
        Debug.Log(m_coef);
    }

    void FixedUpdate()
    {
        Vector3 pos = planetCenter.position - transform.position;
        m_dir = -rb.velocity;
        m_height = pos.magnitude - m_radius;

        if (m_height < m_atm)
        {
            drag = m_dir * m_coef * Mathf.Exp(-m_height * m_height / m_radius);
        }
        else
        {
            drag = Vector3.zero;
        }

        rb.AddForce(drag /* Time.fixedDeltaTime*/, ForceMode.Force);

        //Debuging
        distValue = m_height;
        dragValue = drag.magnitude;
    }
}
