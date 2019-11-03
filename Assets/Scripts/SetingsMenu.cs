using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public Dropdown resolutionDropDown;
    public Slider volumeSlider;

    public Toggle fullScreenToggle;

    Resolution[] resolutions;

    void Start()
    {
        GetResolutions();

        GetFullScreenMode();

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");

        int savedWidth = PlayerPrefs.GetInt("ScreenWidth");
        int savedHeight = PlayerPrefs.GetInt("ScreenHeight");

        Screen.SetResolution(savedWidth, savedHeight, Screen.fullScreen);
    }

    void GetFullScreenMode()
    {
        int mode = PlayerPrefs.GetInt("FullScreen");

        // FullScreen on
        if (mode == 1)
        {         
            Screen.fullScreen = true;
            fullScreenToggle.isOn = true;
        }
        // FullScreen off
        else if (mode == 0)
        {    
            Screen.fullScreen = false;
            fullScreenToggle.isOn = false;
        }
    }

    void GetResolutions()
    {
        // Todas las resoluciones soportadas por el monitor (fulllscreen)
        resolutions = Screen.resolutions;       

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if ((resolutions[i].width == Screen.currentResolution.width) && (resolutions[i].height == Screen.currentResolution.height))
                currentResolutionIndex = i;

            Debug.Log(resolutions[i]);
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    void SaveOptions()
    {
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {     
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ScreenWidth", resolution.width);
        PlayerPrefs.SetInt("ScreenHeight", resolution.height);

        SaveOptions();
    }

    public void SetVolume(float volume)
    {        
        audioMixer.SetFloat("Volume", volume);

        PlayerPrefs.SetFloat("Volume", volume);

        SaveOptions();
    }

    public void SetFullscreen(bool isFullScreen)
    {       
        Screen.fullScreen = isFullScreen;

        if (isFullScreen)
            PlayerPrefs.SetInt("FullScreen", 1);
        else
            PlayerPrefs.SetInt("FullScreen", 0);

        SaveOptions();
    }
}
