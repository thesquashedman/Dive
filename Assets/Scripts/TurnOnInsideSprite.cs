using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnInsideSprite : MonoBehaviour
{
    public GameObject childObject; // The child object you want to enable

    void Start()
    {
        if (childObject != null)
        {
            childObject.SetActive(false); // Make sure child object is turned off at the start
        }
        else
        {
            Debug.LogError("Child object reference is missing.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the colliding object has the tag "Player"
        {
            childObject.SetActive(true); // Enable the child object
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the colliding object has the tag "Player"
        {
            childObject.SetActive(false); // Enable the child object
        }
    }
}
