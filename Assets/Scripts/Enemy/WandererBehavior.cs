using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererBehavior : FishEnemyBehavior
{
    // The radius within which this wanderer will attack the player.
    public float attackRange = 10f;

    // Variables for attacking.
    public bool hostile = true;

    // Variables for idling.
    private float idleTimer = 0f;
    private float idleTime = 0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        idleTime = Random.Range(3f, 5f);
        base.Start();
        SwitchMode("wander");
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
            CheckAttackRange();
        }
        else if (mode == "idle")
        {
            Idle();
            CheckAttackRange();
        }
        Debug.Log(aiPath.reachedEndOfPath);
    }

    // This function checks whether the player is within this wanderer's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to wander mode.
    private void CheckAttackRange()
    {
        if (mode != "attack" && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            SwitchMode("attack");
        }
        else if (mode == "attack" && Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            SwitchMode("wander");
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
            idleTime = Random.Range(3f, 5f);
            SwitchMode("wander");
        }
    }
}
