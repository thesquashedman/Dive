using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    GameObject player;

    // Reference to the player's OxygenSystem script
    public PavelPlayerOxygen playerOxygenSystem;
    public Health playeHealth;
    public PlayerResourcesSystem playerResourceSys;

    public GameObject[] BloodObjects;
    public GameObject mobileUI;
    public GameObject playerState;

    // Reference to the Text UI component
    public Text oxygenText;
    public Text healthText;

    public Text bulletsForPistol;
    public Text recouceOneText;

    public bool isMobileActive;

    void Start() {
        CheckMobileMode();
    }
    // Update is called once per frame
    void Update()
    {
        CheckMobileMode();
        // Update the oxygen text with the current oxygen level from the player's OxygenSystem script
        if (playerOxygenSystem != null && oxygenText != null)
        {
            oxygenText.text = "Oxygen: " + playerOxygenSystem.oxygenLevel.ToString("F1");
            healthText.text = "Health: " + playeHealth.GetHealth().ToString("F1");
            bulletsForPistol.text = "Bullets: " + playerResourceSys.bullets1.ToString("F1");
            recouceOneText.text = "Pearls: " + playerResourceSys.GetResourceOne().ToString("F1");
        }

        if(Input.GetKeyDown(KeyCode.E)) {

        }
        
        BloodEffects();
    }

    void BloodEffects() {
        if (playeHealth.GetHealth() <= 20)
        {
            TurnOffAllBloodEffect();
            BloodObjects[3].SetActive(true);
        }
        else if (playeHealth.GetHealth() <= 40)
        {
            TurnOffAllBloodEffect();
            BloodObjects[2].SetActive(true);
        }
        else if (playeHealth.GetHealth() <= 60)
        {
            TurnOffAllBloodEffect();
            BloodObjects[1].SetActive(true);
        }
        else if (playeHealth.GetHealth() <= 80)
        {
            TurnOffAllBloodEffect();
            BloodObjects[0].SetActive(true);
        }
        else if (playeHealth.GetHealth() > 80)
        {
            TurnOffAllBloodEffect();
        }
    }

    void CheckMobileMode() {
        isMobileActive = PavelPlayerSettingStates.current.mobileMovement;
        if (!isMobileActive) {
            mobileUI.SetActive(false);
            playerState.SetActive(true);
        }
        else {
            mobileUI.SetActive(true);
            playerState.SetActive(false);
        }
    }

    void TurnOffAllBloodEffect()
    {
            BloodObjects[0].SetActive(false);
            BloodObjects[1].SetActive(false);
            BloodObjects[2].SetActive(false);
            BloodObjects[3].SetActive(false);
    }
}
