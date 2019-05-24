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
        autoQualitybool = PlayerPrefs.GetInt("autoQualitybool") == 1 ? true : false;
        fpsBool = PlayerPrefs.GetInt("fpsBool") == 1 ? true : false;
        SetfullScreenBool = PlayerPrefs.GetInt("SetfullScreenBool") == 1 ? true : false;
        volumefloat = PlayerPrefs.GetFloat("volume");
        resulotionInt = PlayerPrefs.GetInt("Resolution");
        QualityInt = PlayerPrefs.GetInt("SetQuality");





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

    float volumefloat;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        volumefloat = volume;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    int resulotionInt;
    public void SetResolution(int ResolutionIndex)
    {
        Resolution resulotion = resolutions[ResolutionIndex];
        Screen.SetResolution(resulotion.width, resulotion.height, Screen.fullScreen);
        resulotionInt = ResolutionIndex;
        PlayerPrefs.SetInt("Resolution", ResolutionIndex);
        PlayerPrefs.Save();

    }

    float QualityInt;
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("SetQuality", qualityIndex);
        QualityInt = qualityIndex;
        PlayerPrefs.Save();
    }

    bool SetfullScreenBool = true;
    public void SetFullscreen(bool isfull)
    {
        Screen.fullScreen = isfull;
        if (SetfullScreenBool == true)
        {
            SetfullScreenBool = true;
            isfull = true;
        }
        else
        {
            SetfullScreenBool = false;
            isfull = false;
        }

        PlayerPrefs.SetInt("FullScreen", isfull ? 1 : 0);
        PlayerPrefs.Save();

    }

    bool fpsBool = true;
    public void FPSCounter(bool isFPS)
    {
        fps.SetActive(isFPS);
        if (isFPS == true)
        {
            fpsBool = true;
            isFPS = true;
        }
        else
        {
            fpsBool = false;
            isFPS = false;
        }
        PlayerPrefs.SetInt("FPS", isFPS ? 1 : 0);
        PlayerPrefs.Save();
    }
    bool autoQualitybool = true;
    public void autoQuality(bool isOn)
    {
        AutoQuality.SetActive(isOn);
        if (isOn == true)
        {
            autoQualitybool = true;
            isOn = true;
        }
        else
        {
            autoQualitybool = false;
            isOn = false;
        }
        PlayerPrefs.SetInt("AutoQuality", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

}
