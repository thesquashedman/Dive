using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{
    private WormBehavior wormBehavior;
    public float maxHealth = 100f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        wormBehavior = GetComponent<WormBehavior>();
    }

    protected override void Die()
    {
        wormBehavior.SwitchMode("dead");
    }
}
