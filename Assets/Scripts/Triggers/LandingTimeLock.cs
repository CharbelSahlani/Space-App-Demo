using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTimeLock : MonoBehaviour
{
    public float scale;

    void OnTriggerEnter(Collider other)
    {
        if (scale > 0)
            Time.timeScale = scale;
    }
}
