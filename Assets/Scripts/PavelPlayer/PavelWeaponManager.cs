using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelWeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    PavelWeapon[] weapons;
    void Start()
    {
        weapons = GetComponentsInChildren<PavelWeapon>(includeInactive: true);
        EventManager.current.onPlayerSwitchWeapon += SwitchWeapon;
        EventManager.current.onPlayerPickupWeapon += PickupWeapon;

    }
    void SwitchWeapon(string weaponName)
    {
        foreach(PavelWeapon weapon in weapons)
        {
            if(weapon.isAquired)
            {
                if(weapon.weaponName == weaponName)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
            }
            
        }
    }

    // Update is called once per frame
    void PickupWeapon(string weaponName)
    {
        foreach(PavelWeapon weapon in weapons)
        {
            if(weapon.weaponName == weaponName)
            {
                weapon.isAquired = true;
            }
        }
    }
}
