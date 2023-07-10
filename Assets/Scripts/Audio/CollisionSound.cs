using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Written by Justin.

Plays a sound on collision, stops on exit.
*/

public class CollisionSound : EntitySound
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //On collision with the player, plays a sound
    //For demo
    private void OnTriggerEnter2D(Collider2D other) {
        audios[0].Play();
    }

    //When player leaves the box, stops the sound.
    private void OnTriggerExit2D(Collider2D other) {
        audios[0].Stop();
    }
}
