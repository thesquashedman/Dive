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

    public List<string> otherTargets = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if (targetTask != "")
        {
            EventManager.current.onTaskCompleted += counter;
            ObjectiveTracker.current.addCountObj(targetTask, targetCount);
        }

        foreach (string s in otherTargets)
        {
            ObjectiveTracker.current.addTextObj(s);
        }
    }

    private void counter(string curr) {
        if(curr == targetTask && ObjectiveTracker.current.completed(targetTask)) {
            EventManager.current.levelWin();
            Debug.Log("Win!");
        }
    }

    private void OnDisable() {
        if (targetTask != "")
        {
            EventManager.current.onTaskCompleted -= counter;
        }
    }
}
