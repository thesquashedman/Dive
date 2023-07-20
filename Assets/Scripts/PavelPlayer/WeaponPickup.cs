using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public string weaponName = "Default";
    public float interactDistance = 1f;
    void Start()
    {
        EventManager.current.onPlayerinteract += Interact;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Interact()
    {
        if(Vector2.Distance(transform.position, PavelPlayerSettingStates.current.transform.position) > interactDistance)
        {
            return;
        }
        EventManager.current.PlayerPickupWeapon(weaponName);
        EventManager.current.PlayerSwitchWeapon(weaponName);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}
