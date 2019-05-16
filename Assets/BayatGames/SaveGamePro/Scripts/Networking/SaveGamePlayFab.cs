using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

using PlayFab;
using PlayFab.ClientModels;

namespace BayatGames.SaveGamePro.Networking
{

	/// <summary>
	/// Save Game Pro Cloud PlayFab API Integration.
	/// </summary>
	public class SaveGamePlayFab : SaveGameCloud
	{
		
		/// <summary>
		/// The save result callback.
		/// </summary>
		public Action<UpdateUserDataResult> saveResultCallback;
		
		/// <summary>
		/// The save error callback.
		/// </summary>
		public Action<PlayFabError> saveErrorCallback;
		
		/// <summary>
		/// The download result callback.
		/// </summary>
		public Action<GetUserDataResult> downloadResultCallback;
		
		/// <summary>
		/// The download error callback.
		/// </summary>
		public Action<PlayFabError> downloadErrorCallback;
		
		/// <summary>
		/// The clear result callback.
		/// </summary>
		public Action<UpdateUserDataResult> clearResultCallback;
		
		/// <summary>
		/// The clear error callback.
		/// </summary>
		public Action<PlayFabError> clearErrorCallback;
		
		/// <summary>
		/// The delete result callback.
		/// </summary>
		public Action<UpdateUserDataResult> deleteResultCallback;
		
		/// <summary>
		/// The delete error callback.
		/// </summary>
		public Action<PlayFabError> deleteErrorCallback;
		
		/// <summary>
		/// The download buffer.
		/// </summary>
		protected byte [] m_DownloadBuffer;
		
		/// <summary>
		/// Is current request Done?
		/// </summary>
		protected bool m_IsDone;
		
		/// <summary>
		/// Is there are any error?
		/// </summary>
		protected bool m_IsError;
		
		/// <summary>
		/// The error.
		/// </summary>
		protected PlayFabError m_Error;

