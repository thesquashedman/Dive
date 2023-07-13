using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererEnemy : Enemy
{
    private WandererBehavior wandererBehavior;
    public float maxHealth = 30f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        wandererBehavior = GetComponent<WandererBehavior>();
    }

    protected override void Die()
    {
        wandererBehavior.SwitchMode("dead");
    }
}
