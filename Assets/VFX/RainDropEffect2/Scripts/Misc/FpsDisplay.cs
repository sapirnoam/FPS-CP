using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FpsDisplay : MonoBehaviour
{
    float interval = 0.2f;
    float startTime = 0f;
    float dt = 0f;
    int flameCnt = 0;
    int fps = 0;
    public Text text;
    void Update()
    {
        dt = Time.time - startTime;
        flameCnt += 1;
        if (dt >= interval)
        {
            fps = (int)(flameCnt / dt);
            flameCnt = 0;
            startTime = Time.time;
        }
        text.text = "FPS:" + fps.ToString();
    }
}
