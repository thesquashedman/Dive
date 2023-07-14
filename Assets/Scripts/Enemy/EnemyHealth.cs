using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private bool isDead = false;

    // This function invokes the enemyDeath event when the enemy's health reaches 0.
    public override void Die()
    {
        if (!isDead)
        {
            if (currentHealth <= 0)
            {
                isDead = true;
                EventManager.current.EnemyDeath(gameObject.GetInstanceID());
            }
        }
    }
}
