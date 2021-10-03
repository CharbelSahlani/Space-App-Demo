using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleLock : MonoBehaviour
{
    private GameObject orion;
    public bool scaleLock;
    public bool fuelLock;
    public int timeIndex;

    void OnTriggerEnter(Collider other)
    {
        orion = GameObject.FindGameObjectWithTag("Orion");

        if (orion != null)
        {
            if (scaleLock)
                orion.GetComponent<PhysicsController>().LockTimeScale(timeIndex);
            else
                orion.GetComponent<PhysicsController>().UnlockTimeScale(timeIndex);

            if (fuelLock)
                orion.GetComponent<PhysicsController>().LockFuelBurn();
            else
                orion.GetComponent<PhysicsController>().UnlockFuelBurn();
        }
    }
    /*
    void LockFunction()
    {
        if (scaleLock)
            orion.GetComponent<PhysicsController>().LockTimeScale(timeIndex);
        else
            orion.GetComponent<PhysicsController>().UnlockTimeScale(timeIndex);

        if (fuelLock)
            orion.GetComponent<PhysicsController>().LockFuelBurn();
        else
            orion.GetComponent<PhysicsController>().UnlockFuelBurn();
    }
    void UnlockFunction()
    {
        if (!scaleLock)
            orion.GetComponent<PhysicsController>().UnlockTimeScale(timeIndex);
        if (!fuelLock)
            orion.GetComponent<PhysicsController>().UnlockFuelBurn();
    }*/
}
