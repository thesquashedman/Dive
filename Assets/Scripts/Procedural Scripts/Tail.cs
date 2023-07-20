// This script is written based on the tutorial provided by Blackthornprod.
// The link to the tutorial is https://www.youtube.com/watch?v=9hTnlp9_wX8

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    // The number of joints of the tail.
    public int length;

    public LineRenderer lineRenderer;

    // The theoratical positions of the joints of the tail. These are used for
    // procedural animation computation.
    private Vector3[] jointPositions;

    // The velocities for the joints of the tail. These are used for procedural
    // animation computation.
    private Vector3[] jointsVelocities;

    // How fast the joints should move towards the position of the previous joint.
    public float smoothTime;

    // The distance from one joint to the next joint.
    public float jointDistance;

    // The distance from the head to the first joint.
    public float headDistance;

    // The speed and magnitude of the wiggling of the tail.
    public float wiggleSpeed;
    public float wiggleMagnitude;

    // Whether there is wiggling or not.
    public bool enableWiggle;

    // The head that this tail should follow.
    public Transform target;

    // The target on which the wiggling is based.
    public Transform wiggleTarget;

    // The game objects that have the rigidbodies of the tail. One rigidbody
    // corresponds to one joint, and the positions of these game objects are
    // the actual positions of the joints.
    public GameObject[] rigidbodies;

    // The prefab of the game boject that has the rigidbody of the tail.
    public GameObject rigidbodyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // The number of joints of the tail and the number of corresponding rigidbodies
        // are the length.
        lineRenderer.positionCount = length;
        jointPositions = new Vector3[length];
        jointsVelocities = new Vector3[length];
        rigidbodies = new GameObject[length];

        // Instantiate the rigidbodies for the tail. The first rigidbody will be the rigidbody
        // of the head, so there is no need to instantiate it.
        int objectID = transform.parent.gameObject.GetInstanceID();
        rigidbodies[0] = new GameObject();
        rigidbodies[0].transform.position = transform.position;
        for (int i = 1; i < length; i++)
        {
            rigidbodies[i] = Instantiate(rigidbodyPrefab);
            rigidbodies[i].transform.position = transform.position;
            rigidbodies[i].GetComponent<BodyPart>().enemyID = objectID;
        }
    }

    void FixedUpdate()
    {
        // The position of the first joint is to be next to the position of the target (head).
        jointPositions[0] = target.position - target.up * headDistance;
        rigidbodies[0].transform.position = jointPositions[0];

        // Compute the smooth movemenet from each of the joints to the previous joint.
        for (int i = 1; i < jointPositions.Length; i++)
        {
            // Compute the theoratical position of the joint based on the actual position.
            Vector3 targetPosition = rigidbodies[i - 1].transform.position - target.up * jointDistance;
            jointPositions[i] = Vector3.SmoothDamp(rigidbodies[i].transform.position, targetPosition, ref jointsVelocities[i], smoothTime);

            // Compute the vector from the actual position of the joint to the theoratical position of the joint.
            Vector3 toTheoraticalPosition = ((Vector2)jointPositions[i] - (Vector2)rigidbodies[i].transform.position);

            // Move the actual position of the joint to the theoratical position.
            if (toTheoraticalPosition.magnitude > jointDistance)
            {
                rigidbodies[i].transform.position = jointPositions[i];
                rigidbodies[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            else
            {
                rigidbodies[i].GetComponent<Rigidbody2D>().velocity = (toTheoraticalPosition * (1f / Time.deltaTime));
            }
        }

        // Apply the theoratical positions of the joints to the tail.
        lineRenderer.SetPositions(jointPositions);

        if (enableWiggle)
        {
            // Wiggle the tail.
            wiggleTarget.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        }
    }

    // Delete all the rigidbodies of the tail when the eel is deleted or dead.
}
