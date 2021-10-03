/**
 *
 * @file		GameplayUI.cs
 * @brief		This script is responsible for displaying the Gameplay UI
 * @details     This script is attached to the Game Manager
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 26, 2021
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

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    public Text spinText;
    public Text heatShieldText;
    public Text parachuteText;
    public Text cushionText;

    public Slider fuelSlider;
    public Image fuelFill;
    public Gradient fuelGradient;

    public Slider altitudeSlider;
    public Image altitudeFill;
    public Gradient altitudeGradient;
    public Text altitudeText;

    public Slider velocitySlider;
    public Image velocityFill;
    public Gradient velocityGradient;
    public Text velocityText;

    public Slider velocityZSlider;
    public Image velocityZFill;
    public Gradient velocityZGradient;
    public Text velocityZText;

    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * This function updates the fuel volume
     * param[in] fuelVolume  represents the updated fuel volume
     */
    public void SetFuelVolume(float fuelVolume)
    {
        fuelSlider.value = fuelVolume;
        fuelFill.color = fuelGradient.Evaluate(fuelSlider.normalizedValue);
    }

    /**
     * This function sets the maximum fuel volume
     * param[in] maxFuelVolume  represents the maximum fuel volume
     */
    public void SetMaxFuelVolume(float maxFuelVolume)
    {
        fuelSlider.maxValue = maxFuelVolume;
        fuelSlider.value = maxFuelVolume;
        // We set the value t 1 to indicate that the bar is full
        fuelFill.color = fuelGradient.Evaluate(1f);
    }

    /**
     * This function updates the altitude 
     * param[in] altitude  represents the updated altitude
     */
    public void SetAltitude(float altitude, float multiplier)
    {
        altitudeSlider.value = altitude;
        altitudeFill.color = altitudeGradient.Evaluate(altitudeSlider.normalizedValue);
        if (altitude > altitudeSlider.maxValue)
            altitudeText.text = "MAX";
        else
        {
            float temp = altitude * multiplier;
            altitudeText.text = temp.ToString("0") + "\nkm";
        }
    }

    /**
     * This function sets the maximum altitude
     * param[in] maxAltitude  represents the maximum altitude
     */
    public void SetMaxAltitude(float maxAltitude)
    {
        altitudeSlider.maxValue = maxAltitude;
        altitudeSlider.value = maxAltitude;
        // We set the value t 1 to indicate that the bar is full
        altitudeFill.color = altitudeGradient.Evaluate(1f);
    }

    /**
     * This function updates the velocity 
     * param[in] velocity  represents the updated velocity
     */
    public void SetVelocity(float velocity, float multiplier)
    {
        velocitySlider.value = velocity;
        velocityFill.color = velocityGradient.Evaluate(velocitySlider.normalizedValue);

        float temp = velocity * multiplier;
        velocityText.text = temp.ToString("0") + "\nkm/h";
    }

    /**
     * This function sets the maximum velocity
     * param[in] maxVelocity represents the max velocity
     */
    public void SetMaxVelocity(float maxVelocity)
    {
        velocitySlider.maxValue = maxVelocity;
        velocitySlider.value = maxVelocity;
        // We set the value t 1 to indicate that the bar is full
        velocityFill.color = velocityGradient.Evaluate(1f);
    }

    /**
     * This function updates the z-velocity 
     * param[in] velocity  represents the updated z-velocity
     */
    public void SetVelocityZ(float velocityZ, float multiplier)
    {
        velocityZSlider.value = velocityZ;
        velocityZFill.color = velocityZGradient.Evaluate(velocityZSlider.normalizedValue);

        float temp = velocityZ * multiplier;
        velocityZText.text = temp.ToString("0") + "\nkm/h";
    }

    /**
     * This function sets the maximum z-velocity
     * param[in] maxVelocityZ represents the max z-velocity
     */
    public void SetMaxVelocityZ(float maxVelocityZ)
    {
        velocityZSlider.maxValue = maxVelocityZ;
        velocityZSlider.value = maxVelocityZ;
        // We set the value t 1 to indicate that the bar is full
        velocityZFill.color = velocityZGradient.Evaluate(1f);
    }

    public void UpdateSpinAndShieldText(bool status)
    {
        spinText.text = "Spin: OFF";
        spinText.color = Color.green;

        if (!status)
        {
            heatShieldText.text = "Heat Shield: READY";
            heatShieldText.color = Color.yellow;
        }
        else
        {
            heatShieldText.text = "Heat Shield: DEPLOYED";
            heatShieldText.color = Color.green;
        }
    }

    public void UpdateParachuteText(bool status)
    {
        if (!status)
        {
            parachuteText.text = "Parachute: READY";
            parachuteText.color = Color.yellow;
        }
        else
        {
            parachuteText.text = "Parachute: DEPLOYED";
            parachuteText.color = Color.green;
        }
    }

    public void UpdateCushionText(bool status)
    {
        if (!status)
        {
            cushionText.text = "Cushion: READY";
            cushionText.color = Color.yellow;
        }
        else
        {
            cushionText.text = "Cushion: DEPLOYS";
            cushionText.color = Color.green;
        }

    }
}