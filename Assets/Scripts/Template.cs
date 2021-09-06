/**
 *
 * @file		Template.cs
 * @brief		This module represents a simple template
 * @details
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 06, 2021
 * @note        any notes that you need to write
 * @see         only if there is need to refer to a document
 * @version 	1
 * @warning     warning that you need to give
 * @copyright
 * 2021 Team Dark Matter
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
    /**
     * This function returns the sum of two integers.
     * @param[in] x	        represents the first integer.
     * @param[in] y	        represents the second integer.
     * @param[out] result   represents the result of the sum.
     * @return result.
     */
    int Sum (int x, int y, int result = 0)
    {
        return result = x + y;
    }
}
