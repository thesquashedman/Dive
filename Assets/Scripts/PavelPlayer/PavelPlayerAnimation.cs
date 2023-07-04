using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator lowerAnimator;
    void Start()
    {
        lowerAnimator.SetBool("Swimming", false);
        EventManager.current.onPlayerStartMove += isMoving;
        EventManager.current.onPlayerStopMove += isNotMoving;
    }
    void OnDisable()
    {
        EventManager.current.onPlayerStartMove -= isMoving;
        EventManager.current.onPlayerStopMove -= isNotMoving;
    }

    // Update is called once per frame
    void isMoving()
    {
        lowerAnimator.SetBool("Swimming", true);
    }
    void isNotMoving()
    {
        lowerAnimator.SetBool("Swimming", false);
    }
    void Update()
    {
        
    }
}
