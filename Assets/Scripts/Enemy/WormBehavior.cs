using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehavior : FishEnemyBehavior
{
     // The radius within which this worm will attack the player.
    public float attackRange;

    // Variables for cooling down.
    private float coolDownTimer = 0f;
    private float coolDownTime;

    public float maxReach;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 100f;
        rotationSpeed = 200f;
        coolDownTime = Random.Range(1f, 3f);
        attackRange = maxReach + 5f;
        base.Start();
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
        if (mode != "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) <= attackRange)
        {
            transform.position = habitat.transform.position;
            SwitchMode("attack");
        }
        else if (mode == "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) > attackRange)
        {
            SwitchMode("idle");
        }
    }

    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, habitat.transform.position) >= maxReach)
        {
            rigidbody.velocity = Vector2.zero;
            SwitchMode("coolDown");
        }
    }

    protected override void CoolDown()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= coolDownTime)
        {
            coolDownTimer = 0f;
            SwitchMode("attack");
        }
    }

    protected override void Idle()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
