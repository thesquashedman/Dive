using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelFlip : MonoBehaviour
{
    //Flip the scale of the object when the players rotation is changed
    // Start is called before the first frame update
    public float lowerAngle = 90;
    public float upperAngle = 270;

    public bool moving = false;
    void Start()
    {
        EventManager.current.onPlayerStartMove += OnPlayerStartMove;
        EventManager.current.onPlayerStopMove += OnPlayerStopMove;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(transform.eulerAngles);
        if(moving)
        {
            if (transform.eulerAngles.z > lowerAngle && transform.eulerAngles.z < upperAngle)
            {
                FlipScale(true);
            }
            else
            {
                FlipScale(false);
            }
        }
        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            if(direction.x < 0)
            {
                FlipScale(true);
            }
            else
            {
                FlipScale(false);
            }
        }
        
    }
    void OnPlayerStartMove()
    {
        moving = true;
    }
    void OnPlayerStopMove()
    {
        moving = false;
    }
    void FlipScale(bool flip)
    {
        if(flip)
        {
            transform.localScale = new Vector3(1, -1, 1);
            EventManager.current.playerFlip(true);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            EventManager.current.playerFlip(false);
        }
    }
}
