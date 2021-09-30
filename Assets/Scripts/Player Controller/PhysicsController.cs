using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsController : MonoBehaviour
{
    //Scenes to check which physics law to apply on the player
    [Tooltip("Element 0: Orbit / Element 1: Landing")]
    public string[] sceneNames = new string[2];

    public float initSpeed;
    public float renderSpeed;
    public Transform planet;
    private Rigidbody rb;
    public bool enableGravity;
    public bool enableDrag;
    public bool sudChange;


    //Planet parameters
    public float planetMass;
    public float G;
    public float d1;
    public float d2;
    public float d3;
    public float parachuteDrag;
    public float dist;
    public float radius;


    //Kinematic parameters
    private Vector3 pos;
    private Vector3 dir;
    private Vector3 velDir;
    private float height;
    private float speed2;
    [SerializeField] private bool parachuteDeployed = false;
    [SerializeField] private bool parachuteActive = false;
    private float dragCoef;


    //Forces
    [HideInInspector] public Vector3 gravity;
    [HideInInspector] public Vector3 drag;
    [HideInInspector] public Vector3 velChange;
    public float thrust;

    private string currentSceneName;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = Vector3.forward * initSpeed;
        rb.velocity = dir;
    }

    void FixedUpdate()
    {
        Time.timeScale = renderSpeed;

        //if player is orbiting mars
        if (SceneManager.GetActiveScene().name == sceneNames[0])
        {
            LanderKinematicsOrbit();
            GravityOrbit();
            DragOrbit();
            DecelerateOrbit();
        }
        else if (SceneManager.GetActiveScene().name == sceneNames[1])
        {
            LanderKinematicsLanding();
            GravityLanding();
            DragLanding();
            ParachuteLanding();
            DecelerateLanding();
        }
    }

    /*
     * Physics in orbit
     */
    void LanderKinematicsOrbit()
    {
        if (planet != null)
            pos = transform.position - planet.transform.position;
        dist = pos.magnitude;
        dir = pos.normalized;
        velDir = rb.velocity.normalized;
    }

    void GravityOrbit()
    {
        gravity = -G * planetMass / (dist * dist) * dir;

        if (enableGravity)
            rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void DragOrbit()
    {
        height = dist - radius;
        drag = -d1 * Mathf.Exp(-d2 * height * height) * velDir * rb.velocity.sqrMagnitude;
        if (enableDrag)
            rb.AddForce(drag, ForceMode.Acceleration);
    }

    void DecelerateOrbit()
    {
        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(-thrust * Time.fixedDeltaTime * rb.velocity.normalized, ForceMode.VelocityChange);
    }


    /*
    * Physics in orbit
    */
    void LanderKinematicsLanding()
    {
        if (planet != null)
            pos = transform.position - planet.transform.position;
        dist = pos.y;
        dir = Vector3.up;
        velDir = rb.velocity.normalized;
    }

    void GravityLanding()
    {
        gravity = -G * dir * Time.fixedDeltaTime;

        if (enableGravity)
            rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void DragLanding()
    {
        height = dist;
        dragCoef = d3 / Mathf.Abs(height);
        dragCoef = Mathf.Clamp(dragCoef, 0, 1);

        if (enableDrag)
            rb.drag = dragCoef;
    }

    void ParachuteLanding()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!parachuteDeployed)
            {
                DeployParachute();
            }
            else if (parachuteActive)
            {
                ReleaseParachute();
            }
        }

        if (parachuteActive)
        {
            dragCoef *= parachuteDrag;
            dragCoef = Mathf.Clamp(dragCoef, 0, 1);
            rb.drag = dragCoef;
        }
    }

    void DecelerateLanding()
    {
        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(-thrust * Time.fixedDeltaTime * rb.velocity.normalized, ForceMode.VelocityChange);
    }

    void DeployParachute()
    {
        parachuteDeployed = true;
        parachuteActive = true;

    }

    void ReleaseParachute()
    {
        parachuteActive = false;

    }
}
