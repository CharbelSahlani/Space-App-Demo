using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public Camera[] cams;
    public Canvas canvas;

    private int index;
    private int arrayLength;

    private Transform planet;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhysicsController>().planet != null)
        {
            index = 0;
            arrayLength = cams.Length;
            CycleCameras();
        }
        else
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            index = (index == arrayLength - 1) ? 0 : index + 1;
            CycleCameras();
        }
    }

    void CycleCameras()
    {
        foreach (Camera c in cams)
        {
            if (c == null)
                continue;
            c.enabled = false;
        }
        if (cams[index] != null)
            cams[index].enabled = true;
    }
}