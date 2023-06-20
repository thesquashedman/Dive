using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FishEnemyBehavior : MonoBehaviour
{
    public GameObject player;

    // The behavior mode of this enemy. The possible modes are the following ones:
    // 1. attack: This enemy will chase and attack the player. This enemy will struggle
    // if it gets stuck along the way.
    //
    // 2. idle: This enemy will not move towards the player; it will stay around a
    // certain area.
    //
    // 3. runAway: This enemy will run away from the player.
    protected string mode = "attack";

    // Variables for the path and movement.
    protected AIPath aiPath;
    protected float speed = 5f;
    protected float acceleration = 10f;

    // Variables for stuck detection and struggling.
    protected Vector3 previousPosition;
    protected float struggleTimer = 0f;
    protected float struggleTime = 0.5f;
    protected float struggleIntensity = 20f;
    protected bool isStuck = false;
    protected Vector3[] unitVectors = new Vector3[] {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        previousPosition = transform.position;
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = speed;
        aiPath.maxAcceleration = acceleration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (mode == "attack")
        {
            CheckStuck();
        }
        else if (mode == "runAway")
        {
            RunAway();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of the enemy.
    protected void SwitchMode(string newMode)
    {
        if (newMode == "attack")
        {
            mode = "attack";
            aiPath.maxSpeed = speed;
        }
        else if (newMode == "idle")
        {
            mode = "idle";
            aiPath.maxSpeed = 0f;
        }
        else if (newMode == "runAway")
        {
            mode = "runAway";
            aiPath.maxSpeed = 0f;
        }
    }

    // This function verifies whether this enemy is stuck and needs to struggle.
    // If the enemy is stuck, this function calls the struggle function.
    protected void CheckStuck()
    {
        if (!isStuck)
        {
            // If the enemy is not moving for half a second and is not close to the
            // main character, the enemy is stuck.
            if (Vector3.Distance(transform.position, previousPosition) < 0.005f)
            {
                struggleTimer += Time.deltaTime;
                if (struggleTimer >= 0.5f)
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance > 1.5f)
                    {
                        isStuck = true;
                        struggleTimer = 0f;
                    }
                }
            }
            // If the enemy is moving, reset the struggleTimer.
            else
            {
                struggleTimer = 0f;
            }
            previousPosition = transform.position;
        }
        else
        {
            // If the enemy is stuck, struggle.
            Struggle();
        }
    }

    // This function is called when the enemy gets stuck with another object.
    // This function makes the enemy struggles for a short time.
    protected void Struggle()
    {
        aiPath.maxSpeed = 0f;
        struggleTimer += Time.deltaTime;
        if (struggleTimer < struggleTime)
        {
            // Choose a random direction.
            Vector3 direction = unitVectors[Random.Range(0, unitVectors.Length)];

            // Move in that direction.
            transform.position += direction * Time.deltaTime * struggleIntensity;
        }
        else
        {
            // Reset the struggleTimer and the speed for the AI path.
            struggleTimer = 0f;
            aiPath.maxSpeed = speed;
            isStuck = false;
        }
    }

    // This function makes the enemy run away from the player.
    protected virtual void RunAway()
    {
        // Find the direction away from the player.
        Vector3 direction = (transform.position - player.transform.position).normalized;

        // Move in that direction.
        transform.position += direction * Time.deltaTime * speed;
    }
}
