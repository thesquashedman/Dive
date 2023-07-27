using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerHealth : Health
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onDealDamagePlayer += DealDamage;
        EventManager.current.onHealPlayer += ChangeHealth;
    }
    private void OnDisable() {
        EventManager.current.onDealDamagePlayer -= DealDamage;
        EventManager.current.onHealPlayer -= ChangeHealth;
    }

    // Update is called once per frame
    void DealDamage(float damage)
    {
        ChangeHealth(-damage);
    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
        EventManager.current.ChangeHealth(currentHealth);
    }

    public override void SetHealth(float newCurrent)
    {
        base.SetHealth(newCurrent);
        EventManager.current.ChangeHealth(currentHealth);
    }

    public override void Die()
    {
        EventManager.current.playerDeath();
    }
}
