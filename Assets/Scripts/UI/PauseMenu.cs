using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject Settings;
    public GameObject pausemenuUI;
    public GameManager gm;
    public AudioSource audiosource;
    public AudioClip OpenMenuSound;
    public AudioClip ClickSound;
    public Button ResumeButton;
    void Update()
    {
        if (Input.GetButtonDown("Settings")) //Esc key
        {
            if (GameIsPaused && Settings.activeSelf == false)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            if (GameIsPaused && Settings.activeSelf == true)
            {
                Settings.SetActive(false);
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
        ResumeButton.Select();
    }
    public void OpenOptions()
    {
        Debug.Log("Options.");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
