using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
Written by Justin.
Class is meant to hold and notify subscribers when events happen 
for the game. 

To use: add a static event variable, with a corresponding method
that invokes the event when necessary. 

In another class, on Start()
subscribe that class to an event, register a method, and also create
an OnDisable() in case of deletion that unsubscribes the event.

Each event can have any number of subscriptions attached to it.

Then, wherever else call the event method, this will trigger the event
for all classes subscribed to the specific event.
*/

public class EventManager : MonoBehaviour
{
    //Singleton instance
    public static EventManager current;

    private void Awake() {
        Debug.Log("EventManager Active.");
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
            //Debug.Log("EventManager Active.");
        }
    }

    
    //Events

    //Event for object interaction
    //Method takes in an integer id, to know which object to activate
    public event Action<int> onObjInteract;

    public void objInteract(int id) {
        onObjInteract?.Invoke(id);
    }

    ///<summary>
    ///Add functions to trigger when the player receives damage
    ///</summary>
    public event Action<float> onDealDamagePlayer;

    ///<summary>
    ///Invoke event to deal damage to player
    ///</summary>
    public void dealDamagePlayer(float damage) {
        onDealDamagePlayer?.Invoke(damage);
    }

    ///<summary>
    ///FOR UI, add functions to trigger when the player changes health. Float is the new health.
    ///</summary>
    public event Action<float> onChangeHealth;

    ///<summary>
    ///Invoke event when health is changed. SHOULD ONLY EVER BE CALLED BY PLAYER HEALTH, DOES NOT ACTUALLY CHANGE HEALTH.
    ///</summary>
    public void ChangeHealth(float newHealth) {
        onChangeHealth?.Invoke(newHealth);
    }

    ///<summary>
    ///Add functions to trigger when the player dies
    ///</summary>
    public event Action onPlayerDeath;

    ///<summary>
    ///Trigger to kill the player
    ///</summary>
    public void playerDeath() {
        onPlayerDeath?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player sprite flips, true = flipped false = normal
    ///</summary>
    public event Action<bool> onPlayerFlip;

    ///<summary>
    ///Trigger when the player flips
    ///</summary>
    public void playerFlip(bool flipped) {
        onPlayerFlip?.Invoke(flipped);
    }

    ///<summary>
    ///Add functions to trigger when the player sprite flips
    ///</summary>
    public event Action onPlayerSuffocate;

    ///<summary>
    ///Trigger to suffocate player
    ///</summary>
    public void playerSuffocate() {
        onPlayerSuffocate?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player picks up a resource
    ///</summary>
    public event Action<string, int> onPlayerPickupResource;

    ///<summary>
    ///Trigger to pick up a resoruce
    ///</summary>
    public void playerPickupResource(string resourceName, int amount) {
        onPlayerPickupResource?.Invoke(resourceName, amount);
    }

    ///<summary>
    ///Triggers when a task is completed in the level.
    ///Uses an name-based system to determine which systems should be alerted.
    ///</summary>
    public event Action<string> onTaskCompleted;

    ///<summary>
    ///Trigger to complete a task by name.
    ///</summary>
    public void taskCompleted(string id) {
        onTaskCompleted?.Invoke(id);
    }
    ///Add functions to trigger when the player attacks
    ///</summary>
    public event Action onPlayerAttack;

    ///<summary>
    ///Trigger player's attack
    ///</summary>
    public void PlayerAttack() {
        onPlayerAttack?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player stops attacking
    ///</summary>
    public event Action onPlayerStopAttack;

    ///<summary>
    ///Stop Player's Attack
    ///</summary>
    public void PlayerStopAttack() {
        onPlayerStopAttack?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player moves
    ///</summary>
    public event Action onPlayerStartMove;

    ///<summary>
    ///Trigger player's movement
    ///</summary>
    public void PlayerStartMove() {
        onPlayerStartMove?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player stops moving
    ///</summary>
    public event Action onPlayerStopMove;

    ///<summary>
    ///Trigger player to stop moving
    ///</summary>
    public void PlayerStopMove() {
        onPlayerStopMove?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player moves
    ///</summary>
    public event Action onPlayerStartAiming;

    ///<summary>
    ///Trigger player's movement
    ///</summary>
    public void PlayerStartAiming() {
        onPlayerStartAiming?.Invoke();
    }

    ///<summary>
    ///Add functions to trigger when the player stops moving
    ///</summary>
    public event Action onPlayerStopAiming;

    ///<summary>
    ///Trigger player to stop moving
    ///</summary>
    public void PlayerStopAiming() {
        onPlayerStopAiming?.Invoke();
    }


    ///<summary>
    ///Add functions to trigger when the player stops moving
    ///</summary>
    public event Action<string> onPlayerSwitchWeapon;

    ///<summary>
    ///Trigger player to stop moving
    ///</summary>
    public void PlayerSwitchWeapon(string weaponName) {
        onPlayerSwitchWeapon?.Invoke(weaponName);
    }



    
    
    

    
}
