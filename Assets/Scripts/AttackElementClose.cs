using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackElementClose : AttackElement
{

    public float attackRange; // Example: range within which attack is effective

    public float attackPeriod = 2f;
    private float attackPeriodTimer = 0f;

    // Optionally, create a constructor for the subclass
    public AttackElementClose(AttackSystem attackSystem, float attackRange) : base(attackSystem)
    {
        this.attackRange = attackRange;
    }

    void Update()
    {
        if (attackPeriodTimer > 0f)
        {
            attackPeriodTimer -= Time.deltaTime;
        }
    }


    private bool IsTargetInRange(Vector3 targetPosition)
    {
        // Implement a method that checks if the target is within the attack range
        return Vector3.Distance(transform.position, targetPosition) <= attackRange;
    }

    // When there is a collision, issue an event based on the tag of the collided object.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (attackPeriodTimer <= 0f)
        {
            if (other.gameObject.CompareTag(tragetTag))
            {
                if (other.gameObject.tag == "Enemie")
                {
                    other.gameObject.GetComponent<EnemyEventManager>().DealDamageEnemy(attackSystem.damageSystem.GetDamage());
                    attackPeriodTimer = attackPeriod;
                }
                else if (other.gameObject.tag == "Player")
                {
                
                }
            }
            else
            {
                if (tragetTag == "Enemie" && other.gameObject.tag == "BodyPart")
                {
                    other.gameObject.GetComponent<BodyPart>().TakeDamage(attackSystem.damageSystem.GetDamage());
                    attackPeriodTimer = attackPeriod;
                }
            }
        }
    }
}
