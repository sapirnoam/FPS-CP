using UnityEngine;
using System.Collections;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadTest : MonoBehaviour
{

    public GameObject NoInternet;
    public GameObject SignOut;
    public GameObject LoginIn;
    public Text UserName;
    public string GameVersion;
    public Text Ver;


    [Header("Stats")]
    public Text Rank;
    public Text KillPoints;
    public Text AliveTime;
    public Text PlayedFor;
    public Text Kills;
    public Text Deaths;
    public Text User;






    /*public Animator NewsAnim;
    public GameObject News;
    private bool NEWSshowd = true; */
    public RankManager rankManager;
    private int Loaded = 0;
    private void Start()
    {
        GameVersion = PlayerPrefs.GetString("Version");
        //PlayerPrefs.SetInt("NewsShowed", 1);
        PlayerPrefs.Save();
        /*if (PlayerPrefs.GetInt("NewsShowed") == 1)
        {
            NEWSshowd = true;
        }
        else
        {
            NEWSshowd = false;
            PlayerPrefs.SetInt("BackedFromGame", 0);
            PlayerPrefs.DeleteKey("BackedFromGame");
            PlayerPrefs.Save();
            News.SetActive(false);
        }
        */

        StartCoroutine(GetGameVersionFromSite());
        YourVersionText.text = "Your Game version:" + Application.version.ToString();
        Ver.text = "Game Version: " + GameVersion;

        LoginIn.SetActive(true);
        SignOut.SetActive(false);
        SignOutRanking();


        if (Application.internetReachability == NetworkReachability.NotReachable) // No Internet
        {
            StartCoroutine(NoConnection());
            LoginIn.SetActive(true);
            SignOut.SetActive(false);
            SignOutRanking();

        }
        else // internet available
        {
            if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
            {
                LoadStats();
                LoginIn.SetActive(false);
                SignOut.SetActive(true);
                /*if (NEWSshowd == false)
                {
                    News.SetActive(true);
                    NEWSshowd = true;
                }
                NewsAnim.SetTrigger("Logged");*/
            }

            if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
            {
                return;
            }
            else
            {
                if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
                {
                    return;
                }
                else
                {
                    GameJoltUI.Instance.ShowSignIn(success =>
                    {
                        GameJoltUI.Instance.QueueNotification(success ? "Welcome " + GameJoltAPI.Instance.CurrentUser.Name + "." : "Closed the window");
                        if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
                        {
                            LoadStats();
                            SignOut.SetActive(true);
                            LoginIn.SetActive(false);
                            GameJoltRankingAutoLogin();
                            /*if (NEWSshowd == false)
                            {
                                News.SetActive(true);
                                NEWSshowd = true;

                            }
                            NewsAnim.SetTrigger("Logged"); */
                        }
                    });
                }
            }
        }
    }
    public GameObject Outofdate;

    public string VersionWebUrl = "https://www.noam3d.com/PofleGameVersion/";
    public GameObject ReloginToGame;
    IEnumerator GetGameVersionFromSite()
    {
        using (WWW www = new WWW(VersionWebUrl))
        {
            yield return www;
            if (www.text == Application.version)
            {
                Debug.Log("Up to date");
                PlayerPrefs.SetString("Version", www.text);
                PlayerPrefs.Save();
            }
            else
            {
                Outofdate.SetActive(true);
                VersionToUpdate.text = "Version required to play: " + www.text.ToString();
            }
        }
    }
    public Text YourVersionText;
    public Text VersionToUpdate;
    public void LateUpdate()
    {
        if (GameJoltAPI.Instance.HasUser)
        {
            UserName.text = GameJoltAPI.Instance.CurrentUser.Name;
        }
        else
        {
            UserName.text = "NOT CONNECTED";
        }
    }

    public void PoflesSlayer()
    {
        if (GameJoltAPI.Instance.HasUser)
        {
        }
    }
    IEnumerator NoConnection()
    {
        NoInternet.SetActive(true);
        yield return new WaitForSeconds(2f);
        NoInternet.SetActive(false);
    }

    public void SignOutButtonClicked()
    {
        GameJoltAPI.Instance.CurrentUser.SignOut();
        UserName.text = "NOT CONNECTED";
        LoginIn.SetActive(true);
        SignOut.SetActive(false);
        SignOutRanking();

    }

    public void IsSignedInButtonClicked()
    {
        if (GameJoltAPI.Instance.HasUser)
        {
            GameJoltAPI.Instance.CurrentUser.SignOut();
            LoginIn.SetActive(true);
            SignOut.SetActive(false);
            SignOutRanking();
        }
        else
        {
            GameJoltUI.Instance.ShowSignIn(success =>
            {
                GameJoltUI.Instance.QueueNotification(success ? "Welcome " + GameJoltAPI.Instance.CurrentUser.Name + "." : "Closed the window");
                if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
                {
                    SignOut.SetActive(true);
                    LoginIn.SetActive(false);
                    GameJoltRankingAutoLogin();
                    LoadStats();
                    /*if (NEWSshowd == false)
                    {
                        News.SetActive(true);
                        NEWSshowd = true;

                    }
                    NewsAnim.SetTrigger("Logged");*/
                }
            });
        }
    }
    public void SignInAsGuest()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowTrophies()
    {
        GameJoltUI.Instance.ShowTrophies();
    }
    public void ShowLeader()
    {
        GameJoltUI.Instance.ShowLeaderboards();
    }
    public void Exit()
    {
        SceneManager.LoadScene(3);
    }
    public void OpenGameJoltPage()
    {
        Application.OpenURL("https://gamejolt.com/games/PofleGame/414330");
    }
    private void OnApplicationQuit()
    {
        //NEWSshowd = false;
        //PlayerPrefs.SetInt("NewsShowed", 0);
        PlayerPrefs.Save();
    }

    public void GameJoltRankingAutoLogin()
    {
        GameJolt.API.DataStore.Get("XPtonextRank", false, (string value) =>
        {
            if (value != null)
            {
                rankManager.XPtonextRank = int.Parse(value);
                Loaded++;
            }
        });
        GameJolt.API.DataStore.Get("Rank", false, (string value) =>
        {
            if (value != null)
            {
                rankManager.Rank = int.Parse(value);
                Loaded++;
            }
        });
        GameJolt.API.DataStore.Get("XP", false, (string value) =>
        {
            if (value != null)
            {
                float.TryParse(value, out rankManager.XP);
                Loaded++;
            }
        });
    }
    public void SignOutRanking()
    {
        rankManager.RankText.text = "Rank: NotConnected";
        rankManager.ValueLeft.text = rankManager.XP.ToString() + "/" + "NotConnected";
        rankManager.Rank = 1;
        rankManager.XP = 0;
        rankManager.XPtonextRank = 100;
    }
    public float Seconds;
    public float Minutes;
    public float Hours;

    public GameObject LoadingStatsPanel;
    private string RankInfo;
    public void LoadStats()
    {
        StartCoroutine(LoadBestTime());
        LoadingStatsPanel.SetActive(true);
    }
    IEnumerator LoadBestTime()
    {
        GameJolt.API.DataStore.Get("HighScore", true, (string value) => //
        {
            KillPoints.text = "Best KillPoints made: " + value.ToString();
        });
        yield return new WaitForSeconds(0.2f);
        GameJolt.API.DataStore.Get("TotalKills", true, (string value) => //
        {
            Kills.text = "Total kills: " + value.ToString();
        });
        yield return new WaitForSeconds(0.2f);

        GameJolt.API.DataStore.Get("Deaths", false, (string value) => //
        {
            Deaths.text = "Total Deaths: " + value.ToString();
        });
        yield return new WaitForSeconds(0.2f);

        yield return new WaitForSeconds(0.2f);
        GameJolt.API.DataStore.Get("SecondsAlive", false, (string value) =>
        {
            Seconds += int.Parse(value);
        });
        yield return new WaitForSeconds(0.2f);

        GameJolt.API.DataStore.Get("MinutesAlive", false, (string value) =>
        {
            Minutes += int.Parse(value);
        });
        yield return new WaitForSeconds(0.2f);

        GameJolt.API.DataStore.Get("HoursAlive", false, (string value) =>
        {
            Hours += int.Parse(value);
        });
        yield return new WaitForSeconds(0.7f);
        AliveTime.text = "Best alive time: " + Hours.ToString() + ":" + Minutes.ToString() + ":" + Seconds.ToString();
        yield return new WaitForSeconds(0.2f);
        GameJolt.API.DataStore.Get("Rank", false, (string value) =>
        {
            Rank.text = "Your rank: " + value;
        });
        yield return new WaitForSeconds(0.1f);
        User.text = "Stats for user: " + GameJoltAPI.Instance.CurrentUser.Name.ToString();
        yield return new WaitForSeconds(0.2f);

        LoadingStatsPanel.SetActive(false);
    }
}