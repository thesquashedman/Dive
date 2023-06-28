using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyBehavior : MonoBehaviour
{
    public GameObject player;

    // The behavior mode of this enemy. The possible modes are the following ones:
    // 1. attack: This enemy will chase and attack the player.
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
    protected Rigidbody2D rigidbody;
    protected EnemyAIPath aiPath;
    protected float speed = 5f;
    protected float rotationSpeed = 250f;

    // Variables for stuck detection and struggling.
    /*
    protected Vector3 previousPosition;
    protected float struggleTimer = 0f;
    protected float struggleTime = 0.5f;
    protected float struggleIntensity = 20f;
    protected bool isStuck = false;
    protected Vector3[] unitVectors = new Vector3[] {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
    */

    // Variables for wandering. These variables are used to compute the area that the
    // enemy can wander around and set the speed.
    public GameObject habitat; // This is the habitat of this enemy.
    public float wanderingAreaWidth = 10f;
    public float wanderingAreaHeight = 10f;
    protected float wanderingSpeed = 5f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<EnemyAIPath>();
        aiPath.speed = speed;
    }

    protected virtual void FixedUpdate()
    {
        if (mode == "attack")
        {
            Attack();
        }
        else if (mode == "coolDown")
        {
            CoolDown();
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
            aiPath.speed = speed;
            aiPath.target = player.transform;
            aiPath.enableRotation = true;
        }
        else if (newMode == "coolDown")
        {
            mode = "coolDown";
            aiPath.speed = 0f;
            aiPath.target = null;
            aiPath.enableRotation = false;
        }
        else if (newMode == "runAway")
        {
            mode = "runAway";
            aiPath.speed = 0f;
            aiPath.target = null;
            aiPath.enableRotation = false;
        }
        else if (newMode == "wander")
        {
            mode = "wander";
            aiPath.speed = wanderingSpeed;
            aiPath.target = null;
            aiPath.enableRotation = true;
        }
        else if (newMode == "idle")
        {
            mode = "idle";
            aiPath.speed = 0f;
            aiPath.target = null;
            aiPath.enableRotation = false;
        }
    }

    // This function verifies whether this enemy is stuck and needs to struggle.
    // If the enemy is stuck, this function calls the struggle function.
    // This function is discarded since it is based on transform.
    /*
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
    */

    // This function is called when the enemy gets stuck with another object.
    // This function makes the enemy struggle for a short time.
    // This function is discarded since it is based on transform.
    /*
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
    */

    protected virtual void Attack()
    {
        // Do nothing.
    }

    protected virtual void CoolDown()
    {
        // Do nothing.
    }

    // This function makes the enemy run away from the player.
    protected virtual void RunAway()
    {
        // Find the direction away from the player.
        Vector2 direction = ((Vector2)transform.position - (Vector2)player.transform.position).normalized;

        // Slowly rotate the enemy towards the run away direction.
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        if (Mathf.Abs(transform.eulerAngles.z - targetAngle) >= 3f)
        {
            float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0f, 0f, newAngle);
        }

        // Move in that direction.
        rigidbody.AddForce(direction * speed);
    }

    // This function is written based on the documentation of the A* Pathfinding Project.
    // The link to the documentation is https://arongranberg.com/astar/docs/wander.html
    //
    // This function makes the enemy wander around an ellipse area of the map.
    protected virtual void Wander()
    {
        if (aiPath.reachedEndOfPath)
        {
            aiPath.targetPosition = GetRandomPointWithinEllipse(habitat.transform.position, wanderingAreaHeight, wanderingAreaWidth);
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
