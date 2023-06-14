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
    public float nextWaypointDistance = 3f;
    private float speed = 3f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        // Generate a path to the target.
        seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
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
        
    }
}
