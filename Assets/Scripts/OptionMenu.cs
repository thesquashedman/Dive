using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 

public class OptionMenu : MonoBehaviour
{

    public AudioMixer musicAudioMixer;
    public AudioMixer effectAudioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    
    Resolution[] resolutions; 

    void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); 
    }
    
    public void SetMainVolumn(float volumn) {
        Debug.Log(volumn);
        // audioMixer.setFloat("mainMixer", volumn);
    }

    public void SetFullscreen(bool isFullscreen) {
        Debug.Log(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
