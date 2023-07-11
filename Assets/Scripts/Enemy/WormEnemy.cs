using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{
    private WormBehavior wormBehavior;
    private float maxHealth = 100f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        wormBehavior = GetComponent<WormBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
