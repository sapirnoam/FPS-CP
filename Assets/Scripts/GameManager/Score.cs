using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJolt.API;
using GameJolt.UI;
public class Score : MonoBehaviour
{
    public float score = 0; // Made in match //V //Should seen in scoreboard
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
    }
    string scoreTextscore = "Score:"; // A string representing the score to be shown on the website.
    string scoreTextKill = "Kills:"; // A string representing the score to be shown on the website.
    string scoreTextWaves = "Waves Surveved:"; // A string representing the score to be shown on the website.
    int tableID = 0; // Set it to 0 for main highscore table.

    public void Dead()
    {
        panelDeath.SetActive(true);
        gm.CursorLock = false;
        GameJolt.API.Scores.Add((int)score, scoreTextscore, 420719, "Score", (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
        GameJolt.API.Scores.Add((int)Kills, scoreTextKill, 421871, "Kills", (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
        GameJolt.API.Scores.Add((int)WavesSurvived, scoreTextWaves, 422057, "Waves", (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
    }
    private void LateUpdate()
    {
        ScoreTextDead.text = "Score: " + score.ToString();
        BestScore.text = "Best Score: NOTSETUP! ";
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
}
