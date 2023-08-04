using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int ammoAmount = 5;
    public string ammoName = "Bullet1";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        EventManager.current.playerPickupResource(ammoName, ammoAmount);
        Destroy(this.gameObject);
    }
}
