using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsController : MonoBehaviour
{
    //Scenes to check which physics law to apply on the player
    [Tooltip("Element 0: Orbit / Element 1: Landing")]
    public string[] sceneNames = new string[2];
    public float[] renderSpeeds;
    [SerializeField] private int renderSpeedIndex;

    public Transform planet;
    private Rigidbody rb;
    private TrailRenderer trail;
    public bool enableGravity;
    public bool enableDrag;
    public bool sudChange;
    private Vector3 centerOfMass;
    private bool timeScaleLocked = false;
    private Transform orbitalModel;


    //Planet parameters
    public float planetMass;
    public float G;
    public float d1;
    public float d2;
    public float gSurf;
    public float dSurf;
    public float parachuteDrag;
    public float dist = 1000;
    public float radius;


    //Kinematic parameters
    public float initSpeedOrbiting;
    public float initSpeedLanding;
    public float thrustOrbiting;
    public float thrustLanding;
    public float torque;


    private Vector3 pos;
    private Vector3 dir;
    private Vector3 velDir;
    private float height;
    private float speed2;

    [SerializeField] private bool parachuteKeyPressed = false;
    [SerializeField] private bool parachuteReady = false;
    [SerializeField] private bool parachuteDeployed = false;
    [SerializeField] private bool parachuteActive = false;
    [SerializeField] private bool thrusterActive = false;
    [SerializeField] private bool torqueActive = false;
    [SerializeField] private bool spinActive = false;

    private float dragCoef;


    //Forces
    [HideInInspector] public Vector3 gravity;
    [HideInInspector] public Vector3 drag;
    [HideInInspector] public Vector3 velChange;

    private string currentSceneName;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponentInChildren<TrailRenderer>();

        foreach (Transform t in transform)
        {
            if (t.CompareTag("Model"))
                orbitalModel = t;
        }

        //if player is orbiting mars
        if (SceneManager.GetActiveScene().name == sceneNames[0])
        {
            Vector3 initDir = transform.forward * initSpeedOrbiting;
            rb.velocity = initDir;
            GetComponentInChildren<TrailRenderer>().enabled = true;
            GetComponent<UIController>().enabled = true;
            GetComponent<CameraSelector>().enabled = true;

            foreach (Transform child in orbitalModel.GetChild(0))
            {
                child.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            orbitalModel.LookAt(planet);
        }

        //if player is landing
        else if (SceneManager.GetActiveScene().name == sceneNames[1])
        {
            Vector3 initDir = transform.forward * initSpeedLanding;
            rb.velocity = initDir;

            GetComponentInChildren<TrailRenderer>().enabled = false;
            GetComponent<UIController>().enabled = false;
            GetComponent<CameraSelector>().enabled = false;
        }

        foreach (Transform child in orbitalModel.GetChild(0))
        {
            child.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if (GetComponent<BoxCollider>() != null)
            centerOfMass = GetComponent<BoxCollider>().center;
        renderSpeedIndex = 1;
    }

    void Update()
    {
        //if player is orbiting mars
        if (SceneManager.GetActiveScene().name == sceneNames[0])
        {
            if (!timeScaleLocked)
            {
                if (Input.GetKeyDown(KeyCode.Minus))
                {
                    renderSpeedIndex = (renderSpeedIndex == 0) ? 0 : renderSpeedIndex - 1;
                }

                if (Input.GetKeyDown(KeyCode.Equals))
                {
                    renderSpeedIndex = (renderSpeedIndex >= renderSpeeds.Length - 1) ? renderSpeedIndex : renderSpeedIndex + 1;
                }
            }

            orbitalModel.LookAt(planet);
        }

        //if player is landing
        else if (SceneManager.GetActiveScene().name == sceneNames[1])
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (parachuteReady || parachuteDeployed)
                    parachuteKeyPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                spinActive = true;
            }

            if (rb.velocity.z < 5)
                torqueActive = true;
        }

        Time.timeScale = renderSpeeds[renderSpeedIndex];
    }

    void FixedUpdate()
    {

        //if player is orbiting mars
        if (SceneManager.GetActiveScene().name == sceneNames[0])
        {
            LanderKinematicsOrbit();
            GravityOrbit();
            DragOrbit();
            DecelerateOrbit();
        }
        //if player is landing
        else if (SceneManager.GetActiveScene().name == sceneNames[1])
        {
            Debug.Log("v=" + rb.velocity.magnitude.ToString());
            Debug.Log("vz=" + rb.velocity.z.ToString());
            LanderKinematicsLanding();
            GravityLanding();
            DragLanding();
            ParachuteLanding();
            DecelerateLanding();
            StopSpinning();
            ApplyStabilizingTorque();
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
            rb.AddForce(-thrustOrbiting * Time.fixedDeltaTime * rb.velocity.normalized, ForceMode.VelocityChange);
    }


    /*
    * Physics near surface
    */
    void LanderKinematicsLanding()
    {
        dist = transform.position.y;
        dir = Vector3.up;
        velDir = rb.velocity.normalized;
    }

    void GravityLanding()
    {
        gravity = -gSurf * dir;// * Time.fixedDeltaTime;

        if (enableGravity)
            rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void DragLanding()
    {
        height = dist;
        dragCoef = dSurf / Mathf.Sqrt(height);
        dragCoef = Mathf.Clamp(dragCoef, 0, 1);

        if (enableDrag)
            rb.drag = dragCoef;
    }

    void ParachuteLanding()
    {
        if (parachuteKeyPressed)
        {
            parachuteKeyPressed = false;
            if (!parachuteDeployed && parachuteReady)
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
        {
            if (thrusterActive)
                rb.AddForce(-thrustLanding * Time.fixedDeltaTime * rb.velocity.normalized, ForceMode.VelocityChange);
        }
    }

    void DeployParachute()
    {
        parachuteDeployed = true;
        parachuteActive = true;
        parachuteReady = false;

    }

    void ReleaseParachute()
    {
        parachuteActive = false;
        thrusterActive = true;
    }

    void StopSpinning()
    {
        float spd = orbitalModel.GetComponentInChildren<Spin>().speed;

        if (spinActive)
        {
            if (spd > 0)
                spd -= 2f * Time.fixedDeltaTime;
            else
            {
                spd = 0;
                spinActive = false;
                parachuteReady = true;
            }

            orbitalModel.GetComponentInChildren<Spin>().speed = spd;
        }
    }

    void ApplyStabilizingTorque()
    {
        if (torqueActive)
        {
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeTorque(torque * transform.right, ForceMode.Acceleration);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeTorque(-torque * transform.right, ForceMode.Acceleration);
            }
        }
    }

    public void LockTimeScale()
    {
        timeScaleLocked = true;
        renderSpeedIndex = 0;
    }

    public void UnlockTimeScale()
    {
        timeScaleLocked = false;
        renderSpeedIndex = 1;
    }

}
