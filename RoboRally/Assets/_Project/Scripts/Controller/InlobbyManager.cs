using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RoboRally.Controller {
	public class InlobbyManager : MonoBehaviour {

		private static InlobbyManager _instance;
		public static InlobbyManager Instance { get { return _instance; } }

		void Start() {
			if(_instance != null && _instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				_instance = this;
			}
		}

		void Update() {

		}

		public void LeaveLobby(string address, int gameId, int playerId) {
			StartCoroutine(LeaveLobbyAsync(address, gameId, playerId));
		}

		public IEnumerator LeaveLobbyAsync(string address, int gameId, int playerId) {
			UnityWebRequest request = Http.CreateDelete(
				address,
				"games/" + gameId + "/players/" + playerId,
				"pat=" + UnityWebRequest.EscapeURL(IngameData.JoinData.Pat)
			);
			yield return request.SendWebRequest();
			if(request.downloadHandler != null) {
				Debug.Log("Deleted");
			}
			SceneManager.LoadScene("Menu_Main");
		}

		public void StartGame(string address, int gameId) {
			StartCoroutine(StartGameAsync(address, gameId));
		}

		public IEnumerator StartGameAsync(string address, int gameId) {
			UnityWebRequest request = Http.CreatePut(
				address,
				"games/" + gameId + "/actions",
				new string[] { 
					"action=3",
					"pat=" + UnityWebRequest.EscapeURL(IngameData.JoinData.Pat)
				},
				null
			);
			yield return request.SendWebRequest();
			if(request.downloadHandler != null) {
				Debug.Log(request.downloadHandler.text);
			}
		}
	}
}