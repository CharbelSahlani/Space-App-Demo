/**
 *
 * @file		MMSEVWheel.cs
 * @brief		This script is responsible for the wheel rotation
 * @details     This script is attached to the MMSEV wheels 
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

public class MMSEVWheel : MonoBehaviour {
    public WheelCollider targetWheel;
    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();
	
	
	// Update is called once per frame
    // This function is responsible for the clockwise/counter-clockwise wheel rotation
	void Update () {
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation); // it gives the output that is the pos and the rotation
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
	}
}
