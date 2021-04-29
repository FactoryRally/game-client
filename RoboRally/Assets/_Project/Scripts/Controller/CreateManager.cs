using RoboRally.Menu;
using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Controller {
	public class CreateManager : MonoBehaviour {

		public bool AutoJoin = true;

		public void CreateLobby(string name, string password, bool namesVis, bool coms, int maxPlayers, string playerName) {
			Http.address = "localhost";
			Http.serverPath = Application.dataPath + "/Server/FactoryRally.exe";
			Http.StartServer();
			StartCoroutine(CreateLobbyAsync(name, password, namesVis, coms, maxPlayers, playerName));
		}

		private IEnumerator CreateLobbyAsync(string name, string password, bool namesVis, bool coms, int maxPlayers, string playerName = "host") {
			string[] body = {
				"player-names-visible=" + namesVis.ToString().ToLower(),
				"max-players="          + maxPlayers.ToString(),
				"name="                 + name.ToString(),
				"robots-per-player=1",
				"password="       + password.ToString(),
				"fill-with-bots=" + coms.ToString().ToLower()
			};//TODO: KALIAN :(
			yield return Http.Send(Http.CreatePost("games", null, body), response => {
				if(AutoJoin) {
                	try {
                		int id = int.Parse(response.downloadHandler.text);
                		string address = Http.GetLocalIPAddress();
                		LobbyManager.Instance.JoinLobby(address, id, password, playerName);
                		IngameData.IsHost = true;
                		IngameData.GameId = id;
                	} catch(FormatException) {
                		Debug.Log("Was not able to join the Game!");
                	}
                }
			});
		}
	}
}