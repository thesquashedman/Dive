using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Written by Justin.

Plays a sound before the item is picked up.
*/
public class ItemSound : EntitySound
{
    // Start is called before the first frame update
    public override void Start()
    {
        audioNames.Add("ItemPickup_2");
        base.Start();
    }

    //Called onDestroy
    //Should be when the item is picked up
    void OnDestroy() {
        audios[0].Play();
    }
}
