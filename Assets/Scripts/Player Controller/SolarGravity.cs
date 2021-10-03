using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class SolarGravity : MonoBehaviour
{
    private float[] distances = new float[8] { 57.9f, 108.2f, 149.6f, 227.9f, 778.6f, 1433.5f, 2872.5f, 4495.1f };
    private float[] speeds = new float[8] { 4.74f, 3.5f, 2.98f, 2.41f, 1.31f, 0.97f, 0.68f, 0.54f };
    private string[] planets = new string[8] { "mercury", "venus", "earth", "mars", "jupiter", "saturn", "uranus", "neptune" };


    private float multiplier = 2.0f;
    private Rigidbody rb;
    private TrailRenderer trail;
    private Transform sun;
    private float gravity = 1300;

    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 position;
    [SerializeField] private float dist;
    [SerializeField] private Vector3 force;
    private bool initial = true;

    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        trail = GetComponent<TrailRenderer>();
        trail.time = 50;
        trail.widthMultiplier = 0.2f;
        trail.enabled = false;

        for (int i = 0; i < 8; i++)
        {
            if (transform.name == planets[i])
            {
                Vector3 temp = transform.position;
                temp.x = multiplier * Mathf.Sqrt(distances[i]);
                transform.position = temp;
                rb.velocity = speeds[i] * Mathf.Sqrt(temp.x) / multiplier * transform.forward;
            }
        }
        Time.timeScale = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.unscaledTime > 2 && initial)
        {
            initial = false;
            trail.enabled = true;
        }

        position = transform.position - sun.position;
        direction = position.normalized;
        dist = position.sqrMagnitude;

        force = -gravity * direction / dist;
        rb.AddForce(force, ForceMode.Acceleration);
    }
}
