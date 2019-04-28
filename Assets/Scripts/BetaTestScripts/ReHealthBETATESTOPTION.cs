using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReHealthBETATESTOPTION : MonoBehaviour
{
    public Health health;
    public GameObject Gameover;
    public GameObject SplatterCamera;


    public void LateUpdate()
    {
        if (Input.GetKeyDown("t") || (Input.GetKeyDown("joystick button 3")))
        {
            if (health.health <= 0)
            {
                health.health = 100;
                Gameover.SetActive(false);
                SplatterCamera.SetActive(false);
            }
        }
    }
}
