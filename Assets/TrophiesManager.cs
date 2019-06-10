using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophiesManager : MonoBehaviour
{
    public Score score;

    private void Start()
    {
        InvokeRepeating("CheckForKillsAndTrophiesBeats", 5.0f, 5.0f);
    }

    void CheckForKillsAndTrophiesBeats()
    {
        if (score.Kills >= 10)
        {
            GameJolt.API.Trophies.Get(107242, (GameJolt.API.Objects.Trophy trophy) => {
                if (trophy == null)
                {
                    GameJolt.API.Trophies.Unlock(107242, (bool success) => {
                        if (success)
                        {
                        }
                    });
                }
            });
        }
    }
}
