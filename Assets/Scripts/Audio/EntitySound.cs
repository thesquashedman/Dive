using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/*
Written by Justin

Plays sound effects for an entity given clips to play
*/
public class EntitySound : MonoBehaviour
{
    //Channels for audio
    //If multiple sounds are to be played, should have multiple channels
    public List<AudioSource> audios;

    //Names of sounds to get
    public List<string> audioNames;

    public virtual void Start() {
        for(int i = 0; i < audioNames.Count; i++) {
            audios.Add(gameObject.AddComponent<AudioSource>());

            //Find which audio to use here
            AudioManager.instance.SetSource(audioNames[i], audios[i]);
        }
    }
}
