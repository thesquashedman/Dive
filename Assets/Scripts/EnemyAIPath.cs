// This script is written based on the tutorial video that is provided by Brackeys.
// The link to the video is https://www.youtube.com/watch?v=jvtFUfJ6CP8

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIPath : MonoBehaviour
{
    // The target that the enemy is moving towards.
    public Transform target = null;

    // The position that the enemy is moving towards. Note that only one of the
    // target and targetPosition variables should be used. If target is set, then
    // targetPosition should be not be used. If target is null, then targetPosition
    // should be used.
    public Vector3 targetPosition = new Vector3(0f, 0f, 0f);

    // The distance to move to the next waypoint.
    public float nextWaypointDistance = 2f;

    // The interval for updating the path.
    public float updatePathInterval = 0.2f;

    // The speed with which the enemy moves.
    public float speed = 5f;

    // The speed with which the enemy rotates.
    public float rotationSpeed = 250f;

    // The path that is computed by the A* algorithm.
    private Path path = null;

    // The direction with which the enemy moved.
    public Vector2 direction = Vector2.up;

    // The index of the current waypoint.
    private int currentWaypoint = 0;

    // Whether the enemy has reached the end of the path.
    public bool reachedEndOfPath = false;

    // The reference to the seeker script.
    private Seeker seeker;

    // The reference to the rigidbody of the enemy object.
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        // Generate a path to the target every interval.
        InvokeRepeating("UpdatePath", 0f, updatePathInterval);
    }

    // This function updates the path to the target if the target is not null.
    public void UpdatePath()
    {
        if (seeker.IsDone() && target != null)
        {
            // Compute a new path to the target.
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    // This function updates the path to the target position if the target is null.
    public void SearchPath()
    {
        if (seeker.IsDone() && target == null)
        {
            // Compute a new path to the target position.
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }
    }

    // This function is called when the path is computed. The path p is the
    // computed path.
    private void OnPathComplete(Path p)
    {
        // Check if the path is valid.
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            // The enemy has no path to follow.
            return;
        }
        else if (currentWaypoint >= path.vectorPath.Count)
        {
            // The enemy has reached the end of the path.
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Compute the direction from the current position to the current waypoint.
        direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;

        // Slowly rotate the enemy towards the direction.
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        if (Math.Abs(transform.eulerAngles.z - targetAngle) >= 5f)
        {
            float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0f, 0f, newAngle);
        }

        // Move the enemy towards the current waypoint.
        rigidbody.AddForce(direction * speed);

        // Check whether the enemy should move to the next waypoint.
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
