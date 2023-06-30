// This script is written based on the tutorial provided by Blackthornprod.
// The link to the tutorial is https://www.youtube.com/watch?v=9hTnlp9_wX8

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    // The length of the tail.
    public int length;

    public LineRenderer lineRenderer;

    // The positions of the joints of the tail.
    private Vector3[] jointPositions;

    // The velocities for the joints of the tail.
    private Vector3[] jointsVelocities;

    // How fast the joints move towards the position of the previous joint.
    public float smoothTime;

    // The variable that is used in the function that slows the movement of the
    // joints that are further away from the head.
    public float smoothTimeDivisor;

    // The distance from one joint to the next joint.
    public float jointDistance;

    // The speed and magnitude of the wiggle of the tail.
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleTarget;

    // The head that this tail should follow.
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // The number of joints of the tail is set to be the length of the tail.
        lineRenderer.positionCount = length;
        jointPositions = new Vector3[length];
        jointsVelocities = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        // Wiggle the tail.
        wiggleTarget.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        
        // The position of the first joint is set to be the position of the target (head).
        jointPositions[0] = target.position;

        // Smoothly move each of the joints towards the position of the previous joint.
        for (int i = 1; i < jointPositions.Length; i++)
        {
            jointPositions[i] = Vector3.SmoothDamp(jointPositions[i], jointPositions[i - 1] - target.up * jointDistance, ref jointsVelocities[i], smoothTime + i / smoothTimeDivisor);
        }

        // Apply the positions of the joints of the tail.
        lineRenderer.SetPositions(jointPositions);
    }
}
