using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public Camera[] cams;
    public Canvas canvas;

    private int index;
    private int arrayLength;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        arrayLength = cams.Length;
        CycleCameras();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            index = (index == arrayLength-1) ? 0 : index + 1;
            CycleCameras();
        }
    }

    void CycleCameras()
    {
        foreach (Camera c in cams)
            c.enabled = false;

        cams[index].enabled = true;
    }
}
