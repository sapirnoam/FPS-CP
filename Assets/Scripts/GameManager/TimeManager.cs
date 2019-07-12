using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public Text text;
    public Text text2;
    float theTime;
    public float speed = 1;
    public float Seconds;
    public float Minutes;
    public float Hours;
    public bool canCount = true;
    // Use this for initialization
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale >= 0.1 && canCount == true)
        {
            theTime += Time.deltaTime * speed;
            string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            Seconds = int.Parse(seconds);
            Minutes = int.Parse(minutes);
            Hours = int.Parse(hours);

            if (int.Parse(hours) >= 1)
            {
                text.text = hours + ":" + minutes + ":" + seconds;
                text2.text = hours + ":" + minutes + ":" + seconds;
            }
            else
            {
                text.text = minutes + ":" + seconds;
                text2.text = minutes + ":" + seconds;
            }
        }
    }
}