using RoboRally.Menu;
using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Controller {
	public class CreateManager : MonoBehaviour {

		public bool AutoJoin = true;

		public void Awake() {

		}

		public void Start() {

		}

		public void Update() {

		}


		public void CreateLobby(string name, string password, bool namesVis, bool coms, int maxPlayers, string playerName) {
			Http.serverPath = Application.dataPath + "/Server/Tgm.Roborally.Server.exe";
			Http.StartServer();
			StartCoroutine(CreateLobbyAsync(name, password, namesVis, coms, maxPlayers, playerName));
		}

		private IEnumerator CreateLobbyAsync(string name, string password, bool namesVis, bool coms, int maxPlayers, string playerName = "host") {
			string[] body = {
				"player-names-visible=" + namesVis.ToString().ToLower(),
				"max-players=" + maxPlayers.ToString(),
				"name=" + name.ToString(),
				"robots-per-player=1",
				"password=" + password.ToString(),
				"fill-with-bots=" + coms.ToString().ToLower()
			};
			UnityWebRequest request = Http.CreatePost("localhost", "games", null, body);
			yield return request.SendWebRequest();
			if(!request.isHttpError && request.downloadHandler != null) {
				Debug.Log("CreateLobby: " + request.downloadHandler.text);
				if(AutoJoin) {
					try {
						int id = int.Parse(request.downloadHandler.text);
						string address = Http.GetLocalIPAddress();
						LobbyManager.Instance.JoinLobby(address, id, password, playerName);
						IngameData.IsHost = true;
						IngameData.ID = id;
					} catch(FormatException) {
						Debug.Log("Was not able to join the Game!");
					}
				}
			} else if(request.downloadHandler != null) {
				Debug.Log("CreateLobby: " + request.downloadHandler.text);
			}
		}
	}
}