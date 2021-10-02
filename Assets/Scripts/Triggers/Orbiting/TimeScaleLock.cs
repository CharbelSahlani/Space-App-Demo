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
                LockScale();
            else
                UnlockScale();
        }
        Destroy(gameObject);
    }

    void LockScale()
    {
        orion.GetComponent<PhysicsController>().LockTimeScale();
        Debug.Log("Now time has slowed and locked so you can focus on the fuel burn\nTry to slow down to 15400 km/h when the altitude reaches 200 km");
    }
    void UnlockScale()
    {
        orion.GetComponent<PhysicsController>().UnlockTimeScale();
        Debug.Log("Time control is back on\nSpeed up the simulation to see orbiting");
    }
}
