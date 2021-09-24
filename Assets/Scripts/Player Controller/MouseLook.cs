/**
 *
 * @file		MouseLook.cs
 * @brief		This script is responsible for controlling the view to point 
 *              to the location where the player is looking
 * @details     This script is attached to the main camera component
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 16, 2021
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

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    //The rotation of the up and down direction
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get information from the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Move only the camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Rotate the entire player body left and right
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
