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
    public event Action<int> onDealDamagePlayer;

    ///<summary>
    ///Invoke event to deal damage to player
    ///</summary>
    public void dealDamagePlayer(int damage) {
        onDealDamagePlayer?.Invoke(damage);
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
    ///Add functions to trigger when the player sprite flips
    ///</summary>
    public event Action onPlayerFlip;

    ///<summary>
    ///Trigger when the player flips
    ///</summary>
    public void playerFlip() {
        onPlayerFlip?.Invoke();
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
    

    
}
