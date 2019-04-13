using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100;
    public Slider slider;
    public GameObject GameOver;
    public bool IsCollide = false;
    public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = 100;
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        slider.value = health;
        if (health < 0)
        {
            GameOver.SetActive(true);
            // Death animation should be write here
        }
        if (health > 100)
        {
            health = 100;
        }
    }
}
