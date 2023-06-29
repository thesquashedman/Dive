using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public Weapon curentWeapon;
    public Button attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        Button btn = attack.GetComponent<Button>();
		btn.onClick.AddListener(Attack);
    }

    void Attack() {
        Debug.Log("Attack!!!!!");
        curentWeapon.Attack();
        Debug.Log("Stop Attack!!!!!");
        curentWeapon.StopAttack();
    }
}
