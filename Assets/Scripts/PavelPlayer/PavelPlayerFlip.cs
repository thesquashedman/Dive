using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerFlip : MonoBehaviour
{
    //Flip the scale of the object when the players rotation is changed
    // Start is called before the first frame update
    public float lowerAngle = 90;
    public float upperAngle = 270;

    bool moving = false;
    bool aiming = false;
    public bool isFlipped = false;
    void Start()
    {
        EventManager.current.onPlayerStartMove += OnPlayerStartMove;
        EventManager.current.onPlayerStopMove += OnPlayerStopMove;
        EventManager.current.onPlayerStartAiming += OnPlayerStartAiming;
        EventManager.current.onPlayerStopAiming += OnPlayerStopAiming;
    }
    private void OnDisable() {
        EventManager.current.onPlayerStartMove -= OnPlayerStartMove;
        EventManager.current.onPlayerStopMove -= OnPlayerStopMove;
        EventManager.current.onPlayerStartAiming -= OnPlayerStartAiming;
        EventManager.current.onPlayerStopAiming -= OnPlayerStopAiming;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(transform.eulerAngles);
        if(moving & !aiming)
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
            
            Vector2 direction = PavelPlayerController.current.aimDirection;
            //turn vector 2 into angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            while(angle < 0)
            {
                angle += 360;
            }
            
            float angle2 = transform.eulerAngles.z;
            //Debug.Log("angle " + angle);
            //Debug.Log("angle2 " + angle2);
            Debug.Log("angle2 - angle " + (angle2 - angle));
            
            
            if((angle2 - angle < 0  && angle2 - angle > -180) || (angle2 - angle > 180 && angle2 - angle < 360))
            {
                FlipScale(true);
            }
            else
            {
                FlipScale(false);
            }
           
            
            /*
            if(direction.x < 0)
            {
                FlipScale(true);
            }
            else
            {
                FlipScale(false);
            }
            */
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
    void OnPlayerStartAiming()
    {
        aiming = true;
    }
    void OnPlayerStopAiming()
    {
        aiming = false;
    }
    void FlipScale(bool flip)
    {
        if(flip)
        {
            transform.localScale = new Vector3(1, -1, 1);
            EventManager.current.playerFlip(true);
            isFlipped = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            EventManager.current.playerFlip(false);
            isFlipped = false;
        }
    }
}
