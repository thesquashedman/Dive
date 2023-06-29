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
    // 2. coolDown: This enemy will stay around a certain area and exhibit an
    // intermediate behavior before switching to other modes.
    //
    // 3. runAway: This enemy will run away from the player.
    //
    // 4. wander: This enemy will wander around a certain area of the map.
    //
    // 5. idle: This enemy will stay at a certain position and do nothing.
    protected string mode = "attack";

    // Variables for the path and movement.
    // protected Rigidbody2D rb;
    protected AIPath aiPath;
    protected AIDestinationSetter aiDestinationSetter;
    protected float speed = 5f;
    protected float acceleration = 10f;

    // The maximum angle in radians that the direction of this enemy can turn in a second.
    protected float deltaAngle = Mathf.PI;

    // The time and decceleration for this enemy to slow down. The decceleration will not
    // be a fixed value because of the SmoothDamp function.
    protected float slowDownTime = 0.4f;
    protected float decceleration = -10f;

    // Variables for stuck detection and struggling.
    protected Vector3 previousPosition;
    protected float struggleTimer = 0f;
    protected float struggleTime = 0.5f;
    protected float struggleIntensity = 20f;
    protected bool isStuck = false;
    protected Vector3[] unitVectors = new Vector3[] {Vector3.up, Vector3.down, Vector3.left, Vector3.right};

    // Variables for wandering. These variables are used to compute the area that the
    // enemy can wander around and set the speed.
    public GameObject habitat; // This is the habitat of this enemy.
    public float wanderingAreaWidth = 10f;
    public float wanderingAreaHeight = 10f;
    protected float wanderingSpeed = 5f;
    protected float wanderingAcceleration = 10f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        previousPosition = transform.position;
        // rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
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
        else if (mode == "coolDown")
        {
            StayAround();
        }
        else if (mode == "runAway")
        {
            RunAway();
        }
        else if (mode == "wander")
        {
            Wander();
        }
        else if (mode == "idle")
        {
            Idle();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of the enemy.
    protected virtual void SwitchMode(string newMode)
    {
        if (newMode == "attack")
        {
            mode = "attack";
            aiPath.maxSpeed = speed;
            aiPath.maxAcceleration = acceleration;
            aiPath.enableRotation = true;
            aiDestinationSetter.target = player.transform;
        }
        else if (newMode == "coolDown")
        {
            mode = "coolDown";
            aiPath.maxSpeed = 0f;
            aiPath.maxAcceleration = 0f;
            aiPath.enableRotation = false;
            aiDestinationSetter.target = null;
        }
        else if (newMode == "runAway")
        {
            mode = "runAway";
            aiPath.maxSpeed = 0f;
            aiPath.maxAcceleration = 0f;
            aiPath.enableRotation = false;
            aiDestinationSetter.target = null;
        }
        else if (newMode == "wander")
        {
            mode = "wander";
            aiPath.maxSpeed = wanderingSpeed;
            aiPath.maxAcceleration = wanderingAcceleration;
            aiPath.enableRotation = true;
            aiDestinationSetter.target = null;
        }
        else if (newMode == "idle")
        {
            mode = "idle";
            aiPath.maxSpeed = 0f;
            aiPath.maxAcceleration = 0f;
            aiPath.enableRotation = false;
            aiDestinationSetter.target = null;
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

    protected virtual void StayAround()
    {
        // Do nothing.
    }

    // This function makes the enemy run away from the player.
    protected virtual void RunAway()
    {
        // Find the direction away from the player.
        Vector2 direction = (transform.position - player.transform.position).normalized;

        // Move in that direction.
        Vector3 newPosition = transform.position;
        newPosition.x += direction.x * speed * Time.deltaTime;
        newPosition.y += direction.y * speed * Time.deltaTime;
        newPosition.z = 0f;
        transform.position = newPosition;
    }

    // This function is written based on the documentation of the A* Pathfinding Project.
    // https://arongranberg.com/astar/docs/wander.html
    //
    // This function makes the enemy wander around an ellipse area of the map.
    protected virtual void Wander()
    {
        if (!aiPath.pathPending && (aiPath.reachedEndOfPath || !aiPath.hasPath))
        {
            aiPath.destination = GetRandomPointWithinEllipse(habitat.transform.position, wanderingAreaHeight, wanderingAreaWidth);
            aiPath.SearchPath();
        }
    }

    // This function gets a random point within an ellipse whose position, height, and width
    // are specified by the parameters.
    protected Vector3 GetRandomPointWithinEllipse(Vector3 position, float height, float width)
    {
        Vector3 point = Random.insideUnitCircle;
        point.x *= (width / 2f);
        point.y *= (height / 2f);
        point += position;
        point.z = 0f;
        return point;
    }

    protected virtual void Idle()
    {
        // Do nothing.
    }
}
