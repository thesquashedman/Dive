using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public EnemyEventManager enemyEventManager;

    // This function issues a damage event through the specified event manager.
    public void TakeDamage(float amount)
    {
        enemyEventManager.DealDamageEnemy(amount);
    }
}
