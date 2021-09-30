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
    public Slider fuelSlider;
    public Image fuelFill;
    public Gradient fuelGradient;
    public Slider altitudeSlider;
    public Image altitudeFill;
    public Gradient altitudeGradient;
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
    public void SetAltitude(float altitude)
    {
        altitudeSlider.value = altitude;
        altitudeFill.color = altitudeGradient.Evaluate(altitudeSlider.normalizedValue);
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
}