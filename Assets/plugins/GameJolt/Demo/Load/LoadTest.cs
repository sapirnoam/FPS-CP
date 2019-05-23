using UnityEngine;
using System.Collections;
using System.Net;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace GameJolt.Demo.Load {
    public class LoadTest : MonoBehaviour {

        public GameObject SignedInSuccess;
        public GameObject SignInAsAguest;
        public GameObject NoInternet;
        public GameObject SignOut;

        private void Start()
        {
            if (GameJoltAPI.Instance.HasUser) // Account signed in
            {
                StartCoroutine(MoveScene());
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    NoInternet.SetActive(true);
                }
            }

            if (Application.internetReachability == NetworkReachability.NotReachable) // No Internet
            {
                StartCoroutine(NoConnection());
                Debug.Log("NoInternet");
            }
            else // internet available
            {
                Debug.Log("InternetAvailable");
                GameJoltUI.Instance.ShowSignIn(success =>
                {
                    GameJoltUI.Instance.QueueNotification(success ? "Welcome " + GameJoltAPI.Instance.CurrentUser.Name + "!" : "Closed the window :(");
                    GameJolt.API.Trophies.Unlock(107285);
                });
                if (GameJoltAPI.Instance.HasUser)
                {
                    StartCoroutine(MoveScene());
                }
            }
        }

        IEnumerator MoveScene()
        {
            SignedInSuccess.SetActive(true);
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(1);
        }
        IEnumerator NoConnection()
        {
            NoInternet.SetActive(true);
            yield return new WaitForSeconds(2f);
            NoInternet.SetActive(false);
            SignInAsAguest.SetActive(true);
        }

        public void SignOutButtonClicked() {
			if(!GameJoltAPI.Instance.HasUser) {
				GameJoltUI.Instance.QueueNotification("You're not signed in");
			} else {
                GameJoltUI.Instance.QueueNotification("See you soon " + GameJoltAPI.Instance.CurrentUser.Name + "!"); GameJoltAPI.Instance.CurrentUser.SignOut();
                GameJoltUI.Instance.ShowSignIn(success =>
                {
                    GameJoltUI.Instance.QueueNotification(success ? "Welcome back " + GameJoltAPI.Instance.CurrentUser.Name + "!" : "Closed the window");
                    GameJolt.API.Trophies.Unlock(107285);
                });
			}
		}

		public void IsSignedInButtonClicked() {
			if(GameJoltAPI.Instance.HasUser) {
				GameJoltUI.Instance.QueueNotification(
					"Signed in as " + GameJoltAPI.Instance.CurrentUser.Name);
			} else {
				GameJoltUI.Instance.QueueNotification("Not Signed In :(");
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
        public void Signin()
        {
            Start();
        }

        public void ShowBestLeader()
        {
            GameJoltUI.Instance.ShowLeaderboards();
        }

        public void ShowTotalLeader()
        {

        }
	}
}
