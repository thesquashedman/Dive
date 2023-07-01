using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Weapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack() {
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentWeapon.Attack();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            currentWeapon.StopAttack();
        }
    }
}
