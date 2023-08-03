using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Written by Justin

Triggers a level win when a certain amount of a target task is completed
in the level.
*/

public class WinConditions : MonoBehaviour
{
    public string targetTask = "";

    public int targetCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTaskCompleted += counter;
        ObjectiveTracker.current.addCountObj(targetTask, targetCount);
        ObjectiveTracker.current.addTextObj("Get to the Exit.");
        ObjectiveTracker.current.addTextObj("Collect Pearls.");
    }

    private void counter(string curr) {
        if(curr == targetTask && ObjectiveTracker.current.completed(targetTask)) {
            EventManager.current.levelWin();
            Debug.Log("Win!");
        }
    }

    private void OnDisable() {
        EventManager.current.onTaskCompleted -= counter;
    }
}
