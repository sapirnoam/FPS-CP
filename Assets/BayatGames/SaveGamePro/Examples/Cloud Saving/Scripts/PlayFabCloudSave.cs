using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PlayFab;
using PlayFab.ClientModels;
using BayatGames.SaveGamePro;
using BayatGames.SaveGamePro.Networking;

namespace BayatGames.SaveGamePro.Examples
{

	/// <summary>
	/// Play fab cloud save example.
	/// This example uses callback to handle async operations, but you can use coroutines to achieve same goal.
	/// </summary>
	public class PlayFabCloudSave : MonoBehaviour
	{
		
		/// <summary>
		/// The identifier.
		/// </summary>
		public string identifier = "helloWorld";
		
		/// <summary>
		/// The login button.
		/// </summary>
		public Button loginButton;
		
		/// <summary>
		/// The save button.
		/// </summary>
		public Button saveButton;
		
		/// <summary>
		/// The load button.
		/// </summary>
		public Button loadButton;
		
		/// <summary>
		/// The clear button.
		/// </summary>
		public Button clearButton;
		
		/// <summary>
		/// The data input field.
		/// </summary>
		public InputField dataInputField;
		
		/// <summary>
		/// The default value.
		/// </summary>
		public string defaultValue = "Hello, World!";
		
		/// <summary>
		/// The instance of the Save Game Pro Cloud PlayFab that was downloaded the user data.
		/// </summary>
		protected SaveGamePlayFab m_DownloadPlayFab;

		void Start ()
		{
			
			// Disable all buttons, except login button.
			loginButton.interactable = true;
			saveButton.interactable = false;
			loadButton.interactable = false;
			clearButton.interactable = false;
			
		}

		/// <summary>
		/// Login the user to the PlayFab.
		/// </summary>
		public void Login ()
		{
			Debug.Log ( "Logging..." );
			
			// Disable login button.
			loginButton.interactable = false;
			var request = new LoginWithCustomIDRequest ();
			request.CreateAccount = true;
			request.CustomId = "CloudSavingTest";
			
			// Send the request.
			PlayFabClientAPI.LoginWithCustomID ( request, OnLoginSuccess, OnLoginFailure );
		}

		/// <summary>
		/// Raises the login success event.
		/// </summary>
		/// <param name="result">Result.</param>
		void OnLoginSuccess ( LoginResult result )
		{
			
			// Disable login button and enable other buttons
			Debug.Log ( "Login Successful" );
			loginButton.interactable = false;
			saveButton.interactable = true;
			loadButton.interactable = true;
			clearButton.interactable = true;
			
		}

		/// <summary>
		/// Raises the login failure event.
		/// </summary>
		/// <param name="error">Error.</param>
		void OnLoginFailure ( PlayFabError error )
		{
			
			// Disable all buttons except login button
			Debug.LogError ( "Login Failed" );
			Debug.LogError ( error.GenerateErrorReport () );
			loginButton.interactable = true;
			saveButton.interactable = false;
			loadButton.interactable = false;
			clearButton.interactable = false;
			
		}

		/// <summary>
		/// Save the user data to the PlayFab.
		/// </summary>
		public void Save ()
		{
			Debug.Log ( "Saving..." );

			// Disable save button.
			saveButton.interactable = false;
			SaveGamePlayFab playFab = new SaveGamePlayFab ();
			playFab.saveResultCallback = OnSaveSuccess;
			playFab.saveErrorCallback = OnSaveFailure;
			
			// Send the request.
			StartCoroutine ( playFab.Save ( identifier, dataInputField.text ) );
		}

		/// <summary>
		/// Raises the save success event.
		/// </summary>
		/// <param name="result">Result.</param>
		void OnSaveSuccess ( UpdateUserDataResult result )
		{
			Debug.Log ( "Save Successful" );
			
			// Enable save button.
			saveButton.interactable = true;
		}

		/// <summary>
		/// Raises the save failure event.
		/// </summary>
		/// <param name="error">Error.</param>
		void OnSaveFailure ( PlayFabError error )
		{
			Debug.LogError ( "Save Failed" );
			Debug.LogError ( error.GenerateErrorReport () );
			
			// Enable save button.
			saveButton.interactable = true;
		}

		/// <summary>
		/// Load the user data from PlayFab.
		/// </summary>
		public void Load ()
		{
			
			// We first need to download the data and then load from the downloaded data.
			Debug.Log ( "Loading..." );

			// Disable load button.
			loadButton.interactable = false;
			m_DownloadPlayFab = new SaveGamePlayFab ();
			m_DownloadPlayFab.downloadResultCallback = OnDownloadSuccess;
			m_DownloadPlayFab.downloadErrorCallback = OnDownloadFailure;
			
			// Send the request.
			StartCoroutine ( m_DownloadPlayFab.Download ( identifier ) );
		}

		/// <summary>
		/// Raises the download success event.
		/// </summary>
		/// <param name="result">Result.</param>
		void OnDownloadSuccess ( GetUserDataResult result )
		{
			Debug.Log ( "Download Successful" );
			
			// Load the data.
			dataInputField.text = m_DownloadPlayFab.Load<string> ( defaultValue );
			
			// Enable load button.
			loadButton.interactable = true;
		}

		/// <summary>
		/// Raises the download failure event.
		/// </summary>
		/// <param name="error">Error.</param>
		void OnDownloadFailure ( PlayFabError error )
		{
			Debug.LogError ( "Download Failed" );
			Debug.LogError ( error.GenerateErrorReport () );
			
			// Enable load button.
			loadButton.interactable = true;
		}

		/// <summary>
		/// Clear user data from PlayFab.
		/// </summary>
		public void Clear ()
		{
			Debug.Log ( "Clearing..." );

			// Disable clear button.
			clearButton.interactable = false;
			SaveGamePlayFab playFab = new SaveGamePlayFab ();
			playFab.clearResultCallback = OnClearSuccess;
			playFab.clearErrorCallback = OnClearFailure;
			
			// Send the request.
			StartCoroutine ( playFab.Clear () );
		}

		/// <summary>
		/// Raises the clear success event.
		/// </summary>
		/// <param name="result">Result.</param>
		void OnClearSuccess ( UpdateUserDataResult result )
		{
			Debug.Log ( "Clear Successful" );
			
			// Enable clear button.
			clearButton.interactable = true;
		}

		/// <summary>
		/// Raises the clear failure event.
		/// </summary>
		/// <param name="error">Error.</param>
		void OnClearFailure ( PlayFabError error )
		{
			Debug.LogError ( "Clear Failed" );
			Debug.LogError ( error.GenerateErrorReport () );
			
			// Enable clear button.
			clearButton.interactable = true;
		}
	
	}

}