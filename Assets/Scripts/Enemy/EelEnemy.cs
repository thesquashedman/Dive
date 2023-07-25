using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelEnemy : Enemy
{
    private EelBehavior eelBehavior;
    private float maxHealth = 40f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        eelBehavior = GetComponent<EelBehavior>();
    }

    protected override void Die(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            base.Die(objectID);
            eelBehavior.SwitchMode("dead");
        }
    }
}
