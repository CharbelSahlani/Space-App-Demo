using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = Vector3.forward * speed;
        rb.velocity = dir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
