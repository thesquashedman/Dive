using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenResource : MonoBehaviour
{
    // Bool variable that determines the behavior of the oxygen resource
    public bool area = false;

    // Amount of oxygen to add if the area is set to false
    public float oxygenToAdd = 20f;

    // Rate at which oxygen should be increased if the area is set to true
    public float oxygenIncreaseRate = 1f;

    // Time passed since the player entered the trigger collider
    private float timePassed = 0f;

    // Whether the player is in the trigger collider
    private bool playerInTrigger = false;

    // Reference to the player's oxygen script
    public PavelPlayerOxygen playerOxygenSystem;

    // Reference to the player's bubble system
    public ParticleSystem bubbleParticles;

    // Function called when an object enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // If area is true, start increasing oxygen over time
            if (area)
            {
                bubbleParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                playerInTrigger = true;
                // The rest of the logic is handled in the Update method
            }
            // If area is false, add a fixed amount of oxygen immediately
            else
            {
                float newOxygenLevel = playerOxygenSystem.oxygenLevel + oxygenToAdd;
                playerOxygenSystem.SetOxygenLevel(newOxygenLevel);
                Destroy(this.gameObject);
            }
        }
    }

    // Function called when an object exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        // If the player exits the trigger and area is true, reset timePassed
        if (area && other.CompareTag("Player"))
        {
            timePassed = 0f;
            playerInTrigger = false;
            bubbleParticles.Play(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If area is true and the player is in the trigger, increase oxygen over time
        if (area && playerInTrigger)
        {
            timePassed += Time.deltaTime;

            if (timePassed >= 0.15f) // 1 second interval
            {
                float newOxygenLevel = playerOxygenSystem.oxygenLevel + oxygenIncreaseRate;
                playerOxygenSystem.SetOxygenLevel(newOxygenLevel);
                timePassed = 0f;
            }
        }
    }
}
