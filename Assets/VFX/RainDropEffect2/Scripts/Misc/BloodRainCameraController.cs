using UnityEngine;
using System.Collections;

public class BloodRainCameraController : MonoBehaviour {

	public RainCameraController FrameBloodCamera;
	public RainCameraController SplatterBloodCamera;

	public int HP = 100;
    public Health healthManager;


	public float FrameEffectInterval = 1f;
	public float Smooth = 2f;

	float timeElapsed = 0f;
	float currentAlpha = 0f;
	float oldAlpha = 0f;
	float lerpStart = 0f;
	float lerpTime = 0f;

	[SerializeField]
	AnimationCurve hpHigh;

	[SerializeField]
	AnimationCurve hpMid;

	[SerializeField]
	AnimationCurve hpLow;

    public void Attack(int damage)
    {
        healthManager.health = Mathf.Max(0, healthManager.health - damage);
        SplatterBloodCamera.Refresh();
        SplatterBloodCamera.Play();
    }

    public void Reset()
    {
        healthManager.health = 100;
        ResetLerpTime();
        FrameBloodCamera.Refresh();
        SplatterBloodCamera.Refresh();
    }

    void Update()
    {
        currentAlpha = (100 - healthManager.health) / 100f;
        if (currentAlpha != oldAlpha)
        {
            lerpTime = 0f;
            lerpStart = oldAlpha;
            oldAlpha = currentAlpha;
        }

        FrameBloodCamera.Play();

        timeElapsed += Time.deltaTime;
        if (timeElapsed > FrameEffectInterval)
        {
            timeElapsed = timeElapsed - FrameEffectInterval;
        }

        lerpTime += Smooth * Time.deltaTime;

        if (healthManager.health >= 100)
        {
            FrameBloodCamera.Alpha = 0f;
        }
        else if (healthManager.health >= 75)
        {
            FrameBloodCamera.Alpha = currentAlpha * LerpTime(lerpTime) * hpHigh.Evaluate(timeElapsed);
        }
        else if (healthManager.health >= 25)
        {
            FrameBloodCamera.Alpha = currentAlpha * LerpTime(lerpTime) * hpMid.Evaluate(timeElapsed);
        }
        else
        {
            FrameBloodCamera.Alpha = currentAlpha * LerpTime(lerpTime) * hpLow.Evaluate(timeElapsed);
        }
    }

    float LerpTime(float lerpTime)
    {
        return Mathf.Lerp(lerpStart, currentAlpha, lerpTime);
    }

    void ResetLerpTime()
    {
        lerpTime = 0f;
    }
}
