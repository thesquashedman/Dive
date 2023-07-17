using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererBehavior : FishEnemyBehavior
{
    // The radius within which this wanderer will attack the player.
    public float attackRange = 10f;

    // Public variables for pathfinding.
    public float attackSpeed = 5f;
    public float pathfindingRotationSpeed = 250f;

    // Variables for attacking.
    public bool hostile = true;
    public bool activeChase = true;
    public float chaseRange = 60f;

    // Variables for idling.
    private float idleTimer = 0f;
    private float idleTime = 0f;
    public float idleIntervalLowerBound = 3f;
    public float idleIntervalUpperBound = 5f;

    // Variables related to blood particles.
    public GameObject bloodParticlesPrefab;
    private ParticleSystem bloodParticles = null;
    private float bloodExistTimer = 0f;
    public float bloodExistTime = 60f;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = attackSpeed;
        rotationSpeed = pathfindingRotationSpeed;
        idleTime = Random.Range(idleIntervalLowerBound, idleIntervalUpperBound);
        base.Start();
        SwitchMode("wander");
    }

    protected override void FixedUpdate()
    {
        if (mode != "dead")
        {
            if (mode == "attack")
            {
                CheckAttackRange();
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
                if (hostile)
                {
                    CheckAttackRange();
                }
            }
            else if (mode == "idle")
            {
                Idle();
                if (hostile)
                {
                    CheckAttackRange();
                }
            }
        }
        else
        {
            BloodParticlesUpdate();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of this wanderer.
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
            else if (newMode == "dead")
            {
                mode = "dead";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;

                rigidbody.gravityScale = gravityScale;

                // Set up the blood particles.
                GameObject temp = Instantiate(bloodParticlesPrefab);
                temp.transform.position = transform.position;
                temp.transform.parent = transform;
                bloodParticles = temp.GetComponent<ParticleSystem>();
            }
        }
    }

    // This function checks whether the player is within this wanderer's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to wander mode.
    private void CheckAttackRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        float distanceToHabitat = Vector2.Distance(transform.position, habitat.transform.position);

        if (mode != "attack" && distanceToPlayer <= attackRange)
        {
            SwitchMode("attack");
        }
        else if (mode == "attack")
        {
            // If activeChase is true, this wanderer will chase the player until the distance from this
            // wanderer to the player exceeds the attack range. If activeChase is false, this wanderer
            // will chase the player until the distance from this wanderer to the habitat exceeds the
            // chase range.
            if (activeChase && distanceToPlayer > attackRange)
            {
                aiPath.SearchPath();
                SwitchMode("wander");
            }
            else if (!activeChase && distanceToHabitat > chaseRange)
            {
                aiPath.SearchPath();
                SwitchMode("wander");
            }
        }
    }

    // This function makes this wanderer wander around an ellipse area of the map and
    // switch to idle mode for a while between each continuous movement.
    protected override void Wander()
    {
        if (aiPath.reachedEndOfPath || !aiPath.hasPath)
        {
            if (idleTimer <= 0f)
            {
                SwitchMode("idle");
            }
            else
            {
                idleTimer = 0f;
                aiPath.targetPosition = GetRandomPointWithinEllipse(habitat.transform.position, wanderingAreaHeight, wanderingAreaWidth);
                aiPath.SearchPath();
            }
        }
    }

    // This function makes this wanderer stay idle for a while and then switch to wander mode.
    protected override void Idle()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleTime)
        {
            idleTime = Random.Range(idleIntervalLowerBound, idleIntervalUpperBound);
            SwitchMode("wander");
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
