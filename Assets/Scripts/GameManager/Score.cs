using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJolt.API;
using GameJolt.UI;
using GameJolt.UI.Controllers;
public class Score : MonoBehaviour
{
    [SerializeField]
    public float score = 0; // Made in match //V //Should seen in scoreboard
    public float HighScore = 0;
    public float TotalKills = 0;
    public int Deaths;

    public int Kills = 0; //Should seen in scoreboard
    public GameObject panelDeath;
    public GameManager gm;
    public TimeManager timeManager;

    [Header("Gameplay TEXTS")]
    public Text scoreText;
    public Text KillsMadeInGame;

    [Header("DeathMenu TEXTS")]
    public Text ScoreTextDead; //v
    public Text BestScore; //v
    public Text KillsTextDead;
    public Text YourTime;
    public Text BestTime;

    public AudioSource AudioS;
    public AudioClip WhatHappend;


    public GameObject Saving;

    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetFloat("Score");
        if (GameJoltAPI.Instance.HasUser || GameJoltAPI.Instance.HasSignedInUser)
        {
            GameJolt.API.DataStore.Get("HighScore", true, (string value) => {
                if (float.Parse(value) > HighScore)
                {
                    HighScore = float.Parse(value);
                }
                else
                    return;
            });
        }
        if (GameJoltAPI.Instance.HasUser || GameJoltAPI.Instance.HasSignedInUser)
        {
            GameJolt.API.DataStore.Get("TotalKills", true, (string value) => {
                if (float.Parse(value) > TotalKills)
                {
                    TotalKills = float.Parse(value);
                }
                else
                    return;
            });
        }
        GameJolt.API.DataStore.Get("MinutesAlive", false, (string value) => {
            Minutes = int.Parse(value);
        });
        GameJolt.API.DataStore.Get("Deaths", false, (string value) => {
            Deaths = int.Parse(value);
        });
        GameJolt.API.DataStore.Get("SecondsAlive", false, (string value) => {
            Seconds = int.Parse(value);
        });
        GameJolt.API.DataStore.Get("HoursAlive", false, (string value) => {
            Hours = int.Parse(value);
        });
    }
    string scoreTextscore = "KillPoints: "; // A string representing the score to be shown on the website.
    string scoreTextKill = "Kills: "; // A string representing the score to be shown on the website.
    string scoreTextWaves = "Waves Surveved:"; // A string representing the score to be shown on the website.
    int tableID = 0; // Set it to 0 for main highscore table.


    GameObject[] Enemys;
    GameObject[] MapObjects;
    public LeaderboardsWindow LeaderWindow;

    private int Seconds;
    private int Minutes;
    private int Hours;
    public void Dead()
    {
        Deaths += 1;
        timeManager.canCount = false;
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (var i = 0; i < Enemys.Length; i++)
            Destroy(Enemys[i]);
        MapObjects = GameObject.FindGameObjectsWithTag("MapObjects");
        for (var i = 0; i < MapObjects.Length; i++)
            Destroy(MapObjects[i]);

        YourTime.text = timeManager.Hours.ToString() + ":" + timeManager.Minutes.ToString() + ":" + timeManager.Seconds.ToString();
        if (Seconds >= timeManager.Seconds && Minutes >= timeManager.Minutes && Hours >= timeManager.Hours)
        {
            BestTime.text = Hours.ToString() + ":" + Minutes.ToString() + ":" + Seconds.ToString();
        }
        else
        {
            YourTime.text = timeManager.Hours.ToString() + ":" + timeManager.Minutes.ToString() + ":" + timeManager.Seconds.ToString();
        }
        panelDeath.SetActive(true);

        Saving.SetActive(true);
        if (Seconds >= timeManager.Seconds && Minutes >= timeManager.Minutes && Hours >= timeManager.Hours)
        {
            GameJolt.API.DataStore.Set("SecondsAlive", timeManager.Seconds.ToString(), false, (bool success) => {
                Debug.Log("Saved Online");
            });
            GameJolt.API.DataStore.Set("MinutesAlive", timeManager.Minutes.ToString(), false, (bool success) => {
                Debug.Log("Saved Online");
            });
            GameJolt.API.DataStore.Set("HoursAlive", timeManager.Hours.ToString(), false, (bool success) => {
                Debug.Log("Saved Online");
            });
        }
        PlayerPrefs.SetInt("ScoreToAddXP", (int)score);
        TotalKills += Kills;
        gm.CursorLock = false;
        GameJolt.API.Scores.Add((int)score, scoreTextscore + (int)score, 420719, "", (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
        GameJolt.API.Scores.Add((int)Kills, scoreTextKill + (int)Kills, 421871, "Kills", (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : ""));
        });
        if (score > HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetFloat("Score", HighScore);
            PlayerPrefs.Save();
            GameJolt.API.DataStore.Set("HighScore", HighScore.ToString(), true, (bool success) => {
            });
        }
        PlayerPrefs.SetFloat("Score", TotalKills);
            PlayerPrefs.Save();
            GameJolt.API.DataStore.Set("TotalKills", TotalKills.ToString(), true, (bool success) => {
                Debug.Log("Saved Online");
            });
        GameJolt.API.DataStore.Set("Deaths", Deaths.ToString(), false, (bool success) => {
            Debug.Log("Saved Online");
        });
        Saving.SetActive(false);
        StartCoroutine(LeaderLoad());
    }
    public GameObject LoadingScores;
    IEnumerator LeaderLoad()
    {
        LoadingScores.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        LeaderWindow.SetScores(0);
        LoadingScores.SetActive(false);
    }
    bool isGlobal = false;
    private void LateUpdate()
    {
        ScoreTextDead.text = score.ToString();
        BestScore.text = HighScore.ToString();

        KillsTextDead.text = Kills.ToString();
    }
    public void PofleAnimator()
    {
        StartCoroutine(WaitBeforeDeadvoid());
    }
    public IEnumerator WaitBeforeDeadvoid()
    {
        yield return new WaitForSeconds(3.2f);
        AudioS.PlayOneShot(WhatHappend);
        Dead();
    }
    void FixedUpdate()
    {
        scoreText.text = "KillPoints: " + score.ToString();
        KillsMadeInGame.text = "Kills: " + Kills.ToString();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("BackedFromGame", 0);
    }
}
