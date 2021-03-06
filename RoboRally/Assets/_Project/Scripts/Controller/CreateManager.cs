using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateManager : MonoBehaviour {


	public void Awake() {

	}

	public void Start() {

	}

	public void Update() {

	}


	public void CreateLobby(string name, string password, bool namesVis, bool coms, int maxPlayers) {
		Http.serverPath = Application.dataPath + "/Server/Tgm.Roborally.Server.exe";
		Http.StartServer();
		StartCoroutine(CreateLobbyAsync(name, password, namesVis, coms, maxPlayers));
	}

	private IEnumerator CreateLobbyAsync(string name, string password, bool namesVis, bool coms, int maxPlayers) {
		string[] body = {
			"player-names-visible=" + namesVis.ToString().ToLower(),
			"max-players=" + maxPlayers.ToString().ToLower(),
			"name=" + name.ToString().ToLower(),
			"robots-per-player=1",
			"password=" + password.ToString().ToLower(),
			"fill-with-bots=" + coms.ToString().ToLower()
		};
		UnityWebRequest request = Http.CreatePost("localhost", "games", body);
		yield return request.SendWebRequest();
		Debug.Log(request.downloadHandler.text);
		if(request.downloadHandler == null || request.downloadHandler.text == null) {

		}
	}
}
