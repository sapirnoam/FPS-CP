using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject fps;
    public GameObject AutoQuality;

    public Dropdown resolutionsDropDown;

    Resolution[] resolutions;


    void Start()
    {

        resolutions = Screen.resolutions;

        resolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResulotionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResulotionIndex = i;
            }
        }

        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = currentResulotionIndex;
        resolutionsDropDown.RefreshShownValue();
    }

    private void LateUpdate()
    {
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resulotion = resolutions[ResolutionIndex];
        Screen.SetResolution(resulotion.width, resulotion.height, Screen.fullScreen);
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isfull)
    {
        Screen.fullScreen = isfull;
    }


    public void FPSCounter(bool isFPS)
    {
        fps.SetActive(isFPS);
    }
    public void autoQuality(bool isOn)
    {
        AutoQuality.SetActive(isOn);
    }

}
