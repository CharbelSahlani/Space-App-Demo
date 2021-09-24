using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Text distText;
    public Text minDistText;
    public Text maxDistText;
    public Text speedText;
    public Text gravText;
    public Text dragText;

    private Rigidbody rb;
    private PhysicsController phy;
    public bool resetting;

    private float dist;
    private float minDist = float.MaxValue;
    private float maxDist = 0;
    private float drag;
    private float gravity;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        phy = GetComponent<PhysicsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetting)
        {
            ResetUI();
            resetting = false;
        }

        dist = phy.dist;

        maxDist = (dist > maxDist) ? dist : maxDist;
        minDist = (dist < minDist) ? dist : minDist;
        speed = rb.velocity.magnitude;
        gravity = phy.gravity.magnitude;
        drag = phy.drag.magnitude;

        distText.text = "Dist: " + dist.ToString();
        minDistText.text = "Min: " + minDist.ToString();
        maxDistText.text = "Max: " + maxDist.ToString();
        speedText.text = "V: " + speed.ToString();
        gravText.text = "g:" + gravity.ToString();
        dragText.text = "d: " + drag.ToString();
    }

    void ResetUI()
    {
        maxDist = 0;
        minDist = 1000;
    }
}
