using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : Enemy
{
    public float maxHealth = 50f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
    }

    protected override void Die(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
