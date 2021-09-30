using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessing : MonoBehaviour
{
    private PhysicsController phy;

    public float h_p;
    public float orbitalPeriod;

    [HideInInspector] public float initSpeed;
    [HideInInspector] public float speedAtPeriapsis;
    [HideInInspector] public float speedForCapture;

    [HideInInspector] public float mu;
    [HideInInspector] public float semMajAxis;
    [HideInInspector] public float planetRad;
    [HideInInspector] public float periRad;
    [HideInInspector] public float apoRad;
    [HideInInspector] public float ecc;
    [HideInInspector] public float aimRad;

    void Awake()
    {
        phy = GetComponent<PhysicsController>();
        mu = phy.G * phy.planetMass;
        initSpeed = phy.initSpeed;

        semMajAxis = Mathf.Pow(orbitalPeriod * Mathf.Sqrt(mu) / (2f * Mathf.PI), 2f / 3f);
        planetRad = phy.radius;
        periRad = planetRad + h_p;
        apoRad = 2 * semMajAxis - periRad;
        ecc = (apoRad - periRad) / (apoRad + periRad);
        aimRad = periRad * Mathf.Sqrt(1 + (2f * mu) / (periRad * initSpeed * initSpeed));
        speedAtPeriapsis = Mathf.Sqrt(initSpeed * initSpeed + 2f * mu / periRad);
        speedForCapture = Mathf.Sqrt(mu * (1f + ecc) / periRad);

        Debug.Log("mu= " + mu.ToString());
        Debug.Log("a= " + semMajAxis.ToString());
        Debug.Log("Rp= " + periRad.ToString());
        Debug.Log("Ra= " + apoRad.ToString());
        Debug.Log("e= " + ecc.ToString());
        Debug.Log("aimRad= " + aimRad.ToString());
        Debug.Log("v_hyp= " + speedAtPeriapsis.ToString());
        Debug.Log("v_cap= " + speedForCapture.ToString());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
