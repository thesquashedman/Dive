using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

/*
Written by Justin.

Manager to play global sounds, such as music,
throughout scenes.

Inspired by this tutorial, with a few tweaks:
https://www.youtube.com/watch?v=6OT43pvUyfY

*/

public class AudioManager : MonoBehaviour
{
    //Instance of AudioManager
    public static AudioManager instance;

    //Array of global sounds
    public Sound[] sounds;

    public string curr;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        AudioMixer myMix = Resources.Load<AudioMixer>("Main");

        AudioMixerGroup musicGroup = myMix.FindMatchingGroups("Music")[0];
        AudioMixerGroup effectGroup = myMix.FindMatchingGroups("Effects")[0];

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
            //Set AudioSource default settings here
            if(s.id == 0) {
                s.source.outputAudioMixerGroup = musicGroup;
                s.source.loop = true;
            } else {
                s.source.outputAudioMixerGroup = effectGroup;
                s.source.loop = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        Play("WaterVibes");
    }

    // Update is called once per frame
    void Update()
    {
        //Delete before updating github!!!
        //For demo purposes only
        if(Input.GetKeyDown(KeyCode.I)) {
            Play("Explosion");
        } else if(Input.GetKeyDown(KeyCode.O)) {
            Play("Ambience");
        } else if(Input.GetKeyDown(KeyCode.P)) {
            Play("WaterVibes");
        } else if(Input.GetKeyDown(KeyCode.Space)) {
            Play("GunShot");
        }
    }

    //Given the name of a Sound in the stored list
    //Plays the audio
    public void Play(string name) {
        Sound s = Find(name);

        if(s == null) {
            Debug.LogWarning("Sound: " + name + " not found in Play!");
            return;
        }

        //Ensures only one music clip can play at once
        if(s.id == 0) {
            Stop(curr);

            curr = name;
        }

        s.source.Play();
    }

    //Given the name, stops the audio
    public void Stop(string name) {
        Sound s = Find(name);

        if(s == null) {
            Debug.LogWarning("Sound: " + name + " not found in Stop!");
            return;
        }

        s.source.Stop();
    }

    public void SetSource(string name, AudioSource curr) {
        Sound s = Find(name);

        if(s == null) {
            Debug.LogWarning("Sound: " + name + " not found in PlayAtSource!");
            return;
        }

        if(curr.clip != s.clip) {
            curr.clip = s.clip;
            curr.volume = s.volume;
            curr.pitch = s.pitch;
            curr.outputAudioMixerGroup = s.source.outputAudioMixerGroup;
            curr.loop = s.source.loop;
        }
    }

    //Private find method for the sound array
    private Sound Find(string name) {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
