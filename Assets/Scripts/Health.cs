using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;

    private void Start() {
        EventManager.current.onHealPlayer += playerHeal;
    }

    void Update()
    {
        
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void SetHealth(float newCurrent)
    {
        currentHealth = newCurrent;
    }

    public virtual void ChangeHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }

        Debug.Log(currentHealth);
    }

    private void playerHeal(float amount) {
        //Debug.Log("Healing");
        if(gameObject.tag == "Player") {
            //Debug.Log("isPLayer");
            ChangeHealth(amount);
        }
    }

    public virtual void Die()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable() {
        EventManager.current.onHealPlayer -= playerHeal;
    }
}

