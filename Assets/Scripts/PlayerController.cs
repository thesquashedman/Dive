using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Weapon curentWeapon;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            curentWeapon.Attack();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            curentWeapon.StopAttack();
        }
    }
}
