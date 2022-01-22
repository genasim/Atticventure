using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    private Resolution[] resolutions;
    [SerializeField] TMP_Dropdown res_Dropdown; 

    private void Awake() {
       ConfigureResolutionSettings();
    }

    private void ConfigureResolutionSettings() {
        resolutions = Screen.resolutions;
        res_Dropdown.ClearOptions();

        int currentResIndex = 0;
        List<string> res_Options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            res_Options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
            { currentResIndex = i; }
        }

        res_Dropdown.AddOptions(res_Options);
        res_Dropdown.value = currentResIndex;
        res_Dropdown.RefreshShownValue();
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool setFullscreen) {
        Screen.fullScreen = setFullscreen;
    }

    public void SetScreenResolution(int resolutionsIndex) {
        Resolution res = resolutions[resolutionsIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    } 
}
