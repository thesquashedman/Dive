using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f; // Rotation speed in degrees per second

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculate the target angle to face the direction of the mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z - transform.position.z));
        Vector3 direction = (mousePosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Smoothly rotate the player to the target angle
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Add force in the direction of the mouse if "k" is pressed
        if (Input.GetKey("m"))
        {
            rb.AddForce(direction * speed);
        }

        // Add force in the opposite direction of the mouse if "j" is pressed
        if (Input.GetKey("n"))
        {
            rb.AddForce(-direction * speed);
        }
    }
}
