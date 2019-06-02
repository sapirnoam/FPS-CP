using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.API;
using GameJolt;

public class ResetPlayer : MonoBehaviour
{
    string scoreTextKill = "Kills: ";
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        GameJolt.API.DataStore.Delete("TotalKills", true);
        GameJolt.API.DataStore.Delete("HighScore", true);
    }
}
