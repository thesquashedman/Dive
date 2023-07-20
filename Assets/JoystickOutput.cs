using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickOutput : MonoBehaviour
{

    public Joystick joystick_l;
    public Joystick joystick_r;
    public Vector3 direction_l;
    public Vector3 direction_r;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction_l = Vector3.up * joystick_l.Vertical + Vector3.right * joystick_l.Horizontal;
        direction_r = Vector3.up * joystick_r.Vertical + Vector3.right * joystick_r.Horizontal;
    }
}
