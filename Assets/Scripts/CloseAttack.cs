using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : AttackSystem
{
    public bool attackOnce;
    public float damagePeriods;

    public GameObject atackElement;

    //private AttackElement attackElement;

    private void Start()
    {
        atackElement.SetActive(false);
    }

    public override void Attack(string tragetTag) {
        atackElement.GetComponent<AttackElement>().SetAttackTargetTag(tragetTag);
        this.CloseAttackStart();
        
    }

    public override void StopAttack()
    {
        this.CloseAttackEnd();
    }

    public void CloseAttackStart()
    {
        Debug.Log("AAAAAAAAA");
        atackElement.SetActive(true);
    }

    public void CloseAttackEnd()
    {
        atackElement.SetActive(false);
    }

    public void CloseAttackFinsih()
    {
        atackElement.SetActive(false);
    }
}
