using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().planet;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(planet.transform);
        Vector3 dir = transform.parent.transform.position - planet.transform.position;
        dir = Vector3.Normalize(dir);
        transform.position = transform.parent.transform.position + dir * 5f + Vector3.up * 2f;
    }
}
