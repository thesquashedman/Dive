using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelWeaponContact : PavelWeapon
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        /*
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            attackPeriodTimer += Time.deltaTime;
            if (attackPeriodTimer > attackPeriod) {
                int damage = attackSystem.damageSystem.GetDamage();
                damage *= -1;
                other.gameObject.GetComponent<Health>().ChangeHealth(damage);

                attackPeriodTimer = 0;
            }

        }
        */
    }
}
