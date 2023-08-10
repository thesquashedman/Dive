using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : Enemy
{
    public float maxHealth = 50f;

    public ParticleSystem myParticle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
        health.SetMaxHealth(maxHealth);

        EventManager.current.onDealDamageEnemy += playParticle;
    }

    private void playParticle(int objectID, float dmg) {
        if(myParticle != null && objectID == gameObject.GetInstanceID()) {
            myParticle.Play();
        }
    }

    protected override void Die(int objectID)
    {
        if (objectID == gameObject.GetInstanceID())
        {
            EventManager.current.onDealDamageEnemy -= DecreaseHealth;
            EventManager.current.onEnemyDeath -= Die;
            EventManager.current.onDealDamageEnemy -= playParticle;

            Destroy(gameObject);
        }
    }
}
