using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100;
    public Slider slider;
    public Text textHealth;
    public GameObject GameOver;
    public Animator anim;
    public RainCameraController BloodController;
    public GameObject FrictionFlowRain;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 100;
        textHealth.text = health.ToString();
        slider.minValue = 0;
        slider.value = 100;
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        slider.value = health;
        textHealth.text = health.ToString();

        if (health < 0 || health == 0)
        {
          GameOver.SetActive(true);
          // Death animation should be write here
          health = 0;
          BloodController.Play();
        }

        if (health > 100)
        {
           health = 100;
        }

      if (health < 8)
     {
         FrictionFlowRain.SetActive(true);
     }
    }
}
