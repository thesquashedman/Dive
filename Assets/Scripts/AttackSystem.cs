using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public string EnemyClass { get; set; }

    public DamageSystem damageSystem;

    public AttackSystem()
    {
        damageSystem = new DamageSystem();
    }

    public virtual void Attack(string tragetTag) {
        //Debug.Log("BBBBBBBBBB");
    }

    public virtual void StopAttack()
    {
        //Debug.Log("BBBBBBBBBB");
    }

    public void ApplyDamage(Health health)
    {
        int damage = damageSystem.GetDamage();
        damage *= -1;
        health.ChangeHealth(damage);
    }
}
