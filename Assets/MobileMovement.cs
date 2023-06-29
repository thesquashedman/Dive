using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody2D rb;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (Mathf.Abs(joystick.Vertical) >= 0.05 && Mathf.Abs(joystick.Horizontal) >= 0.05)
            direction = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (Mathf.Abs(joystick.Vertical) >= 0.05 && Mathf.Abs(joystick.Horizontal) >= 0.05)
            rb.AddForce(direction * speed);
    }
}
