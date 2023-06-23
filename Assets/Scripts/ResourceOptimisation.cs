using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOptimisation : MonoBehaviour
{
    public GameObject childObject; // Assign the child object in the inspector
    public Collider2D collider;

    // Ensure that this GameObject has a trigger collider and call this function to configure it
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        childObject.SetActive(false);

        if (collider == null)
        {
            Debug.LogError("No collider attached to the GameObject. Please add a collider component.");
            return;
        }
        collider.isTrigger = true;
        if (childObject == null)
        {
            Debug.LogError("No child object assigned. Please assign the child object in the inspector.");
        }
    }

    // This function is called when something enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // If it's the player, enable the child object
            childObject.SetActive(true);
        }
    }

    // This function is called when something leaves the trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object that left the trigger is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // If it's the player, disable the child object
            childObject.SetActive(false);
        }
    }
}
