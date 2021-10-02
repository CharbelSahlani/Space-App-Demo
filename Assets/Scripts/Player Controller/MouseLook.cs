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
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    //The rotation of the up and down direction
    float xRotation = 0f;
    // Start is called before the first frame update
    //get the table of regex
    //IDictionary<string, string[]> regex_array = new Dictionary<string, string[]>();
    HandleRegex regex_script;
    IDictionary<string, string[]> regex_arr;
    string last_planet_name = "";
    int last_random = 0;
    [SerializeField] GameObject ai_panel;
    [SerializeField] Text ai_text;
    int rdx = 0;

    void Start()
    {
       regex_script = FindObjectOfType<HandleRegex>();
        regex_arr = regex_script.get_regex_array();
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


        //raycast 
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            //Debug.Log(objectHit + "name " + objectHit.name);

            //check the planets
            
            foreach (KeyValuePair<string, string[]> kvp in regex_arr)
            {
                 
                    //Debug.Log(kvp.Key + str);
                    if (objectHit.name == kvp.Key && last_planet_name != kvp.Key)
                    {
                    ai_panel.SetActive(true);
                    last_planet_name = kvp.Key;
                    System.Random rnd = new System.Random();
                    rdx = rnd.Next() % regex_arr[kvp.Key].Length;
                    while(rdx == last_random)
                    {
                        rdx = rnd.Next() % regex_arr[kvp.Key].Length;
                    }
                    Debug.Log(kvp.Value[rdx]);
                    ai_text.text = kvp.Value[rdx];
                    }
                
            }

        }
        else
        {
            last_planet_name = "";
            ai_text.text = "";
            ai_panel.SetActive(false);
            last_random = rdx;
        }

     
    }
}




/*
 * switch (objectHit.name)
            {
                case "earth":
                    break;
                case "sun":
                    break;
                case "mercury":
                    break;
                case "venus":
                    break;
                case "mars":
                    break;
                case "jupiter":
                    break;

            }
*/