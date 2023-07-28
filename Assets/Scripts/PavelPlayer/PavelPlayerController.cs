using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    /*

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
    */

    // Update is called once per frame
    void Update()
    {
        if(PavelPlayerSettingStates.current.mobileMovement)
        {
            MobileMovementInput();
        }
        else
        {
            if (PavelPlayerSettingStates.current.WASDMovement)
            {
                WASDMovementInput();
            }
            else if (PavelPlayerSettingStates.current.mouseMovement)
            {
                MouseMovementInput();
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PavelPlayerSettingStates.current.isAttacking = true;
                EventManager.current.PlayerAttack();
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                PavelPlayerSettingStates.current.isAttacking = false;
                EventManager.current.PlayerStopAttack();
            }
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                EventManager.current.PlayerSwitchWeapon("ProjectileGun");
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                EventManager.current.PlayerSwitchWeapon("Saw");
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                EventManager.current.PlayerSwitchWeapon("Unarmed");
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                EventManager.current.PlayerSwitchWeapon("RaycastGun");
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                PavelPlayerSettingStates.current.isInteracting = true;
                EventManager.current.PlayerInteract();
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                PavelPlayerSettingStates.current.isInteracting = false;
            }
        }
        
        
        
    }
    void MobileMovementInput()
    {
        // Mobile Aiming
        PavelPlayerSettingStates.current.aimDirection = MobileController.current.directionRight;
        if (PavelPlayerSettingStates.current.aimDirection != Vector2.zero) {
            PavelPlayerSettingStates.current.isAiming = true;
            EventManager.current.PlayerStartAiming();
        }
        else {
            PavelPlayerSettingStates.current.isAiming = false;
            EventManager.current.PlayerStopAiming();
        }

        // Mobile Moving
        PavelPlayerSettingStates.current.moveDirection = MobileController.current.directionLeft;
        if (PavelPlayerSettingStates.current.moveDirection != Vector2.zero) {
            PavelPlayerSettingStates.current.isMoving = true;
            EventManager.current.PlayerStartMove();
        }
        else {
            PavelPlayerSettingStates.current.isMoving = false;
            EventManager.current.PlayerStopMove();
        }

        // Mobile Attacking
        PavelPlayerSettingStates.current.isAttacking = MobileController.current.isAttacking;
        if (PavelPlayerSettingStates.current.isAttacking) {
            EventManager.current.PlayerAttack();
        }
        else {
            EventManager.current.PlayerStopAttack();
        }

        // Mobile Interacting
        PavelPlayerSettingStates.current.isInteracting = MobileController.current.isInteracting;
        if (PavelPlayerSettingStates.current.isInteracting) {
            EventManager.current.PlayerInteract();
        }

        // Mobile Switching Weapon
        // private string[] weapons = {"Unequipped", "Saw", "ProjectileGun", "RaycastGun"};
        // private weaponIndex = MobileController.current.weaponIndex;
        EventManager.current.PlayerSwitchWeapon(MobileController.current.weaponName);

    }
    void WASDMovementInput()
    {
        if(!PavelPlayerSettingStates.current.isAiming)
        {
            PavelPlayerSettingStates.current.isAiming = true;
            EventManager.current.PlayerStartAiming();
        }
        PavelPlayerSettingStates.current.moveDirection = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            PavelPlayerSettingStates.current.moveDirection += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S))
        {
            PavelPlayerSettingStates.current.moveDirection += Vector2.down;
        }
        if(Input.GetKey(KeyCode.A))
        {
            PavelPlayerSettingStates.current.moveDirection += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            PavelPlayerSettingStates.current.moveDirection += Vector2.right;
        }
        PavelPlayerSettingStates.current.moveDirection.Normalize();
        //direction.Normalize();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PavelPlayerSettingStates.current.aimDirection = (mousePosition - (Vector2)transform.position).normalized;
        

        if(PavelPlayerSettingStates.current.moveDirection != Vector2.zero && !PavelPlayerSettingStates.current.isMoving)
        {
            PavelPlayerSettingStates.current.isMoving = true;
            EventManager.current.PlayerStartMove();
        }
        else if(PavelPlayerSettingStates.current.moveDirection == Vector2.zero && PavelPlayerSettingStates.current.isMoving)
        {
            PavelPlayerSettingStates.current.isMoving = false;
            EventManager.current.PlayerStopMove();
        }

    }
    void MouseMovementInput()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PavelPlayerSettingStates.current.aimDirection = (mousePosition - (Vector2)transform.position).normalized;
        PavelPlayerSettingStates.current.moveDirection = (mousePosition - (Vector2)transform.position).normalized;
        

        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            PavelPlayerSettingStates.current.isMoving = true;
            EventManager.current.PlayerStartMove();
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            PavelPlayerSettingStates.current.isMoving = false;
            EventManager.current.PlayerStopMove();
        }
        if(!PavelPlayerSettingStates.current.isMoving && !PavelPlayerSettingStates.current.isAiming)
        {
            PavelPlayerSettingStates.current.isAiming = true;
            EventManager.current.PlayerStartAiming();
        }
        else if(PavelPlayerSettingStates.current.isMoving && PavelPlayerSettingStates.current.isAiming)
        {
            PavelPlayerSettingStates.current.isAiming = false;
            EventManager.current.PlayerStopAiming();
        }
        
        
    }
    
}
