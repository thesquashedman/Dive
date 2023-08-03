using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //bool shouldMove = false;
    public float speed = 5f;
    public float rotationSpeed = 200f; // Rotation speed in degrees per second

    //public bool selfRight = false;

    Rigidbody2D rb;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        /*
        EventManager.current.onPlayerStartMove += StartMove;
        EventManager.current.onPlayerStopMove += StopMove;
        */
    }
    /*
    private void OnDisable() {
        EventManager.current.onPlayerStartMove -= StartMove;
        EventManager.current.onPlayerStopMove -= StopMove;
    }
    */

    // Update is called once per frame

    /*
    void StartMove()
    {
        shouldMove = true;
    }
    void StopMove()
    {
        shouldMove = false;
    }
    */
    
    void FixedUpdate()
    {
        if(PavelPlayerSettingStates.current.ForwardMove)
        {
            bool shouldMove = PavelPlayerSettingStates.current.isMoving;
            if(shouldMove)
            {
                Vector2 direction = PavelPlayerSettingStates.current.moveDirection;
                

                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
                rb.AddForce(transform.up * speed);
            }
            else
            {
                if(PavelPlayerSettingStates.current.selfRighting)
                {
                    float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 0, rotationSpeed * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, 0, angle);
                }
                
            }
        }
        else
        {
            Vector2 direction = PavelPlayerSettingStates.current.moveDirection;
                

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            

            bool shouldMove = PavelPlayerSettingStates.current.isMoving;
            if(shouldMove)
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
                
                rb.AddForce(direction * speed);
            }
            
        }
        
    }

}
