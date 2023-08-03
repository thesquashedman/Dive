using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    // The cooldown period between each attack.
    public float attackPeriod = 2f;

    // The timer for the cooldown period.
    private float attackPeriodTimer = 0f;

    // The tag of the target object.
    private string targetTag = "Player";

    // The ID of the enemy that this system belongs to.
    public int enemyID = 0;

    // The damage system of the enemy.
    public DamageSystem damageSystem;

    public float knockbackForce = 500f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (attackPeriodTimer > 0f)
        {
            attackPeriodTimer -= Time.deltaTime;
        }
    }

    public void SetTargetTag(string newTag)
    {
        targetTag = newTag;
    }

    // When there is a collision, this function issues an event based on the tag of the
    // collided object.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (attackPeriodTimer <= 0f)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                if (other.gameObject.tag == "Player")
                {
                    EventManager.current.dealDamagePlayer(damageSystem.GetDamage());
                    other.GetComponent<Rigidbody2D>().AddForce((other.transform.position - this.transform.position).normalized * knockbackForce);

                    if (enemyID != 0)
                    {
                        EventManager.current.EnemyAttackSuccess(enemyID);
                    }
                    
                    attackPeriodTimer = attackPeriod;
                }
            }
        }
    }
}
