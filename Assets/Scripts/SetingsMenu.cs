using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public Dropdown resolutionDropDown;
    public Slider volumeSlider;

    Resolution[] resolutions;
    /*
    void Start(){

        resolutions = Screen.resolutions;
        
        resolutionDropDown.ClearOptions();

        List<string> screenOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++){

            if (resolutions[i].width >= 1024 && resolutions[i].height >= 768){

                string option = resolutions[i].width + " x " + resolutions[i].height;

                screenOptions.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){

                    currentResolutionIndex = i;
                }

                Debug.Log(resolutions[i].width + " x " + resolutions[i].height);
            }
        }

        resolutionDropDown.AddOptions(screenOptions);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }*/

    public void SetVolume (float volume){

        audioMixer.SetFloat("Volume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullScreen (bool isFullScreen){

        Screen.fullScreen = isFullScreen;
    }
    /*
    public void SetResolution(int resolutionIndex){

        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }*/
}
