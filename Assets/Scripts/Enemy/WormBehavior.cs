using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehavior : FishEnemyBehavior
{
    // The maximum distance that this worm should reach from its habitat.
    public float maxReach = 30f;

    // The minimum distance that this worm should reach from its habitat.
    public float minReach = 3f;

    // The radius from the habitat within which this worm will attack the player.
    public float attackRange = 20f;

    // Variables for cooling down.
    private float coolDownTimer = 0f;
    private float coolDownTime;

    // Variables for idling.
    private float wiggleTimer = 0f;
    public float wiggleInterval = 3f;
    public float wiggleSpeed = 3f;

    // Whether this worm is already out of the wall.
    private bool awake = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 100f;
        rotationSpeed = 250f;
        coolDownTime = Random.Range(1f, 4f);
        base.Start();
        transform.position = habitat.transform.position;
        SwitchMode("idle");
    }

    protected override void FixedUpdate()
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
        }
        else if (mode == "idle")
        {
            Idle();
            CheckAttackRange();
        }

        if (awake && transform.parent.transform.position != habitat.transform.position)
        {
            Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, habitat.transform.position, 30f * Time.deltaTime);
            transform.parent.transform.position = position;
        }
    }

    // This function checks whether the player is within this worm's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to idle mode.
    private void CheckAttackRange()
    {
        if (mode != "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) <= attackRange)
        {
            awake = true;
            SwitchMode("attack");
        }
        else if (mode == "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) > attackRange)
        {
            SwitchMode("idle");
        }
    }

    // This function makes this worm cool down for a short amount of time before
    // switching to attack mode.
    protected override void CoolDown()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= coolDownTime)
        {
            coolDownTimer = 0f;
            coolDownTime = Random.Range(1f, 4f);
            SwitchMode("attack");
        }
    }

    protected override void Idle()
    {
        if (awake)
        {
            Vector2 habitatToWorm = ((Vector2)transform.position - (Vector2)habitat.transform.position);
            wiggleTimer += Time.deltaTime;
            if (wiggleTimer < wiggleInterval)
            {
                if (Vector3.Magnitude(habitatToWorm) <= minReach)
                {
                    wiggleTimer = 0f;
                }
                else
                {
                    rigidbody.AddForce(-habitatToWorm.normalized * wiggleSpeed);
                }
            }
            else
            {
                if (Vector3.Magnitude(habitatToWorm) >= maxReach)
                {
                    wiggleTimer = 0f;
                }
                else
                {
                    rigidbody.AddForce(habitatToWorm.normalized * wiggleSpeed);
                }

            }
        }
    }
}
