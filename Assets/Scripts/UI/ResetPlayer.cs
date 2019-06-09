using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.API;
using GameJolt;

public class ResetPlayer : MonoBehaviour
{
    string scoreTextscore = "KillPoints: "; // A string representing the score to be shown on the website.
    string scoreTextKill = "Kills: "; // A string representing the score to be shown on the website.
    public void Reset()
    {
        //GameJolt.API.DataStore.Delete("TotalKills", true); //Values to reset player progress (Story when will be added..)
        //GameJolt.API.DataStore.Delete("HighScore", true);
        GameJolt.API.DataStore.Delete("Rank", false, (bool success) => { Debug.Log("Reseted"); });
        GameJolt.API.DataStore.Delete("XP", false, (bool success) => { Debug.Log("Reseted"); });
        GameJolt.API.DataStore.Delete("XPtonextRank", false, (bool success) => { Debug.Log("Reseted"); });

        PlayerPrefs.DeleteAll();

        GameJoltAPI.Instance.CurrentUser.SignOut();
        Application.Quit();
        Debug.Log("All reseted.");
    }

}
