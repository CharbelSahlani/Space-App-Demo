using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitVerifier : MonoBehaviour
{
    public float minMomentum;
    public float maxMomentum;

    private GameObject orion;
    private float speed;
    private float dist;
    private float momentum;
    private int index;

    void OnTriggerEnter(Collider other)
    {
        orion = GameObject.FindGameObjectWithTag("Orion");

        if (orion != null)
        {
            speed = orion.GetComponent<Rigidbody>().velocity.magnitude;
            dist = orion.GetComponent<PhysicsController>().dist;
            momentum = speed * dist;
            Debug.Log("h= " + momentum.ToString());

            if (momentum > maxMomentum)
            {
                index = 10;

            }
            else if (momentum < minMomentum)
            {
                index = 9;
            }
            else
                index = 0;
        }

        GetComponent<TriggerAIController>().UpdateAIText(index, false);
        //Destroy(gameObject);
    }

    
}
