using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;
    private float lifeTime = 5f;
    private float speed = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the direction from the main character towards the mouse.
        direction = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime >= 0)
        {
            // Travel with the found direction.
            Vector3 position = transform.position;
            position.x += direction.x * speed * Time.deltaTime;
            position.y += direction.y * speed * Time.deltaTime;
            position.z += direction.z * speed * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            // Delete this bullet.
            GameObject.Destroy(gameObject);
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
