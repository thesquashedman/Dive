using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f; // Rotation speed in degrees per second

    private Rigidbody2D rb;

    public Transform arms;

    public Transform head;

    public Flip flip;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculate the target angle to face the direction of the mouse

        /*
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
        */
    }
    void FixedUpdate() {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //When leftControl, only move arms and head
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(flip.isFlipped)
            {
                Debug.Log("Flipped");
                float angle = Mathf.MoveTowardsAngle(arms.localEulerAngles.z, transform.eulerAngles.z - targetAngle, rotationSpeed * Time.deltaTime);
                arms.localEulerAngles = new Vector3(0, 0, angle);

                angle = Mathf.MoveTowardsAngle(head.localEulerAngles.z, (transform.eulerAngles.z + 80) - targetAngle, rotationSpeed * Time.deltaTime);
                head.localEulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                float angle = Mathf.MoveTowardsAngle(arms.localEulerAngles.z, targetAngle - transform.eulerAngles.z, rotationSpeed * Time.deltaTime);
                arms.localEulerAngles = new Vector3(0, 0, angle);

                angle = Mathf.MoveTowardsAngle(head.localEulerAngles.z, (targetAngle + 80) - transform.eulerAngles.z, rotationSpeed * Time.deltaTime);
                head.localEulerAngles = new Vector3(0, 0, angle);
            }
            
    
        }
        else
        {
            float angle = Mathf.MoveTowardsAngle(arms.localEulerAngles.z, 0, rotationSpeed * Time.deltaTime);
            arms.localEulerAngles = new Vector3(0, 0, angle);
            angle = Mathf.MoveTowardsAngle(head.localEulerAngles.z, 80, rotationSpeed * Time.deltaTime);
            head.localEulerAngles = new Vector3(0, 0, angle);

            // Smoothly rotate the player to the target angle
            angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
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
}
