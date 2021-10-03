using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 orbitOffset;
    public Vector3 landingOffset;
    public Transform orion;
    [HideInInspector] public float multiplier;
    private Transform planet;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("FollowerCamera").GetComponent<Camera>();
        cam.GetComponent<AudioListener>().enabled = false;
        orion = GameObject.FindGameObjectWithTag("Orion").transform;

        if (GetComponent<PhysicsController>().planet != null)
        {
            planet = GetComponent<PhysicsController>().planet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (planet != null)
        {
            cam.transform.LookAt(planet);
            Vector3 dir = orion.position - planet.transform.position;
            dir = Vector3.Normalize(dir);
            cam.transform.position = orion.position + dir * 5f + orbitOffset;
        }
        else
        {
            cam.transform.position = orion.position + landingOffset * (1 + multiplier);
            cam.transform.LookAt(orion.position);
        }
    }
}
