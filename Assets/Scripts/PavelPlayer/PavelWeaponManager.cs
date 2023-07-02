using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelWeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool attacking = false;
    void Start()
    {
        EventManager.current.onPlayerAttack += Attack;
        EventManager.current.onPlayerStopAttack += StopAttack;
    }

    void Attack()
    {
        attacking = true;
    }
    void StopAttack()
    {
        attacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
