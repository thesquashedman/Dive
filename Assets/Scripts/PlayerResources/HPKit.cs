using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPKit : MonoBehaviour
{
    //Name of the pickup object
    public string name = "";

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            
        }
    }
}
