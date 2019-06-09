using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJolt.API;

public class RankManager : MonoBehaviour
{
    public int Rank = 1;
    public float XP;
    public int XPtonextRank;
    private int ScoreToAddToXp;

    public Text RankText;
    public Text ValueLeft;

    public int BackedFromGame = 0; //If the player came back from game write xp and stuff, If not, Dont write.

    public GameObject Loading;
    private void Start()
    {
        BackedFromGame = PlayerPrefs.GetInt("BackedFromGame");

        if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
        {
            if (BackedFromGame == 1) // Player returned from match.
            {
                Loading.SetActive(true);
                GameJolt.API.DataStore.Get("Rank", false, (string value) =>
                {
                    Rank = int.Parse(value);
                });
                GameJolt.API.DataStore.Get("XP", false, (string value) =>
                {
                    XP = float.Parse(value);
                });
                GameJolt.API.DataStore.Get("XPtonextRank", false, (string value) =>
                {
                    XPtonextRank = int.Parse(value);
                });
                StartCoroutine(AddXP());
            }
            else {
                return;
            }
        }
    }
    private bool IsRankingUp = false;
    void LateUpdate()
    {
        if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
        {
            if (XP >= XPtonextRank && IsRankingUp == false)
            {
                Loading.SetActive(true);
                IsRankingUp = true;
                Rank += 1;
                XP -= XPtonextRank;
                if (XP < 0)
                    XP = 0;
                XPtonextRank *= 2;
                StartCoroutine(AddRank());
            }
        }
        RankText.text = "Rank: " + Rank.ToString();
        ValueLeft.text = XP.ToString() + "/" + XPtonextRank.ToString();
    }

    void CheckForNewScore()
    {
        GameJolt.API.DataStore.Get("Rank", false, (string value) =>
        {
            Rank = int.Parse(value);
        });
        GameJolt.API.DataStore.Get("XP", false, (string value) =>
        {
            XP = float.Parse(value);
        });
        GameJolt.API.DataStore.Get("XPtonextRank", false, (string value) =>
        {
            XPtonextRank = int.Parse(value);
        });
    }


    private void OnApplicationQuit()
    {
        BackedFromGame = 0;
        PlayerPrefs.SetInt("BackedFromGame", 0);
        PlayerPrefs.SetInt("NewsShowed", 0);
    }

    IEnumerator AddRank()
    {
        CancelInvoke();
        //Loading gameobject

        GameJolt.API.DataStore.Set("XP", XP.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Set("Rank", Rank.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Set("XPtonextRank", XPtonextRank.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.6f);

        GameJolt.API.DataStore.Get("Rank", false, (string value) =>
        {
            Rank = int.Parse(value);
        });
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Get("XP", false, (string value) =>
        {
            XP = float.Parse(value);
        });
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Get("XPXPtonextRank", false, (string value) =>
        {
            XPtonextRank = int.Parse(value);
        });
        yield return new WaitForSeconds(0.4f);

        //Loaded gameobject
        Debug.Log("All saved online.");
        IsRankingUp = false;
        Loading.SetActive(false);
        InvokeRepeating("CheckForNewScore", 5.0f, 5.0f);
    }

    IEnumerator AddXP()
    {
        CancelInvoke();
        GameJolt.API.DataStore.Get("XP", false, (string value) =>
        {
            XP = float.Parse(value);
        });
        yield return new WaitForSeconds(1.5f);
        //Loading gameobject
        ScoreToAddToXp = PlayerPrefs.GetInt("ScoreToAddXP"); // Dont remove
        XP += ScoreToAddToXp;
        GameJolt.API.DataStore.Set("XP", XP.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Set("Rank", Rank.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Set("XPtonextRank", XPtonextRank.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.2f);
        Debug.Log(ScoreToAddToXp.ToString());
        ScoreToAddToXp = 0;
        PlayerPrefs.DeleteKey("ScoreToAddXP"); //Dont remove

        BackedFromGame = 0;
        PlayerPrefs.SetInt("BackedFromGame", 0);
        //Loaded gameobject
        Debug.Log("All saved online.");
        InvokeRepeating("CheckForNewScore", 5.0f, 5.0f);
        Loading.SetActive(false);
    }
}
