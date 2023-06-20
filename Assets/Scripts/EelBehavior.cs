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

    // Variables for running away from the player.
    private float runAwayTimer = 0f;
    private float runAwayTime = 1.4f;
    private Vector3 runAwayDirection = Vector3.zero;
    private bool isShinedOn = false;

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
        headlightDistance = 7f;
    }

    // This function checks whether the eel is shined on by the player's headlight.
    // If this eel is shined on, it will run away from the player.
    void CheckShined()
    {
        if (!isShinedOn)
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -headlightToEel, Vector3.Magnitude(headlightToEel), obstacleLayerMask);
            
                // If there is not an obstacle, this eel is shined on.
                if (hit.collider == null)
                {
                    isShinedOn = true;
                    runAwayDirection = headlightToEel.normalized;
                    SwitchMode("runAway");
                }
            }
        }
    }

    // This function is called when this eel is shined on; this eel will run away
    // from the player for a short amount of time.
    protected override void RunAway()
    {
        aiPath.enableRotation = false;
        runAwayTimer += Time.deltaTime;
        if (runAwayTimer < runAwayTime)
        {
            transform.position += (runAwayDirection * speed * Time.deltaTime);
            // Have a turnaround timer.
            transform.rotation = Quaternion.LookRotation(Vector3.forward, runAwayDirection);
        }
        else
        {
            runAwayTimer = 0f;
            aiPath.enableRotation = true;
            isShinedOn = false;
            SwitchMode("attack");
        }
    }

    // This function that this eels stays around just outside the player's headlight range.
    void StayAround()
    {

    }
}
