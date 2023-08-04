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

    private int myCount = 0;

    public int targetCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTaskCompleted += counter;
    }

    private void counter(string curr) {
        if(curr == targetTask) {
            myCount++;

            if(myCount == targetCount) {
                EventManager.current.levelWin();
            }
        }
    }

    private void OnDisable() {
        EventManager.current.onTaskCompleted -= counter;
    }
}
