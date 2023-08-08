using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAnimationController : MonoBehaviour
{
    // References to other components.
    private Animator animator;
    public WormBehavior wormBehavior;
    private int wormID;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        wormID = wormBehavior.gameObject.GetInstanceID();
        // EventManager.current.onEnemyAttackSuccess += SwitchToBiteAnimation;
    }

    private void OnEnable()
    {
        EventManager.current.onEnemyAttackSuccess += SwitchToBiteAnimation;
    }

    private void onDisable()
    {
        EventManager.current.onEnemyAttackSuccess -= SwitchToBiteAnimation;
    }

    private void FixedUpdate()
    {
        bool isBiting = animator.GetBool("isBiting");
        string mode = wormBehavior.GetMode();

        // Set the animator's parameters based on the current mode of this worm.
        if (mode == "dead" && animator.speed != 0f)
        {
            animator.speed = 0f;
        }
        else if (isBiting && animator.GetCurrentAnimatorStateInfo(0).IsName("Worm Bite"))
        {
            animator.SetBool("isBiting", false);
        }   
    }
    
    // This function plays the biting animation.
    private void SwitchToBiteAnimation(int objectID)
    {
        if (objectID == wormID)
        {
            animator.SetBool("isBiting", true);
        }
    }
}
