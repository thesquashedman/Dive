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

    //Event for player's death
    public event Action onPlayerDeath;

    public void playerDeath() {
        onPlayerDeath?.Invoke();
    }


}
