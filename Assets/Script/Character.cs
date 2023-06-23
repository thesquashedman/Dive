using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Basic Attribute")]
    public int maxHealth;
    public int currentHealth;

    [Header("Invulnerable")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable) {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0) {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker) {
        if (invulnerable)
            return;
        currentHealth -= attacker.damage;
        TriggerInvulnerable();
    }

    private void TriggerInvulnerable() {
        if (!invulnerable) {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
