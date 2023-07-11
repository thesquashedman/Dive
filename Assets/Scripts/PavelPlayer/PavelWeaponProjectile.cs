using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelWeaponProjectile : PavelWeapon
{
    // Start is called before the first frame update
    public float attackPeriod = 0.25f;
    public float attackPeriodTimer = 0f;
    public float damage = 10f;
    public float projectileSpeed = 100f;
    bool attackReady = false;

    public GameObject projectilePrefab;

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
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        projectile.transform.position = firePoint.position;
        projectile.GetComponent<PavelBullet>().damage = damage;
        projectile.GetComponent<PavelBullet>().enemyTags = enemyTags;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * projectileSpeed, ForceMode2D.Impulse);
        attackPeriodTimer = 0;
        attackReady = false;

    }
}
