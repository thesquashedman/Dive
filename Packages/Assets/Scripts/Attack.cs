using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage;
    public float attackRange;
    public float attackRate;
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
