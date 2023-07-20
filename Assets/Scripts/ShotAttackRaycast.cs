using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotAttackRaycast : AttackSystem
{
    public bool attackOnce;
    public float damagePeriods;

    //public GameObject atackElement;

    //public GameObject objectToSpawn; // Assign the object prefab you want to spawn in the Inspector
    public float spawnInterval = 2.0f; // Time interval in seconds between spawns
    public float forceAmount = 10.0f; // Amount of force to add to the object
    public float attackRange = 50.0f;

    private float timer = 0f;

    public Transform shotPos;

    public bool OneBulletPerPress = false;

    public GameObject bulletTrail;

    //private AttackElement attackElement;

    private void Start()
    {
        //atackElement.SetActive(false);
        bulletTrail.SetActive(false);
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= (spawnInterval * 3f / 4f))
        {
            bulletTrail.SetActive(false);
        }
    }

    public override void Attack(string tragetTag)
    {
        if (OneBulletPerPress)
        {
            RaycastAttack();
        }
        else
        {
            if (timer <= 0f)
            {
                bulletTrail.SetActive(true);
                // Debug.Log("Raycast");
                RaycastAttack();
                timer = spawnInterval;
            }
        }
        //atackElement.GetComponent<AttackElement>().SetAttackTargetTag(tragetTag);
        //this.CloseAttackStart();
    }

    void RaycastAttack()
    {
        //// Define the ray
        //Ray2D ray = new Ray(shotPos.position, shotPos.right);
        //RaycastHit hit;

        //Debug.Log("Ray!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //// Cast the ray
        //if (Physics.Raycast(ray, out hit, attackRange))
        //{
        //    // Check if the object hit has the tag "Enemy"
        //    if (hit.collider.CompareTag("Enemie"))
        //    {
        //        //Debug.Log("Enemy!");
        //        int damage = damageSystem.GetDamage();
        //        damage *= -1;
        //        hit.transform.GetComponent<Health>().ChangeHealth(damage);
        //        Debug.Log(hit.transform.name);
        //    }
        //}

        // Debug.Log("Raycast2");

        RaycastHit2D hit = Physics2D.Raycast(shotPos.position, shotPos.right, attackRange);
        Debug.DrawRay(shotPos.position, shotPos.right * attackRange, Color.red, 10.0f);

        // Check if the ray hit something
        if (hit.collider != null)
        {
            // Debug.Log("Raycast3");

            // Debug.Log(hit.transform.name);
            // When there is a hit, issue an event based on the tag of the hit object.
            if (hit.collider.tag == "Enemie")
            {
                EventManager.current.DealDamageEnemy(hit.collider.gameObject.GetInstanceID(), damageSystem.GetDamage());
            }
            else if (hit.collider.tag == "Player")
            {
                
            }
            else if (hit.collider.tag == "BodyPart")
            {
                EventManager.current.DealDamageEnemy(hit.collider.gameObject.GetInstanceID(), damageSystem.GetDamage());
            }
        }
    }

        //public override void StopAttack()
        //{
        //    this.CloseAttackEnd();
        //}

        //public void CloseAttackStart()
        //{
        //    // Debug.Log("AAAAAAAAA");
        //    atackElement.SetActive(true);
        //}

        //public void CloseAttackEnd()
        //{
        //    atackElement.SetActive(false);
        //}

        //public void CloseAttackFinsih()
        //{
        //    atackElement.SetActive(false);
        //}
}
