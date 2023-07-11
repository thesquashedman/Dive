using UnityEngine.Audio;
using UnityEngine;

/*
Written by Justin.

Object that encapulates a sound clip, with different settings.
Taken from this tutorial: https://www.youtube.com/watch?v=6OT43pvUyfY
*/

[System.Serializable]
public class Sound 
{
    //Name for the sound clip
    public string name;

    //Reference to the clip
    public AudioClip clip;

    //Volume control
    [Range(0f, 1f)]
    public float volume = 1.0f;

    //Pitch control
    [Range(0f, 1f)]
    public float pitch = 1.0f;

    //Source that plays the clip
    [HideInInspector]
    public AudioSource source;

    //The id for what kind of sound this is
    //0 = music, 1 = effect
    public int id;
}
