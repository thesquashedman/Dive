using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSaw : Weapon
{

    // Override the Attack method for Sword
    public override void Attack()
    {
        // Logic specific to the sword attack
        Debug.Log(weaponName + " slashes with " + damage + " damage!");
    }

    // Override the SpecialAttack method for Sword
    public override void SpecialAttack()
    {
        // Logic specific to the sword's special attack
        Debug.Log(weaponName + " performs a spinning slash!");
    }
}
