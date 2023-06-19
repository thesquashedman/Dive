// This script is written based on the tutorial video that is provided by Brackeys.
// The link to the video is: https://www.youtube.com/watch?v=jvtFUfJ6CP8

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIBehavior : MonoBehaviour
{
    public Transform target;
    public float nextWaypointDistance = 1f;
    private float speed = 3f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    // private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        // rigidbody = GetComponent<Rigidbody2D>();

        // Generate a path to the target every half a second.
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // This function is called every half a second.
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            // Compute a new path to the target.
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    // This function is called when the path is computed. The path p is the
    // computed path.
    void OnPathComplete(Path p)
    {
        // Check if the path is valid.
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
        {
            // We have no path to follow.
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            // We have reached the end of the path.
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Compute the direction from the current position to the current waypoint.
        Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint] - transform.position).normalized;

        // Compute and apply the translation that is needed to move the enemy towards the current waypoint.
        Vector3 translation = direction * speed * Time.deltaTime;
        print(translation);
        transform.Translate(translation);

        // Check whether the enemy should move to the next waypoint.
        float distance = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
