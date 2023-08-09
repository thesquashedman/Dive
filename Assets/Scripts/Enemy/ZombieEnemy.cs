using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : Enemy
{
    private ZombieBehavior zombieBehavior;
    public float maxHealth = 50f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);
        zombieBehavior = GetComponent<ZombieBehavior>();
    }

    protected virtual void OnEnable()
    {
        // Subscribe to events.
        EventManager.current.onDealDamageEnemy += DecreaseHealth;
        EventManager.current.onDealDamageEnemy += WakeUp;
        EventManager.current.onEnemyDeath += Die;
    }
   
    protected virtual void OnDisable()
    {
        // Unsubscribe to events.
        EventManager.current.onDealDamageEnemy -= DecreaseHealth;
        EventManager.current.onDealDamageEnemy -= WakeUp;
        EventManager.current.onEnemyDeath -= Die;
    }

    private void WakeUp(int objectID, float amount)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            zombieBehavior.WakeUp();
        }
    }

    protected override void Die(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            // Unsubscribe to events.
            EventManager.current.onDealDamageEnemy -= DecreaseHealth;
            EventManager.current.onDealDamageEnemy -= WakeUp;
            EventManager.current.onEnemyDeath -= Die;
            
            zombieBehavior.SwitchMode("dead");
        }
    }
}
