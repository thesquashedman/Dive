using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackElementShoot : AttackElement
{

    public float attackRange; // Example: range within which attack is effective

    public float attackPeriod = 2;
    public float attackPeriodTimer = 0;

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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tragetTag))
        {
            Debug.Log("Attack");
            int damage = attackSystem.damageSystem.GetDamage();
            damage *= -1;
            other.gameObject.GetComponent<Health>().ChangeHealth(damage);
        }
        if ((other.gameObject.tag != "Player") && (other.gameObject.tag  != "Bullet")) {
            Destroy(this.gameObject);
        }
    }
}
