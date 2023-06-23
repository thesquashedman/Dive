using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackElementClose : AttackElement
{

    public float attackRange; // Example: range within which attack is effective

    // Optionally, create a constructor for the subclass
    public AttackElementClose(AttackSystem attackSystem, float attackRange) : base(attackSystem)
    {
        this.attackRange = attackRange;
    }


    private bool IsTargetInRange(Vector3 targetPosition)
    {
        // Implement a method that checks if the target is within the attack range
        return Vector3.Distance(transform.position, targetPosition) <= attackRange;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tragetTag))
        {
            //Debug.Log(other.gameObject.name);
            //attackSystem.ApplyDamage(other.gameObject.GetComponent<Health>());

            int damage = attackSystem.damageSystem.GetDamage();
            damage *= -1;
            other.gameObject.GetComponent<Health>().ChangeHealth(damage);
        }
    }

}
