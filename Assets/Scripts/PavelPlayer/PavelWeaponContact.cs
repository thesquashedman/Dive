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

    void Start()
    {
        attackPeriodTimer = 0;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        
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
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(attackReady)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null && other.gameObject.tag != "Player")
            {
                Debug.Log("Hit");
                other.gameObject.GetComponent<Health>().ChangeHealth(-damage);
                attackReady = false;
            }

            
        }
        
        
    }
}
