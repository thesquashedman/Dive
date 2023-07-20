using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelAnimationController : MonoBehaviour
{
    // References to other components.
    private Animator animator;
    public EelBehavior eelBehavior;
    private int eelID;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        eelID = eelBehavior.gameObject.GetInstanceID();
        EventManager.current.onEnemyAttackSuccess += SwitchToBiteAnimation;
    }

    private void onDisable()
    {
        EventManager.current.onEnemyAttackSuccess -= SwitchToBiteAnimation;
    }

    private void FixedUpdate()
    {
        bool isBiting = animator.GetBool("isBiting");
        string mode = eelBehavior.GetMode();

        // Set the animator's parameters based on the current mode of this eel.
        if (mode == "dead" && animator.speed != 0f)
        {
            animator.speed = 0f;
        }
        else if (isBiting && animator.GetCurrentAnimatorStateInfo(0).IsName("Eel Bite"))
        {
            animator.SetBool("isBiting", false);
        }   
    }
    
    // This function plays the biting animation.
    private void SwitchToBiteAnimation(int objectID)
    {
        if (objectID == eelID)
        {
            animator.SetBool("isBiting", true);
        }
    }
}