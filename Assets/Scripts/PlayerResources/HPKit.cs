using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPKit : MonoBehaviour
{
    //Name of the pickup object
    public string name = "";
    public int healthPoints = 10;
    public PavelPlayerHealth pH;
    public float MinHealthToPickUp = 85;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Debug.Log(pH.GetCurentHealth());

            if (pH.GetCurentHealth() < MinHealthToPickUp) {
                pH.ChangeHealth(healthPoints);
                Destroy(gameObject);
            }
        }
    }
}
