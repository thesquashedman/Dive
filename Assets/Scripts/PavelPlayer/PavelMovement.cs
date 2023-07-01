using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelMovement : MonoBehaviour
{
    // Start is called before the first frame update
    bool shouldMove = false;
    public float speed = 5f;
    public float rotationSpeed = 200f; // Rotation speed in degrees per second

    Rigidbody2D rb;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        EventManager.current.onPlayerStartMove += StartMove;
        EventManager.current.onPlayerStopMove += StopMove;
    }
    private void OnDisable() {
        EventManager.current.onPlayerStartMove -= StartMove;
        EventManager.current.onPlayerStopMove -= StopMove;
    }

    // Update is called once per frame
    void StartMove()
    {
        shouldMove = true;
    }
    void StopMove()
    {
        shouldMove = false;
    }
    
    void FixedUpdate()
    {
        
        if(shouldMove)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            rb.AddForce(direction * speed);

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 0, rotationSpeed * Time.deltaTime * 2);
            transform.eulerAngles = new Vector3(0, 0, angle);

        }
    }

}
