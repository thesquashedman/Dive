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

    // Variables for attacking.
    public float attackSpeed = 25f;

    // Variables for idling.
    private float wiggleTimer = 0f;
    public float wiggleIntervalLowerBound = 2f;
    public float wiggleIntervalUpperBound = 4f;
    public float wiggleSpeed = 3f;
    private Vector3 initialPosition;

    // Whether this worm should be awaken and out of the wall.
    private bool isAwaken = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 80f;
        rotationSpeed = 250f;
        base.Start();
        initialPosition = transform.parent.transform.position;
        transform.position = habitat.transform.position;
        SwitchMode("idle");
    }

    protected override void FixedUpdate()
    {
        if (mode == "attack")
        {
            Attack();
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
        }
        else if (mode == "idle")
        {
            Idle();
            CheckAttackRange();
        }
    }

    // This function checks whether the player is within this worm's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to idle mode.
    private void CheckAttackRange()
    {
        if (isAwaken)
        {
            if (mode != "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) <= initialAttackRange)
            {
                SwitchMode("attack");
            }
            else if (mode == "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) > subsequentAttackRange)
            {
                SwitchMode("idle");
            }
        }
        else if (Vector2.Distance(habitat.transform.position, player.transform.position) <= initialAttackRange)
        {
            isAwaken = true;
            SwitchMode("attack");
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

    // This function moves this worm partially back into the wall and makes this worm
    // wiggle while idling.
    protected override void Idle()
    {
        if (isAwaken)
        {
            Vector2 direction = ((Vector2)initialPosition - (Vector2)habitat.transform.position).normalized;
            Vector3 targetRootPosition = (Vector2)habitat.transform.position + direction * wormLength / 2f;
            Vector3 targetHeadPosition = (Vector2)habitat.transform.position - direction * wormLength / 2;

            if (transform.parent.transform.position != targetRootPosition)
            {
                Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, targetRootPosition, attackSpeed / 4f * Time.deltaTime);
                transform.parent.transform.position = position;
            }

            if (transform.position != targetHeadPosition)
            {
                direction = ((Vector2)targetHeadPosition - (Vector2)transform.position).normalized;
                rigidbody.AddForce(direction * speed / 2f);
            }
            
        }

    }
}
