using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Environment;

/*
Written by Justin.

Updates the Objective Tracker text UI.
*/

public class ObjectiveTracker : MonoBehaviour
{
    //Singleton instance
    public static ObjectiveTracker current;

    //Text display
    public TMP_Text text;

    //SortedList to store info
    public SortedList<string, int[]> objectives;


    private void Awake() {
        Debug.Log("ObjectiveTracker Active.");
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
        }

        this.text = GetComponent<TMP_Text>();

        this.objectives = new SortedList<string, int[]>();

        EventManager.current.onTaskCompleted += updateCount;
    }

    //Adds an objective without a counter
    public void addTextObj(string input) {
        objectives.Add(input, new int[] {-1});
        updateText();
    }

    //Adds an objective with the given name, with a target number
    public void addCountObj(string name, int finish) {
        objectives.Add(name, new int[] {0, finish});
        updateText();
    }

    //Updates the stored value under the given name
    //If it is only a string objective, or the target has been met
    //returns without doing anything.
    private void updateCount(string name) {
        if(objectives.ContainsKey(name)) {
            int[] curr = objectives[name];

            if(curr[0] == -1 || curr[0] == curr[1]) {
                return;
            }

            objectives[name][0]++;

            updateText();
        }
    }

    //Updates the TMP_Text 
    //Called when new objectives are added, or the tracker is updated.
    private void updateText() {
        string output = "";

        foreach(var entry in objectives) {
            if(entry.Value[0] == -1) {
                output += entry.Key + NewLine;
            } else {
                output += entry.Key + ": " + entry.Value[0] + "/" + entry.Value[1] + NewLine;
            }
        }

        Debug.Log(output);

        text.text = output;
    }

    //Returns the current value for the name
    //Returns -1 if no key has the given name.
    public int getValue(string name) {
        if(objectives.ContainsKey(name)) {
            return objectives[name][0];
        } 

        return -1;
    }

    //Returns if the value of the name matches the target
    //Also returns false if no name matches
    public bool completed(string name) {
        if(objectives.ContainsKey(name)) {
            return objectives[name][0] == objectives[name][1];
        } 

        return false;
    }

    private void OnDisable() {
        EventManager.current.onTaskCompleted -= updateCount;
    }
}
