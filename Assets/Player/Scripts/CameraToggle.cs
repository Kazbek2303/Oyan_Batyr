using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera targetCamera; // Reference to the camera you want to toggle

    private void Start()
    {
        if (targetCamera == null)
        {
            Debug.LogError("Target camera is not assigned!");
            return;
        }

        // Initially disable the camera
        targetCamera.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle the camera's enabled state
            targetCamera.enabled = !targetCamera.enabled;
        }
    }

}
