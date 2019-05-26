using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJolt.API;
using GameJolt.UI;
public class Score : MonoBehaviour
{
    [SerializeField]
    public float score = 0; // Made in match //V //Should seen in scoreboard
    public float HighScore = 0;
    public float TotalKills = 0;

    public int WavesSurvived = 0; // Made in match //V //Should seen in scoreboard
    public int Kills = 0; //Should seen in scoreboard
    public float GameCoins = 0f; // Made in match + Match coins //V
    public int Platinum = 0; // Made in match //Should be a popup window

    public GameObject panelDeath;
    public GameManager gm;

    [Header("Gameplay TEXTS")]
    public Text scoreText;
    public Text GameCoinsText;
    public Text WavesSurvivedText;

    [Header("DeathMenu TEXTS")]
    public Text ScoreTextDead; //v
    public Text BestScore; //v
    public Text WavesSurvivedTextDead; //v
    public Text KillsTextDead;
    public Text PlatinumEarned;

    public bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetFloat("Score");
        if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
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
        TotalKills = PlayerPrefs.GetFloat("Score");
        if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
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
    }
    string scoreTextscore = "KillPoints: "; // A string representing the score to be shown on the website.
    string scoreTextKill = "Kills: "; // A string representing the score to be shown on the website.
    string scoreTextWaves = "Waves Surveved:"; // A string representing the score to be shown on the website.
    int tableID = 0; // Set it to 0 for main highscore table.

    public void Dead()
    {
        panelDeath.SetActive(true);
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
    }
    bool isGlobal = false;
    private void LateUpdate()
    {
        ScoreTextDead.text = "Score: " + score.ToString();
        BestScore.text = "Best Score: " + HighScore.ToString();

        WavesSurvivedTextDead.text = "Survived until wave: " + WavesSurvived.ToString();
        KillsTextDead.text = "Kills: " + Kills.ToString();
        PlatinumEarned.text = "Platinum collected: " + Platinum.ToString();
    }
    public void PofleAnimator()
    {
        StartCoroutine(WaitBeforeDeadvoid());
    }
    public IEnumerator WaitBeforeDeadvoid()
    {
        yield return new WaitForSeconds(3.2f);
        Dead();
    }
    void FixedUpdate()
    {
        scoreText.text = "KillPoints: " + score.ToString();
        GameCoinsText.text = GameCoins.ToString() + "$";
        WavesSurvivedText.text = "Waves Survived: " + WavesSurvived.ToString();
    }
    private void FieldCheatDetector_OnFieldCheatDetected()
    {
        Debug.Log("");
    }
}
