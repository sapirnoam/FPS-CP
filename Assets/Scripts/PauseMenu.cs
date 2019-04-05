using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pausemenuUI;
    public GameManager gm;
    public AudioSource audiosource;
    public AudioClip OpenMenuSound;
    public AudioClip ClickSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();

            }
        }
    }
    public void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        gm.CursorLock = true;
        AudioListener.pause = false;
        AudioListener.volume = 1;
    }

    void Pause()
    {
        pausemenuUI.SetActive(true);
        GameIsPaused = true;
        gm.CursorLock = false;
        AudioListener.pause = true;
        AudioListener.volume = 0;
        Time.timeScale = 0f;

    }
    public void OpenOptions()
    {
        Debug.Log("Options.");

    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
    }

}
