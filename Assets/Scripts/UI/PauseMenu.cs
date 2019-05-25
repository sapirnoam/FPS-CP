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
    void LateUpdate()
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
    }
    public void OpenOptions()
    {
        Debug.Log("Options.");

    }



    public void QuitGame()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        SceneManager.LoadScene(0);
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        SceneManager.LoadScene(0);
    }
}
