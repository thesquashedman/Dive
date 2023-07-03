using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public Weapon curentWeapon;
    public Button attack;
    public float curTime;
    public float maxTime;
    public bool timerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        maxTime = 0.5f;
        Button btn = attack.GetComponent<Button>();
		btn.onClick.AddListener(Attack);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(timerOn);
        if (timerOn) {
            curTime -= Time.deltaTime;
            // Debug.Log(curTime);
            if (curTime <= 0.0f)
                TimerEnded();
        }
    }

    

    void Attack() {
        if (!timerOn) {
            Debug.Log("Attack!!!!!");
            curentWeapon.Attack();
            curTime = maxTime;
            timerOn = true;
        }
    }

    void TimerEnded() {
        // if (timerOn) {
            Debug.Log("Stop Attack!!!!!");
            curentWeapon.StopAttack();
            timerOn = false;
        // }
    }
    
}
