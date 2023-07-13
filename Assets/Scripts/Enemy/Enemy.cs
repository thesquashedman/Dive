using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // References to other compoenents.
    protected EnemyHealth health;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = GetComponent<EnemyHealth>();

        // Subscribe to events.
        EventManager.current.onDealDamageEnemy += DecreaseHealth;
        EventManager.current.onEnemyDeath += Die;
    }
   
    protected virtual void OnDisable()
    {
        // Unsubscribe to events.
        EventManager.current.onDealDamageEnemy -= DecreaseHealth;
        EventManager.current.onEnemyDeath -= Die;
    }

    protected virtual void DecreaseHealth(int objectID, float amount)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            health.ChangeHealth(-amount);
        }
    }

    protected virtual void Die(int objectID)
    {
    }
}
