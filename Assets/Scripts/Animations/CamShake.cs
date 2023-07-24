using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Written by Jusin using code from this tutorial:
https://www.youtube.com/watch?v=7sArQk03ccE

Shakes the camera when prompted
*/

public class CamShake : MonoBehaviour
{
    private Animator anim;

    //Name of a sound to play
    public string mySound = "SpookyBreathing";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) {
            shakeCamera();
        }
    }

    //Tells the animator to shake the camera.
    public void shakeCamera() {
        AudioManager.instance.Play(mySound);
        anim.SetTrigger("shake");
    }
}
