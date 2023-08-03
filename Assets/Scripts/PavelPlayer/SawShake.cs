using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawShake : MonoBehaviour
{
    //https://medium.com/nice-things-ios-android-development/basic-2d-screen-shake-in-unity-9c27b56b516

    
    public float shakeMagnitude = 0.7f;

    Vector3 initialPosition;


    string currWeapon;
    void OnEnable()
    {
        initialPosition = transform.localPosition;
        EventManager.current.onPlayerSwitchWeapon += switchWeapon;
        
    }
    void Update()
    {
        if (PavelPlayerSettingStates.current.isAttacking && currWeapon == "Saw")
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }
    public void switchWeapon(string weapon)
    {
        currWeapon = weapon;
    }
    
}

