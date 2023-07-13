using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // References to other compoenents.
    protected EnemyHealth health;
    protected EnemyEventManager enemyEventManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = GetComponent<EnemyHealth>();
        enemyEventManager = GetComponent<EnemyEventManager>();

        // Subscribe to events.
        enemyEventManager.onDealDamageEnemy += DecreaseHealth;
        enemyEventManager.onEnemyDeath += Die;
    }
   
    protected virtual void OnDisable()
    {
        // Unsubscribe to events.
        enemyEventManager.onDealDamageEnemy -= DecreaseHealth;
        enemyEventManager.onEnemyDeath -= Die;
    }

    protected virtual void DecreaseHealth(float amount)
    {
        health.ChangeHealth(-amount);
    }

    protected virtual void Die()
    {

    }
}
