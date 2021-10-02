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
                TooFast();
            else if (momentum < minMomentum)
                TooSlow();
        }
        Destroy(gameObject);
    }

    void TooFast()
    {
        Debug.Log("Too Fast");
    }

    void TooSlow()
    {
        Debug.Log("Too Slow");
    }
}
