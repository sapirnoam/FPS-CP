using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    private bool NEWSshowd = true;

    private void Start()
    {
        PlayerPrefs.SetInt("NewsShowed", 1);
        if (PlayerPrefs.GetInt("NewsShowed") == 0)
        { }
    }

    private void OnApplicationQuit()
    {
        NEWSshowd = false;
        PlayerPrefs.SetInt("NewsShowed", 0);
    }
    #region Singleton
    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
}
