using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    // The ID of the enemy that this body part belongs to.
    public int enemyID;

    // This function issues a damage event through the event manager.
    public void TakeDamage(float amount)
    {
        EventManager.current.DealDamageEnemy(enemyID, amount);
    }
}
