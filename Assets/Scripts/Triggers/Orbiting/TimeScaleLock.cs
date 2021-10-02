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
        GetComponent<TriggerAIController>().UpdateAIText();

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
    }
    void UnlockScale()
    {
        orion.GetComponent<PhysicsController>().UnlockTimeScale();
    }
}
