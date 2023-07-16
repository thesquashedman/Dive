using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackElementShoot : AttackElement
{

    public float attackRange; // Example: range within which attack is effective

    public float attackPeriod = 2f;
    private float attackPeriodTimer = 0f;

    private void Start()
    {
        GameObject thisObj = this.gameObject;
        gameObject.SetActive(true);
    }

    // Optionally, create a constructor for the subclass
    public AttackElementShoot(AttackSystem attackSystem, float attackRange) : base(attackSystem)
    {
        this.attackRange = attackRange;
    }


    private bool IsTargetInRange(Vector3 targetPosition)
    {
        // Implement a method that checks if the target is within the attack range
        return Vector3.Distance(transform.position, targetPosition) <= attackRange;
    }

    // When there is a collision, issue an event based on the tag of the collided object.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tragetTag))
        {
            if (other.gameObject.tag == "Enemie")
            {
                EventManager.current.DealDamageEnemy(other.gameObject.GetInstanceID(), attackSystem.damageSystem.GetDamage());
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
            }
        }

        if ((other.gameObject.tag != "Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
