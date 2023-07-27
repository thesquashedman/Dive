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
            Die();
        }
    }
    void Start()
    {
        EventManager.current.onDealDamagePlayer += DealDamage;
    }
    private void OnDisable() {
        EventManager.current.onDealDamagePlayer -= DealDamage;
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
       maxHealth = JsonUtility.FromJson<SaveData>(data).maxHealth;
    }


    public bool OnSaveCondition()
    {
       return true;
    }
}
