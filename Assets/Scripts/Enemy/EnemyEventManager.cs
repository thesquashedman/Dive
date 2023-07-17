using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyEventManager : MonoBehaviour
{
    // Add functions to trigger when the enemy receives damage.
    public event Action<float> onDealDamageEnemy;

    ///<summary>
    /// This function invokes an event to deal with the situation
    /// in which the enemy takes damage.
    ///</summary>
    public void DealDamageEnemy(float damage)
    {
        onDealDamageEnemy?.Invoke(damage);
    }

    // Add functions to trigger when the enemy dies.
    public event Action onEnemyDeath;

    ///<summary>
    /// This functions invokes an event to deal with the death of the enemy.
    ///</summary>
    public void EnemyDeath()
    {
        onEnemyDeath?.Invoke();
    }

    // Add functions to trigger when the enemy starts attacking.
    public event Action onEnemyAttack;

    ///<summary>
    /// This functions invokes an event to indicate that the enemy starts attacking.
    /// This event should only be triggered once for each attack attempt.
    ///</summary>
    public void EnemyAttack()
    {
        onEnemyAttack?.Invoke();
    }
}
