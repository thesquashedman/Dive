using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : FishEnemyBehavior
{
    // Whether this zombie is awake.
    private bool awake = false;

    // The radius within which this zombie will attack the player.
    public float attackRange = 10f;

    // Public variables for pathfinding.
    public float attackSpeed = 10f;
    public float pathfindingRotationSpeed = 200f;

    // Variables for attacking.
    public bool hostile = true;
    public bool activeChase = true;
    public float chaseRange = 60f;

    // Variables for returning to this zombie's territory.
    public float returningSpeed = 10f;

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

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = gravityScale;

        aiPath = GetComponent<EnemyAIPath>();
        aiPath.speed = 0f;
        aiPath.rotationSpeed = rotationSpeed;
        aiPath.target = null;
        aiPath.enableRotation = false;

        attackSystem.GetComponent<EnemyAttackSystem>().enemyID = gameObject.GetInstanceID();
        attackSystem.SetActive(false);
    }

    protected override void FixedUpdate()
    {
        if (awake)
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
        else
        {
            CheckAwake();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of this zombie.
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
                attackSystem.SetActive(true);
                idleTimer = 0f;
                rigidbody.gravityScale = 0f;

                // Issue the enemy attack event.
                EventManager.current.EnemyAttack(gameObject.GetInstanceID());
            }
            else if (newMode == "coolDown")
            {
                mode = "coolDown";
                aiPath.speed = returningSpeed;
                aiPath.target = habitat.transform;
                aiPath.enableRotation = true;
                attackSystem.SetActive(false);
            }
            else if (newMode == "runAway")
            {
                mode = "runAway";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;
                attackSystem.SetActive(false);
            }
            else if (newMode == "wander")
            {
                mode = "wander";
                aiPath.speed = wanderingSpeed;
                aiPath.target = null;
                aiPath.enableRotation = true;
                attackSystem.SetActive(false);
            }
            else if (newMode == "idle")
            {
                mode = "idle";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;
                attackSystem.SetActive(false);
            }
            else if (newMode == "dead")
            {
                mode = "dead";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;
                attackSystem.SetActive(false);
                rigidbody.gravityScale = gravityScale;

                // Set up the blood particles.
                GameObject temp = Instantiate(bloodParticlesPrefab);
                temp.transform.position = transform.position;
                temp.transform.parent = transform;
                bloodParticles = temp.GetComponent<ParticleSystem>();
            }
        }
    }

    // This function checks whether the player is within this zombie's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to wander mode.
    private void CheckAttackRange()
    {
        distanceToPlayer = GetDistanceToPlayer();
        float distanceToHabitat = Vector2.Distance(transform.position, habitat.transform.position);

        if (mode != "attack" && distanceToPlayer <= attackRange)
        {
            SwitchMode("attack");
        }
        else if (mode == "attack")
        {
            // If activeChase is true, this zombie will chase the player until the distance from this
            // zombie to the player exceeds the attack range. If activeChase is false, this zombie
            // will chase the player until the distance from this zombie to the habitat exceeds the
            // chase range.
            if (activeChase && distanceToPlayer > attackRange)
            {
                SwitchMode("coolDown");
            }
            else if (!activeChase && distanceToHabitat > chaseRange)
            {
                SwitchMode("coolDown");
            }
        }
    }

    // This function wakes this zombie up if the player is within the attack range.
    private void CheckAwake()
    {
        distanceToPlayer = GetDistanceToPlayer();
        if (distanceToPlayer <= attackRange)
        {
            WakeUp();
        }
    }

    public void WakeUp()
    {
        awake = true;
        SwitchMode("attack");
    }

    protected override void CoolDown()
    {
        if (aiPath.reachedEndOfPath)
        {
            SwitchMode("wander");
        }
    }

    // This function makes this zombie wander around an ellipse area of the map and
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

    // This function makes this zombie stay idle for a while and then switch to wander mode.
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
