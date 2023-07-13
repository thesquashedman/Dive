using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackElement : MonoBehaviour
{
    public AttackSystem attackSystem;

    public Sprite sprite;
    protected Collider attackElementCollider;
    public string tragetTag = "";

    public AttackElement(AttackSystem attackSystem)
    {
        this.attackSystem = attackSystem;
    }

    public void Attack()
    {
        // Here you could return an event to AttackSystem.
        // Implement the attack logic.
    }

    public void AttackEffect()
    {
        // Implement the attack effect logic.
    }

    public void AttackMoveEffect()
    {
        // Implement the attack move effect logic.
    }

    public void AttackDestroyEffect()
    {
        // Implement the attack destroy effect logic.
    }

    // When there is a collision, issue an event based on the tag of the collided object.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tragetTag))
        {
            if (other.gameObject.tag == "Enemie")
            {
                other.gameObject.GetComponent<EnemyEventManager>().DealDamageEnemy(attackSystem.damageSystem.GetDamage());
            }
            else if (other.gameObject.tag == "Player")
            {
                
            }
        }
        else
        {
            if (tragetTag == "Enemie" && other.gameObject.tag == "BodyPart")
            {
                other.gameObject.GetComponent<BodyPart>().TakeDamage(attackSystem.damageSystem.GetDamage());
            }
        }
    }

    public void SetAttackTargetTag(string tragetTag)
    {
        this.tragetTag = tragetTag;
    }
}
