using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform planet;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        if (GetComponent<PhysicsController>().planet != null)
        {
            planet = GetComponent<PhysicsController>().planet;
        }
        else
        {
            cam.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (planet != null)
        {
            cam.transform.LookAt(planet);
            Vector3 dir = transform.transform.position - planet.transform.position;
            dir = Vector3.Normalize(dir);
            cam.transform.position = transform.transform.position + dir * 5f + Vector3.up * 2f;
        }
    }
}
