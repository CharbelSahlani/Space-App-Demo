/**
 *
 * @file		Gravity.cs
 * @brief		This script is responsible for gravity simulation
 * @details     This script is attached to the player
 * @author		Joseph El Hage (joe.m.hage@gmail.com)
 * @date		Sep 19, 2021
 * @note        
 * @see        
 * @version 	2
 * @warning     
 * @copyright
 * 2021 Team Dark Matter
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float renderSpeed = 1;   //time scale variable to change the simulations speed

    [SerializeField] private PlanetController planet;
    private Rigidbody rb;

    private float planetMass;
    public float G; //Gravitational constant
    [HideInInspector] public float dist;

    //triggers to apply the required velocity changes for orbiting
    public bool trigger = false;
    public bool signChange = false;
    public float newSpeed;


    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
        planetMass = planet.mass;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Time.timeScale = renderSpeed;

        /*
         * Currently, the trigger is manual, a ReadMe file is attached with all instructions
         * From version 3, all calculations and applications will be automatic.
         * For the final version, only the calculation will be automatic and
         * the user has to apply them by adjusting the necessary inputs 
         */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            trigger = true;
        }

        if (trigger)
        {
            trigger = false;
            //oldSign = newSign;
            Vector3 temp = rb.velocity;
            rb.velocity = temp.normalized * newSpeed;
        }

        /*
         * This function apply the gravity force on the player
         */

        GravitationalPull();
    }

    void GravitationalPull()
    {
        Vector3 diff = planet.transform.position - transform.position;
        dist = diff.magnitude;
        Vector3 dir = diff.normalized;

        float grav = G * planetMass / (dist * dist);

        rb.AddForce(grav * dir, ForceMode.Acceleration);
    }

}
