using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotationSpeed = 5.0f;
    public float zoomSpeed = 3.0f;
    public float minZoomDistance = 1.5f;
    public float maxZoomDistance = 15f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        // Mouse rotation
        if (Input.GetMouseButton(1)) // Right mouse button held
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            yaw += mouseX;
            pitch += mouseY; 
            pitch = Mathf.Clamp(pitch, -89f, 89f); // Avoid flipping
        }

        // Zoom with scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            float offsetMagnitude = offset.magnitude;
            offsetMagnitude -= scrollInput * zoomSpeed;
            offsetMagnitude = Mathf.Clamp(offsetMagnitude, minZoomDistance, maxZoomDistance);
            offset = offset.normalized * offsetMagnitude;
        }

        // Apply rotation and follow
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target);
    }
}
