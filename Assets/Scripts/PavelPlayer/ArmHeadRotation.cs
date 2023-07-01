using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHeadRotation : MonoBehaviour
{
    public Transform shoulders;
    public Transform head;

    bool isFlipped;

    bool isMoving;
    // Start is called before the first frame update
    public float rotationSpeed = 200f; // Rotation speed in degrees per second

    void Start()
    {
        EventManager.current.onPlayerFlip += onFlip;
        EventManager.current.onPlayerStartMove += onMove;
        EventManager.current.onPlayerStopMove += onStopMove;
    }
    private void OnDisable() {
        EventManager.current.onPlayerFlip -= onFlip;
        EventManager.current.onPlayerStartMove -= onMove;
        EventManager.current.onPlayerStopMove -= onStopMove;
    }

    // Update is called once per frame
    void onFlip(bool flipped)
    {
        isFlipped = flipped;
    }
    void onMove()
    {
        isMoving = true;
    }
    void onStopMove()
    {
        isMoving = false;
    }

    void Update()
    {
        if(!isMoving)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2 direction = (mousePosition - (Vector2)head.position).normalized;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            if(!isFlipped)
            {
                targetAngle += 180;
            }
            float angle = Mathf.MoveTowardsAngle(head.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            head.eulerAngles = new Vector3(0, 0, angle);
            shoulders.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            float targetAngle = 90;
            /*
            if(!isFlipped)
            {
                targetAngle += 180;
            }
            */
            float angle = Mathf.MoveTowardsAngle(head.localEulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            head.localEulerAngles = new Vector3(0, 0, angle);
            shoulders.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
