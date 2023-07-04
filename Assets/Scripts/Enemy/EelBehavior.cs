using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelBehavior : FishEnemyBehavior
{
    public GameObject playerHeadlight;
    
    // Variables that define the range of the player's headlight.
    private float headlightAngle;
    private float headlightDistance;

    // The layer of the obstacles.
    private LayerMask obstacleLayerMask;

    // The radius within which this eel will attack the player.
    public float attackRange = 30f;

    // Variables for running away from the player.
    private float runAwayTimer = 0f;
    private float runAwayTime = 2f;
    private Vector2 currentDirection = Vector2.up;
    private Vector2 runAwayDirection = Vector2.up;

    // Variables for returning to this eel's habitat and staying idle.
    private float returningSpeed = 5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 20f;
        runAwaySpeed = 30f;
        rotationSpeed = 200f;
        base.Start();
        obstacleLayerMask = LayerMask.GetMask("Obstacles");
        SetHeadlightRange(30f, 12f);
        SwitchMode("idle");
    }

    protected override void FixedUpdate()
    {
        if (mode == "attack")
        {
            CheckAttackRange();
            CheckShined();
        }
        else if (mode == "coolDown")
        {
            StayAround();
            CheckShined();
        }
        else if (mode == "runAway")
        {
            RunAway();
        }
        else if (mode == "wander")
        {
            Wander();
            CheckShined();
        }
        else if (mode == "idle")
        {
            Idle();
            CheckAttackRange();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of the eel.
    protected override void SwitchMode(string newMode)
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
            aiPath.speed = 0;
            aiPath.target = player.transform;
            aiPath.enableRotation = true;
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
            aiPath.speed = returningSpeed;
            aiPath.target = habitat.transform;
            aiPath.enableRotation = true;
        }
    }

    public void SetHeadlightRange(float angle, float distance)
    {
        headlightAngle = angle;
        headlightDistance = distance;
    }

    // This function checks whether the player is within this eel's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to idle mode.
    private void CheckAttackRange()
    {
        if (mode != "attack" && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            SwitchMode("attack");
        }
        else if (mode == "attack" && Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            SwitchMode("idle");
        }
    }

    // This function checks whether the eel is shined on by the player's headlight.
    // If this eel is shined on, it will run away from the player.
    private void CheckShined()
    {
        // Compute the vector from the player's headlight.
        float angle = playerHeadlight.transform.rotation.eulerAngles.z;
        float radianAngle = angle * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(radianAngle);
        float cosAngle = Mathf.Cos(radianAngle);
        
        Vector2 vector = Vector2.up;
        float rotatedX = vector.x * cosAngle - vector.y * sinAngle;
        float rotatedY = vector.x * sinAngle + vector.y * cosAngle;
        Vector2 headlightDirection = new Vector2(rotatedX, rotatedY);
        
        // Compute the vector from the player's headlight to this eel.
        Vector2 headlightToEel = ((Vector2)transform.position - (Vector2)playerHeadlight.transform.position);
        
        // If this eel is within the headlight's range, do a raycast to see if there 
        // is an obstacle between the player's headlight and this eel.
        if (Vector2.Angle(headlightDirection, headlightToEel) <= headlightAngle && Vector3.Magnitude(headlightToEel) <= headlightDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -(headlightToEel.normalized), Vector3.Magnitude(headlightToEel), obstacleLayerMask);

            // If there is not an obstacle, this eel is shined on.
            if (hit.collider == null)
            {
                currentDirection = aiPath.direction;
                if (Vector3.Magnitude(currentDirection) <= 0.9f)
                {
                    currentDirection = -(headlightToEel.normalized);
                }
                runAwayDirection = headlightToEel.normalized;
                SwitchMode("runAway");
            }
        }
    }
    
    // This function is called when this eel is shined on; this eel will run away
    // from the player for a short amount of time and then stay around before
    // continuing to attack the player.
    protected override void RunAway()
    {
        runAwayTimer += Time.deltaTime;
        if (runAwayTimer < runAwayTime)
        {
            // Rotate the current direction gradually.
            if (!Vector3.Equals(currentDirection, runAwayDirection))
            {
                float deltaAngle = Mathf.PI * 3f;
                currentDirection = Vector3.RotateTowards(currentDirection, runAwayDirection, deltaAngle * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(Vector3.forward, currentDirection);
            }
            // Rotate the eel back if necessary.
            else
            {
                float targetAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90f;
                if (Mathf.Abs(transform.eulerAngles.z - targetAngle) >= 8f)
                {
                    float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0f, 0f, newAngle);
                }
            }
            
            // Move towards the current direction.
            rigidbody.AddForce(currentDirection * runAwaySpeed);
        }
        else
        {
            runAwayTimer = 0f;
            SwitchMode("coolDown");
        }
    }

    // This function makes this eel stays around (outside the player's headlight range).
    // This eel will start attacking again only if the player looks away.
    protected void StayAround()
    {
        // Compute the vector from the player's headlight.
        float angle = playerHeadlight.transform.rotation.eulerAngles.z;
        float radianAngle = angle * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(radianAngle);
        float cosAngle = Mathf.Cos(radianAngle);
        
        Vector2 vector = Vector2.up;
        float rotatedX = vector.x * cosAngle - vector.y * sinAngle;
        float rotatedY = vector.x * sinAngle + vector.y * cosAngle;
        Vector2 headlightDirection = new Vector2(rotatedX, rotatedY);
        
        // Compute the vector from the player's headlight to this eel.
        Vector2 headlightToEel = ((Vector2)transform.position - (Vector2)playerHeadlight.transform.position);

        // Stay outside of the headlight range.
        if (Vector3.Magnitude(headlightToEel) > headlightDistance + 1f)
        {
            aiPath.speed = 1f;
        }
        else
        {
            aiPath.speed = 0f;
        }

        // Switch to attack mode if the player looks away.
        if (Vector2.Angle(headlightDirection, headlightToEel) > headlightAngle)
        {   
            SwitchMode("attack");
        }
        // Otherwise, switch to attack mode if the this eel is in the shadow.
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -(headlightToEel.normalized), Vector3.Magnitude(headlightToEel), obstacleLayerMask);
            if (hit.collider != null)
            {
                SwitchMode("attack");
            }
        }
    }
}
