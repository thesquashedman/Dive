using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyEventManager enemyEventManager;

    // Start is called before the first frame update
    private void Start()
    {
        enemyEventManager = GetComponent<EnemyEventManager>();    
    }

    // This function invokes the enemyDeath event when the enemy's health reaches 0.
    public override void Die()
    {
        if (currentHealth <= 0)
        {
            enemyEventManager.EnemyDeath();
        }
    }
}
