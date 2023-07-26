using UnityEngine;
using Lowscope.Saving;

public class PavelPlayerOxygen :  MonoBehaviour, ISaveable
{
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

    public struct SaveData
    {
        public float oxygenLevel;
        public float maxOxygenLevel;
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

    // Function to set the oxygen level
    public void SetOxygenLevel(float newOxygenLevel)
    {
        // If the new oxygen level is above the maximum level,
        // set it to the maximum level.
        if (newOxygenLevel > maxOxygenLevel)
        {
            oxygenLevel = maxOxygenLevel;
        }
        // If the new oxygen level is below 0, set it to 0.
        else if (newOxygenLevel < 0f)
        {
            oxygenLevel = 0f;
        }
        // Otherwise, set the oxygen level to the new oxygen level.
        else
        {
            oxygenLevel = newOxygenLevel;
        }
    }

    public float GetOxygenLevel() { 
        return oxygenLevel;
    }

    public string OnSave()
    {
        return JsonUtility.ToJson(new SaveData { oxygenLevel = oxygenLevel, maxOxygenLevel = maxOxygenLevel  });
    }

    public void OnLoad(string data)
    {
       oxygenLevel = JsonUtility.FromJson<SaveData>(data).oxygenLevel;
       maxOxygenLevel = JsonUtility.FromJson<SaveData>(data).maxOxygenLevel;
    }

    public bool OnSaveCondition()
    {
        return true;
    }
}
