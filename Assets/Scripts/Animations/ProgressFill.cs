using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Written by Justin.

Inspired by this video tutorial: https://www.youtube.com/watch?v=J1ng1zA3-Pk
Changes the fillAmount of an attached image to resemble a progress bar.
*/

public class ProgressFill : MonoBehaviour
{
    //Current status for the bar
    public float current = 0.0f;

    //The max for this bar
    public float max = 100f;

    //The image for the bar
    public Image myImage;

    //Base underlying the bar
    public Image baseImage;

    //Color for the base
    private Color baseColor;

    //Color for the bar
    private Color myColor;

    //If this should be active or not
    public bool isActive = false;

    void Start() {
        baseColor = baseImage.color;
        myColor = myImage.color;

        setActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) {
            float fill = (float) current / (float) max;
            myImage.fillAmount = fill;
        }
    }

    //Toggles visibility for the bar
    public void setActive(bool input) {
        isActive = input;

        if(!isActive) {
            myColor.a = 0f;
            baseColor.a = 0f;
        } else {
            myColor.a = 255f;
            baseColor.a = 255f;
        }

        myImage.color = myColor;
        baseImage.color = baseColor;
    }

    //Sets the current value for the bar
    public void setCurr(float input) {
        current = input;

        if(current > max) {
            current = max;
        }
    }

    //Adds to the current value
    public void addCurr(float input) {
        setCurr(input + current);
    }

}
