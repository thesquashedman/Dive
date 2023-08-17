using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    public string weaponName;

    // Start is called before the first frame update
    void Start()
    {
        if (!PavelPlayerSettingStates.current.mobileMovement)
        {
            // Update the bullets UI when the player switches weapons or picks up a weapon.
            EventManager.current.onPlayerSwitchWeapon += UpdateBullet;
            EventManager.current.onPlayerPickupWeapon += UpdateBullet;
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update the bullets UI with the current weapon's ammo.
    private void UpdateBullet(string newWeapon)
    {
        if (newWeapon == weaponName)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestory()
    {
        if (!PavelPlayerSettingStates.current.mobileMovement)
        {
            EventManager.current.onPlayerSwitchWeapon -= UpdateBullet;
            EventManager.current.onPlayerPickupWeapon -= UpdateBullet;
        }
    }
}
