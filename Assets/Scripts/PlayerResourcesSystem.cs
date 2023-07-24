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

    //Number of healing kits
    public int HPKit;

    //Keycode to use a HPKit
    public KeyCode healKey;

    //Amount that using a HPKit should heal
    private float healAmount;

    public int[] weapons; // This can represent slots for weapons, number of slots depends on the length of the array

    //Singleton instance
    public static PlayerResourcesSystem current;

    private void Awake()
    {
        Debug.Log("PlayerResourcesSystem Active.");
        //Singleton pattern
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
            //Debug.Log("EventManager Active.");
        }
    }

    // You can add a constructor to initialize your variables if needed
    public PlayerResourcesSystem(int staminaMax, int staminaCur, int bullets1, int bullets2, int bullets3, int bombs, int HPKit, int numWeaponSlots)
    {
        this.staminaMax = staminaMax;
        this.staminaCur = staminaCur;
        this.bullets1 = bullets1;
        this.bullets2 = bullets2;
        this.bullets3 = bullets3;
        this.bombs = bombs;
        this.HPKit = HPKit;
        this.weapons = new int[numWeaponSlots]; // Initialize weapon slots with given number
    }

    void Start()
    {
        EventManager.current.onPlayerPickupResource += PickupResource;

        this.healKey = KeyCode.F;
        this.healAmount = 20f;
    }

    private void Update() {
        if(Input.GetKeyDown(healKey)) {
            UseHPKit();
        }
    }

    public void PickupResource(string resourceName, int amount)
    {
        if(resourceName == "ResourceOne")
        {
            resouceOne += amount;
        }
        if(resourceName == "Bullet1")
        {
            bullets1 += (int)amount;
        }
        if(resourceName == "Bullet2")
        {
            bullets2 += (int)amount;
        }
        if(resourceName == "Bullet3")
        {
            bullets3 += (int)amount;
        }
        if(resourceName == "Bomb")
        {
            bombs += (int)amount;
        }
        if(resourceName == "HPKit")
        {
            HPKit += (int)amount;
        } else {
            Debug.Log("Resource " + resourceName + " not found!");
        }
    }

    //Change the number of HPKits by the input
    public void ChangeHPKit(int change) {
        HPKit += change;
    }

    //If there is 1 or more HPKits, triggers the healPlayer event
    //Using the healAmount set in Start()
    public void UseHPKit() {
        bool output = (HPKit > 0);

        if(output) {
            HPKit--;
            //Debug.Log(this.healAmount);
            EventManager.current.healPlayer(this.healAmount);
        }
    }

    public void ChangeResourceOne(float chage)
    {
        resouceOne += chage;
    }

    public float GetResourceOne() { 
        return resouceOne;
    }

    private void OnDisable() {
        EventManager.current.onPlayerPickupResource -= PickupResource;
    }
}
