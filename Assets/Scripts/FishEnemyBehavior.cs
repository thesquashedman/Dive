using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FishEnemyBehavior : MonoBehaviour
{
    public GameObject player;

    // Variables for the path and movement.
    protected AIPath aiPath;
    protected float speed = 5f;
    protected float acceleration = 10f;

    // Variables for stuck detection and struggling.
    protected Vector3 previousPosition;
    protected float timer = 0f;
    protected float struggleTime = 0.5f;
    protected float struggleIntensity = 20f;
    protected bool isStuck = false;
    protected bool isAsleep = false;
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
        CheckStuck();
    }

    // This function verifies whether this enemy is stuck and needs to struggle.
    // If the enemy is stuck, this function calls the struggle function.
    protected void CheckStuck()
    {
        if (!isStuck)
        {
            // If the enemy is not moving for half a second, is not close to the
            // main character, and is not asleep, the enemy is stuck.
            if (Vector3.Distance(transform.position, previousPosition) < 0.005f)
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance > 1f && !isAsleep)
                    {
                        isStuck = true;
                        timer = 0f;
                    }
                }
            }
            // If the enemy is moving, reset the timer.
            else
            {
                timer = 0f;
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
        timer += Time.deltaTime;
        if (timer < struggleTime)
        {
            // Choose a random direction.
            Vector3 direction = unitVectors[Random.Range(0, unitVectors.Length)];

            // Move in that direction.
            transform.position += direction * Time.deltaTime * struggleIntensity;
        }
        else
        {
            // Reset the timer and the speed for the AI path.
            timer = 0f;
            aiPath.maxSpeed = speed;
            isStuck = false;
        }
    }
}
