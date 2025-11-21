using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Public variable to control how fast the camera moves
    public float cameraSpeed;

    // Update is called once per frame
    void Update()
    {
        // Continuously move the camera forward along the X-axis (right)
        // at a constant speed, relative to the time passed since the last frame.
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }
}