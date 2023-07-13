using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    // This function invokes the enemyDeath event when the enemy's health reaches 0.
    public override void Die()
    {
        if (currentHealth <= 0)
        {
            EventManager.current.EnemyDeath(gameObject.GetInstanceID());
        }
    }
}
