using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{
    private WormBehavior wormBehavior;
    public int healtCount = 2;

    // Start is called before the first frame update
    protected override void Start()
    {
        wormBehavior = GetComponent<WormBehavior>();
    }

    // When the worm is hit, its health count is decreased. The worm will go back
    // into the wall when the health count reaches 0.
    protected override void DecreaseHealth(int objectID, float amount)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            healtCount--;
            if (healtCount <= 0)
            {
                wormBehavior.SwitchMode("idle");
            }
        }
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
