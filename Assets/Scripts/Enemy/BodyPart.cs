using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    // The ID of the enemy that this body part belongs to.
    public int enemyID;

    private void Start()
    {
        EventManager.current.onDealDamageEnemy += TakeDamage;
        EventManager.current.onEnemyDeath += Die;
    }

    private void OnDisable()
    {
        EventManager.current.onDealDamageEnemy -= TakeDamage;
        EventManager.current.onEnemyDeath -= Die;
    }

    // This function issues a damage event through the event manager.
    private void TakeDamage(int objectID, float amount)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            EventManager.current.DealDamageEnemy(enemyID, amount);
        }
    }

    // This function unsubscribes from events when the enemy dies.
    private void Die(int objectID)
    {
        if (objectID == enemyID)
        {
            EventManager.current.onDealDamageEnemy -= TakeDamage;
            EventManager.current.onEnemyDeath -= Die;
        }
    }
}
