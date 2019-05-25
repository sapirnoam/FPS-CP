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
    private void Start()
    {
        LoginIn.SetActive(true);
        SignOut.SetActive(false);
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
                    }
                });
                }
            }
        }
    }


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
}