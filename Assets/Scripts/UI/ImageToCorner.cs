/**
 *
 * @file		ImageToCorner.cs
 * @brief		This script is responsible for UI effect when collecting samples
 * @details     This script is attached to the Corner Asteroid
 * @author		Charbel Al Sahlani (charbel.alsahlani@gmail.com)
 * @date		Sep 20, 2021
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

public class ImageToCorner : MonoBehaviour
{
    public float speed = 0.5f; /* Sensible default */
    public float time = 5f; /* Sensible default */
    // private RectTransform rectTransform;
    private GameObject cornerObj;
    public static RectTransform ImageToCornerRect;
    public static bool runCoroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        ImageToCornerRect = GetComponent<RectTransform>();
        cornerObj = GameObject.FindGameObjectWithTag("CornerObj");
    }

    // Update is called once per frame
    void Update()
    {
        if (runCoroutine)
            StartCoroutine(LerpMove(ImageToCornerRect, cornerObj.GetComponent<RectTransform>(), time));
    }

    /**
     * This function gets moves the image from the 3D position 
     * of the collectible unto the 2D position of the corner image
     */
    public IEnumerator LerpMove(RectTransform a, RectTransform b, float time)
    {
        runCoroutine = false;
        float i = 0.0f;
        float rate = (1.0f / time) * speed;


        ImageToCornerRect.anchorMin = b.anchorMin;
        ImageToCornerRect.anchorMax = b.anchorMax;
        transform.position = Collectible.collectibleTransform.position;
        while (i < 1.0f)
        {
            //lerp the anchoredPosition and update each time
            i += Time.deltaTime * rate;
            ImageToCornerRect.anchoredPosition = Vector3.Lerp(a.anchoredPosition, b.anchoredPosition, i);
            if (Mathf.Abs(ImageToCornerRect.anchoredPosition.x - b.anchoredPosition.x) <= 1)
                break;

            yield return null;
        }
        cornerObj.GetComponent<Animator>().SetTrigger("ScaleCornerImg");
        gameObject.SetActive(false);
    }
}
