using Newtonsoft.Json;
using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoboRally.Controller {
	public class InlobbyManager : MonoBehaviour {

		private static InlobbyManager _instance;
		public static  InlobbyManager Instance { get { return _instance; } }
		public         GameObject      PlayerList;
		public         GameObject     PlayerCardPrefab;
		void Awake() {
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
				new Dictionary<string, object>(){
					{"pat",IngameData.JoinData.Pat}
				}
			);
			yield return request.SendWebRequest();
			if(!request.isHttpError && request.downloadHandler != null) {
				Debug.Log("LeaveGame: " + request.downloadHandler.text);
			} else if(request.downloadHandler != null) {
				Debug.Log("LeaveGame: " + request.downloadHandler.text);
			}
			SceneManager.LoadScene("Menu_Main");
			FindObjectOfType<GlobalEventHandler>().StopListening();
		}

		public void StartGame(string address, int gameId) {
			StartCoroutine(StartGameAsync(address, gameId));
		}

		public IEnumerator StartGameAsync(string address, int gameId) {
			UnityWebRequest request = Http.CreatePut(
				"games/" + gameId + "/actions",
				Http.Auth(new Dictionary<string, object>{
					{"action",ActionType.STARTGAME},
				})
			);
			yield return Http.Send(request, e => {
				SceneManager.LoadScene("Game");
			});
			
		}

		public void OnPlayerJoins(JoinEvent joinEvent) {
			Debug.Log("Player Joined");
			GameObject obj = Instantiate(PlayerCardPrefab, PlayerList.transform);
			obj.GetComponentInChildren<Text>().text = "Player "+joinEvent.JoinedId;
		}
	}
}