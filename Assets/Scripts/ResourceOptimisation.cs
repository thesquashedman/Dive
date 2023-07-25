using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOptimisation : MonoBehaviour
{
    // The objects to enable/disable when the player enters/exits the trigger.
    public List<GameObject> objects;

    // Ensure that this game object has a trigger collider and call this function to configure it.
    private void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("No collider attached to the GameObject. Please add a collider component.");
            return;
        }
        collider.isTrigger = true;

        if (objects.Count == 0)
        {
            Debug.LogError("No objects assigned. Please assign objects in the inspector.");
        }
        else
        {
            // Set all the objects to inactive.
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }

    // This function is called when something enters the trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player.
        if (other.gameObject.CompareTag("Player"))
        {
            // If it's the player, enable the objects in the list.
            foreach (GameObject obj in objects)
            {
                obj.SetActive(true);
            }
        }
    }

    // This function is called when something leaves the trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object that left the trigger is the player.
        if (other.gameObject.CompareTag("Player"))
        {
            // If it's the player, disable the objects in the list.
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}
