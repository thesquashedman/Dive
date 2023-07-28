using UnityEngine;
using Lowscope.Saving;
using System.Collections;
public class PavelPlayerHealth : Health, ISaveable
{

    public float healthRejenerations = 1;
    public float regenerationPeriod = 5f;
    public float timer = 0f;

    // Start is called before the first frame update
    public struct SaveData
    {
        public float health;
        public float maxHealth;


    }
    void Update()
    {
        if(currentHealth <= 0 && !PavelPlayerSettingStates.current.isDead)
        {
            this.Die();
        }
        HelthRegeneration();
    }
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
        PavelPlayerSettingStates.current.isDead = true;
        EventManager.current.playerDeath();
        

    }

    public string OnSave()
    {
        return JsonUtility.ToJson(new SaveData { health = currentHealth, maxHealth = maxHealth  });
    }

    public void OnLoad(string data)
    {
       currentHealth = JsonUtility.FromJson<SaveData>(data).health;
       if(currentHealth < 50)
       {
           currentHealth = 50;
       }
       maxHealth = JsonUtility.FromJson<SaveData>(data).maxHealth;
    }


    public bool OnSaveCondition()
    {
       return !PavelPlayerSettingStates.current.isDead;
    }

    void HelthRegeneration() {
        timer += Time.deltaTime;

        if (timer >= regenerationPeriod)
        {
            if (currentHealth < 100)
            {
                base.ChangeHealth(healthRejenerations);
            }
            timer = 0;
        }
    }
}
