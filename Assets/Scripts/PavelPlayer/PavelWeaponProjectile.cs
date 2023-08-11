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
    public bool useBullet1 = true;
    public bool useBullet2 = false;

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
                if(useBullet1)
                {
                    if(PlayerResourcesSystem.current.bullets1 > 0)
                    {
                        FireProjectile();
                    }
                    else
                    {
                        EmptyClip();
                    }
                }
                else if(useBullet2)
                {
                    if(PlayerResourcesSystem.current.bullets2 > 0)
                    {
                        FireProjectile();
                    }
                    else
                    {
                        EmptyClip();
                    }
                }
                
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
        Debug.Log("fire");
        EventManager.current.PlayPlayerRecoil();
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.transform.rotation = firePoint.transform.rotation;
        projectile.transform.position = firePoint.position;
        projectile.GetComponent<PavelBullet>().damage = damage;
        projectile.GetComponent<PavelBullet>().enemyTags = enemyTags;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * projectileSpeed, ForceMode2D.Impulse);
        attackPeriodTimer = 0;
        attackReady = false;
        if(useBullet1)
        {
            PlayerResourcesSystem.current.bullets1 -= 1;
        }
        else if(useBullet2)
        {
            PlayerResourcesSystem.current.bullets2 -= 1;
        }
        

    }
    void EmptyClip()
    {
        attackPeriodTimer = 0;
        attackReady = false;
    }
}
