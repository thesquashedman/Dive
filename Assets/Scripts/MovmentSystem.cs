using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentSystem : MonoBehaviour
{
    public float speed = 5f; // Variable to control the speed of the character
    public float rotationSpeed = 200f; // Variable to control the rotation speed of the character

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the user for movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create a 2D vector for the direction
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        // Add force to the Rigidbody2D to move the character
        rb.AddForce(direction * speed);

        // Rotate the character using 'N' and 'M' keys
        if (Input.GetKey(KeyCode.N))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.M))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}
