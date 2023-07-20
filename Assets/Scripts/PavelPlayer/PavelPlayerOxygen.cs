using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerOxygen : MonoBehaviour
{
    // Start is called before the first frame update
    public float lackOfOxygenDamage = 10f;

    private float timePassedDamage = 0.0f;

    private float lackOfOxygenDamageInterval = 3.0f;

    // Oxygen Level in percentages (0 to 100)
    public float oxygenLevel = 100.0f;

    // Maximum oxygen level that can be achieved
    public float maxOxygenLevel = 100.0f;

    // Interval in seconds at which oxygen should be reduced
    public float oxygenReductionInterval = 1.0f;

    // Amount by which oxygen should be reduced in each interval
    public float oxygenReductionAmount = 1.0f;

    // Time passed since the last reduction
    private float timePassed = 0.0f;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
         timePassed += Time.deltaTime;

        // Check if the time passed is greater than or equal to the oxygen reduction interval
        if (timePassed >= oxygenReductionInterval)
        {
            // Reduce the oxygen level
            oxygenLevel -= oxygenReductionAmount;

            // Clamp the oxygen level to a minimum of 0
            oxygenLevel = Mathf.Max(oxygenLevel, 0.0f);

            // Reset the time passed
            timePassed = 0.0f;
        }

        if (oxygenLevel <= 0) {
            timePassedDamage += Time.deltaTime;
            if (timePassedDamage > lackOfOxygenDamageInterval) {
                EventManager.current.dealDamagePlayer(lackOfOxygenDamage);
                EventManager.current.playerSuffocate();
                timePassedDamage = 0;
            }
        }
    }
}
