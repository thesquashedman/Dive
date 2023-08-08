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

    protected override void OnEnable()
    {
        // Subscribe to events.
        EventManager.current.onDealDamageEnemy += DecreaseHealth;
        EventManager.current.onEnemyDeath += Die;
        EventManager.current.onEnemyAttackSuccess += TimeReset;
    }
   
    protected override void OnDisable()
    {
        // Unsubscribe to events.
        EventManager.current.onDealDamageEnemy -= DecreaseHealth;
        EventManager.current.onEnemyDeath -= Die;
        EventManager.current.onEnemyAttackSuccess -= TimeReset;
    }

    protected override void Die(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            // Unsubscribe to events.
            EventManager.current.onDealDamageEnemy -= DecreaseHealth;
            EventManager.current.onEnemyDeath -= Die;
            EventManager.current.onEnemyAttackSuccess -= TimeReset;
            wormBehavior.SwitchMode("dead");
        }
    }

    private void TimeReset(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            wormBehavior.TimeReset();
        }
    }
}
