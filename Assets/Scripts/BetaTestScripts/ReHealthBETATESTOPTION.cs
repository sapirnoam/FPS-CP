using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReHealthBETATESTOPTION : MonoBehaviour
{
    public Health health;
    public GameObject Gameover;
    public GameManager gm;
    public void LateUpdate()
    {
        if (Input.GetKeyDown("t"))
        {
            if (health.health <= 0)
            {
                health.health = 100;
                Gameover.SetActive(false);
            }
        }
    }
}
