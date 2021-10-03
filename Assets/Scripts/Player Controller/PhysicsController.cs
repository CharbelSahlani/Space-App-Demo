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
    public Transform orbitalModel;
    public GameObject orion;

    private Rigidbody rb;
    private TrailRenderer trail;
    private Animator anim;

    public bool enableGravity;
    public bool enableDrag;
    public bool sudChange;
    private Vector3 centerOfMass;
    private bool timeScaleLocked = false;
    private bool fuelDisabled = true;

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

    //HUD parameters
    public float totalFuel;
    public float fuelUsage;


    private Vector3 pos;
    private Vector3 dir;
    private Vector3 velDir;
    private float height;
    private float speed2;

    [SerializeField] private bool parachuteKeyPressed = false;
    [SerializeField] private bool parachuteReady = false;
    [SerializeField] private bool parachuteDeployed = false;
    [SerializeField] private bool parachuteActive = false;
    [SerializeField] private bool cushionActive = false;
    [SerializeField] private bool cushionReady = false;
    [SerializeField] private bool torqueActive = false;
    [SerializeField] private bool spinActive = false;

    private float dragCoef;


    //Forces
    /*[HideInInspector]*/
    public Vector3 gravity;
    [HideInInspector] public Vector3 drag;
    [HideInInspector] public Vector3 velChange;

    private string currentSceneName;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponentInChildren<TrailRenderer>();

        //if player is orbiting mars
        if (SceneManager.GetActiveScene().name == sceneNames[0])
        {
            Vector3 initDir = transform.forward * initSpeedOrbiting;
            rb.velocity = initDir;
            GetComponentInChildren<TrailRenderer>().enabled = true;
            GetComponent<CameraSelector>().enabled = true;

            DisableShadows();
            orbitalModel.LookAt(planet);

            GameplayUI.instance.SetMaxFuelVolume(totalFuel);
            GameplayUI.instance.SetMaxAltitude(150f);
            GameplayUI.instance.SetMaxVelocity(6f);
        }

        //if player is landing
        else if (SceneManager.GetActiveScene().name == sceneNames[1])
        {
            Vector3 initDir = transform.forward * initSpeedLanding;
            rb.velocity = initDir;

            GetComponentInChildren<TrailRenderer>().enabled = false;
            GetComponent<CameraSelector>().enabled = false;

            EnableShadows();
            anim = orion.GetComponent<Animator>();
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

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!cushionActive && cushionReady)
                {
                    cushionActive = true;
                    anim.SetTrigger("CushionDeploy");
                }
            }

            /*if (rb.velocity.z < 5)
                torqueActive = true;*/
            if (Mathf.Abs(rb.velocity.y) < 15f && parachuteDeployed && Vector3.Angle(Vector3.up,orion.transform.up)<1f)
            {
                cushionReady = true;
                torqueActive = false;
            }
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
            Debug.Log("v=" + rb.velocity + "\tp=" + transform.position.y);
            LanderKinematicsLanding();
            GravityLanding();
            DragLanding();
            ParachuteLanding();
            //DecelerateLanding();
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

        GameplayUI.instance.SetVelocity(rb.velocity.magnitude);
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
        GameplayUI.instance.SetAltitude(height);

        drag = -d1 * Mathf.Exp(-d2 * height * height) * velDir * rb.velocity.sqrMagnitude;
        if (enableDrag)
            rb.AddForce(drag, ForceMode.Acceleration);
    }

    void DecelerateOrbit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!fuelDisabled)
            {
                rb.AddForce(-thrustOrbiting * Time.fixedDeltaTime * rb.velocity.normalized, ForceMode.VelocityChange);
                totalFuel -= fuelUsage * Time.fixedDeltaTime;
                GameplayUI.instance.SetFuelVolume(totalFuel);
            }
        }
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
    /*
    void DecelerateLanding()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (thrusterActive)
                rb.AddForce(-thrustLanding * Time.fixedDeltaTime * rb.velocity.normalized, ForceMode.VelocityChange);
        }
    }*/

    void DeployParachute()
    {
        parachuteDeployed = true;
        parachuteActive = true;
        parachuteReady = false;
        anim.SetTrigger("ParachuteDeploy");
    }

    void ReleaseParachute()
    {
        parachuteActive = false;
        //thrusterActive = true;
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
                torqueActive = true;
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
                if (transform.eulerAngles.x < 74.9f)
                    rb.AddRelativeTorque(torque * transform.right, ForceMode.Acceleration);
                else
                    transform.eulerAngles = new Vector3(75f, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                if (transform.eulerAngles.x > 10.1f)
                    rb.AddRelativeTorque(-torque * transform.right, ForceMode.Acceleration);
                else
                    transform.eulerAngles = new Vector3(10f, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
    }

    public void LockTimeScale(int index)
    {
        timeScaleLocked = true;
        renderSpeedIndex = index;
    }

    public void UnlockTimeScale(int index)
    {
        timeScaleLocked = false;
        renderSpeedIndex = index;
    }

    public void LockFuelBurn()
    {
        fuelDisabled = true;
    }

    public void UnlockFuelBurn()
    {
        fuelDisabled = false;
    }

    void DisableShadows()
    {
        foreach (Transform child in orion.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
                child.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
                child.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    void EnableShadows()
    {
        foreach (Transform child in orion.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
                child.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
                child.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
