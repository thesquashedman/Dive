using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelWeaponRaycast : PavelWeapon
{
    public float attackPeriod = 0.25f;
    public float attackPeriodTimer = 0f;
    public float damage = 10f;
    public float attackRange = 50.0f;
    bool attackReady = false;
    public string[] enemyTags;

    public Transform firePoint;
    void Start()
    {
        attackPeriodTimer = attackPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if(PavelPlayerSettingStates.current.isAttacking)
        {
            
            if(attackReady)
            {
                FireProjectile();
            }
            
        }
        if(!attackReady)
        {
            attackPeriodTimer += Time.deltaTime;
        }
        if(!attackReady && attackPeriodTimer > attackPeriod)
        {
            attackPeriodTimer = 0;
            attackReady = true;
            
        }
    }
    void FireProjectile()
    {

        //need to change raycast to ignore player layer
        LayerMask mask = LayerMask.GetMask("Player");
        mask = ~mask;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, attackRange, mask);
        Debug.DrawRay(firePoint.position, firePoint.right * attackRange, Color.red, 50.0f);

        

        // Check if the ray hit something
        if (hit.collider != null)
        {

            Debug.Log(hit.transform.name);
            // Check if the object hit has the tag "Enemy"
            if (enemyTags.Length > 0)
            {
                foreach (string tag in enemyTags)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        // Get the Health component and call ChangeHealth
                        Health enemyHealth = hit.collider.GetComponent<Health>();
                        if (enemyHealth != null)
                        {
                            
                            enemyHealth.ChangeHealth(-damage);
                        }
                    }
                }
            }
            
        }
        attackPeriodTimer = 0;
        attackReady = false;

    }
}
