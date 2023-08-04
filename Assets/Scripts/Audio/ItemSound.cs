using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Written by Justin.

Plays a sound before the item is picked up.
*/
public class ItemSound : MonoBehaviour
{
    //Called onDestroy
    //Should be when the item is picked up
    void OnDisable() {
        AudioManager.instance.Play("ItemPickup_2");
    }
}
