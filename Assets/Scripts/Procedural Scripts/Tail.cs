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

    // The distance from one joint to the next joint.
    public float jointDistance;

    // The speed and magnitude of the wiggling of the tail.
    public float wiggleSpeed;
    public float wiggleMagnitude;

    // Whether there is wiggling or not.
    public bool enableWiggle;

    // The head that this tail should follow.
    public Transform target;

    // The target on which the wiggling is based.
    public Transform wiggleTarget;

    // The transforms of the rigidbodies of the tail. One rigidbody corresponds to one joint.
    public Transform[] rigidbodies;

    // The prefab of the rigidbody of the tail.
    public GameObject rigidbodyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // The number of joints of the tail and the number of corresponding rigidbodies
        // are set to be the length of the tail.
        lineRenderer.positionCount = length;
        jointPositions = new Vector3[length];
        jointsVelocities = new Vector3[length];
        rigidbodies = new Transform[length];

        
        // Instantiate the rigidbodies of the tail. The first rigidbody will be the rigidbody
        // of the head, so there is no need to instantiate it.
        rigidbodies[0] = transform;
        for (int i = 1; i < length; i++)
        {
            rigidbodies[i] = Instantiate(rigidbodyPrefab).transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // The position of the first joint is set to be the position of the target (head).
        jointPositions[0] = target.position;

        // Smoothly move each of the joints towards the position of the previous joint.
        for (int i = 1; i < jointPositions.Length; i++)
        {
            Vector3 targetPosition;
            // if (enableWiggle)
            {
                targetPosition = jointPositions[i - 1] - target.up * jointDistance;
            }
            // else
            // {
            //     targetPosition = jointPositions[i - 1] + (jointPositions[i] - jointPositions[i - 1]).normalized * jointDistance;
            // }
            jointPositions[i] = Vector3.SmoothDamp(rigidbodies[i].position, targetPosition, ref jointsVelocities[i], smoothTime);
            rigidbodies[i].position = jointPositions[i];
        }

        // Apply the positions of the joints of the tail.
        lineRenderer.SetPositions(jointPositions);

        if (enableWiggle)
        {
            // Wiggle the tail.
            wiggleTarget.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        }
    }
}
