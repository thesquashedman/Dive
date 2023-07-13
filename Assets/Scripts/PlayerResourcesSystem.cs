using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesSystem : MonoBehaviour
{
    public int staminaMax;
    public int staminaCur;

    public float resouceOne = 0;

    public int bullets1;
    public int bullets2;
    public int bullets3;

    public int bombs;

    public int hpKit;

    public int[] weapons; // This can represent slots for weapons, number of slots depends on the length of the array

    // You can add a constructor to initialize your variables if needed
    public PlayerResourcesSystem(int staminaMax, int staminaCur, int bullets1, int bullets2, int bullets3, int bombs, int hpKit, int numWeaponSlots)
    {
        this.staminaMax = staminaMax;
        this.staminaCur = staminaCur;
        this.bullets1 = bullets1;
        this.bullets2 = bullets2;
        this.bullets3 = bullets3;
        this.bombs = bombs;
        this.hpKit = hpKit;
        this.weapons = new int[numWeaponSlots]; // Initialize weapon slots with given number
    }

    public void ChangeResourceOne(float chage)
    {
        resouceOne += chage;
    }

    public float GetResourceOne() { 
        return resouceOne;
    }
}
