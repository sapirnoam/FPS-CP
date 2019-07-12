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

    public bool AllLoaded = false;
    public GameObject Loading;
    private void Start()
    {
        BackedFromGame = PlayerPrefs.GetInt("BackedFromGame");

        if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
        {
            if (BackedFromGame == 1) // Player returned from match.
            {
                GameJolt.API.DataStore.Get("Rank", false, (string value) =>
                {
                    if (value != null)
                    {
                        Rank = int.Parse(value);
                    }
                });
                DataStore.Get("XP", false, value => {
                    if (value != null)
                    {
                        XP = float.Parse(value);
                    }
                });
                GameJolt.API.DataStore.Get("XPtonextRank", false, (string value) =>
                {
                    if (value != null)
                    {
                        XPtonextRank = int.Parse(value);
                    }
                });
                StartCoroutine(AddXP());
            }
            else
            {
                return;
            }
        }
        InvokeRepeating("CheckForNewScore", 5.0f, 5.0f);
    }
    void LateUpdate()
    {
        if (AllLoaded == true)
        {
            if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
            {
                if (XP >= XPtonextRank && canrankup == true)
                {
                    Rank++;
                    XP -= XPtonextRank;
                    if (XP < 0)
                    {
                        XP = 0;
                    }
                    XPtonextRank *= 2;
                    StartCoroutine(AddXP());
                }
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
        //GameJolt.API.DataStore.Get("XP", false, (string value) =>
        //{
        //    XP = float.Parse(value);
        //});
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
        PlayerPrefs.Save();
    }

    IEnumerator AddRank()
    {
        yield return new WaitForSeconds(1.5f);
        DataStore.SetSegmented("XP", XP.ToString(), false, success => { });
        GameJolt.API.DataStore.Set("XPtonextRank", XPtonextRank.ToString(), false, (bool success) => { });
        GameJolt.API.DataStore.Set("Rank", Rank.ToString(), false, (bool success) => { });
    }
    private bool canrankup = true;
    IEnumerator AddXP()
    {
        canrankup = false;
        Loading.SetActive(true);
        CancelInvoke();
        yield return new WaitForSeconds(1.5f);
        //Loading gameobject
        ScoreToAddToXp = PlayerPrefs.GetInt("ScoreToAddXP"); // Dont remove
        XP += ScoreToAddToXp;
        GameJolt.API.DataStore.Set("Rank", Rank.ToString(), false, (bool success) => { });

        DataStore.SetSegmented("XP", XP.ToString(), false, success => { });
        yield return new WaitForSeconds(0.4f);
        yield return new WaitForSeconds(0.4f);

        GameJolt.API.DataStore.Set("XPtonextRank", XPtonextRank.ToString(), false, (bool success) => { });
        yield return new WaitForSeconds(0.7f);
        Debug.Log(ScoreToAddToXp.ToString());
        ScoreToAddToXp = 0;
        PlayerPrefs.DeleteKey("ScoreToAddXP"); //Dont remove

        BackedFromGame = 0;
        PlayerPrefs.SetInt("BackedFromGame", 0);
        PlayerPrefs.Save();
        //Loaded gameobject
        InvokeRepeating("CheckForNewScore", 5.0f, 5.0f);
        Loading.SetActive(false);
        canrankup = true;
    }

}
