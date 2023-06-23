using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererEnemy : Enemy
{
    private WandererBehavior wandererBehavior;
    private float maxHealth = 100f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        wandererBehavior = GetComponent<WandererBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
