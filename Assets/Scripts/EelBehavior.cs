using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EelBehavior : FishEnemyBehavior
{
    public GameObject playerHeadlight;
    
    // Variables that define the range of the player's headlight.
    private float headlightAngle;
    private float headlightDistance;

    private LayerMask obstacleLayerMask;


    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 7f;
        acceleration = 12f;
        base.Start();
        UpdateHeadlightRange();
        obstacleLayerMask = LayerMask.GetMask("Obstacles");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckShined();
    }

    void UpdateHeadlightRange()
    {
        headlightAngle = 30f;
        headlightDistance = 14f;
    }

    // This function checks whether the eel is shined on by the player's headlight.
    // If this eel is shined on, it will run away from the player.
    void CheckShined()
    {
        // Compute the vector of the player's headlight.
        float angle = playerHeadlight.transform.rotation.eulerAngles.z;
        float radianAngle = angle * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(radianAngle);
        float cosAngle = Mathf.Cos(radianAngle);
        
        Vector3 vector = Vector3.up;
        float rotatedX = vector.x * cosAngle - vector.y * sinAngle;
        float rotatedY = vector.x * sinAngle + vector.y * cosAngle;
        Vector3 headlightDirection = new Vector3(rotatedX, rotatedY, 0f);
        
        // Compute the vector from the player's headlight to this eel.
        Vector3 headlightToEel = (transform.position - playerHeadlight.transform.position);
        
        // If this eel is within the headlight's range, do a raycast to see if there 
        // is an obstacle between the player's headlight and this eel.
        if (Vector3.Angle(headlightDirection, headlightToEel) <= headlightAngle && Vector3.Magnitude(headlightToEel) <= headlightDistance)
        {
            Vector2 start = new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(start, -headlightToEel, Vector3.Magnitude(headlightToEel), obstacleLayerMask);
            
            // If there is not an obstacle, this eel is shined on.
            if (hit.collider == null)
            {
                print("Shined on!");
                aiPath.canMove = false;
                transform.position += (headlightToEel.normalized * speed * Time.deltaTime);
            }
            else
            {
                aiPath.canMove = true;
            }
        }
        else
        {
            aiPath.canMove = true;
        }
    }
}
