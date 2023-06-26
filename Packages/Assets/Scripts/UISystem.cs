using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    GameObject player;

    // Reference to the player's OxygenSystem script
    public OxygenSystem playerOxygenSystem;
    public Health playeHealth;

    // Reference to the Text UI component
    public Text oxygenText;
    public Text healthText;

    // Update is called once per frame
    void Update()
    {
        // Update the oxygen text with the current oxygen level from the player's OxygenSystem script
        if (playerOxygenSystem != null && oxygenText != null)
        {
            oxygenText.text = "Oxygen: " + playerOxygenSystem.oxygenLevel.ToString("F1");
            healthText.text = "Health: " + playeHealth.GetHealth().ToString("F1");
        }
    }
}
