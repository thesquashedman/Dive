using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHeadRotation : MonoBehaviour
{
    public Transform shoulders;
    public Transform head;

    bool isFlipped;

    bool isAiming;
    // Start is called before the first frame update
    public float rotationSpeed = 200f; // Rotation speed in degrees per second

    void Start()
    {
        EventManager.current.onPlayerFlip += onFlip;
        EventManager.current.onPlayerStartAiming += onAiming;
        EventManager.current.onPlayerStopAiming += OnStopAiming;
    }
    private void OnDisable() {
        EventManager.current.onPlayerFlip -= onFlip;
        EventManager.current.onPlayerStartAiming -= onAiming;
        EventManager.current.onPlayerStopAiming -= OnStopAiming;
    }

    // Update is called once per frame
    void onFlip(bool flipped)
    {
        isFlipped = flipped;
    }
    void onAiming()
    {
        isAiming = true;
    }
    void OnStopAiming()
    {
        isAiming = false;
    }

    void Update()
    {
        if(isAiming)
        {
            
            
            Vector2 direction = PavelPlayerController.current.aimDirection;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
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
            float targetAngle = 100;
            /*
            if(!isFlipped)
            {
                targetAngle += 180;
            }
            */
            float angle = Mathf.MoveTowardsAngle(head.localEulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            head.localEulerAngles = new Vector3(0, 0, angle);
            float angle2 = Mathf.MoveTowardsAngle(shoulders.localEulerAngles.z, targetAngle - 80, rotationSpeed * Time.deltaTime);
            shoulders.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
