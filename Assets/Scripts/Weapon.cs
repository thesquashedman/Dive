using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "Weapon";
    public string tragetTag = "Enemie";
    public int damage = 10;
    public float range = 5.0f;

    public bool attackMode = false; //for test

    [SerializeField] DamageSystem damageSystem;
    [SerializeField] AttackSystem attackSystem;

    private void Start()
    {
        damageSystem = GetComponent<DamageSystem>();
        attackSystem = GetComponent<AttackSystem>();
    }

    private void Update()
    {
        if (attackMode) {
            Attack();
        }
    }

    // This is a virtual method, meaning it can be overridden by derived classes
    public virtual void Attack()
    {

        attackSystem.Attack(tragetTag);

        // Logic for the basic weapon attack
        Debug.Log(weaponName + " attacks with " + damage + " damage!");
    }

    public virtual void StopAttack()
    {

        attackSystem.StopAttack();

        // Logic for the basic weapon attack
        Debug.Log(weaponName + " attacks with " + damage + " damage!");
    }

    // A method that derived classes may also override, if needed
    public virtual void SpecialAttack()
    {
        // Logic for the basic weapon special attack
        Debug.Log(weaponName + " performs a special attack!");
    }
}
