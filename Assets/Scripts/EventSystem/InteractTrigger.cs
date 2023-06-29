using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Written by Justin.

Simple trigger when an entity enters the area defined by
a collider on the object this script is attached to and ALSO
presses a defined button.

Key is set through the Unity menus.

Remember that attached object should be trigger only, and that
the entity should have both a RigidBody and a collider.
*/

public class ObjTrigger : MonoBehaviour
{
    //If another object is within the area
    private bool withinArea = false;
    
    //Object id
    public int id;

    //Trigger key
    public KeyCode myKey;

    // Update is called once per frame
    void Update() {
        if(withinArea && Input.GetKeyDown(myKey)) {
            EventManager.current.objInteract(id);
            //Debug.Log("Interacting...");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Player")) 
            return;

        withinArea = true;
        //Debug.Log("Is within Area.");
    }

    void OnTriggerExit2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Player")) 
            return;
            
        withinArea = false;
        //Debug.Log("Exited area.");
    }

}
