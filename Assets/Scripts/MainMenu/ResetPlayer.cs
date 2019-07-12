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

        GameJolt.API.DataStore.Delete("SecondsAlive", false, (bool success) => { Debug.Log("Reseted"); });
        GameJolt.API.DataStore.Delete("MinutesAlive", false, (bool success) => { Debug.Log("Reseted"); });
        GameJolt.API.DataStore.Delete("HoursAlive", false, (bool success) => { Debug.Log("Reseted"); });

        GameJolt.API.DataStore.Delete("TotalKills", true, (bool success) => { Debug.Log("Reseted"); });
        GameJolt.API.DataStore.Delete("Deaths", false, (bool success) => { Debug.Log("Reseted"); });
        GameJolt.API.DataStore.Delete("HighScore", true, (bool success) => { Debug.Log("Reseted"); });

        GameJolt.API.DataStore.Set("XP", 0.ToString(), false, (bool success) => { });
        GameJolt.API.DataStore.Set("XPtonextRank", 100.ToString(), false, (bool success) => { });
        GameJolt.API.DataStore.Set("Rank", 1.ToString(), false, (bool success) => { InvokeRepeating("Quit", 2.0f, 2.0f); });

        GameJolt.API.DataStore.Set("Deaths", 0.ToString(), false, (bool success) => { InvokeRepeating("Quit", 2.0f, 2.0f); });
        GameJolt.API.DataStore.Set("TotalKills", 0.ToString(), false, (bool success) => { InvokeRepeating("Quit", 2.0f, 2.0f); });
    }
    private void Quit()
    {
        GameJolt.API.DataStore.Set("XP", 0.ToString(), false, (bool success) => { });
        GameJolt.API.DataStore.Set("XPtonextRank", 100.ToString(), false, (bool success) => { });
        GameJolt.API.DataStore.Set("Rank", 1.ToString(), false, (bool success) => { });
        GameJoltAPI.Instance.CurrentUser.SignOut();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All reseted.");
        Application.Quit();
    }

}
