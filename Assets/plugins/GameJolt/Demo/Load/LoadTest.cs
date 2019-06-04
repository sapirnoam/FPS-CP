using UnityEngine;
using System.Collections;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadTest : MonoBehaviour {

    public GameObject SignedInSuccess;
    public GameObject SignInAsAguest;
    public GameObject NoInternet;
    public GameObject SignOut;
    public GameObject LoginIn;
    public Text UserName;
    public string GameVersion;
    public Text Ver;
    public Animator NewsAnim;
    public GameObject News;
    private void Start()
    {
        GameVersion = PlayerPrefs.GetString("Version");

        StartCoroutine(GetGameVersionFromSite());
        YourVersionText.text = "Your Game version:" + Application.version.ToString();
        Ver.text = "Game Version: " + GameVersion;
        LoginIn.SetActive(true);
        SignOut.SetActive(false);
        News.SetActive(false);
        if (Application.internetReachability == NetworkReachability.NotReachable) // No Internet
        {
            StartCoroutine(NoConnection());
            LoginIn.SetActive(true);
            SignOut.SetActive(false);
        }
        else // internet available
        {
            if (GameJoltAPI.Instance.HasUser && GameJoltAPI.Instance.HasSignedInUser)
            {
                LoginIn.SetActive(false);
                SignOut.SetActive(true);
                News.SetActive(true);
                NewsAnim.SetTrigger("Logged");
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
                        SignOut.SetActive(true);
                        LoginIn.SetActive(false);
                        News.SetActive(true);
                        NewsAnim.SetTrigger("Logged");

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
            else if (NoInternet == true && GameVersion == Application.version)
            {
                Outofdate.SetActive(false);
                StartCoroutine(NoConnection());
                SignOut.SetActive(false);
                LoginIn.SetActive(false);
                ReloginToGame.SetActive(true);
            }
            else if (www.text == "ALPHA")
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
            SignInAsAguest.SetActive(true);
        }

        public void SignOutButtonClicked()
        {
            GameJoltAPI.Instance.CurrentUser.SignOut();
            UserName.text = "NOT CONNECTED";
            LoginIn.SetActive(true);
            SignOut.SetActive(false);
        }

        public void IsSignedInButtonClicked() {
        if (GameJoltAPI.Instance.HasUser)
        {
            GameJoltAPI.Instance.CurrentUser.SignOut();
            LoginIn.SetActive(true);
            SignOut.SetActive(false);
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
            Application.Quit();
        }
    public void OpenGameJoltPage()
    {
        Application.OpenURL("https://gamejolt.com/games/PofleGame/414330");
    }
}