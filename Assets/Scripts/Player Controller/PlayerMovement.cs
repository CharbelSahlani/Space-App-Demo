/**
 *
 * @file		PlayerMovement.cs
 * @brief		This script is responsible for controlling the player
 *              movement
 * @details     This script is attached to the player component
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
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    //The player's movement speed
    public float speed = 12f;
    //The gravity on Mars's surface
    public float gravity = -3.721f;

    public float jumpHeight = 3f;
    public Transform groundCheck;
    //This is the radius of the sphere that we are using for ground check
    public float groundDistance = 0.4f;
    //To control what objects the sphere should check for
    public LayerMask groundMask;

    Vector3 velocity = new Vector3 (0f, 0f, 0f);
    bool isGrounded = true;
    private CheckpointController CC;
     void Start()
    {
        CC = GameObject.FindGameObjectWithTag("Checkpoint Controller").GetComponent<CheckpointController>();
        transform.position = CC.lastCheckpointPos;
    }
    // Update is called once per frame
    void Update()
    {
        //TODO delete this later
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //Check if the player isGrounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Reset the velocity when grounded
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        //Get input from the keyboard
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        //Move the player relatively to the where they are looking
        Vector3 move = transform.right * xAxis + transform.forward * zAxis;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        //Following the formula deltaY = 0.5*gravity*delatTime^2
        controller.Move(velocity * Time.deltaTime * 0.5f);
    }
}
