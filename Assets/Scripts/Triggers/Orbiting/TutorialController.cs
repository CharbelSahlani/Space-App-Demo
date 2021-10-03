using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private GameObject orion;
    public int index;
    public bool randomize;

    //private int randIndex = 0;

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<TriggerAIController>() != null)
        {
            GetComponent<TriggerAIController>().UpdateAIText(index, randomize);
        }
    }
}
