/**
 *
 * @file		MMSEVEngine.cs
 * @brief		This script is responsible for controlling the MMSEV
 * @details     This script is attached to the MMSEV
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 29, 2021
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

public class MMSEVEngine : MonoBehaviour {

    public float maxSteerAngle = 45f;
    public float maxBrakeTorque = 150f;
    public WheelCollider wheel1Right;
    public WheelCollider wheel2Right;
    public WheelCollider wheel3Right;
    public WheelCollider wheel1Left;
    public WheelCollider wheel2Left;
    public WheelCollider wheel3Left;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float lowSpeed;
    public float averageSpeed;
    public float highSpeed;
    AudioSource mmsevAudioSource;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public bool isBraking = true;
    public float turnSpeed = 5f;

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    }

    private void FixedUpdate()
    {
        //Breaking is done with the space bar
        if (Input.GetButton("Jump"))
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }
        MMSEVEngineSound();
        Drive();
        Braking();
        LerpToSteerAngle();
    }

    /**
    * This function enables the player to drive the MMSEV
    * forward or backward when pressing the forward key "W" 
    * and the backward key "S"
    */
    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheel1Left.radius * wheel1Left.rpm * 60 / 1000; //speed formula
        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheel1Left.motorTorque = maxMotorTorque * Input.GetAxis("Vertical"); //set to max torque
            wheel1Right.motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
            wheel2Left.motorTorque = wheel1Left.motorTorque;
            wheel2Right.motorTorque = wheel1Right.motorTorque;
        }
        else
        {
            wheel1Left.motorTorque = 0;
            wheel1Right.motorTorque = 0;
        }
    }

    /**
    * This function is responsible of braking and decelerating the MMSEV
    */
    private void Braking()
    {
        if (isBraking)
        {
            wheel3Left.brakeTorque = maxBrakeTorque; //braking torque set to max
            wheel3Right.brakeTorque = maxBrakeTorque;//braking torque set to max
        }
        else
        {
            wheel3Left.brakeTorque = 0; //braking torque set to 0
            wheel3Right.brakeTorque = 0;//braking torque set to 0
        }
    }

    /**
    * This function is responsible for aplying smooth turns on the MMSEV
    * the player turns left by pressing the "A" key and right by 
    * pressing the "D" key
    */
    private void LerpToSteerAngle()
    {
        // Getting the key press of the player and multiplying it by the max steering angle
        float targetSteeringAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
        //Wheels rotation left and right
        wheel1Left.steerAngle = Mathf.Lerp(wheel1Left.steerAngle, targetSteeringAngle, Time.deltaTime * turnSpeed);
        wheel1Right.steerAngle = Mathf.Lerp(wheel1Right.steerAngle, targetSteeringAngle, Time.deltaTime * turnSpeed);
        wheel3Left.steerAngle = Mathf.Lerp(wheel3Left.steerAngle, targetSteeringAngle, Time.deltaTime * turnSpeed);
        wheel3Right.steerAngle = Mathf.Lerp(wheel3Right.steerAngle, targetSteeringAngle, Time.deltaTime * turnSpeed);
    }


    /**
    * This function plays different pitches of the engine sound
    * relatively to the current speed of the MMSEV
    */
    private void MMSEVEngineSound()
    {
        // Get the absolute value of current speed
        currentSpeed = Mathf.Abs(currentSpeed);
        if (lowSpeed <= currentSpeed && currentSpeed < averageSpeed)
        {
            mmsevAudioSource.pitch = 0.3f; /* Sensible default */
      
        }
        else if (averageSpeed <= currentSpeed && currentSpeed < highSpeed)
        {
            mmsevAudioSource.pitch = 0.4f; /* Sensible default */
        }
        else if (highSpeed <= currentSpeed)
        {
            mmsevAudioSource.pitch = 0.45f; /* Sensible default */
        }
    }

    /**
    * This function stops the engine sound when the script is disabled
    */
    private void OnDisable()
    {
        mmsevAudioSource.loop = false;
        mmsevAudioSource.Stop();
    }

    /**
    * This function plays the engine sound when the script is enabled
    */
    private void OnEnable()
    {
        mmsevAudioSource = GetComponent<AudioSource>();
        mmsevAudioSource.loop = true;
        mmsevAudioSource.Play();
    }
}

