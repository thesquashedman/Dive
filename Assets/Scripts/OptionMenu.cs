using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void SetMainVolumn(float volumn) {
        Debug.Log(volumn);
        // audioMixer.setFloat("mainMixer", volumn);
    }
}
