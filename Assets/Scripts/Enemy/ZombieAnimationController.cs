using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    // References to other components.
    private Animator animator;
    public ZombieBehavior zombieBehavior;
    private int zombieID;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        zombieID = zombieBehavior.gameObject.GetInstanceID();
        // EventManager.current.onEnemyAttackSuccess += SwitchToBiteAnimation;
    }

    private void OnEnable()
    {
        EventManager.current.onEnemyAttackSuccess += SwitchToAttackAnimation;
    }

    private void onDisable()
    {
        EventManager.current.onEnemyAttackSuccess -= SwitchToAttackAnimation;
    }

    private void FixedUpdate()
    {
        bool isAttacking = animator.GetBool("isAttacking");
        string mode = zombieBehavior.GetMode();

        // Set the animator's parameters based on the current mode of this zombie.
        if (animator.speed != 0f && mode == "dead")
        {
            animator.speed = 0f;
        }
        else if (animator.speed == 0f && zombieBehavior.IsAwake() && mode != "dead")
        {
            animator.speed = 1f;
        }
        else if (isAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack"))
        {
            animator.SetBool("isAttacking", false);
        }
    }
    
    // This function plays the attacking animation.
    private void SwitchToAttackAnimation(int objectID)
    {
        if (objectID == zombieID)
        {
            animator.SetBool("isAttacking", true);
        }
    }
}
