using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : AttackSystem
{
    public bool attackOnce;
    public float damagePeriods;

    public GameObject attackElement;
    //private AttackElement attackElement;

    private void Start()
    {
        attackElement.SetActive(false);
    }

    public override void Attack(string tragetTag) {
        attackElement.GetComponent<AttackElement>().SetAttackTargetTag(tragetTag);
        this.CloseAttackStart();
    }

    public override void StopAttack()
    {
        this.CloseAttackEnd();
    }

    public void CloseAttackStart()
    {
        Debug.Log("AAAAAAAAA");
        attackElement.SetActive(true);
    }

    public void CloseAttackEnd()
    {
        attackElement.SetActive(false);
    }

    public void CloseAttackFinsih()
    {
        attackElement.SetActive(false);
    }
}
