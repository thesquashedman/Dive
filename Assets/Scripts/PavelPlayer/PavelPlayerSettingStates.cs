using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PavelPlayerSettingStates : MonoBehaviour
{
    public static PavelPlayerSettingStates current;

    #region Settings
    [Header("Settings")]
    [Tooltip("Use Mouse Movement scheme")]
    public bool mouseMovement = true;
    [Tooltip("Use WASD Movement scheme")]
    public bool WASDMovement = false;
    [Tooltip("Use Mobile Movement scheme")]
    public bool mobileMovement = false;
    [Tooltip("When aiming, flips the sprite when over 180 degrees")]
    public bool flipOnAim = true;
    [Tooltip("Self right when stopped moving")]
    public bool selfRighting = false;

    #endregion

    #region States
    [Header("States")]
    [Tooltip("Is the player moving?")]
    public bool isMoving = false;
    [Tooltip("Is the player flipped?")]
    public bool isFlipped = false;
    [Tooltip("Is the player aiming?")]
    public bool isAiming = false;
    [Tooltip("Is the player attacking?")]
    public bool isAttacking = false;
    [Tooltip("Direction to move if moving")]
    public Vector2 moveDirection = Vector2.zero;
    [Tooltip("Direction to aim if aiming")]
    public Vector2 aimDirection = Vector2.zero;
    [Tooltip("Is the player interacting?")]
    public bool isInteracting = false;

    [Tooltip("Is the player dead?")]
    public bool isDead = false;

    /*
    [Tooltip("Current Weapon of player, null mean's unequipped")]
    public PavelWeapon equippedWeapon;
    */
    #endregion


    private void Awake() {
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(current);
        } else {
            current = this;
        }
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
