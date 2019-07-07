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
        if (score.Kills >= 100)
        {
            GameJolt.API.Trophies.Get(107245, (GameJolt.API.Objects.Trophy trophy) => {
                if (trophy == null)
                {
                    GameJolt.API.Trophies.Unlock(107245, (bool success) => {
                        if (success)
                        {
                        }
                    });
                }
            });
        }
        if (score.Kills >= 500)
        {
            GameJolt.API.Trophies.Get(107246, (GameJolt.API.Objects.Trophy trophy) => {
                if (trophy == null)
                {
                    GameJolt.API.Trophies.Unlock(107246, (bool success) => {
                        if (success)
                        {
                        }
                    });
                }
            });
        }
        if (score.Kills >= 1000)
        {
            GameJolt.API.Trophies.Get(107247, (GameJolt.API.Objects.Trophy trophy) => {
                if (trophy == null)
                {
                    GameJolt.API.Trophies.Unlock(107247, (bool success) => {
                        if (success)
                        {
                        }
                    });
                }
            });
        }
        if (score.Kills >= 2000)
        {
            GameJolt.API.Trophies.Get(107248, (GameJolt.API.Objects.Trophy trophy) => {
                if (trophy == null)
                {
                    GameJolt.API.Trophies.Unlock(107248, (bool success) => {
                        if (success)
                        {
                        }
                    });
                }
            });
        }
    }
}
