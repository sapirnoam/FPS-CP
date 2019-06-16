using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    int sysHour = System.DateTime.Now.Hour;
    public Material matDay;
    public Material matNight;
    public GameObject SUN;
    public GameObject Fire;
    public GameObject MOON;
    public float RotateSkysSpeed = 1.2f;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        if (sysHour >= 19) //Night
        {
            RenderSettings.skybox = matNight;
            SUN.SetActive(false);
            MOON.SetActive(true);
            Fire.SetActive(true);
        }
        else if (sysHour >= 17)
        {
            RenderSettings.skybox = matDay;
            Fire.SetActive(true);
            SUN.SetActive(true);
            MOON.SetActive(false);
        }
        else
        {
            RenderSettings.skybox = matDay;
            Fire.SetActive(false);
            SUN.SetActive(true);
            MOON.SetActive(false);
        }
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSkysSpeed);
    }
}