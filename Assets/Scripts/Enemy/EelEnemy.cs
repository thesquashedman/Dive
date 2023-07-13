using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelEnemy : Enemy
{
    private EelBehavior eelBehavior;
    private float maxHealth = 20f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        eelBehavior = GetComponent<EelBehavior>();
    }

    protected override void Die()
    {
        eelBehavior.SwitchMode("dead");
    }
}
