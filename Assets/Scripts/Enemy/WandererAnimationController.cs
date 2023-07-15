using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererAnimationController : MonoBehaviour
{
    // References to other components.
    private Animator animator;
    public WandererBehavior wandererBehavior;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        bool isMoving = animator.GetBool("isMoving");
        string mode = wandererBehavior.GetMode();

        // Set the animator's parameters based on the current mode of this wanderer.
        if (isMoving && mode == "idle")
        {
            animator.SetBool("isMoving", false);
        }
        else if (!isMoving && mode != "idle")
        {
            animator.SetBool("isMoving", true);
        }        
    }
}
