using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    // Start is called before the first frame update

    bool PickedUp = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PickUp(Transform parent, Vector2 offset)
    {
        this.transform.parent = parent;
        this.transform.localPosition += (Vector3)offset;
        //Turn off rigidbody and collider.

    }
    void Drop()
    {
        this.transform.parent = null;
        //Turn on rigidbody and collider
    }
    
}
