using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EelBehavior : FishEnemyBehavior
{
    public GameObject playerHeadlight;
    
    // Variables that define the range of the player's headlight.
    private float headlightAngle;
    private float headlightDistance;

    // Variables for running away from the player.
    private float runAwayTimer = 0f;
    private float runAwayTime = 1.4f;
    private Vector2 currentDirection = Vector2.zero;
    private Vector2 runAwayDirection = Vector2.zero;
    private float currentSpeed = 0f;
    private bool isShinedOn = false;
    private LayerMask obstacleLayerMask;

    // Variables for staying around.
    private float stayAroundTimer = 0f;
    private float stayAroundTime = 1f;
    private float lingeringSpeed = 1.2f;
    private float lingeringAcceleration = 0.6f;

    // Variables for returning to this eel's habitat and staying idle.
    private float returingSpeed = 4f;
    private float returingAcceleration = 6f;

    // The radius within which this eel will attack the player.
    public float attackRange = 30f;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 7f;
        acceleration = 12f;
        decceleration = -16f;
        stayAroundTime = Random.Range(1f, 3f);
        base.Start();
        UpdateHeadlightRange();
        obstacleLayerMask = LayerMask.GetMask("Obstacles");
        SwitchMode("idle");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (mode == "attack")
        {
            CheckAttackRange();
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
            CheckAttackRange();
        }
        CheckShined();
    }

    // This function acts as the common interface for switching the action mode
    // of the eel.
    protected override void SwitchMode(string newMode)
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
            aiPath.maxSpeed = lingeringSpeed;
            aiPath.maxAcceleration = lingeringAcceleration;
            aiPath.enableRotation = true;
            aiDestinationSetter.target = player.transform;
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
            aiPath.maxSpeed = returingSpeed;
            aiPath.maxAcceleration = returingAcceleration;
            aiPath.enableRotation = true;
            aiDestinationSetter.target = habitat.transform;
        }
    }

    public void UpdateHeadlightRange()
    {
        headlightAngle = 30f;
        headlightDistance = 7f;
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
    void CheckShined()
    {
        if (!isShinedOn)
        {
            // Compute the vector of the player's headlight.
            float angle = playerHeadlight.transform.rotation.eulerAngles.z;
            float radianAngle = angle * Mathf.Deg2Rad;
            float sinAngle = Mathf.Sin(radianAngle);
            float cosAngle = Mathf.Cos(radianAngle);
        
            Vector2 vector = Vector2.up;
            float rotatedX = vector.x * cosAngle - vector.y * sinAngle;
            float rotatedY = vector.x * sinAngle + vector.y * cosAngle;
            Vector2 headlightDirection = new Vector2(rotatedX, rotatedY);
        
            // Compute the vector from the player's headlight to this eel.
            Vector2 headlightToEel = (transform.position - playerHeadlight.transform.position);
        
            // If this eel is within the headlight's range, do a raycast to see if there 
            // is an obstacle between the player's headlight and this eel.
            if (Vector2.Angle(headlightDirection, headlightToEel) <= headlightAngle && Vector3.Magnitude(headlightToEel) <= headlightDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -headlightToEel, Vector3.Magnitude(headlightToEel), obstacleLayerMask);

                // If there is not an obstacle, this eel is shined on.
                if (hit.collider == null)
                {
                    isShinedOn = true;
                    currentDirection = aiPath.desiredVelocity.normalized;
                    runAwayDirection = headlightToEel.normalized;
                    currentSpeed = speed;
                    SwitchMode("runAway");
                }
            }
        }
    }

    // This function is called when this eel is shined on; this eel will run away
    // from the player for a short amount of time and then stay around for a
    // short amount of time before continuing to attack the player.
    protected override void RunAway()
    {
        runAwayTimer += Time.deltaTime;
        if (runAwayTimer < runAwayTime)
        {
            // Rotate the current direction gradually.
            if (currentDirection != runAwayDirection)
            {
                deltaAngle = Mathf.PI * 3f;
                currentDirection = Vector3.RotateTowards(currentDirection, runAwayDirection, deltaAngle * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(Vector3.forward, currentDirection);
            }
            Vector3 newPosition = transform.position;
            newPosition.x += currentDirection.x * currentSpeed * Time.deltaTime;
            newPosition.y += currentDirection.y * currentSpeed * Time.deltaTime;
            newPosition.z = 0f;
            transform.position = newPosition;
        }
        else if (runAwayTimer < runAwayTime + slowDownTime)
        {
            // Slow down gradually.
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref decceleration, slowDownTime);
            Vector3 newPosition = transform.position;
            newPosition.x += currentDirection.x * currentSpeed * Time.deltaTime;
            newPosition.y += currentDirection.y * currentSpeed * Time.deltaTime;
            newPosition.z = 0f;
            transform.position = newPosition;
        }
        else
        {
            runAwayTimer = 0f;
            isShinedOn = false;
            SwitchMode("coolDown");
        }
    }

    // This function makes this eel stays around outside the player's headlight range
    // for a short amount of time. After this, this eel will start attacking again.
    protected override void StayAround()
    {
        stayAroundTimer += Time.deltaTime;
        if (stayAroundTimer >= stayAroundTime)
        {
            stayAroundTimer = 0f;
            stayAroundTime = Random.Range(1f, 3f);
            SwitchMode("attack");
        }
    }
}
