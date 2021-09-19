/**
 *
 * @file		UIController.cs
 * @brief		This script is responsible for gravity simulation
 * @details     This script is attached to the player
 * @author		Joseph El Hage (joe.m.hage@gmail.com)
 * @date		Sep 19, 2021
 * @note        For debugging purposes only
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
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Text distText;
    public Text minDistText;
    public Text maxDistText;
    public Text speedText;

    private Rigidbody rb;
    private Gravity grv;
    public bool resetting;

    private float dist;
    private float minDist=float.MaxValue;
    private float maxDist = 0;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grv = GetComponent<Gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        if(resetting)
        {
            ResetUI();
            resetting = false;
        }

        dist = grv.dist;

        maxDist = (dist > maxDist) ? dist : maxDist;
        minDist = (dist < minDist) ? dist : minDist;
        speed = rb.velocity.magnitude;

        distText.text = "Dist: " + dist.ToString();
        minDistText.text = "Min: " + minDist.ToString();
        maxDistText.text = "Max: " + maxDist.ToString();
        speedText.text = "V: " + speed.ToString();
    }

    void ResetUI()
    {
        maxDist = 0;
        minDist = 1000;
    }
}
