using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject gameObjet;

    private void Update()
    {
        Die();
    }

    public void ChangeHealth(int change) {
        health += change;
    }

    public void SetHealth(int set) {
        health += set;
    }

    public void Die() {
        if (health <= 0) {
            Destroy(gameObjet);
        }
    }

}
