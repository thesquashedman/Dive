using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // References to other compoenents.
    protected Health health;
    // protected DamageSystem damageSys;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = GetComponent<Health>();
    }
}
