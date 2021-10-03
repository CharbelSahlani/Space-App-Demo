using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSceneLoader : MonoBehaviour
{
    private GameObject orion;
    public int index;

    void OnTriggerEnter(Collider other)
    {
        orion = GameObject.FindGameObjectWithTag("Orion");
        if (orion != null)
        {
            orion.GetComponent<PhysicsController>().enabled = false;
            LoadLandingScene();
        }
        Destroy(gameObject);
    }

    void LoadLandingScene()
    {
        GetComponent<TriggerAIController>().UpdateAIText(index);
    }
}
