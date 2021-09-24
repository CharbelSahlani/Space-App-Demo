using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public float renderSpeed;
    public Transform planet;
    private Rigidbody rb;
    public bool enableGravity;
    public bool enableDrag;

    //Planet parameters
    public float planetMass;
    public float G;
    public float d1;
    public float d2;
    public float dist;
    public float radius;

    //Kinematic parameters
    private Vector3 pos;
    private Vector3 dir;
    private Vector3 velDir;
    private float height;
    private float speed2;

    //Forces
    [HideInInspector] public Vector3 gravity;
    [HideInInspector] public Vector3 drag;
    [HideInInspector] public Vector3 velChange;
    public float thrust;

    /*
    public bool trigger = false;
    public bool signChange = false;
    private float oldSign, newSign;
    public float newSpeed;
    */


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Time.timeScale = renderSpeed;

        LanderKinematics();
        if (enableGravity)
            Gravity();
        if (enableDrag)
            Drag();
        DeltaV();
    }

    void LanderKinematics()
    {
        pos = transform.position - planet.transform.position;
        dist = pos.magnitude;
        dir = pos.normalized;
        velDir = rb.velocity.normalized;

        //speed2 = rb.velocity.sqrMagnitude;
    }

    void Gravity()
    {
        gravity = -G * planetMass / (dist * dist) * dir;

        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void Drag()
    {
        height = dist - radius;
        drag = -d1 * Mathf.Exp(-d2 * height) * velDir * rb.velocity.sqrMagnitude;

        rb.AddForce(drag, ForceMode.Acceleration);
    }

    void DeltaV()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            velChange = -thrust * Time.fixedDeltaTime * velDir;

            rb.AddForce(velChange, ForceMode.VelocityChange);
        }
    }
}
