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

    // The head that this tail should follow.
    public Transform target;

    // The target to make it wiggle.
    public Transform wiggleTarget;

    // The transforms of the rigidbodies of the tail.
    public Transform[] rigidbodies;

    // The numbers of rigidbodies of the tail.
    public int rigidbodyCount;

    private int mod;

    // Start is called before the first frame update
    void Start()
    {
        // The number of joints of the tail is set to be the length of the tail.
        lineRenderer.positionCount = length;
        jointPositions = new Vector3[length];
        jointsVelocities = new Vector3[length];

        /*
        for (int i = 0; i < rigidbodyCount; i++)
        {
            GameObject newObject = new GameObject("Rigidbody" + i, typeof(Rigidbody2D), typeof(CircleCollider2D));
            newObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            newObject.GetComponent<CircleCollider2D>().radius = 0.5f;
            rigidbodies[i] = newObject.transform;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        // Wiggle the tail.
        wiggleTarget.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        // The position of the first joint is set to be the position of the target (head).
        jointPositions[0] = target.position;
        rigidbodies[0].position = jointPositions[0];

        // Smoothly move each of the joints towards the position of the previous joint.
        for (int i = 1; i < jointPositions.Length; i++)
        {
            Vector3 targetPosition = jointPositions[i - 1] - transform.up * jointDistance;
            jointPositions[i] = Vector3.SmoothDamp(rigidbodies[i].position, targetPosition, ref jointsVelocities[i], smoothTime + i / smoothTimeDivisor);
            rigidbodies[i].position = jointPositions[i];
        }

        // Apply the positions of the joints of the tail.
        lineRenderer.SetPositions(jointPositions);
    }
}
