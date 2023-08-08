using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehavior : FishEnemyBehavior
{
    // The radius from the habitat within which this worm will attack the player.
    public float initialAttackRange = 25f;
    public float subsequentAttackRange = 40f;

    // The estimated length of this worm.
    public float wormLength = 30f;

    // The bones of this worm.
    public List<GameObject> bodyBones = new List<GameObject>();
    public List<GameObject> jawBones = new List<GameObject>();

    // The prefabs for the colliders of this worm.
    public GameObject bodyColliderPrefab;
    public GameObject jawColliderPrefab;

    // Variables for attacking.
    public float attackSpeed = 15f;
    public float attackLastingTime = 4f;
    private float attackTimer = 0f;

    // Variables for idling and returning.
    private float wiggleTimer = 0f;
    public float wiggleIntervalLowerBound = 2f;
    public float wiggleIntervalUpperBound = 4f;
    public float wiggleSpeed = 3f;
    private Vector3 initialPosition;
    private Vector2 returnDirection;
    private Vector3 tempHeadPosition;
    private bool wiggle = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 80f;
        rotationSpeed = 250f;
        base.Start();
        initialPosition = transform.parent.transform.position;
        transform.position = habitat.transform.position;
        returnDirection = ((Vector2)initialPosition - (Vector2)habitat.transform.position).normalized;
        tempHeadPosition = habitat.transform.position - (Vector3)returnDirection * wormLength;
        SwitchMode("idle");

        // Set up the colliders for this worm.
        int wormID = gameObject.GetInstanceID();
        for (int i = 0; i < bodyBones.Count; i++)
        {
            GameObject temp = Instantiate(bodyColliderPrefab);
            temp.transform.position = bodyBones[i].transform.position;
            temp.transform.parent = bodyBones[i].transform;
            temp.GetComponent<BodyPart>().enemyID = wormID;
        }

        for (int i = 0; i < jawBones.Count; i++)
        {
            GameObject temp = Instantiate(jawColliderPrefab);
            temp.transform.position = jawBones[i].transform.position;
            temp.transform.parent = jawBones[i].transform;
            temp.GetComponent<BodyPart>().enemyID = wormID;
        }
    }

    protected override void FixedUpdate()
    {
        if (mode != "dead")
        {
            if (mode == "attack")
            {
                Attack();
                CheckAttackRange();
                CheckAttackTimer();
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
                if (wiggle)
                {
                    CheckAttackRange();
                }
            }
        }
        else
        {
            Die();
        }
    }

    // This function acts as the common interface for switching the action mode
    // of the worm.
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
                wiggle = false;

                // Issue the enemy attack event.
                EventManager.current.EnemyAttack(gameObject.GetInstanceID());
            }
            else if (newMode == "coolDown")
            {
                mode = "coolDown";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;
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
                attackTimer = 0f;
            }
            else if (newMode == "dead")
            {
                mode = "dead";
                aiPath.speed = 0f;
                aiPath.target = null;
                aiPath.enableRotation = false;
                attackSystem.SetActive(false);
                attackTimer = 0f;
            }
        }
    }

    // This function checks whether the player is within this worm's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to idle mode.
    private void CheckAttackRange()
    {
        float habitatToPlayer = Vector2.Distance(habitat.transform.position, player.transform.position);
        if (mode != "attack" && habitatToPlayer <= initialAttackRange)
        {
            SwitchMode("attack");
        }
        else if (mode == "attack" && habitatToPlayer > subsequentAttackRange)
        {
            SwitchMode("idle");
        }
    }

    // This function increments the attack timer and switches to idle mode when the attack
    // timer reaches the attack lasting time.
    private void CheckAttackTimer()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackLastingTime)
        {            
            SwitchMode("idle");
        }
    }

    // This function moves this worm out of the wall.
    protected override void Attack()
    {
        if (transform.parent.transform.position != habitat.transform.position)
        {
            Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, habitat.transform.position, attackSpeed * Time.deltaTime);
            transform.parent.transform.position = position;
        }
    }

    // This function moves this worm back into the wall and makes this worm
    // wiggle while idling.
    protected override void Idle()
    {
        Vector3 targetRootPosition = initialPosition;
        Vector3 targetHeadPosition = habitat.transform.position;

        // Move this worm back.
        if (transform.parent.transform.position != targetRootPosition)
        {
            Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, targetRootPosition, attackSpeed / 2f * Time.deltaTime);
            transform.parent.transform.position = position;
            position = Vector2.MoveTowards(transform.position, tempHeadPosition, attackSpeed * Time.deltaTime);
            transform.position = position;
        }
        else if (!wiggle && transform.position != targetHeadPosition)
        {
            Vector2 position = Vector2.MoveTowards(transform.position, targetHeadPosition, attackSpeed * Time.deltaTime);
            transform.position = position;
        }
        else
        {
            wiggle = true;
        }

        // Make the worm wiggle.
        if (wiggle)
        {     
            wiggleTimer += Time.deltaTime;
            if (wiggleTimer < wiggleIntervalLowerBound)
            {
                Vector2 direction = new Vector2(returnDirection.y, -returnDirection.x);
                rigidbody.AddForce(direction * wiggleSpeed);
            }
            else if (wiggleTimer < wiggleIntervalUpperBound)
            {
                Vector2 direction = new Vector2(-returnDirection.y, returnDirection.x);
                rigidbody.AddForce(direction * wiggleSpeed);
            }
            else
            {
                wiggleTimer = 0f;
            }
        }
    }

    // This function moves the worm completely into the wall.
    private void Die()
    {
        Vector3 targetRootPosition = (Vector2)initialPosition + returnDirection * wormLength;
        Vector3 targetHeadPosition = initialPosition;

        // Move this worm into the wall.
        if (transform.parent.transform.position != targetRootPosition)
        {
            Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, targetRootPosition, attackSpeed * Time.deltaTime);
            transform.parent.transform.position = position;
            position = Vector2.MoveTowards(transform.position, tempHeadPosition, attackSpeed * Time.deltaTime);
            transform.position = position;                
        }
        else if (transform.position != targetHeadPosition)
        {
            Vector2 position = Vector2.MoveTowards(transform.position, targetHeadPosition, attackSpeed * 2f * Time.deltaTime);
            transform.position = position;
        }
    }
}
