using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class v : MonoBehaviour
{
    public Transform target; // target object to move towards to
    public float speed = 10.0f; // speed at which the object will move

    void Update()
    {
        // Check if the target object has been set in the Inspector
        if (target == null)
        {
            Debug.LogError("Target not assigned");
            return;
        }

        // Calculate the next position of the object in 2D space
        Vector2 nextPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Update the object's position
        transform.position = nextPosition;
    }
}
