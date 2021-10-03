/**
 *
 * @file		EnterMMSEV.cs
 * @brief		This script is responsible for entering the MMSEV
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
using TMPro;
public class EnterMMSEV : MonoBehaviour
{
    private TextMeshProUGUI keyPressText;
    private bool inVehicle = false;
    MMSEVEngine mmsevEngineScript;
    CharacterController characterController;
    PlayerMovement playerMovement;
    public GameObject cylinder;
    public GameObject mmsevCamera1;
    public GameObject mmsevCamera2;
    bool delay = true;
    // Start is called before the first frame update
    void Start()
    {
        mmsevCamera1.SetActive(false);
        mmsevCamera2.SetActive(false);
        mmsevEngineScript = GetComponent<MMSEVEngine>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        keyPressText = GameObject.FindGameObjectWithTag("Press Key Text").GetComponent<TextMeshProUGUI>();
        keyPressText.text = "";
        mmsevEngineScript.enabled = false;
        cylinder.SetActive(true);
        characterController.enabled = true;
        playerMovement.enabled = true;
    }

    // Update is called once per frame
    //This function allows the player to exit the MMSEV
    void Update()
    {
        CameraSwitch();
        if (inVehicle == true && Input.GetKeyDown(KeyCode.E) && !delay)
        {
            mmsevEngineScript.enabled = false;
            cylinder.SetActive(true);
            characterController.enabled = true;
            playerMovement.enabled = true;
            mmsevCamera1.SetActive(false);
            mmsevCamera2.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").transform.parent = null;
            //Set the player rotation to their default rotation
            GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.identity;
            inVehicle = false;
        }
    }

    /**
    * This function enables the player to enter MMSEV
    * when the "E" key is pressed. 
    * @param[in] other	represents the collider that caused the trigger.
    */
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && inVehicle == false)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //Display the press key message
                keyPressText.text = "Press \"E\" to enter the MMSEV";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Erase the press key message
                    keyPressText.text = "";
                    mmsevEngineScript.enabled = true;
                    cylinder.SetActive(false);
                    characterController.enabled = false;
                    playerMovement.enabled = false;
                    inVehicle = true;
                    mmsevCamera1.SetActive(true);
                    player.transform.parent = gameObject.transform;
                    StartCoroutine(Delay());
                    StartCoroutine(CameraSwitchMessage());
                }
            }
        }
    }

    /**
     * This function erases the press key message when the player 
     * moves far away from the lab
     */
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            keyPressText.text = "";
        }
    }

    /**
     * This function introduces a key bounce off delay
     */
    IEnumerator Delay()
    {
        delay = true;
        yield return new WaitForSeconds(0.5f);
        delay = false;
    }

    /**
     * This function switches between two diffferent cameras
     */
    void CameraSwitch()
    {
        if (inVehicle == true && Input.GetKeyDown(KeyCode.V) && !delay)
        {
            mmsevCamera1.SetActive(!mmsevCamera1.activeSelf);
            mmsevCamera2.SetActive(!mmsevCamera2.activeSelf);
            StartCoroutine(Delay());
        }
    }

    /**
     * This function displays the key to switch between cameras
     */
    IEnumerator CameraSwitchMessage()
    {
        keyPressText.text = "You can switch between Fisrt and third person view by pressing \"V\"";
        yield return new WaitForSeconds(5f);
        keyPressText.text = "";
    }
}
