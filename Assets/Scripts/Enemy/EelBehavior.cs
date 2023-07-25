using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelBehavior : FishEnemyBehavior
{
    public GameObject playerHeadlight;

    // References to other components.
    public Tail tail;
    
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
    private Vector2 runAwayDirection = Vector2.up;
    private float runAwayRotationSpeed = 300f;
    private GameObject targetObject;
    public GameObject targetPrefab;

    // Variables for returning to this eel's habitat and staying idle.
    private float returningSpeed = 5f;

    // Variables related to blood particles.
    public GameObject bloodParticlesPrefab;
    private ParticleSystem bloodParticles = null;
    private float bloodExistTimer = 0f;
    public float bloodExistTime = 60f;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 25f;
        runAwaySpeed = 40f;
        rotationSpeed = 200f;
        base.Start();
        attackSystem.GetComponent<BodyPart>().enemyID = gameObject.GetInstanceID();
        obstacleLayerMask = LayerMask.GetMask("Obstacles");
        targetObject = Instantiate(targetPrefab, transform.position, Quaternion.identity);
        SetHeadlightRange(30f, 15f);
        SwitchMode("idle");
    }

    protected override void FixedUpdate()
    {
        if (mode != "dead")
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
        else
        {
            BloodParticlesUpdate();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of the eel.
    public override void SwitchMode(string newMode)
    {
        if (mode != "dead")
        {
            if (newMode == "attack")
            {
                mode = "attack";
                aiPath.speed = speed;
                aiPath.target = player.transform;
                aiPath.enableRotation = true;
                aiPath.rotationSpeed = rotationSpeed;
                attackSystem.SetActive(true);
                tail.enableWiggle = false;

                // Issue the enemy attack event.
                EventManager.current.EnemyAttack(gameObject.GetInstanceID());
            }
            else if (newMode == "coolDown")
            {
                mode = "coolDown";
                aiPath.speed = 0;
                aiPath.target = player.transform;
                aiPath.enableRotation = true;
                aiPath.rotationSpeed = rotationSpeed;
                attackSystem.SetActive(false);
                tail.enableWiggle = true;
            }
            else if (newMode == "runAway")
            {
                mode = "runAway";
                aiPath.speed = runAwaySpeed;
                aiPath.target = targetObject.transform;
                aiPath.enableRotation = true;
                aiPath.rotationSpeed = runAwayRotationSpeed;
                attackSystem.SetActive(false);
                tail.enableWiggle = false;
            }
            else if (newMode == "wander")
            {
                mode = "wander";
                aiPath.speed = wanderingSpeed;
                aiPath.target = null;
                aiPath.enableRotation = true;
                aiPath.rotationSpeed = rotationSpeed;
                attackSystem.SetActive(false);
                tail.enableWiggle = true;
            }
            else if (newMode == "idle")
            {
                mode = "idle";
                aiPath.speed = returningSpeed;
                aiPath.target = habitat.transform;
                aiPath.enableRotation = true;
                aiPath.rotationSpeed = rotationSpeed;
                attackSystem.SetActive(false);
                tail.enableWiggle = true;
            }
            else if (newMode == "dead")
            {
                mode = "dead";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;
                aiPath.rotationSpeed = 0f;
                attackSystem.SetActive(false);
                rigidbody.gravityScale = gravityScale;
                tail.enableWiggle = false;

                // Set up the blood particles.
                GameObject temp = Instantiate(bloodParticlesPrefab);
                temp.transform.position = transform.position;
                temp.transform.parent = transform;
                bloodParticles = temp.GetComponent<ParticleSystem>();
            }
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
        distanceToPlayer = GetDistanceToPlayer();
        if (mode != "attack" && distanceToPlayer <= attackRange)
        {
            SwitchMode("attack");
        }
        else if (mode == "attack" && distanceToPlayer > attackRange)
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
                runAwayDirection = headlightToEel.normalized;
                targetObject.transform.position = transform.position;
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
            targetObject.GetComponent<Rigidbody2D>().AddForce(runAwayDirection * 50f);
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

    // This function stops the blood particles after a certain amount of time.
    protected void BloodParticlesUpdate()
    {
        if (bloodParticles != null && bloodParticles.isEmitting)
        {
            bloodExistTimer += Time.deltaTime;
            if (bloodExistTimer >= bloodExistTime)
            {
                bloodParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                bloodParticles = null;
            }
        }
    }
}
