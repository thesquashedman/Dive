using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Written by Justin.
Basic event subscription that is triggered by id.

Works if there is many of the same kind of object, but only
one/few should trigger.

Default id is 0.
*/
public class ObjInteraction : MonoBehaviour
{
    //Object id
    public int id;

    //Active state
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onObjInteract += interact;
    }

    //Defines what happens when the event occurs
    private void interact(int id) {
        if(id == this.id) {
            //Toggle active state
            isActive = !isActive;

            //Defines behavior depending on state
            if(isActive) {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            } else {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void OnDisable() {
        EventManager.current.onObjInteract -= interact;
    }
}