		/// <summary>
		/// Gets the download buffer.
		/// </summary>
		/// <value>The download buffer.</value>
		public virtual byte[] DownloadBuffer
		{
			get
			{
				return m_DownloadBuffer;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this request is done.
		/// </summary>
		/// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
		public virtual bool IsDone
		{
			get
			{
				return m_IsDone;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this request has error.
		/// </summary>
		/// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
		public virtual bool IsError
		{
			get
			{
				return m_IsError;
			}
		}

		/// <summary>
		/// Gets the error.
		/// </summary>
		/// <value>The error.</value>
		public virtual PlayFabError Error
		{
			get
			{
				return m_Error;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BayatGames.SaveGamePro.Networking.SaveGamePlayFab"/> class.
		/// </summary>
		public SaveGamePlayFab () : base ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BayatGames.SaveGamePro.Networking.SaveGamePlayFab"/> class.
		/// </summary>
		/// <param name="settings">Settings.</param>
		public SaveGamePlayFab ( SaveGameSettings settings ) : base ( settings )
		{
		}

		/// <summary>
		/// Save the specified value using the identifier.
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		/// <param name="value">Value.</param>
		/// <param name="settings">Settings.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public override IEnumerator Save ( string identifier, object value, SaveGameSettings settings )
		{
			m_IsDone = false;
			Save ( identifier, value, OnSaveSuccess, OnSaveFailed, settings );
			while ( !m_IsDone )
			{
				yield return null;
			}
		}

		protected virtual void OnSaveSuccess ( UpdateUserDataResult result )
		{
			m_IsError = false;
			m_IsDone = true;
			m_Error = null;
			if ( saveResultCallback != null )
			{
				saveResultCallback.Invoke ( result );
			}
		}

		protected virtual void OnSaveFailed ( PlayFabError error )
		{
			m_IsError = true;
			m_Error = error;
			m_IsDone = true;
			if ( saveErrorCallback != null )
			{
				saveErrorCallback.Invoke ( error );
			}
		}

		/// <summary>
		/// Save the specified identifier, value, resultCallback, errorCallback and settings.
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		/// <param name="value">Value.</param>
		/// <param name="resultCallback">Result callback.</param>
		/// <param name="errorCallback">Error callback.</param>
		/// <param name="settings">Settings.</param>
		public virtual void Save ( string identifier, object value, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, SaveGameSettings settings )
		{
			var request = new UpdateUserDataRequest ();
			request.Data = new Dictionary<string, string> ();
			using ( MemoryStream stream = new MemoryStream () )
			{
				settings.Formatter.Serialize ( stream, value, settings );
				request.Data.Add ( identifier, Convert.ToBase64String ( stream.ToArray () ) );
			}
			PlayFabClientAPI.UpdateUserData ( request, resultCallback, errorCallback );
		}

		/// <summary>
		/// Download the specified identifier.
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		/// <param name="settings">Settings.</param>
		public override IEnumerator Download ( string identifier, SaveGameSettings settings )
		{
			m_IsDone = false;
			Download ( identifier, OnDownloadSuccess, OnDownloadFailed, settings );
			while ( !m_IsDone )
			{
				yield return null;
			}
		}

		protected virtual void OnDownloadSuccess ( GetUserDataResult result )
		{
			m_IsError = false;
			m_IsDone = true;
			m_Error = null;
			if ( downloadResultCallback != null )
			{
				downloadResultCallback.Invoke ( result );
			}
		}

		protected virtual void OnDownloadFailed ( PlayFabError error )
		{
			m_IsError = true;
			m_Error = error;
			m_IsDone = true;
			if ( downloadErrorCallback != null )
			{
				downloadErrorCallback.Invoke ( error );
			}
		}

		/// <summary>
		/// Download the specified identifier, resultCallback, errorCallback and settings.
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		/// <param name="resultCallback">Result callback.</param>
		/// <param name="errorCallback">Error callback.</param>
		/// <param name="settings">Settings.</param>
		public virtual void Download ( string identifier, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, SaveGameSettings settings )
		{
			var request = new GetUserDataRequest ();
			request.Keys = new List<string> () { identifier };
			PlayFabClientAPI.GetUserData ( request, result =>
			{
				if ( !result.Data.ContainsKey ( identifier ) )
				{
					PlayFabError error = new PlayFabError ();
					error.Error = PlayFabErrorCode.Unknown;
					error.ErrorMessage = "The given identifier does not exists.";
					errorCallback.Invoke ( error );
					return;
				}
				m_DownloadBuffer = Convert.FromBase64String ( result.Data [ identifier ].Value );
				resultCallback.Invoke ( result );
			}, errorCallback );
		}

		/// <summary>
		/// Load the value, if not exists, return the default value.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <param name="settings">Settings.</param>
		public override object Load ( Type type, object defaultValue, SaveGameSettings settings )
		{
			object result = null;
			using ( MemoryStream stream = new MemoryStream ( m_DownloadBuffer ) )
			{
				result = settings.Formatter.Deserialize ( stream, type, settings );
			}
			if ( result == null )
			{
				result = defaultValue;
			}
			return result;
		}

		/// <summary>
		/// Load the data into the value.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="settings">Settings.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public override void LoadInto ( object value, SaveGameSettings settings )
		{
			using ( MemoryStream stream = new MemoryStream ( m_DownloadBuffer ) )
			{
				settings.Formatter.DeserializeInto ( stream, value, settings );
			}
		}

		/// <summary>
		/// Clear the user data.
		/// </summary>
		/// <param name="settings">Settings.</param>
		public override IEnumerator Clear ( SaveGameSettings settings )
		{
			m_IsDone = false;
			Clear ( OnClearSuccess, OnClearFailed, settings );
			while ( !m_IsDone )
			{
				yield return null;
			}
		}

		protected virtual void OnClearSuccess ( UpdateUserDataResult result )
		{
			m_IsError = false;
			m_IsDone = true;
			m_Error = null;
			if ( clearResultCallback != null )
			{
				clearResultCallback.Invoke ( result );
			}
		}

		protected virtual void OnClearFailed ( PlayFabError error )
		{
			m_IsError = true;
			m_Error = error;
			m_IsDone = true;
			if ( clearErrorCallback != null )
			{
				clearErrorCallback.Invoke ( error );
			}
		}

		/// <summary>
		/// Clear the specified resultCallback, errorCallback and settings.
		/// </summary>
		/// <param name="resultCallback">Result callback.</param>
		/// <param name="errorCallback">Error callback.</param>
		/// <param name="settings">Settings.</param>
		public virtual void Clear ( Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, SaveGameSettings settings )
		{
			var request = new GetUserDataRequest ();
			PlayFabClientAPI.GetUserData ( request, result =>
			{
				var clearRequest = new UpdateUserDataRequest ();
				clearRequest.KeysToRemove = new List<string> ( result.Data.Keys );
				if ( clearRequest.KeysToRemove.Count > 0 )
				{
					PlayFabClientAPI.UpdateUserData ( clearRequest, resultCallback, errorCallback );
				}
			}, errorCallback );
		}

		/// <summary>
		/// Delete the specified identifier.
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		/// <param name="settings">Settings.</param>
		public override IEnumerator Delete ( string identifier, SaveGameSettings settings )
		{
			Delete ( identifier, OnDeleteSuccess, OnDeleteFailed, settings );
			yield return null;
		}

		protected virtual void OnDeleteSuccess ( UpdateUserDataResult result )
		{
			m_IsError = false;
			m_IsDone = true;
			if ( deleteResultCallback != null )
			{
				deleteResultCallback.Invoke ( result );
			}
		}

		protected virtual void OnDeleteFailed ( PlayFabError error )
		{
			m_IsError = true;
			m_Error = error;
			m_IsDone = true;
			if ( deleteErrorCallback != null )
			{
				deleteErrorCallback.Invoke ( error );
			}
		}

		/// <summary>
		/// Delete the specified identifier, resultCallback, errorCallback and settings.
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		/// <param name="resultCallback">Result callback.</param>
		/// <param name="errorCallback">Error callback.</param>
		/// <param name="settings">Settings.</param>
		public virtual void Delete ( string identifier, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, SaveGameSettings settings )
		{
			var request = new UpdateUserDataRequest ();
			request.KeysToRemove = new List<string> () { identifier };
			PlayFabClientAPI.UpdateUserData ( request, resultCallback, errorCallback );
		}
	
	}

}