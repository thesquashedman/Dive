using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererAnimationController : MonoBehaviour
{
    // References to other components.
    private Animator animator;
    public WandererBehavior wandererBehavior;
    private int wandererID;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        wandererID = wandererBehavior.gameObject.GetInstanceID();
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
        bool isMoving = animator.GetBool("isMoving");
        bool isBiting = animator.GetBool("isBiting");
        string mode = wandererBehavior.GetMode();

        // Set the animator's parameters based on the current mode of this wanderer.
        if (mode == "dead" && animator.speed != 0f)
        {
            animator.speed = 0f;
        }
        else if (isBiting && animator.GetCurrentAnimatorStateInfo(0).IsName("Small Fish Bite"))
        {
            animator.SetBool("isBiting", false);
        }
        else if (isMoving && mode == "idle")
        {
            animator.SetBool("isMoving", false);
        }
        else if (!isMoving && mode != "idle")
        {
            animator.SetBool("isMoving", true);
        }        
    }
    
    // This function plays the biting animation.
    private void SwitchToBiteAnimation(int objectID)
    {
        if (objectID == wandererID)
        {
            animator.SetBool("isBiting", true);
        }
    }
}
