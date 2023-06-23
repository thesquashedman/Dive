using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The character that the camera will follow
    public float distance = 10f; // The distance the camera will keep from the character

    void Update()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Set the camera's position to follow the target while maintaining the distance
            transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
        }
    }
}
