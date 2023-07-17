using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wirefixing : ObjTrigger
{
    //Stores max time to hold 
    public int gaugeMax = 5;
    
    //Stores current gauge
    public float gaugeCurr = 0f;

    //Time between presses that should pass before reset
    private float waitTime = 5f;

    //Current time in between button holds
    public float nullTime = 0f;

    //Name of task to trigger
    public string taskName;

    //If completed or not. Makes completion trigger only once.
    private bool completed = false;

    // Update is called once per frame
    void Update()
    {
        if(withinArea && !completed) {
            if(Input.GetKey(myKey)) {
                gaugeCurr += Time.smoothDeltaTime;
                nullTime = 0f;

                if(gaugeCurr >= gaugeMax) {
                    completed = true;
                    EventManager.current.taskCompleted(taskName);
                    Debug.Log("Interacting...");
                }
            } else {
                nullTime += Time.smoothDeltaTime;

                if(nullTime >= waitTime) {
                    gaugeCurr = 0f;
                }
            }
        }
    }
}
