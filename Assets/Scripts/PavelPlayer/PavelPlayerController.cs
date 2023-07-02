using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    bool mouseMovement = true;

    [SerializeField]
    bool WASDMovement = false;
    [SerializeField]
    bool mobileMovement = false;

    public Vector2 direction = Vector2.zero;
    public Vector2 aimDirection = Vector2.zero;
    public bool isMoving = false;
    public bool isAiming = false;
    public static PavelPlayerController current;

    private void Awake() {
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
            //Debug.Log("EventManager Active.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mobileMovement)
        {
            MobileMovementInput();
        }
        else
        {
            if (WASDMovement)
            {
                WASDMovementInput();
            }
            else if (mouseMovement)
            {
                MouseMovementInput();
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                EventManager.current.PlayerAttack();
            }
            if(Input.GetKeyUp(KeyCode.X))
            {
                EventManager.current.PlayerStopAttack();
            }
        }
        
        
        
    }
    void MobileMovementInput()
    {

    }
    void WASDMovementInput()
    {
        if(!isAiming)
        {
            isAiming = true;
            EventManager.current.PlayerStartAiming();
        }
        direction = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if(Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        direction.Normalize();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePosition - (Vector2)transform.position).normalized;
        

        if(direction != Vector2.zero && !isMoving)
        {
            isMoving = true;
            EventManager.current.PlayerStartMove();
        }
        else if(direction == Vector2.zero && isMoving)
        {
            isMoving = false;
            EventManager.current.PlayerStopMove();
        }
    }
    void MouseMovementInput()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, transform.position);
        direction = (mousePosition - (Vector2)transform.position).normalized;
        aimDirection = direction;

        
        if(distance < 0.5f && isMoving)
        {
            isMoving = false;
            EventManager.current.PlayerStopMove();
            
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            
            isMoving = true;
            EventManager.current.PlayerStartMove();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false;
            EventManager.current.PlayerStopMove();
        }
        if(isMoving == false && isAiming == false)
        {
            
            isAiming = true;
            EventManager.current.PlayerStartAiming();
        }
        else if(isMoving == true && isAiming == true)
        {
            isAiming = false;
            EventManager.current.PlayerStopAiming();
        }
        
    }
    
}
