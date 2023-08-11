using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelWeaponContact : PavelWeapon
{
    // Start is called before the first frame update
    public float attackPeriod = 0.25f;
    public float attackPeriodTimer = 0f;
    public float damage = 10f;
    bool attackReady = false;

    public string[] enemyTags;

    void Start()
    {
        attackPeriodTimer = 0;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        EventManager.current.onPlayerAttack += TriggerRecoil;

        
    }

    void TriggerRecoil()
    {
        EventManager.current.PlayPlayerRecoil();
    }

    // Update is called once per frame
    void Update()
    {
        if(PavelPlayerSettingStates.current.isAttacking)
        {
            
            if(!attackReady)
            {
                attackPeriodTimer += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            }
            
        }
        else
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        if(!attackReady && attackPeriodTimer > attackPeriod)
        {
            attackPeriodTimer = 0;
            attackReady = true;
            
        }
        
        
    }
    private void OnTriggerStay2D(Collider2D hit)
    {
        
        if(attackReady)
        {
            if (enemyTags.Length > 0)
            {
                foreach (string tag in enemyTags)
                {
                    if (hit.gameObject.CompareTag(tag))
                    {
                        EventManager.current.DealDamageEnemy(hit.gameObject.GetInstanceID(), damage);
                    }
                }
            }
            attackReady = false;

            
        }
        
        
    }

    private void OnDisable() {
        EventManager.current.onPlayerAttack -= TriggerRecoil;
    }
}
