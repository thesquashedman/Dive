using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{
    private WormBehavior wormBehavior;
    private float maxHealth = 60f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        wormBehavior = GetComponent<WormBehavior>();
    }

    protected override void Die(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            base.Die(objectID);
            wormBehavior.SwitchMode("dead");
        }
    }
}
