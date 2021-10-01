using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleLock : MonoBehaviour
{
    private GameObject orion;
    public bool lockScale;

    void OnTriggerEnter(Collider other)
    {
        orion = GameObject.FindGameObjectWithTag("Orion");
        if (orion != null)
        {
            if (lockScale)
                orion.GetComponent<PhysicsController>().LockTimeScale();
            else
                orion.GetComponent<PhysicsController>().UnlockTimeScale();
        }
        Destroy(gameObject);
    }
}
