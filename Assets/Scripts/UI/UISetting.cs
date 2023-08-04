using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class UISetting : MonoBehaviour
{
    [Header("GAME SETTINGS")]
    // public TMPro.TMP_Dropdown resolutionDropdown;
    public GameObject resolutionDropdown;
    public GameObject fullscreentext;
    Resolution[] resolutions; 

    // AudioMixer
    public AudioMixer audioMixer;

    // Sliders
    public GameObject musicSlider;
    public GameObject effectSlider;

    private float musicSliderValue = 0.0f;
    private float effectSliderValue = 0.0f;
	// private float sliderValueXSensitivity = 0.0f;
	// private float sliderValueYSensitivity = 0.0f;
	// private float sliderValueSmoothing = 0.0f;

    void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.GetComponent<TMP_Dropdown>().ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.GetComponent<TMP_Dropdown>().AddOptions(options);
        resolutionDropdown.GetComponent<TMP_Dropdown>().value = currentResolutionIndex;
        resolutionDropdown.GetComponent<TMP_Dropdown>().RefreshShownValue(); 

        // check full screen
		if(Screen.fullScreen == true){
			fullscreentext.GetComponent<TMP_Text>().text = "on";
		}
		else if(Screen.fullScreen == false){
			fullscreentext.GetComponent<TMP_Text>().text = "off";
		}

        // check slider values
        musicSliderValue = GetVolumn("Music");
        // SetMusicVolumn();
        // musicSlider.GetComponent<Slider>().value = musicSliderValue;

        effectSliderValue = GetVolumn("Effect");
        // SetEffectVolumn();
		// effectSlider.GetComponent<Slider>().value = effectSliderValue;
    }

    void Update() {
        //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        musicSliderValue = musicSlider.GetComponent<Slider>().value;
        effectSliderValue = effectSlider.GetComponent<Slider>().value;
    }


    // public void SetFullscreen(bool isFullscreen) {
    //     Debug.Log(isFullscreen);
    //     Screen.fullScreen = isFullscreen;
    // }

    public void FullScreen (){
		Screen.fullScreen = !Screen.fullScreen;

		if(Screen.fullScreen == true){
			fullscreentext.GetComponent<TMP_Text>().text = "on";
        }
		else if(Screen.fullScreen == false){
			fullscreentext.GetComponent<TMP_Text>().text = "off";
		}
	}

    public void SetResolution() {
        //Debug.Log(resolutionIndex);
        Resolution resolution = resolutions[resolutionDropdown.GetComponent<TMP_Dropdown>().value];
        Debug.Log(resolution.width + "x" + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
    }

    public void SetMusicVolumn() {
        audioMixer.SetFloat("Music", musicSliderValue);
    }

    public void SetEffectVolumn() {
        audioMixer.SetFloat("Effects", effectSliderValue);
    }

    public void MusicSlider (){
        SetMusicVolumn();
	}

    public void EffectSlider() {
        SetEffectVolumn();
    }

    public float GetVolumn(string name) {
        float value;
        bool result = audioMixer.GetFloat(name, out value);
        if (result) {
            return value;
        }
        else {
            return 0f;
        }
    }
}
