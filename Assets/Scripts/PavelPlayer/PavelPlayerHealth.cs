using UnityEngine;
using Lowscope.Saving;
public class PavelPlayerHealth : Health, ISaveable
{
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
}
